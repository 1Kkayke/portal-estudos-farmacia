# 🚀 Guia de Deploy - Portal Estudos no Netlify

## 📋 Pré-requisitos

- Conta no Netlify (gratuita): https://app.netlify.com/signup
- Conta no GitHub (seu projeto já está local, vamos conectar)
- Backend hospedado (Azure, Railway, Render, ou AWS)

---

## 🔐 PARTE 1: Segurança - Configurar Variáveis de Ambiente

### Frontend (Netlify)

1. **Criar arquivo `.env` local para desenvolvimento** (NÃO commitar!):
   ```bash
   # No diretório: portal-estudos-client/
   VITE_API_BASE_URL=http://localhost:5000/api
   VITE_APP_ENV=development
   ```

2. **No Netlify Dashboard** (após criar o site):
   - Vá em: `Site settings` → `Environment variables`
   - Adicione:
     ```
     VITE_API_BASE_URL=https://sua-api-production.azurewebsites.net/api
     VITE_APP_ENV=production
     VITE_ENABLE_LOGGING=false
     ```

### Backend (Azure/Railway/Render)

Você precisará configurar estas variáveis no serviço de hospedagem:

```bash
# Database
DB_CONNECTION_STRING=seu-connection-string-producao

# JWT (CRÍTICO - gere uma chave forte!)
JWT_SECRET_KEY=sua-chave-super-secreta-minimo-32-caracteres-aleatoria-ABC123XYZ789

# CORS (seu domínio Netlify)
CORS_ALLOWED_ORIGINS=https://seu-app.netlify.app

# Environment
ASPNETCORE_ENVIRONMENT=Production

# Rate Limiting
RATE_LIMIT_ENABLED=true
RATE_LIMIT_WINDOW_SECONDS=60
RATE_LIMIT_MAX_REQUESTS=100
```

**⚠️ IMPORTANTE: Nunca use senhas/chaves hardcoded em produção!**

---

## 📦 PARTE 2: Deploy do Frontend no Netlify

### Opção A: Deploy via Netlify CLI (Recomendado)

1. **Instalar Netlify CLI**:
   ```powershell
   npm install -g netlify-cli
   ```

2. **Login no Netlify**:
   ```powershell
   netlify login
   ```

3. **Navegar para o diretório do frontend**:
   ```powershell
   cd C:\GITHUB\PortalEstudos\portal-estudos-client
   ```

4. **Inicializar o site**:
   ```powershell
   netlify init
   ```
   - Escolha: `Create & configure a new site`
   - Team: Escolha seu team
   - Site name: `portal-estudos` (ou outro nome disponível)
   - Build command: `npm run build`
   - Directory to deploy: `dist`

5. **Deploy**:
   ```powershell
   netlify deploy --prod
   ```

### Opção B: Deploy via Netlify Dashboard (Manual)

1. **Fazer build local**:
   ```powershell
   cd C:\GITHUB\PortalEstudos\portal-estudos-client
   npm run build
   ```

2. **No Netlify Dashboard**:
   - Vá em: https://app.netlify.com
   - Clique: `Add new site` → `Deploy manually`
   - Arraste a pasta `dist` para a área de upload

3. **Configurar domínio**:
   - Vá em: `Site settings` → `Domain management`
   - Seu site estará em: `https://random-name.netlify.app`
   - Você pode mudar para: `https://portal-estudos.netlify.app`

### Opção C: Deploy via GitHub (Recomendado para produção)

1. **Criar repositório no GitHub** (se ainda não tiver):
   ```powershell
   cd C:\GITHUB\PortalEstudos
   git init
   git add .
   git commit -m "Initial commit - Portal Estudos"
   gh repo create portal-estudos --private --source=. --push
   ```

2. **No Netlify Dashboard**:
   - Clique: `Add new site` → `Import from Git`
   - Escolha: `GitHub`
   - Selecione: seu repositório `portal-estudos`
   - Base directory: `portal-estudos-client`
   - Build command: `npm run build`
   - Publish directory: `portal-estudos-client/dist`
   - Clique: `Deploy site`

