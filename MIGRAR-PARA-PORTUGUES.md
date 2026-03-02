# 🚀 MIGRAR BANCO PARA PORTUGUÊS - PostgreSQL Render

## 1️⃣ **Configure a Senha (IMPORTANTE):**

Edite o arquivo `appsettings.Production.json` e substitua `SUA_SENHA_AQUI` pela senha real do PostgreSQL:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=dpg-d6gv2q4r85hc73a30mug-a.oregon-postgres.render.com;Port=5432;Database=portalestudos;Username=portalestudos_user;Password=SENHA_REAL_AQUI;SslMode=Require;"
}
```

## 2️⃣ **Aplicar Migração em Português:**

```bash
# Na pasta PortalEstudos.API
cd PortalEstudos.API

# Aplicar migração no PostgreSQL (Render)
dotnet ef database update --environment Production

# Se der erro, force com:
dotnet ef database update NomesDescritivos_EmPortugues --environment Production
```

## 3️⃣ **Alternativa: Usar Variável de Ambiente**

Se preferir usar variável de ambiente (mais seguro):

```bash
# Definir a connection string
$env:ConnectionStrings__DefaultConnection="Host=dpg-d6gv2q4r85hc73a30mug-a.oregon-postgres.render.com;Port=5432;Database=portalestudos;Username=portalestudos_user;Password=SUA_SENHA;SslMode=Require;"

# Aplicar migração
dotnet ef database update --environment Production
```

## 4️⃣ **Verificar se Funcionou:**

Depois da migração, as tabelas devem estar assim:
- `Topics` → `Disciplinas` 
- `Documents` → `Documentos`
- `Questions` → `Questoes`
- `AspNetUsers` → `Usuarios`

## ⚠️ **Observação Importante:**
- A migração vai **RENOMEAR** as tabelas existentes
- **NÃO vai perder dados** 
- É seguro aplicar