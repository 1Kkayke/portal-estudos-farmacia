namespace PortalEstudos.API.Data;

using PortalEstudos.API.Models;

public static partial class StudyContentSeeder
{
    public record DocDef(string Titulo, string Resumo, string Dificuldade, int Minutos);
    public record QDef(string Enunciado, string[] Opcoes, int Correta, string Explicacao, string Dificuldade);

    private static readonly Dictionary<int, (DocDef[] Docs, QDef[] Qs)> _data = BuildAllTopicData();

    // ═══════════════════════════════════════════════════
    //  PUBLIC API
    // ═══════════════════════════════════════════════════

    public static List<Topic> GetTopics()
    {
        return new List<Topic>
        {
            new() { Id = 1, Nome = "Farmacologia Geral", Descricao = "Princípios básicos de farmacologia", Categoria = "Ciências Farmacêuticas", Icone = "💊", Cor = "#3B82F6" },
            new() { Id = 2, Nome = "Farmacologia Clínica", Descricao = "Aplicação clínica da farmacologia", Categoria = "Ciências Farmacêuticas", Icone = "🏥", Cor = "#10B981" },
            new() { Id = 3, Nome = "Farmacognosia", Descricao = "Estudo de fármacos de origem natural", Categoria = "Ciências Farmacêuticas", Icone = "🌿", Cor = "#059669" },
            new() { Id = 4, Nome = "Farmacotécnica", Descricao = "Manipulação e preparação de medicamentos", Categoria = "Ciências Farmacêuticas", Icone = "⚗️", Cor = "#8B5CF6" },
            new() { Id = 5, Nome = "Tecnologia Farmacêutica", Descricao = "Desenvolvimento industrial de medicamentos", Categoria = "Ciências Farmacêuticas", Icone = "🏭", Cor = "#6366F1" },
            new() { Id = 6, Nome = "Farmácia Hospitalar", Descricao = "Gestão e assistência farmacêutica hospitalar", Categoria = "Assistência Farmacêutica", Icone = "🏨", Cor = "#EC4899" },
            new() { Id = 7, Nome = "Atenção Farmacêutica", Descricao = "Seguimento farmacoterapêutico", Categoria = "Assistência Farmacêutica", Icone = "👥", Cor = "#F59E0B" },
            new() { Id = 8, Nome = "Farmacovigilância", Descricao = "Monitoramento de reações adversas", Categoria = "Assistência Farmacêutica", Icone = "🔍", Cor = "#EF4444" },
            new() { Id = 9, Nome = "Farmacoepidemiologia", Descricao = "Epidemiologia aplicada a medicamentos", Categoria = "Assistência Farmacêutica", Icone = "📊", Cor = "#14B8A6" },
            new() { Id = 10, Nome = "Farmacoterapia", Descricao = "Uso racional de medicamentos", Categoria = "Assistência Farmacêutica", Icone = "💉", Cor = "#06B6D4" },
            new() { Id = 11, Nome = "Química Farmacêutica", Descricao = "Química medicinal e planejamento de fármacos", Categoria = "Ciências Químicas", Icone = "🧪", Cor = "#8B5CF6" },
            new() { Id = 12, Nome = "Química Orgânica", Descricao = "Química dos compostos de carbono", Categoria = "Ciências Químicas", Icone = "⚛️", Cor = "#A855F7" },
            new() { Id = 13, Nome = "Química Analítica", Descricao = "Análise e controle de qualidade", Categoria = "Ciências Químicas", Icone = "🔬", Cor = "#9333EA" },
            new() { Id = 14, Nome = "Controle de Qualidade", Descricao = "Garantia da qualidade farmacêutica", Categoria = "Ciências Químicas", Icone = "✓", Cor = "#7C3AED" },
            new() { Id = 15, Nome = "Análises Toxicológicas", Descricao = "Detecção e quantificação de toxicantes", Categoria = "Análises Clínicas", Icone = "☠️", Cor = "#DC2626" },
            new() { Id = 16, Nome = "Bioquímica", Descricao = "Processos químicos dos seres vivos", Categoria = "Ciências Biológicas", Icone = "🧬", Cor = "#10B981" },
            new() { Id = 17, Nome = "Físico-Química", Descricao = "Propriedades físico-químicas de fármacos", Categoria = "Ciências Químicas", Icone = "⚡", Cor = "#F59E0B" },
            new() { Id = 18, Nome = "Microbiologia Clínica", Descricao = "Diagnóstico microbiológico", Categoria = "Análises Clínicas", Icone = "🦠", Cor = "#3B82F6" },
            new() { Id = 19, Nome = "Parasitologia", Descricao = "Estudo de parasitas e parasitoses", Categoria = "Análises Clínicas", Icone = "🐛", Cor = "#84CC16" },
            new() { Id = 20, Nome = "Imunologia", Descricao = "Sistema imune e resposta imunológica", Categoria = "Ciências Biológicas", Icone = "🛡️", Cor = "#06B6D4" },
            new() { Id = 21, Nome = "Hematologia", Descricao = "Estudo do sangue e hemostasia", Categoria = "Análises Clínicas", Icone = "🩸", Cor = "#EF4444" },
            new() { Id = 22, Nome = "Fisiologia Humana", Descricao = "Funcionamento do organismo", Categoria = "Ciências Biológicas", Icone = "❤️", Cor = "#F43F5E" },
            new() { Id = 23, Nome = "Anatomia e Histologia", Descricao = "Estrutura do corpo humano", Categoria = "Ciências Biológicas", Icone = "🫀", Cor = "#EC4899" },
            new() { Id = 24, Nome = "Patologia Geral", Descricao = "Processos patológicos básicos", Categoria = "Ciências Biológicas", Icone = "🔬", Cor = "#A855F7" },
            new() { Id = 25, Nome = "Genética e Biologia Molecular", Descricao = "Genética aplicada à farmácia", Categoria = "Ciências Biológicas", Icone = "🧬", Cor = "#8B5CF6" },
            new() { Id = 26, Nome = "Botânica Farmacêutica", Descricao = "Plantas medicinais e identificação", Categoria = "Ciências Naturais", Icone = "🌱", Cor = "#22C55E" },
            new() { Id = 27, Nome = "Epidemiologia", Descricao = "Distribuição e determinantes de doenças", Categoria = "Saúde Coletiva", Icone = "🗺️", Cor = "#14B8A6" },
            new() { Id = 28, Nome = "Saúde Pública e SUS", Descricao = "Sistema de saúde brasileiro", Categoria = "Saúde Coletiva", Icone = "🏛️", Cor = "#0EA5E9" },
            new() { Id = 29, Nome = "Gestão Farmacêutica", Descricao = "Administração de farmácias e drogarias", Categoria = "Gestão", Icone = "📈", Cor = "#F97316" },
            new() { Id = 30, Nome = "Oncologia Farmacêutica", Descricao = "Farmacoterapia do câncer", Categoria = "Assistência Farmacêutica", Icone = "🎗️", Cor = "#EC4899" },
            new() { Id = 31, Nome = "Farmacologia Cardiovascular", Descricao = "Medicamentos cardiovasculares", Categoria = "Ciências Farmacêuticas", Icone = "💓", Cor = "#EF4444" },
            new() { Id = 32, Nome = "Farmacologia do SNC", Descricao = "Medicamentos do sistema nervoso", Categoria = "Ciências Farmacêuticas", Icone = "🧠", Cor = "#8B5CF6" },
            new() { Id = 33, Nome = "Dermatologia Farmacêutica", Descricao = "Medicamentos dermatológicos", Categoria = "Ciências Farmacêuticas", Icone = "🧴", Cor = "#F472B6" },
            new() { Id = 34, Nome = "Farmacologia Anti-infecciosa", Descricao = "Antimicrobianos e antivirais", Categoria = "Ciências Farmacêuticas", Icone = "🦠", Cor = "#10B981" },
            new() { Id = 35, Nome = "Farmacologia Endócrina", Descricao = "Medicamentos hormonais", Categoria = "Ciências Farmacêuticas", Icone = "⚖️", Cor = "#06B6D4" },
            new() { Id = 36, Nome = "Farmacologia Respiratória", Descricao = "Medicamentos respiratórios", Categoria = "Ciências Farmacêuticas", Icone = "🫁", Cor = "#0EA5E9" },
            new() { Id = 37, Nome = "Toxicologia", Descricao = "Efeitos tóxicos de substâncias", Categoria = "Ciências Farmacêuticas", Icone = "☠️", Cor = "#DC2626" },
            new() { Id = 38, Nome = "Plantas Medicinais e Fitoterapia", Descricao = "Terapêutica com plantas", Categoria = "Ciências Naturais", Icone = "🍃", Cor = "#059669" },
            new() { Id = 39, Nome = "Farmácia Magistral", Descricao = "Manipulação de formulações", Categoria = "Ciências Farmacêuticas", Icone = "⚗️", Cor = "#A855F7" },
            new() { Id = 40, Nome = "Biofarmácia", Descricao = "Biodisponibilidade e bioequivalência", Categoria = "Ciências Farmacêuticas", Icone = "📊", Cor = "#6366F1" },
            new() { Id = 41, Nome = "Farmacometria", Descricao = "Farmacocinética clínica", Categoria = "Ciências Farmacêuticas", Icone = "📐", Cor = "#8B5CF6" },
            new() { Id = 42, Nome = "Micologia Clínica", Descricao = "Diagnóstico de infecções fúngicas", Categoria = "Análises Clínicas", Icone = "🍄", Cor = "#A78BFA" }
        };
    }

