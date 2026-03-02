# Execute estes comandos para migrar para português:

# 1. Definir a variável com sua senha do PostgreSQL
$env:POSTGRES_PASSWORD="SUA_SENHA_REAL"

# 2. Ir para a pasta da API  
cd PortalEstudos.API

# 3. Aplicar a migração no PostgreSQL
dotnet ef database update --environment Production

# 4. Verificar se funcionou
echo "Migração concluída! Teste a consulta agora:"