# 🚀 DEPLOY RÁPIDO - Portal Estudos

## ⚡ Opção 1: Deploy Imediato via Netlify CLI (5 minutos)

```powershell
# 1. Instalar Netlify CLI
npm install -g netlify-cli

# 2. Login
netlify login

# 3. Navegar para o frontend
cd C:\GITHUB\PortalEstudos\portal-estudos-client

# 4. Deploy direto (sem configurar nada ainda)
netlify deploy --prod

# Selecione:
# - Create & configure a new site
# - Team: Seu team
# - Site name: portal-estudos (ou outro nome)
# - Publish directory: dist
```

**⚠️ ATENÇÃO**: A API ainda estará apontando para `localhost`. Você precisa:
1. Hospedar a API em Azure/Railway/Render (veja DEPLOY-NETLIFY.md)
2. Configurar variável `VITE_API_BASE_URL` no Netlify Dashboard

---

## ⚡ Opção 2: Deploy via Netlify Dashboard (Drag & Drop)

```powershell
# 1. Fazer build local
cd C:\GITHUB\PortalEstudos\portal-estudos-client
npm run build

# 2. Ir para: https://app.netlify.com
# 3. Clicar: "Add new site" → "Deploy manually"
# 4. Arrastar a pasta dist\ para a área de upload
```

---

## 🔧 Configurar depois do deploy

### No Netlify Dashboard:

1. **Site settings → Environment variables**:
   ```
   VITE_API_BASE_URL = https://sua-api.azurewebsites.net/api
   VITE_APP_ENV = production
   ```

2. **Site settings → Domain management**:
   - Mude o nome do site para algo memorável

3. **Rebuild**:
   ```
   Deploys → Trigger deploy → Deploy site
   ```

---

## 🎯 PRÓXIMO PASSO CRÍTICO: Hospedar a API

**A API não pode ficar em localhost!** Escolha uma opção:

### Azure (Recomendado para produção)
```powershell
cd C:\GITHUB\PortalEstudos\PortalEstudos.API
# Veja comandos completos em DEPLOY-NETLIFY.md seção "Azure"
```

### Railway (Mais rápido, gratuito inicial)
```powershell
npm install -g @railway/cli
railway login
cd C:\GITHUB\PortalEstudos\PortalEstudos.API
railway init
railway up
```

### Render (Simples, gratuito)
- https://render.com → New Web Service
- Conecte GitHub → Selecione projeto
- Configure e deploy

---

## ✅ Checklist Rápido

- [ ] Frontend no Netlify funcionando
- [ ] API hospedada (Azure/Railway/Render)
- [ ] `VITE_API_BASE_URL` configurada no Netlify
- [ ] `CORS_ALLOWED_ORIGINS` configurada na API
- [ ] `JWT_SECRET_KEY` gerada aleatoriamente e configurada na API
- [ ] Testado login/logout
- [ ] Verificado headers de segurança em https://securityheaders.com

---

**Para guia completo, veja: DEPLOY-NETLIFY.md**