    public static List<Document> GetDocuments()
    {
        var list = new List<Document>();
        int id = 1;
        foreach (var (topicId, (docs, _)) in _data.OrderBy(x => x.Key))
        {
            int order = 1;
            foreach (var d in docs)
            {
                list.Add(new Document
                {
                    Id = id,
                    TopicId = topicId,
                    Titulo = d.Titulo,
                    Resumo = d.Resumo,
                    Conteudo = GenerateDocHtml(topicId, d.Titulo, d.Resumo, order),
                    Ordem = order,
                    Dificuldade = d.Dificuldade,
                    LeituraMinutos = d.Minutos,
                });
                id++;
                order++;
            }
        }
        return list;
    }

    public static List<Question> GetQuestions()
    {
        var list = new List<Question>();
        int id = 1;
        foreach (var (topicId, (_, qs)) in _data.OrderBy(x => x.Key))
        {
            int order = 1;
            foreach (var q in qs)
            {
                list.Add(new Question
                {
                    Id = id,
                    TopicId = topicId,
                    Enunciado = q.Enunciado,
                    Explicacao = q.Explicacao,
                    RespostaCorreta = q.Correta,
                    Dificuldade = q.Dificuldade,
                    Ordem = order,
                });
                id++;
                order++;
            }
        }
        return list;
    }

