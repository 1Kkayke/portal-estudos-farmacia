-- TESTE RÁPIDO - DESCOBRIR ESTRUTURA DO BANCO

-- 1. Ver todas as tabelas que existem
SELECT 
    schemaname,
    tablename 
FROM pg_tables 
WHERE schemaname = 'public'
ORDER BY tablename;

-- 2. Se a tabela Topics existe (schema inglês), testar:
SELECT 
    "Id",
    "Nome", 
    "Categoria"
FROM "Topics" 
LIMIT 3;

-- 3. Se a tabela Disciplinas existe (schema português), testar:
-- SELECT 
--     DisciplinaId,
--     NomeDisciplina, 
--     CategoriaDisciplina
-- FROM Disciplinas 
-- LIMIT 3;

-- 4. Verificar colunas de uma tabela específica
SELECT 
    column_name,
    data_type 
FROM information_schema.columns 
WHERE table_name = 'Topics' 
ORDER BY ordinal_position;