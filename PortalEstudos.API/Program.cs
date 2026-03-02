using PortalEstudos.API.Data;
using PortalEstudos.API.Extensions;
using PortalEstudos.API.Security;
using PortalEstudos.API.Middleware;
using PortalEstudos.API.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var isProduction = builder.Environment.IsProduction();

// Configuração de serviços organizadas por extensões
builder.Services
    .AddDatabaseConfiguration(builder.Configuration, isProduction)
    .AddIdentityConfiguration()
    .AddJwtAuthentication(builder.Configuration, isProduction)
    .AddApplicationServices()
    .AddPerformanceOptimizations()
    .AddCorsConfiguration(builder.Configuration)
    .AddSecurityConfiguration();

// Controllers e API
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCustomRateLimiting(builder.Configuration);

var app = builder.Build();

// Pipeline de middleware
await ConfigureMiddlewarePipelineAsync(app);

// Inicialização do banco de dados
var dbInitialized = await InitializeDatabaseAsync(app);
if (!dbInitialized && app.Environment.IsProduction())
{
    // Fallback usando inicializador robusto
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("🔄 Tentando inicialização alternativa do banco...");
    await DatabaseInitializer.InitializeSafelyAsync(app, logger);
}

// Executar aplicação
app.Run();

static async Task ConfigureMiddlewarePipelineAsync(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseForwardedHeaders();
    app.UseResponseCompression();
    app.UseResponseCaching();

    if (!app.Environment.IsDevelopment())
    {
        app.UseHsts();
    }

    app.UseSecurityHeaders();
    app.UseStaticFiles();
    app.UseCors("AllowFrontend");
    app.UseRateLimiter();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}

static async Task<bool> InitializeDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    
    try
    {
        logger.LogInformation("Iniciando verificação do banco de dados...");
        
        // Verifica se consegue conectar no banco
        var canConnect = await context.Database.CanConnectAsync();
        if (!canConnect)
        {
            logger.LogWarning("Não foi possível conectar ao banco de dados. Tentando criar...");
            await context.Database.EnsureCreatedAsync();
            logger.LogInformation("Banco de dados criado com sucesso");
            return true;
        }

        // Verifica se as tabelas básicas já existem
        var tablesExist = await CheckIfTablesExistAsync(context, logger);
        if (tablesExist)
        {
            logger.LogInformation("Tabelas já existem. Verificando migrações pendentes...");
            
            // Se as tabelas existem, verifica apenas se há migrações pendentes
            try
            {
                var appliedMigrations = await context.Database.GetAppliedMigrationsAsync();
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
                
                logger.LogInformation("Migrações aplicadas: {Applied}, Pendentes: {Pending}", 
                    appliedMigrations.Count(), pendingMigrations.Count());
                
                if (pendingMigrations.Any())
                {
                    logger.LogInformation("Aplicando {Count} migrações pendentes...", pendingMigrations.Count());
                    await context.Database.MigrateAsync();
                    logger.LogInformation("Migrações aplicadas com sucesso");
                }
            }
            catch (Exception migrationEx)
            {
                logger.LogWarning(migrationEx, "Erro ao verificar/aplicar migrações, mas tabelas existem - continuando");
            }
        }
        else
        {
            // Se as tabelas não existem, cria tudo
            logger.LogInformation("Tabelas não existem. Criando banco de dados completo...");
            await context.Database.EnsureCreatedAsync();
            logger.LogInformation("Banco de dados criado com sucesso");
        }
        
        // Fazer seeding apenas em desenvolvimento
        if (app.Environment.IsDevelopment())
        {
            logger.LogInformation("Executando seeding de dados de desenvolvimento...");
            try
            {
                StudyContentSeeder.EnsureSeeded(context);
                logger.LogInformation("Seeding concluído com sucesso");
            }
            catch (Exception seedEx)
            {
                logger.LogWarning(seedEx, "Erro durante seeding, mas continuando execução");
            }
        }
        
        return true;
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Erro durante inicialização do banco de dados");
        
        // Em produção, retorna false para tentar método alternativo
        if (app.Environment.IsProduction())
        {
            logger.LogWarning("Falha na inicialização padrão - tentará método alternativo");
            return false;
        }
        else
        {
            throw;
        }
    }
}

static async Task<bool> CheckIfTablesExistAsync(ApplicationDbContext context, ILogger logger)
{
    try
    {
        // Tenta executar uma query simples na tabela AspNetRoles
        var count = await context.Database.SqlQuery<int>(
            $"SELECT COUNT(*) as Value FROM information_schema.tables WHERE table_name = 'AspNetRoles'"
        ).FirstOrDefaultAsync();
        
        var exists = count > 0;
        logger.LogInformation("Verificação de tabelas: AspNetRoles existe = {Exists}", exists);
        return exists;
    }
    catch (Exception ex)
    {
        logger.LogInformation("Tabelas não existem (erro esperado): {Error}", ex.Message);
        return false;
    }
}