    public static List<QuestionOption> GetQuestionOptions()
    {
        var list = new List<QuestionOption>();
        int optId = 1;
        int qId = 1;
        foreach (var (_, (_, qs)) in _data.OrderBy(x => x.Key))
        {
            foreach (var q in qs)
            {
                for (int i = 0; i < q.Opcoes.Length && i < 4; i++)
                {
                    list.Add(new QuestionOption
                    {
                        Id = optId++,
                        QuestionId = qId,
                        Texto = q.Opcoes[i],
                        Indice = i,
                    });
                }
                qId++;
            }
        }
        return list;
    }

    public static void EnsureSeeded(ApplicationDbContext db)
    {
        if (db.Topics.Any() || db.Documents.Any() || db.Questions.Any()) return;

        var topics = GetTopics();
        var documents = GetDocuments();
        var questions = GetQuestions();
        var options = GetQuestionOptions();

        db.Topics.AddRange(topics);
        db.Documents.AddRange(documents);
        db.Questions.AddRange(questions);
        db.QuestionOptions.AddRange(options);
        db.SaveChanges();
    }

    // ═══════════════════════════════════════════════════
    //  HTML CONTENT GENERATOR
    // ═══════════════════════════════════════════════════

    private static string GenerateDocHtml(int topicId, string titulo, string resumo, int idx)
    {
        return $@"
<h2>{titulo}</h2>
<p class='lead'>{resumo}</p>

<h3>Introdução</h3>
<p>Este documento aborda os conceitos fundamentais relacionados a <strong>{titulo.ToLower()}</strong>, um tema essencial para a formação do profissional farmacêutico. O domínio deste conteúdo é frequentemente cobrado em provas de concursos e residências farmacêuticas.</p>

<h3>Conceitos Fundamentais</h3>
<p>A compreensão aprofundada deste tema requer o conhecimento de princípios teóricos e sua aplicação na prática farmacêutica. Os principais pontos a serem estudados incluem definições, classificações, mecanismos e aplicações clínicas.</p>
<ul>
<li>Definições e terminologia técnica específica da área</li>
<li>Classificação e categorização sistemática</li>
<li>Mecanismos de ação e princípios fundamentais</li>
<li>Aplicações práticas no contexto farmacêutico</li>
<li>Correlações clínicas e relevância terapêutica</li>
</ul>

<h3>Aspectos Práticos</h3>
<p>Na prática profissional, o farmacêutico deve ser capaz de aplicar estes conhecimentos para tomada de decisões clínicas, orientação de pacientes e garantia da qualidade dos serviços prestados.</p>

<h3>Pontos-Chave para Revisão</h3>
<ul>
<li>Dominar os conceitos teóricos fundamentais</li>
<li>Compreender as aplicações clínicas e práticas</li>
<li>Conhecer a legislação e normas relacionadas</li>
<li>Relacionar com outras disciplinas da formação farmacêutica</li>
</ul>
";
    }