3. **Configurar variáveis de ambiente**:
   - `Site settings` → `Environment variables`
   - Adicione as variáveis listadas na PARTE 1

---

## 🔧 PARTE 3: Deploy do Backend (.NET API)

O backend precisa estar hospedado em um serviço que suporte .NET. Opções:

### Opção A: Azure App Service (Recomendado)

1. **Instalar Azure CLI**:
   ```powershell
   winget install Microsoft.AzureCLI
   ```

2. **Login**:
   ```powershell
   az login
   ```

3. **Criar Resource Group**:
   ```powershell
   az group create --name PortalEstudosRG --location eastus
   ```

4. **Criar App Service Plan**:
   ```powershell
   az appservice plan create --name PortalEstudosPlan --resource-group PortalEstudosRG --sku B1 --is-linux
   ```

5. **Criar Web App**:
   ```powershell
   az webapp create --resource-group PortalEstudosRG --plan PortalEstudosPlan --name portal-estudos-api --runtime "DOTNET|8.0"
   ```

6. **Configurar variáveis de ambiente**:
   ```powershell
   az webapp config appsettings set --resource-group PortalEstudosRG --name portal-estudos-api --settings `
     ASPNETCORE_ENVIRONMENT="Production" `
     JWT_SECRET_KEY="sua-chave-segura" `
     CORS_ALLOWED_ORIGINS="https://portal-estudos.netlify.app" `
     DB_CONNECTION_STRING="seu-connection-string"
   ```

7. **Deploy**:
   ```powershell
   cd C:\GITHUB\PortalEstudos\PortalEstudos.API
   dotnet publish -c Release -o ./publish
   az webapp deployment source config-zip --resource-group PortalEstudosRG --name portal-estudos-api --src ./publish
   ```

### Opção B: Railway (Mais simples, gratuito para começar)

1. **Criar conta**: https://railway.app
2. **Instalar Railway CLI**:
   ```powershell
   npm install -g @railway/cli
   ```
3. **Login**:
   ```powershell
   railway login
   ```
4. **Criar projeto**:
   ```powershell
   cd C:\GITHUB\PortalEstudos\PortalEstudos.API
   railway init
   ```
5. **Configurar variáveis**:
   ```powershell
   railway variables set JWT_SECRET_KEY="sua-chave-segura"
   railway variables set CORS_ALLOWED_ORIGINS="https://portal-estudos.netlify.app"
   ```
6. **Deploy**:
   ```powershell
   railway up
   ```

### Opção C: Render (Simples e gratuito)

1. Vá em: https://render.com
2. `New` → `Web Service`
3. Conecte seu repositório GitHub
4. Configure:
   - Name: `portal-estudos-api`
   - Environment: `.NET`
   - Build Command: `dotnet publish -c Release -o out`
   - Start Command: `cd out && dotnet PortalEstudos.API.dll`
5. Adicione as variáveis de ambiente no dashboard

---

## ✅ PARTE 4: Checklist Final de Segurança

### Frontend (Netlify)

- [ ] `VITE_API_BASE_URL` configurada para sua API em produção
- [ ] Arquivo `.env` local está no `.gitignore`
- [ ] Headers de segurança configurados no `netlify.toml` ✅
- [ ] HTTPS forçado (Netlify faz automaticamente) ✅
- [ ] CSP configurada no `netlify.toml` ✅
- [ ] Build de produção funcionando

### Backend (API)

- [ ] `JWT_SECRET_KEY` gerada aleatoriamente (mínimo 32 caracteres)
- [ ] `CORS_ALLOWED_ORIGINS` configurada com domínio Netlify correto
- [ ] Database connection string segura (não usar SQLite em produção)
- [ ] HTTPS habilitado (Azure/Railway/Render forçam automaticamente)
- [ ] Rate limiting ativo ✅
- [ ] Security headers middleware ativo ✅
- [ ] Input validation ativa ✅
- [ ] Logs não expõem dados sensíveis

### Banco de Dados

