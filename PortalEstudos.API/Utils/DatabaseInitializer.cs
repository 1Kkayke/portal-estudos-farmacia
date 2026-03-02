using Microsoft.EntityFrameworkCore;
using PortalEstudos.API.Data;
using PortalEstudos.API.Extensions;

namespace PortalEstudos.API.Utils;

/// <summary>
/// Utilitário para inicialização segura do banco de dados em produção
/// </summary>
public static class DatabaseInitializer
{
    public static async Task<bool> InitializeSafelyAsync(
        WebApplication app,
        ILogger logger,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            logger.LogInformation("🔍 Verificando status do banco de dados...");
            
            // Estratégia 1: Verifica conexão
            var canConnect = await context.Database.CanConnectAsync(cancellationToken);
            if (!canConnect)
            {
                logger.LogWarning("❌ Não foi possível conectar. Criando banco...");
                await context.Database.EnsureCreatedAsync(cancellationToken);
                logger.LogInformation("✅ Banco de dados criado");
                return true;
            }
            
            // Estratégia 2: Verifica se tabelas essenciais existem
            var tablesExist = await CheckEssentialTablesAsync(context, logger, cancellationToken);
            
            if (!tablesExist)
            {
                logger.LogInformation("📋 Criando estrutura inicial do banco...");
                await context.Database.EnsureCreatedAsync(cancellationToken);
                logger.LogInformation("✅ Estrutura criada com sucesso");
                return true;
            }
            
            // Estratégia 3: Aplicar apenas migrações necessárias
            var applied = await context.Database.GetAppliedMigrationsAsync(cancellationToken);
            var pending = await context.Database.GetPendingMigrationsAsync(cancellationToken);
            
            logger.LogInformation("📊 Migrações - Aplicadas: {Applied}, Pendentes: {Pending}", 
                applied.Count(), pending.Count());
            
            if (pending.Any())
            {
                logger.LogInformation("🔄 Aplicando migrações pendentes...");
                
                // Aplica migrações uma por vez para melhor controle
                foreach (var migration in pending)
                {
                    try
                    {
                        await context.Database.MigrateAsync(migration, cancellationToken);
                        logger.LogInformation("✅ Migração aplicada: {Migration}", migration);
                    }
                    catch (Exception ex)
                    {
                        logger.LogWarning("⚠️ Falha na migração {Migration}: {Error}", migration, ex.Message);
                        
                        // Se é o erro de tabela já existe, ignora
                        if (ex.Message.Contains("already exists") || ex.Message.Contains("42P07"))
                        {
                            logger.LogInformation("ℹ️ Ignorando erro de tabela existente para {Migration}", migration);
                            continue;
                        }
                        
                        throw;
                    }
                }
            }
            
            logger.LogInformation("✅ Banco de dados inicializado com sucesso");
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ Erro na inicialização do banco de dados");
            
            // Em produção, não falha a aplicação
            if (app.Environment.IsProduction())
            {
                logger.LogWarning("⚠️ Continuando sem inicialização completa do banco");
                return false;
            }
            
            throw;
        }
    }
    
    private static async Task<bool> CheckEssentialTablesAsync(
        ApplicationDbContext context, 
        ILogger logger,
        CancellationToken cancellationToken)
    {
        var essentialTables = new[] 
        {
            "AspNetUsers",
            "AspNetRoles", 
            "Topics",
            "Documents"
        };
        
        try
        {
            foreach (var tableName in essentialTables)
            {
                var count = await context.Database.SqlQuery<int>(
                    $"SELECT COUNT(*) as Value FROM information_schema.tables WHERE table_name = {tableName}"
                ).FirstOrDefaultAsync(cancellationToken);
                
                if (count == 0)
                {
                    logger.LogInformation("❌ Tabela essencial {Table} não encontrada", tableName);
                    return false;
                }
            }
            
            logger.LogInformation("✅ Todas as tabelas essenciais existem");
            return true;
        }
        catch (Exception ex)
        {
            logger.LogInformation("❌ Erro ao verificar tabelas: {Error}", ex.Message);
            return false;
        }
    }
}