    // ═══════════════════════════════════════════════════
    //  DOC BUILDER HELPER
    // ═══════════════════════════════════════════════════

    internal static DocDef[] BuildDocs(string topicName, string[] titles)
    {
        var difs = new[] { "Básico", "Intermediário", "Avançado" };
        var docs = new DocDef[Math.Min(titles.Length, 20)];
        for (int i = 0; i < docs.Length; i++)
        {
            docs[i] = new DocDef(
                titles[i],
                $"Estudo detalhado sobre {titles[i].ToLower()} no contexto de {topicName}. Abordagem teórica e prática com correlações clínicas.",
                difs[i % 3],
                7 + (i % 6)
            );
        }
        return docs;
    }

    internal static QDef[] BuildFromTerms(string topicName, QDef[] baseQs, (string Term, string Def)[] terms)
    {
        var qs = new List<QDef>();
        string[] difs = { "Fácil", "Médio", "Médio", "Difícil" };

        if (baseQs.Length > 0)
            qs.AddRange(baseQs);

        var termList = terms.Select(t => t.Term).ToArray();
        var defList = terms.Select(t => t.Def).ToArray();

        for (int i = 0; i < terms.Length && qs.Count < 40; i++)
        {
            var term = terms[i].Term;
            var def = terms[i].Def;

            var termToDef = BuildOptions(def, defList, i);
            qs.Add(new QDef(
                $"Em {topicName}, \"{term}\" refere-se a:",
                termToDef.Options,
                termToDef.CorrectIndex,
                def,
                difs[qs.Count % 4]));

            if (qs.Count >= 40) break;

            var defToTerm = BuildOptions(term, termList, i + 1);
            qs.Add(new QDef(
                $"Qual termo corresponde a: {def}",
                defToTerm.Options,
                defToTerm.CorrectIndex,
                $"O termo correto e {term}.",
                difs[qs.Count % 4]));
        }

        return qs.Take(40).ToArray();
    }

    private static (string[] Options, int CorrectIndex) BuildOptions(string correct, string[] pool, int seed)
    {
        var options = new List<string>(4) { correct };
        int idx = seed % pool.Length;
        while (options.Count < 4)
        {
            var candidate = pool[idx % pool.Length];
            if (!options.Contains(candidate))
                options.Add(candidate);
            idx++;
        }

        int correctIndex = seed % 4;
        var arranged = new string[4];
        arranged[correctIndex] = correct;
        int k = 0;
        for (int i = 0; i < 4; i++)
        {
            if (i == correctIndex) continue;
            arranged[i] = options[++k];
        }

        return (arranged, correctIndex);
    }

    // ═══════════════════════════════════════════════════
    //  ALL 42 TOPICS DATA BUILDER
    // ═══════════════════════════════════════════════════

