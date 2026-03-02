-- ========================================
-- CONSULTAS DE TESTE - VERIFICAR ESTRUTURA PRIMEIRO
-- ========================================

-- VERIFICAR O QUE MUDOU APÓS A MIGRAÇÃO
SELECT 
    schemaname,
    tablename 
FROM pg_tables 
WHERE schemaname = 'public'
ORDER BY tablename;

-- TESTE 1: Se colunas mudaram para português (na tabela Topics)
SELECT 
    "DisciplinaId",
    "NomeDisciplina",
    "CategoriaDisciplina"
FROM "Topics" 
LIMIT 3;

-- TESTE 2: Se colunas ainda estão em inglês (fallback)
-- SELECT 
--     "Id" as DisciplinaId,
--     "Nome" as NomeDisciplina,
--     "Categoria" as CategoriaDisciplina
-- FROM "Topics" 
-- LIMIT 3;

-- 2. Contar documentos por disciplina
-- (Assumindo que tabelas estão em inglês, mas colunas podem estar em português)
SELECT 
    t."NomeDisciplina",
    COUNT(d."DocumentoId") as QuantidadeDocumentos
FROM "Topics" t
LEFT JOIN "Documents" d ON t."DisciplinaId" = d."DisciplinaId"
GROUP BY t."DisciplinaId", t."NomeDisciplina"
ORDER BY QuantidadeDocumentos DESC;

-- Fallback se colunas ainda em inglês:
-- SELECT 
--     t."Nome" as NomeDisciplina,
--     COUNT(d."Id") as QuantidadeDocumentos
-- FROM "Topics" t
-- LEFT JOIN "Documents" d ON t."Id" = d."TopicId"
-- GROUP BY t."Id", t."Nome"
-- ORDER BY QuantidadeDocumentos DESC;

-- 3. Consultar documentos de uma disciplina específica (INGLÊS)
SELECT 
    d."Id" as DocumentoId,
    d."Titulo" as TituloDocumento,
    d."Resumo" as ResumoDocumento,
    d."Dificuldade" as NivelDificuldade,
    d."LeituraMinutos" as TempoLeituraMinutos,
    d."Ordem" as OrdemDocumento,
    t."Nome" as NomeDisciplina
FROM "Documents" d
INNER JOIN "Topics" t ON d."TopicId" = t."Id"
WHERE t."Nome" LIKE '%Farmacologia%'
ORDER BY d."Ordem";

-- 4. Listar questões de uma disciplina
SELECT 
    q.QuestaoId,
    q.EnunciadoQuestao,
    q.NivelDificuldade,
    q.OrdemQuestao,
    d.NomeDisciplina
FROM Questoes q
INNER JOIN Disciplinas d ON q.DisciplinaId = d.DisciplinaId
WHERE d.DisciplinaId = 1
ORDER BY q.OrdemQuestao
LIMIT 5;

-- 5. Verificar usuários cadastrados
SELECT 
    UsuarioId,
    NomeUsuario,
    Email,
    NomeCompleto,
    DataCadastro,
    DataNascimento
FROM Usuarios
ORDER BY DataCadastro DESC;

-- 6. Consultar anotações de um usuário
SELECT 
    a.AnotacaoId,
    a.TituloAnotacao,
    LEFT(a.ConteudoAnotacao, 100) as PreviewConteudo,
    a.DataCriacao,
    a.DataAtualizacao,
    d.NomeDisciplina,
    u.NomeUsuario
FROM Anotacoes a
INNER JOIN Disciplinas d ON a.DisciplinaId = d.DisciplinaId
INNER JOIN Usuarios u ON a.UsuarioId = u.UsuarioId
ORDER BY a.DataAtualizacao DESC
LIMIT 10;

-- 7. Estatísticas de simulados
SELECT 
    s.SimuladoId,
    s.TituloSimulado,
    s.QuantidadeQuestoes,
    s.DuracaoMinutos,
    d.NomeDisciplina,
    COUNT(t.TentativaId) as TotalTentativas
FROM Simulados s
INNER JOIN Disciplinas d ON s.DisciplinaId = d.DisciplinaId
LEFT JOIN TentativasSimulado t ON s.SimuladoId = t.SimuladoId
GROUP BY s.SimuladoId, s.TituloSimulado, s.QuantidadeQuestoes, s.DuracaoMinutos, d.NomeDisciplina
ORDER BY TotalTentativas DESC;

-- 8. Verificar estrutura das tabelas principais
SELECT 
    table_name as NomeTabela,
    column_name as NomeColuna,
    data_type as TipoDados,
    is_nullable as AceitaNulo
FROM information_schema.columns 
WHERE table_name IN ('Disciplinas', 'Documentos', 'Questoes', 'Usuarios', 'Anotacoes')
ORDER BY table_name, ordinal_position;

-- 9. Disciplinas com mais interesse dos usuários
SELECT 
    d.NomeDisciplina,
    COUNT(i.InteresseId) as QuantidadeInteresses,
    d.CategoriaDisciplina
FROM Disciplinas d
LEFT JOIN InteressesDisciplina i ON d.DisciplinaId = i.DisciplinaId
GROUP BY d.DisciplinaId, d.NomeDisciplina, d.CategoriaDisciplina
ORDER BY QuantidadeInteresses DESC
LIMIT 10;

-- 10. Atividades recentes dos usuários
SELECT 
    u.NomeUsuario,
    d.NomeDisciplina,
    a.DataUltimoAcesso,
    a.QuantidadeAcessos
FROM AtividadesDisciplina a
INNER JOIN Usuarios u ON a.UsuarioId = u.UsuarioId
INNER JOIN Disciplinas d ON a.DisciplinaId = d.DisciplinaId
ORDER BY a.DataUltimoAcesso DESC
LIMIT 15;

-- ========================================
-- CONSULTAS PARA VERIFICAR INTEGRIDADE
-- ========================================

-- Verificar se todas as foreign keys estão corretas
SELECT 'Documentos sem disciplina' as Verificacao, COUNT(*) as Quantidade
FROM Documentos d
LEFT JOIN Disciplinas disc ON d.DisciplinaId = disc.DisciplinaId
WHERE disc.DisciplinaId IS NULL

UNION ALL

SELECT 'Questões sem disciplina' as Verificacao, COUNT(*) as Quantidade
FROM Questoes q
LEFT JOIN Disciplinas disc ON q.DisciplinaId = disc.DisciplinaId
WHERE disc.DisciplinaId IS NULL

UNION ALL

SELECT 'Anotações sem usuário' as Verificacao, COUNT(*) as Quantidade
FROM Anotacoes a
LEFT JOIN Usuarios u ON a.UsuarioId = u.UsuarioId
WHERE u.UsuarioId IS NULL;