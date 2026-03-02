-- VERIFICAR O QUE MUDOU APÓS A MIGRAÇÃO

-- 1. Ver se as tabelas ainda estão em inglês
SELECT 
    schemaname,
    tablename 
FROM pg_tables 
WHERE schemaname = 'public'
ORDER BY tablename;

-- 2. Verificar se as COLUNAS da tabela Topics mudaram para português
SELECT 
    column_name,
    data_type 
FROM information_schema.columns 
WHERE table_name = 'Topics' 
ORDER BY ordinal_position;

-- 3. Testar se as colunas agora estão em português
SELECT 
    "DisciplinaId",
    "NomeDisciplina", 
    "CategoriaDisciplina"
FROM "Topics" 
LIMIT 3;

-- 4. Se der erro acima, então as colunas ainda estão em inglês:
SELECT 
    "Id" as DisciplinaId,
    "Nome" as NomeDisciplina, 
    "Categoria" as CategoriaDisciplina
FROM "Topics" 
LIMIT 3;