- [ ] **CRÍTICO**: Migrar de SQLite para PostgreSQL/SQL Server/MySQL em produção
- [ ] Backups automáticos configurados
- [ ] Connection string com senha forte
- [ ] Acesso restrito por IP (se possível)

---

## 🔄 PARTE 5: Atualizar `.gitignore` (NÃO commitar secrets!)

Verifique se o `.gitignore` contém:

```gitignore
# Frontend
portal-estudos-client/.env
portal-estudos-client/.env.local
portal-estudos-client/.env.production.local
portal-estudos-client/dist/

# Backend
PortalEstudos.API/.env
PortalEstudos.API/.env.local
PortalEstudos.API/.env.production
PortalEstudos.API/appsettings.Local.json
PortalEstudos.API/*.db
PortalEstudos.API/*.db-shm
PortalEstudos.API/*.db-wal

# Secrets
*.pem
*.key
*.cert
```

---

## 🧪 PARTE 6: Testar Deploy

### 1. Testar Frontend

```powershell
# Navegue para seu site Netlify
https://portal-estudos.netlify.app

# Teste:
# - Login funciona?
# - API responde (verifique console do navegador F12)?
# - Headers de segurança presentes? (F12 → Network → Headers)
```

### 2. Testar Backend

```powershell
# Teste health check
curl https://sua-api.azurewebsites.net/api/health

# Teste CORS
curl -H "Origin: https://portal-estudos.netlify.app" -I https://sua-api.azurewebsites.net/api/topics
```

### 3. Testar Segurança

Use: https://securityheaders.com
- Digite: `https://portal-estudos.netlify.app`
- Deve ter nota A ou A+

---

## 🔐 PARTE 7: Gerar JWT Secret Seguro

**NUNCA use a chave padrão do `appsettings.json` em produção!**

Gere uma chave forte:

```powershell
# PowerShell
[Convert]::ToBase64String((1..64 | ForEach-Object { Get-Random -Minimum 0 -Maximum 256 }))

# Ou online (NÃO use em produção real por segurança):
# https://generate-random.org/api-token-generator?count=1&length=64&type=alphanumeric

# Resultado exemplo:
# xK9mP2nQ5wT8yU3vA6bC1zD4eF7hJ0iL
```

---

## 📊 PARTE 8: Monitoramento (Opcional)

### Netlify Analytics
- Habilite em: `Site settings` → `Analytics`

### Backend Monitoring
- Azure: Application Insights
- Railway: Logs integrados
- Render: Logs integrados

---

## 🆘 Troubleshooting

### Erro: "API_BASE_URL must use HTTPS in production"

**Solução**: Configure `VITE_API_BASE_URL` no Netlify com `https://`

### Erro: CORS Blocked

**Solução**: Configure `CORS_ALLOWED_ORIGINS` na API com seu domínio Netlify exato

### Erro: 401 Unauthorized

**Solução**: Verifique se `JWT_SECRET_KEY` é a mesma em desenvolvimento e produção (cuidado!)

### Erro: Rate Limited (429)

**Solução**: Ajuste `RATE_LIMIT_MAX_REQUESTS` para valor maior se necessário

---

## 📞 Suporte

- Netlify Docs: https://docs.netlify.com
- Azure Docs: https://docs.microsoft.com/azure
- Railway Docs: https://docs.railway.app
- Render Docs: https://render.com/docs

---

## ✨ Próximos Passos (Opcional)

1. **Custom Domain**: Compre um domínio (namecheap.com, godaddy.com) e configure no Netlify
2. **Database Migration**: Migre de SQLite para PostgreSQL (Supabase, ElephantSQL, Azure Database)
3. **CI/CD**: Configure GitHub Actions para deploy automático
4. **Monitoring**: Configure Sentry para tracking de erros
5. **Analytics**: Configure Google Analytics ou Plausible
6. **Email**: Configure SendGrid ou AWS SES para emails transacionais

---

**🎉 Pronto! Seu Portal Estudos está seguro e pronto para produção!**
