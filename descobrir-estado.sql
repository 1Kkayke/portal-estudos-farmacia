-- DESCOBRIR O ESTADO ATUAL DO BANCO APÓS MIGRAÇÃO

-- 1. LISTAR TODAS AS TABELAS
SELECT schemaname, tablename FROM pg_tables WHERE schemaname = 'public' ORDER BY tablename;

-- 2. VERIFICAR COLUNAS DA TABELA Topics  
SELECT column_name, data_type FROM information_schema.columns WHERE table_name = 'Topics' ORDER BY ordinal_position;

-- 3. TESTE A: Colunas em português na tabela Topics?
-- SELECT "DisciplinaId", "NomeDisciplina" FROM "Topics" LIMIT 1;

-- 4. TESTE B: Colunas ainda em inglês na tabela Topics?
SELECT "Id", "Nome" FROM "Topics" LIMIT 1;

-- 5. VERIFICAR COLUNAS DA TABELA Documents
SELECT column_name, data_type FROM information_schema.columns WHERE table_name = 'Documents' ORDER BY ordinal_position;

-- 6. TESTE DOCUMENTS: Colunas em português?
-- SELECT "DocumentoId", "TituloDocumento" FROM "Documents" LIMIT 1;

-- 7. TESTE DOCUMENTS: Colunas em inglês?  
SELECT "Id", "Titulo" FROM "Documents" LIMIT 1;