    private static Dictionary<int, (DocDef[] Docs, QDef[] Qs)> BuildAllTopicData()
    {
        var data = new Dictionary<int, (DocDef[], QDef[])>();

        // ─── Topic 1: Farmacologia Geral ───
        data[1] = (
            new DocDef[] {
                new("Introdução à Farmacologia", "Conceitos fundamentais de farmacologia, definições de fármaco, droga e medicamento. Histórico e importância na prática clínica.", "Básico", 8),
                new("Farmacocinética: Absorção", "Mecanismos de absorção de fármacos, biodisponibilidade, fatores que influenciam a absorção e vias de administração.", "Intermediário", 10),
                new("Farmacocinética: Distribuição", "Distribuição de fármacos nos tecidos, ligação a proteínas plasmáticas, volume de distribuição e barreiras biológicas.", "Intermediário", 10),
                new("Farmacocinética: Metabolismo", "Biotransformação hepática, reações de fase I e fase II, sistema CYP450, indução e inibição enzimática.", "Avançado", 12),
                new("Farmacocinética: Excreção", "Vias de excreção renal, biliar e pulmonar. Depuração, meia-vida e estado de equilíbrio.", "Intermediário", 10),
                new("Farmacodinâmica: Receptores", "Tipos de receptores farmacológicos, mecanismos de transdução de sinal, agonistas e antagonistas.", "Intermediário", 12),
                new("Curva Dose-Resposta", "Relação dose-resposta, potência, eficácia máxima, DE50, DL50 e índice terapêutico.", "Intermediário", 8),
                new("Interações Medicamentosas", "Interações farmacocinéticas e farmacodinâmicas, sinergismo, antagonismo e implicações clínicas.", "Avançado", 12),
                new("Farmacologia do SNA", "Sistema nervoso autônomo: agonistas e antagonistas colinérgicos e adrenérgicos.", "Intermediário", 12),
                new("Anti-inflamatórios Não Esteroidais", "AINEs: mecanismo de inibição da COX, classificação, indicações e efeitos adversos.", "Intermediário", 10),
                new("Corticosteroides", "Glicocorticoides e mineralocorticoides: mecanismo, indicações, efeitos colaterais e retirada gradual.", "Intermediário", 10),
                new("Analgésicos Opioides", "Classificação dos opioides, receptores mu/kappa/delta, tolerância, dependência e uso clínico.", "Avançado", 12),
                new("Antibióticos Beta-Lactâmicos", "Penicilinas, cefalosporinas, carbapenêmicos: mecanismo de ação, espectro e resistência.", "Intermediário", 12),
                new("Aminoglicosídeos e Quinolonas", "Mecanismo de ação, espectro antibacteriano, nefrotoxicidade e ototoxicidade.", "Intermediário", 10),
                new("Antifúngicos", "Azólicos, poliênicos, equinocandinas: mecanismos de ação e indicações clínicas.", "Intermediário", 10),
                new("Anti-hipertensivos", "Classes de anti-hipertensivos: IECA, BRA, BCC, diuréticos e betabloqueadores.", "Intermediário", 12),
                new("Antidiabéticos", "Insulinas, metformina, sulfonilureias, inibidores de SGLT2 e agonistas GLP-1.", "Intermediário", 12),
                new("Anticoagulantes", "Heparina, varfarina, DOACs: mecanismo de ação, monitoramento e manejo de sangramento.", "Avançado", 10),
                new("Psicotrópicos: Antidepressivos", "ISRS, IRSN, tricíclicos, IMAO: mecanismo, indicações e síndrome serotoninérgica.", "Avançado", 12),
                new("Psicotrópicos: Antipsicóticos", "Típicos e atípicos, bloqueio dopaminérgico, efeitos extrapiramidais e síndrome metabólica.", "Avançado", 12),
            },
            new QDef[] {
                new("Qual fase da farmacocinética envolve a passagem do fármaco do local de administração para a circulação sistêmica?", new[]{"Absorção","Distribuição","Metabolismo","Excreção"}, 0, "A absorção é a primeira fase farmacocinética, envolvendo a transferência do fármaco do local de administração para a corrente sanguínea.", "Fácil"),
                new("O volume de distribuição (Vd) elevado indica que o fármaco:", new[]{"Concentra-se nos tecidos","Permanece no plasma","É pouco absorvido","Tem alta ligação proteica"}, 0, "Um Vd elevado indica ampla distribuição tecidual, ultrapassando o volume plasmático.", "Médio"),
                new("As reações de fase I do metabolismo hepático envolvem principalmente:", new[]{"Oxidação, redução e hidrólise","Conjugação com ácido glicurônico","Acetilação e metilação","Sulfatação e glutationação"}, 0, "As reações de fase I (funcionalização) incluem oxidação, redução e hidrólise, catalisadas pelo CYP450.", "Médio"),
                new("A biodisponibilidade de um fármaco administrado por via intravenosa é:", new[]{"100%","Variável","50%","Depende do metabolismo"}, 0, "Por via IV, o fármaco atinge diretamente a circulação sistêmica sem passagem pelo efeito de primeira passagem, resultando em biodisponibilidade de 100%.", "Fácil"),
                new("Qual enzima do CYP450 é responsável pelo metabolismo da maioria dos fármacos?", new[]{"CYP3A4","CYP2D6","CYP1A2","CYP2C19"}, 0, "O CYP3A4 metaboliza aproximadamente 50% dos fármacos de uso clínico, sendo a isoforma mais importante.", "Médio"),
                new("O índice terapêutico de um fármaco é calculado pela razão:", new[]{"DL50/DE50","DE50/DL50","Cmax/Cmin","AUC/MIC"}, 0, "O índice terapêutico é a razão entre a dose letal mediana (DL50) e a dose efetiva mediana (DE50). Quanto maior, mais seguro o fármaco.", "Médio"),
                new("Um antagonista competitivo reversível:", new[]{"Desloca a curva dose-resposta para a direita sem reduzir o efeito máximo","Reduz o efeito máximo do agonista","Liga-se irreversivelmente ao receptor","Atua em sítio diferente do agonista"}, 0, "O antagonista competitivo reversível compete pelo mesmo sítio, deslocando a curva para a direita, mas o efeito máximo pode ser atingido com doses maiores de agonista.", "Médio"),
                new("A meia-vida de eliminação (t½) é o tempo necessário para:", new[]{"A concentração plasmática reduzir pela metade","O fármaco ser completamente eliminado","Atingir o estado de equilíbrio","A absorção ser completada"}, 0, "A meia-vida é o tempo para a concentração plasmática cair 50%. São necessárias ~5 meias-vidas para alcançar o estado de equilíbrio.", "Fácil"),
                new("AINEs como ibuprofeno atuam inibindo:", new[]{"Cicloxigenases (COX-1 e COX-2)","Lipoxigenase","Fosfolipase A2","Receptor de prostaglandina"}, 0, "Os AINEs tradicionais inibem de forma não seletiva a COX-1 e COX-2, reduzindo a síntese de prostaglandinas.", "Fácil"),
                new("O principal efeito adverso dos aminoglicosídeos é:", new[]{"Nefrotoxicidade e ototoxicidade","Hepatotoxicidade","Cardiotoxicidade","Mielossupressão"}, 0, "Os aminoglicosídeos podem causar dano renal (nefrotoxicidade) e lesão do VIII par craniano (ototoxicidade).", "Médio"),
                new("Qual classe de anti-hipertensivo inibe a enzima conversora de angiotensina?", new[]{"IECA (ex: enalapril)","BRA (ex: losartana)","BCC (ex: anlodipino)","Betabloqueador (ex: atenolol)"}, 0, "Os IECA (Inibidores da Enzima Conversora de Angiotensina) como enalapril e captopril bloqueiam a conversão de angiotensina I em II.", "Fácil"),
                new("A síndrome serotoninérgica pode ocorrer com a associação de:", new[]{"Dois fármacos que aumentam serotonina","Dois betabloqueadores","AINE e corticoide","Diurético e IECA"}, 0, "A síndrome serotoninérgica é uma emergência causada por excesso de serotonina, geralmente pela combinação de dois serotoninérgicos (ex: ISRS + IMAO).", "Médio"),
                new("A metformina atua principalmente:", new[]{"Reduzindo a produção hepática de glicose","Estimulando a secreção de insulina","Retardando a absorção de glicose","Aumentando a excreção renal de glicose"}, 0, "A metformina é uma biguanida que atua primariamente no fígado, reduzindo a gliconeogênese e aumentando a sensibilidade à insulina.", "Médio"),
                new("O antídoto para intoxicação por varfarina é:", new[]{"Vitamina K (fitomenadiona)","Protamina","N-acetilcisteína","Flumazenil"}, 0, "A vitamina K é o antídoto específico para varfarina, pois restaura a síntese dos fatores de coagulação dependentes de vitamina K (II, VII, IX, X).", "Fácil"),
                new("Os receptores muscarínicos M3 estão localizados principalmente em:", new[]{"Músculo liso e glândulas exócrinas","Coração","SNC","Placa motora"}, 0, "Os receptores M3 medeiam contração do músculo liso (brônquios, bexiga, TGI) e secreção glandular.", "Médio"),
                new("Qual é o mecanismo de ação das cefalosporinas?", new[]{"Inibição da síntese de parede celular bacteriana","Inibição da síntese proteica","Inibição da síntese de ácido fólico","Dano à membrana celular"}, 0, "As cefalosporinas, como os demais beta-lactâmicos, inibem a transpeptidase (PBP), bloqueando a síntese do peptidoglicano da parede celular.", "Fácil"),
                new("A fluoxetina pertence à classe dos:", new[]{"Inibidores seletivos da recaptação de serotonina (ISRS)","Antidepressivos tricíclicos","Inibidores da MAO","Inibidores da recaptação de noradrenalina"}, 0, "A fluoxetina é um ISRS que bloqueia seletivamente o transportador de serotonina (SERT), aumentando a disponibilidade de 5-HT na fenda sináptica.", "Fácil"),
                new("O efeito de primeira passagem hepática:", new[]{"Reduz a biodisponibilidade de fármacos orais","Aumenta a absorção intestinal","Não afeta a biodisponibilidade","Ocorre apenas com fármacos intravenosos"}, 0, "O metabolismo de primeira passagem ocorre quando o fármaco oral passa pelo fígado antes de atingir a circulação sistêmica, reduzindo sua biodisponibilidade.", "Fácil"),
                new("Os inibidores de SGLT2 (gliflozinas) atuam:", new[]{"Inibindo a reabsorção renal de glicose","Estimulando a secreção de insulina","Bloqueando a alfa-glicosidase intestinal","Ativando receptores PPAR-gama"}, 0, "As gliflozinas (dapagliflozina, empagliflozina) inibem o cotransportador SGLT2 no túbulo proximal renal, promovendo glicosúria.", "Médio"),
                new("A clozapina é um antipsicótico atípico indicado para:", new[]{"Esquizofrenia refratária","Depressão leve","Ansiedade generalizada","Insônia"}, 0, "A clozapina é reservada para esquizofrenia resistente a outros antipsicóticos, mas requer monitoramento de agranulocitose.", "Médio"),
                new("O pKa de um fármaco ácido fraco determina que ele será melhor absorvido em:", new[]{"pH ácido (estômago)","pH alcalino (intestino)","pH neutro","Independe do pH"}, 0, "Ácidos fracos ficam mais na forma não ionizada em pH ácido, favorecendo absorção no estômago (teoria da partição de pH).", "Médio"),
                new("A nitroglicerina sublingual é usada para:", new[]{"Alívio rápido de angina","Controle de hipertensão crônica","Tratamento de arritmias","Prevenção de trombose"}, 0, "A nitroglicerina sublingual libera óxido nítrico, causa vasodilatação venosa e alívio rápido da angina pectoris.", "Fácil"),
                new("Qual é o principal efeito adverso dos corticosteroides em uso prolongado?", new[]{"Síndrome de Cushing iatrogênica","Hipoglicemia","Hipotensão","Broncoespasmo"}, 0, "O uso crônico causa face em lua cheia, estrias, hiperglicemia, osteoporose, imunossupressão — síndrome de Cushing iatrogênica.", "Médio"),
                new("A naloxona é antagonista específico de receptores:", new[]{"Opioides (mu)","Benzodiazepínicos (GABA-A)","Colinérgicos (muscarínicos)","Dopaminérgicos (D2)"}, 0, "A naloxona é antagonista competitivo de receptores opioides mu, kappa e delta, usada na reversão de overdose de opioides.", "Fácil"),
                new("A atropina bloqueia receptores:", new[]{"Muscarínicos","Nicotínicos","Beta-adrenérgicos","Alfa-adrenérgicos"}, 0, "A atropina é um antagonista muscarínico não seletivo, bloqueando M1-M5, causando taquicardia, midríase, boca seca.", "Fácil"),
                new("O sinergismo farmacológico ocorre quando:", new[]{"O efeito combinado é maior que a soma individual","Os fármacos se anulam","Um fármaco reduz o efeito do outro","Há competição pelo mesmo receptor"}, 0, "No sinergismo, a combinação de dois fármacos produz efeito superior ao esperado pela simples adição dos efeitos individuais.", "Fácil"),
                new("A digoxina atua no coração por:", new[]{"Inibição da bomba Na+/K+-ATPase","Bloqueio de canais de cálcio","Ativação de receptores beta","Inibição da ECA"}, 0, "A digoxina inibe a Na+/K+-ATPase, aumentando o cálcio intracelular e a força de contração (efeito inotrópico positivo).", "Médio"),
                new("O omeprazol pertence à classe dos:", new[]{"Inibidores da bomba de prótons","Antagonistas H2","Antiácidos","Procinéticos"}, 0, "O omeprazol inibe irreversivelmente a H+/K+-ATPase (bomba de prótons) das células parietais gástricas.", "Fácil"),
                new("A resistência bacteriana mediada por beta-lactamases é superada por:", new[]{"Associação com inibidores de beta-lactamase (ácido clavulânico)","Aumento da dose do antibiótico","Troca por aminoglicosídeo","Uso de corticosteroide"}, 0, "Inibidores de beta-lactamase (clavulanato, sulbactam, tazobactam) protegem o beta-lactâmico da degradação enzimática.", "Médio"),
                new("A fenitoína apresenta cinética de eliminação:", new[]{"De ordem zero (saturável) em doses terapêuticas","De primeira ordem linear","Biexponencial","Independente da dose"}, 0, "A fenitoína é um exemplo clássico de fármaco com cinética de saturação (Michaelis-Menten), onde pequenos aumentos de dose causam grandes elevações de nível sérico.", "Difícil"),
                new("O carbonato de lítio é utilizado como:", new[]{"Estabilizador de humor no transtorno bipolar","Antidepressivo","Antipsicótico","Ansiolítico"}, 0, "O lítio é o estabilizador de humor padrão-ouro para transtorno bipolar, requerendo monitoramento de nível sérico (0,6-1,2 mEq/L).", "Médio"),
                new("A taquifilaxia é definida como:", new[]{"Perda rápida do efeito com doses repetidas","Reação alérgica ao fármaco","Aumento da sensibilidade ao fármaco","Efeito prolongado após suspensão"}, 0, "A taquifilaxia (dessensibilização rápida) é a perda progressiva e rápida do efeito farmacológico com administrações repetidas em curto intervalo.", "Médio"),
                new("Qual fármaco é considerado protótipo dos betabloqueadores não seletivos?", new[]{"Propranolol","Atenolol","Metoprolol","Bisoprolol"}, 0, "O propranolol bloqueia beta-1 e beta-2, sendo o protótipo dos betabloqueadores não seletivos. Atenolol e metoprolol são beta-1 seletivos.", "Fácil"),
                new("A via de administração que evita completamente o efeito de primeira passagem é:", new[]{"Intravenosa","Oral","Retal","Intramuscular"}, 0, "A via IV introduz o fármaco diretamente na circulação, eliminando qualquer metabolismo pré-sistêmico.", "Fácil"),
                new("Os agonistas beta-2 adrenérgicos como salbutamol são indicados para:", new[]{"Broncodilatação na asma","Redução da frequência cardíaca","Constrição pupilar","Aumento da motilidade gástrica"}, 0, "O salbutamol é um agonista seletivo beta-2 que relaxa a musculatura lisa brônquica, sendo broncodilatador de curta ação.", "Fácil"),
                new("A janela terapêutica de um fármaco é:", new[]{"A faixa de concentração entre efeito mínimo e toxicidade","A dose máxima tolerada","O tempo de meia-vida","A taxa de absorção"}, 0, "A janela terapêutica é o intervalo entre a concentração efetiva mínima (CEM) e a concentração tóxica mínima (CTM).", "Médio"),
                new("O metotrexato atua como antagonista de:", new[]{"Ácido fólico (inibidor da di-hidrofolato redutase)","Vitamina B12","Vitamina K","Ácido ascórbico"}, 0, "O metotrexato é um antimetabólito que inibe a DHFR, bloqueando a síntese de purinas e pirimidinas.", "Médio"),
                new("Os diuréticos de alça (furosemida) atuam no:", new[]{"Ramo ascendente espesso da alça de Henle","Túbulo contorcido proximal","Túbulo coletor","Glomérulo"}, 0, "A furosemida inibe o cotransportador Na+/K+/2Cl- no ramo ascendente espesso da alça de Henle, sendo o diurético mais potente.", "Médio"),
                new("A anfotericina B atua contra fungos por:", new[]{"Ligação ao ergosterol da membrana fúngica","Inibição da síntese de parede celular","Inibição da síntese proteica","Inibição da síntese de ácidos nucleicos"}, 0, "A anfotericina B é um poliênico que se liga ao ergosterol, formando poros na membrana fúngica e causando lise celular.", "Médio"),
                new("A varfarina exerce seu efeito anticoagulante por:", new[]{"Inibição da epóxi-redutase da vitamina K","Ativação da antitrombina III","Inibição direta da trombina","Bloqueio da agregação plaquetária"}, 0, "A varfarina inibe a VKORC1 (vitamina K epóxi-redutase), impedindo a carboxilação dos fatores II, VII, IX e X.", "Médio"),
            }
        );

        // ─── Topics 2-42: Generated in partial class files ───
        GenerateTopics2to14(data);
        GenerateTopics15to28(data);
        GenerateTopics29to42(data);

        return data;
    }

    // Partial methods defined in other files
    static partial void GenerateTopics2to14(Dictionary<int, (DocDef[], QDef[])> data);
    static partial void GenerateTopics15to28(Dictionary<int, (DocDef[], QDef[])> data);
    static partial void GenerateTopics29to42(Dictionary<int, (DocDef[], QDef[])> data);
}
