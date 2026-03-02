-- TESTES RÁPIDOS - SCHEMA EM PORTUGUÊS

-- 1. Verificar se as tabelas existem
SELECT table_name FROM information_schema.tables 
WHERE table_schema = 'public' 
ORDER BY table_name;

-- 2. Disciplinas básicas (teste simples)
SELECT DisciplinaId, NomeDisciplina, CategoriaDisciplina 
FROM Disciplinas 
LIMIT 5;

-- 3. Contar registros principais
SELECT 
    'Disciplinas' as Tabela, COUNT(*) as Total FROM Disciplinas
UNION ALL
SELECT 
    'Documentos' as Tabela, COUNT(*) as Total FROM Documentos
UNION ALL
SELECT 
    'Questoes' as Tabela, COUNT(*) as Total FROM Questoes;

-- 4. Primeira disciplina completa
SELECT * FROM Disciplinas WHERE DisciplinaId = 1;

-- 5. Primeiro documento
SELECT DocumentoId, TituloDocumento, NivelDificuldade 
FROM Documentos 
LIMIT 1;