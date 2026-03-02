# Como conectar e testar

## 1. Conectar via psql (substitua SUA_SENHA pela senha real):
```bash
psql "postgresql://portalestudos_user:SUA_SENHA@dpg-d6gv2q4r85hc73a30mug-a.oregon-postgres.render.com:5432/portalestudos?sslmode=require"
```

## 2. Teste rápido após conectar:
```sql
-- Ver tabelas criadas
\dt

-- Consulta simples
SELECT DisciplinaId, NomeDisciplina FROM Disciplinas LIMIT 3;
```

## 3. Se der erro de conexão, teste primeiro:
```bash
nslookup dpg-d6gv2q4r85hc73a30mug-a.oregon-postgres.render.com
```

## 4. Para usar no DataGrip/PostgreSQL Explorer:
- Host: dpg-d6gv2q4r85hc73a30mug-a.oregon-postgres.render.com
- Port: 5432  
- Database: portalestudos
- User: portalestudos_user
- Password: [sua senha]
- SSL Mode: require