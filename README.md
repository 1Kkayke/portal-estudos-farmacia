# 📚 Portal de Estudos de Farmácia

Uma plataforma completa para estudos farmacêuticos com materiais organizados, simulados e ferramentas de produtividade.

## ✨ O que você pode fazer aqui

- **📖 Estudar por tópicos** - Materiais organizados por disciplinas e assuntos
- **📝 Fazer anotações** - Sistema integrado de notas pessoais
- **❓ Resolver questões** - Banco de questões com feedback imediato
- **📊 Simular provas** - Simulados completos para testar conhecimentos
- **⏱️ Pomodoro integrado** - Técnica de estudos com timer
- **🃏 Flashcards** - Memorização ativa de conceitos importantes

## 🚀 Como executar o projeto

### Pré-requisitos
- Node.js 18+ 
- .NET 8
- PostgreSQL ou SQLite

### 1. Backend (API)
```bash
cd PortalEstudos.API
dotnet restore
dotnet run
```
💡 **A API estará disponível em:** `https://localhost:7296`

### 2. Frontend (Cliente)
```bash
cd portal-estudos-client
npm install
npm run dev
```
💡 **O frontend estará disponível em:** `http://localhost:5173`

## 👤 Primeiro acesso

1. **Registre-se** - Crie sua conta de estudante
2. **Onboarding** - Escolha seus tópicos de interesse  
3. **Comece a estudar** - Explore materiais, questões e simulados

## 🛠️ Stack tecnológica

**Frontend:**
- React 18 + Vite
- Tailwind CSS
- React Router
- Axios

**Backend:**
- ASP.NET Core 8
- Entity Framework Core
- JWT Authentication
- PostgreSQL/SQLite

## 📁 Arquitetura

```
├── portal-estudos-client/    # Frontend React
│   ├── src/
│   │   ├── components/       # Componentes reutilizáveis
│   │   ├── pages/           # Páginas da aplicação
│   │   ├── contexts/        # Contextos (Auth, etc)
│   │   └── services/        # Serviços de API
├── PortalEstudos.API/       # Backend .NET
│   ├── Controllers/         # Endpoints REST
│   ├── Models/             # Modelos de dados
│   ├── Services/           # Lógica de negócio
│   └── Data/              # Configuração EF Core
```

## 🔧 Configuração de ambiente

### Variáveis de ambiente (.env):
```env
# Frontend
VITE_API_BASE_URL=http://localhost:5000/api

# Backend
ConnectionStrings__DefaultConnection=Data Source=PortalEstudos.db
JWT_SECRET_KEY=your-secret-key-here
```

## 🚀 Deploy

- **Frontend:** Netlify/Vercel ready
- **Backend:** Docker + Cloud platforms
- **Database:** PostgreSQL recomendado para produção

---

*Transformando o estudo farmacêutico através de tecnologia moderna e intuitiva*