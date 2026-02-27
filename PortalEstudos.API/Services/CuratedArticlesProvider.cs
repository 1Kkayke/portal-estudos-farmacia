using PortalEstudos.API.Models;

namespace PortalEstudos.API.Services;

/// <summary>
/// Artigos curados detalhados a nível de notícia/reportagem educacional.
/// Cada artigo simula matéria de portal jornalístico especializado em Farmácia.
/// </summary>
public static class CuratedArticlesProvider
{
    // Unsplash images (publicly embeddable for non-commercial / educational use)
    private const string Img = "https://images.unsplash.com/";

    public static List<BlogArticle> GetArticles()
    {
        var today = DateTime.UtcNow.Date;
        return new List<BlogArticle>
        {
            // ───────────── 1 ─────────────
            new()
            {
                Id = "curated-001",
                Titulo = "Resistência Antimicrobiana: A Pandemia Silenciosa que Ameaça a Medicina Moderna",
                Subtitulo = "OMS classifica a RAM como uma das 10 maiores ameaças à saúde pública; farmacêuticos estão na linha de frente do combate",
                Resumo = "A resistência antimicrobiana (RAM) já causa mais de 1,27 milhão de mortes diretas por ano no mundo. Entenda o mecanismo, os super-bugs mais perigosos e o papel crucial do farmacêutico na contenção dessa crise.",
                Autor = "Profa. Dra. Mariana Costa",
                Fonte = "Portal Farmácia Estudos",
                ImagemUrl = Img + "photo-1584308666744-24d5c474f2ae?w=900&h=450&fit=crop&auto=format&q=80",
                ImagemCredito = "Unsplash / Science in HD",
                Categoria = "Microbiologia",
                Tags = new() { "RAM", "antibióticos", "superbactérias", "OMS", "SNGPC" },
                DataPublicacao = today,
                LeituraMinutos = 14,
                Conteudo = @"
<p class='lead'>Estima-se que, até 2050, infecções resistentes a antimicrobianos poderão matar <strong>10 milhões de pessoas por ano</strong> — mais do que o câncer atualmente. A resistência antimicrobiana (RAM) é considerada pela Organização Mundial da Saúde uma das maiores ameaças à saúde global, e o farmacêutico é peça-chave na contenção desse cenário.</p>

<h2>O que é Resistência Antimicrobiana?</h2>
<p>A RAM ocorre quando microrganismos (bactérias, fungos, vírus, parasitas) desenvolvem mecanismos que os tornam insensíveis aos medicamentos projetados para eliminá-los. Não é o paciente que se torna resistente, mas o microrganismo.</p>
<p>Os principais mecanismos de resistência bacteriana incluem:</p>
<ul>
<li><strong>Alteração do sítio-alvo</strong> — mutações nos ribossomos (resistência a macrolídeos), nas PBPs (resistência a β-lactâmicos).</li>
<li><strong>Inativação enzimática</strong> — produção de β-lactamases, carbapenemases (KPC, NDM).</li>
<li><strong>Efluxo ativo</strong> — bombas de efluxo que expulsam o antibiótico da célula.</li>
<li><strong>Redução da permeabilidade</strong> — alteração de porinas na membrana externa de gram-negativos.</li>
</ul>

<h2>Os Super-bugs Mais Preocupantes</h2>
<p>A OMS publicou em 2024 a lista atualizada de patógenos prioritários:</p>
<table>
<thead><tr><th>Prioridade</th><th>Patógeno</th><th>Resistência</th></tr></thead>
<tbody>
<tr><td>Crítica</td><td><em>Acinetobacter baumannii</em></td><td>Carbapenêmicos</td></tr>
<tr><td>Crítica</td><td><em>Pseudomonas aeruginosa</em></td><td>Carbapenêmicos</td></tr>
<tr><td>Crítica</td><td>Enterobacterales (Klebsiella, E. coli)</td><td>Carbapenêmicos, ESBL</td></tr>
<tr><td>Alta</td><td><em>Staphylococcus aureus</em> (MRSA)</td><td>Meticilina/Oxacilina</td></tr>
<tr><td>Alta</td><td><em>Enterococcus faecium</em> (VRE)</td><td>Vancomicina</td></tr>
</tbody>
</table>

<h2>Números Alarmantes no Brasil</h2>
<p>Dados do sistema GLASS da OMS e da ANVISA (2024) mostram que:</p>
<ul>
<li>Infecções por <em>Klebsiella pneumoniae</em> produtora de KPC aumentaram <strong>45%</strong> em UTIs brasileiras nos últimos 5 anos.</li>
<li>O consumo de antibióticos no Brasil cresceu 65% entre 2000 e 2015, um dos maiores aumentos do planeta.</li>
<li>A RDC nº 20/2011, que tornou obrigatória a retenção de receita para antimicrobianos, reduziu o consumo em 18% logo no primeiro ano — mostrando que regulamentação funciona.</li>
</ul>

<h2>O Papel do Farmacêutico</h2>
<p>O profissional farmacêutico atua em múltiplas frentes contra a RAM:</p>
<h3>Na dispensação (drogaria e farmácia)</h3>
<ul>
<li>Cumprimento rigoroso da RDC 20/2011: retenção da segunda via da receita, registro no SNGPC.</li>
<li>Orientação sobre posologia completa, importância de não interromper o tratamento e descarte correto de sobras.</li>
<li>Recusa de dispensação sem receita válida, mesmo sob pressão comercial.</li>
</ul>
<h3>Na farmácia hospitalar</h3>
<ul>
<li>Participação no <strong>Programa de Gerenciamento de Antimicrobianos (PGA / Antimicrobial Stewardship)</strong>: revisão de prescrições, de-escalonamento, auditoria de culturas.</li>
<li>Farmacocinética clínica: ajuste de dose de vancomicina (vale sérico 15-20 µg/mL), aminoglicosídeos, e monitoramento de função renal.</li>
<li>Elaboração de protocolos de profilaxia cirúrgica (escolha do antimicrobiano, momento da administração, duração ≤24h).</li>
</ul>
<h3>Na pesquisa e indústria</h3>
<ul>
<li>Descoberta de novos antimicrobianos: peptídeos antimicrobianos, bacteriófagos, inibidores de β-lactamase de nova geração (avibactam, vaborbactam).</li>
<li>Desenvolvimento de testes diagnósticos rápidos (PCR em tempo real para detecção de genes de resistência).</li>
</ul>

<h2>O que Estudar para Provas e Concursos</h2>
<ul>
<li>Mecanismos de ação dos antibióticos (β-lactâmicos, aminoglicosídeos, quinolonas, macrolídeos, glicopeptídeos).</li>
<li>Classificação da resistência: intrínseca vs. adquirida; cromossômica vs. plasmidial.</li>
<li>Legislação: RDC 20/2011 (dispensação de antimicrobianos), RDC 44/2009 (boas práticas).</li>
<li>Conceito de MIC (Concentração Inibitória Mínima) e MBC (Concentração Bactericida Mínima).</li>
</ul>

<h2>Referências</h2>
<ul>
<li>OMS – Global Antimicrobial Resistance and Use Surveillance System (GLASS) Report 2024.</li>
<li>Murray CJL et al. <em>Lancet</em>, 2022. Global burden of bacterial antimicrobial resistance.</li>
<li>ANVISA – Boletim Segurança do Paciente e Qualidade em Serviços de Saúde nº 29.</li>
</ul>"
            },

            // ───────────── 2 ─────────────
            new()
            {
                Id = "curated-002",
                Titulo = "Vacinas de mRNA: Como a Revolução Molecular Está Redefinindo a Imunologia Farmacêutica",
                Subtitulo = "Da COVID-19 ao câncer — a tecnologia de RNA mensageiro abre fronteiras inimagináveis",
                Resumo = "As vacinas de mRNA, consagradas na pandemia de COVID-19, agora são testadas contra câncer, malária, HIV e doenças autoimunes. Entenda a tecnologia, os desafios de formulação e o horizonte farmacêutico.",
                Autor = "Prof. Dr. Rafael Oliveira",
                Fonte = "Portal Farmácia Estudos",
                ImagemUrl = Img + "photo-1632833239869-a37e3a5806d2?w=900&h=450&fit=crop&auto=format&q=80",
                ImagemCredito = "Unsplash / CDC",
                Categoria = "Imunologia",
                Tags = new() { "mRNA", "vacinas", "Pfizer", "Moderna", "nanotecnologia" },
                DataPublicacao = today.AddDays(-1),
                LeituraMinutos = 12,
                Conteudo = @"
<p class='lead'>A aprovação das vacinas BNT162b2 (Pfizer-BioNTech) e mRNA-1273 (Moderna) marcou um ponto de inflexão na história farmacêutica. Pela primeira vez, a plataforma de RNA mensageiro demonstrou eficácia em larga escala — e agora a mesma tecnologia é aplicada contra dezenas de outras doenças.</p>

<h2>Como Funciona uma Vacina de mRNA</h2>
<ol>
<li><strong>Design in silico</strong>: a sequência gênica do antígeno-alvo (ex: proteína Spike do SARS-CoV-2) é obtida e a sequência de mRNA correspondente é projetada computacionalmente.</li>
<li><strong>Otimização de códons</strong>: substituição de uridina por N1-metilpseudouridina para reduzir resposta imune inata contra o próprio mRNA e aumentar estabilidade.</li>
<li><strong>Cap 5' e cauda poli-A</strong>: adição de estruturas que protegem o mRNA da degradação por RNases.</li>
<li><strong>Encapsulamento em LNP</strong>: nanopartículas lipídicas (lipídio ionizável + colesterol + DSPC + PEG-lipídio) protegem o mRNA e facilitam a endocitose celular.</li>
<li><strong>Tradução celular</strong>: o mRNA é traduzido pelos ribossomos do hospedeiro → proteína antigênica → apresentação ao sistema imune → resposta humoral e celular.</li>
</ol>

<h2>Composição das Nanopartículas Lipídicas (LNP)</h2>
<table>
<thead><tr><th>Componente</th><th>Função</th><th>Exemplo</th></tr></thead>
<tbody>
<tr><td>Lipídio ionizável</td><td>Encapsulamento do mRNA; fusão endossomal</td><td>ALC-0315 (Pfizer), SM-102 (Moderna)</td></tr>
<tr><td>Colesterol</td><td>Estabilidade da bicamada</td><td>Colesterol</td></tr>
<tr><td>Fosfolipídio</td><td>Estrutura da membrana</td><td>DSPC</td></tr>
<tr><td>PEG-lipídio</td><td>Furtividade imune, evita opsonização</td><td>ALC-0159, PEG-DMG 2000</td></tr>
</tbody>
</table>

<h2>Desafios de Tecnologia Farmacêutica</h2>
<ul>
<li><strong>Cadeia fria ultrabaixa</strong>: a vacina da Pfizer exigia −70°C inicialmente. A formulação liofilizada e novas LNPs buscam estabilidade a 2-8°C.</li>
<li><strong>Escalonamento (scale-up)</strong>: produção de mRNA por transcrição in vitro (IVT) com T7 RNA polimerase; purificação por cromatografia de afinidade.</li>
<li><strong>Controle de qualidade</strong>: verificação de integridade do mRNA (eletroforese capilar), encapsulamento (>90%), tamanho de partícula (60-100 nm por DLS), endotoxinas.</li>
</ul>

<h2>Novas Fronteiras: Além da COVID-19</h2>
<ul>
<li><strong>Oncologia</strong>: vacinas personalizadas de neoantígenos (mRNA-4157/V940 da Moderna para melanoma — Fase III com resultados promissores em 2024).</li>
<li><strong>Malária</strong>: candidata de mRNA codificando antígenos CSP (<em>Plasmodium falciparum</em>).</li>
<li><strong>Gripe universal</strong>: vacinas multivalentes de mRNA contra 20 subtipos de influenza (Universidade da Pensilvânia, 2023).</li>
<li><strong>HIV</strong>: Moderna e IAVI testam mRNA para induzir anticorpos amplamente neutralizantes (bnAbs).</li>
<li><strong>Doenças autoimunes</strong>: mRNA regulatório para induzir tolerância imunológica (BioNTech, pré-clínico).</li>
</ul>

<h2>Referências</h2>
<ul>
<li>Polack FP et al. <em>N Engl J Med</em>, 2020. Safety and Efficacy of BNT162b2.</li>
<li>Hou X et al. <em>Nat Rev Mater</em>, 2021. Lipid nanoparticles for mRNA delivery.</li>
<li>Weber JS et al. <em>Lancet</em>, 2024. mRNA-4157/V940 in melanoma.</li>
</ul>"
            },

            // ───────────── 3 ─────────────
            new()
            {
                Id = "curated-003",
                Titulo = "Cannabis Medicinal no Brasil: Regulamentação, Evidências Clínicas e o Papel do Farmacêutico",
                Subtitulo = "RDC 327/2019, prescrição, formulação magistral e o futuro da cannabis farmacêutica no país",
                Resumo = "O mercado de cannabis medicinal no Brasil cresce mais de 100% ao ano. Entenda a legislação, as indicações baseadas em evidência, os canabinoides principais e como o farmacêutico atua desde a formulação até a dispensação.",
                Autor = "Profa. Dra. Camila Andrade",
                Fonte = "Portal Farmácia Estudos",
                ImagemUrl = Img + "photo-1515377905703-c4788e51af15?w=900&h=450&fit=crop&auto=format&q=80",
                ImagemCredito = "Unsplash / Botanical",
                Categoria = "Farmacognosia",
                Tags = new() { "cannabis", "CBD", "THC", "magistral", "ANVISA" },
                DataPublicacao = today.AddDays(-2),
                LeituraMinutos = 13,
                Conteudo = @"
<p class='lead'>O Brasil é um dos mercados que mais crescem em cannabis medicinal na América Latina. Desde a publicação da RDC 327/2019, mais de 20 produtos à base de cannabis foram registrados na ANVISA, e a demanda por formulações magistrais dispara nas farmácias de manipulação.</p>

<h2>Sistema Endocanabinoide: Base Fisiológica</h2>
<p>O sistema endocanabinoide (SEC) é composto por:</p>
<ul>
<li><strong>Receptores CB1</strong> — predominam no SNC (córtex, hipocampo, gânglios da base, cerebelo). Mediam efeitos psicoativos e analgésicos.</li>
<li><strong>Receptores CB2</strong> — predominam em células imunes e tecidos periféricos. Modulam inflamação.</li>
<li><strong>Endocanabinoides</strong> — anandamida (AEA) e 2-araquidonoilglicerol (2-AG).</li>
<li><strong>Enzimas de degradação</strong> — FAAH (degrada AEA) e MAGL (degrada 2-AG).</li>
</ul>

<h2>Principais Canabinoides e Evidências</h2>
<table>
<thead><tr><th>Canabinoide</th><th>Receptor principal</th><th>Indicações com evidência</th></tr></thead>
<tbody>
<tr><td>CBD (Canabidiol)</td><td>Fraca afinidade CB1/CB2; 5-HT1A, TRPV1</td><td>Epilepsia refratária (Dravet, Lennox-Gastaut), ansiedade, dor neuropática</td></tr>
<tr><td>THC (Δ9-THC)</td><td>Agonista parcial CB1</td><td>Dor oncológica, náusea/vômito por quimioterapia, espasticidade na esclerose múltipla</td></tr>
<tr><td>CBG (Canabigerol)</td><td>Agonista parcial CB1/CB2</td><td>Anti-inflamatório, neuroprotetor (estudos pré-clínicos)</td></tr>
<tr><td>CBN (Canabinol)</td><td>Agonista fraco CB1</td><td>Sedação, potencial imunomodulador (dados limitados)</td></tr>
</tbody>
</table>

<h2>Legislação Brasileira</h2>
<h3>RDC 327/2019</h3>
<ul>
<li>Autoriza a fabricação e importação de <strong>produtos de cannabis para fins medicinais</strong>.</li>
<li>Produtos com THC ≤ 0,2% podem ser prescritos por qualquer médico.</li>
<li>Produtos com THC > 0,2% são restritos a pacientes terminais ou sem alternativas terapêuticas — prescrição em formulário próprio.</li>
<li>Venda exclusiva em farmácias e drogarias, com retenção de receita.</li>
</ul>
<h3>Formulação magistral</h3>
<p>A RDC 660/2022 atualizou as normas de manipulação. Farmácias com Autorização Especial (AE) podem manipular produtos contendo CBD e THC a partir de insumos com certificado de análise.</p>

<h2>Atuação do Farmacêutico</h2>
<ul>
<li><strong>Orientação ao paciente</strong>: efeitos adversos (sonolência, boca seca, alteração do apetite), interações (CBD é inibidor do CYP3A4 e CYP2C19 → risco com clobazam, varfarina).</li>
<li><strong>Controle de qualidade</strong>: análise por HPLC do teor de canabinoides no insumo e no produto acabado.</li>
<li><strong>Farmacovigilância</strong>: notificação de RAM via Notivisa/ANVISA.</li>
</ul>

<h2>Referências</h2>
<ul>
<li>ANVISA – RDC 327/2019 e Notas Técnicas sobre Cannabis Medicinal.</li>
<li>Devinsky O et al. <em>N Engl J Med</em>, 2017. Trial of Cannabidiol for Drug-Resistant Seizures in Dravet Syndrome.</li>
<li>CFF – Guia para o farmacêutico sobre cannabis medicinal, 2023.</li>
</ul>"
            },

            // ───────────── 4 ─────────────
            new()
            {
                Id = "curated-004",
                Titulo = "Farmacogenômica: O Futuro da Prescrição Personalizada Já Começou",
                Subtitulo = "Testes genéticos orientam a escolha do medicamento certo, na dose certa, para o paciente certo",
                Resumo = "A farmacogenômica estuda como variações genéticas influenciam a resposta aos medicamentos. Entenda os polimorfismos do CYP450, exemplos clínicos concretos e como o farmacêutico clínico utiliza esses dados na prática.",
                Autor = "Prof. Dr. Lucas Mendes",
                Fonte = "Portal Farmácia Estudos",
                ImagemUrl = Img + "photo-1532187863486-abf9dbad1b69?w=900&h=450&fit=crop&auto=format&q=80",
                ImagemCredito = "Unsplash / National Cancer Institute",
                Categoria = "Farmacologia",
                Tags = new() { "farmacogenômica", "CYP450", "polimorfismo", "medicina personalizada" },
                DataPublicacao = today.AddDays(-3),
                LeituraMinutos = 11,
                Conteudo = @"
<p class='lead'>Aproximadamente <strong>95% da população</strong> possui ao menos uma variante farmacogenômica clinicamente acionável. A farmacogenômica permite prever quem metaboliza rápido ou lento um fármaco, quem terá efeito adverso grave e quem não responderá ao tratamento — antes de administrar a primeira dose.</p>

<h2>Conceitos Fundamentais</h2>
<ul>
<li><strong>Polimorfismo de nucleotídeo único (SNP)</strong> — variação em uma única base do DNA que pode alterar a função de uma proteína (enzima, transportador, receptor).</li>
<li><strong>Fenótipos metabólicos</strong>: Metabolizador ultrarrápido (UM), extensivo/normal (EM/NM), intermediário (IM) e lento (PM).</li>
<li><strong>Gene-fármaco</strong>: relação entre genótipo do paciente e resposta a um fármaco específico.</li>
</ul>

<h2>Polimorfismos Clinicamente Relevantes</h2>
<table>
<thead><tr><th>Gene</th><th>Enzima</th><th>Fármacos afetados</th><th>Implicação clínica</th></tr></thead>
<tbody>
<tr><td>CYP2D6</td><td>Citocromo P450 2D6</td><td>Codeína, tamoxifeno, metoprolol</td><td>PM: sem conversão de codeína em morfina (analgesia ausente). UM: toxicidade por excesso de morfina.</td></tr>
<tr><td>CYP2C19</td><td>Citocromo P450 2C19</td><td>Clopidogrel, omeprazol, escitalopram</td><td>PM: clopidogrel não é ativado → falha antitrombótica. UM: metabolismo rápido de IBPs.</td></tr>
<tr><td>CYP2C9 + VKORC1</td><td>CYP2C9 / Vitamina K epóxido redutase</td><td>Varfarina</td><td>Variantes reduzem dose necessária em até 50%. Risco de hemorragia se dose padrão.</td></tr>
<tr><td>HLA-B*5701</td><td>Antígeno leucocitário humano</td><td>Abacavir</td><td>Portadores: reação de hipersensibilidade grave. Teste obrigatório antes da prescrição.</td></tr>
<tr><td>TPMT / NUDT15</td><td>Tiopurina metiltransferase</td><td>Azatioprina, mercaptopurina</td><td>PM: mielossupressão grave. Dose deve ser reduzida 50-90%.</td></tr>
</tbody>
</table>

<h2>Caso Clínico: Clopidogrel e CYP2C19</h2>
<p>Paciente de 58 anos, pós-angioplastia com stent farmacológico, em uso de clopidogrel 75 mg/dia. Após 3 meses, apresenta trombose de stent. Teste farmacogenômico revela genótipo CYP2C19 *2/*2 (metabolizador lento).</p>
<p><strong>Conduta</strong>: substituição por <strong>prasugrel ou ticagrelor</strong>, que não dependem de bioativação pelo CYP2C19. O CPIC (Clinical Pharmacogenetics Implementation Consortium) recomenda essa troca para todos os PMs.</p>

<h2>O Papel do Farmacêutico</h2>
<ul>
<li>Interpretação de laudos farmacogenômicos e tradução em recomendações posológicas.</li>
<li>Integração de dados genômicos ao prontuário eletrônico (alertas de prescrição).</li>
<li>Educação da equipe médica sobre as diretrizes do CPIC e DPWG (Dutch Pharmacogenetics Working Group).</li>
</ul>

<h2>Referências</h2>
<ul>
<li>CPIC Guidelines — cpicpgx.org</li>
<li>Relling MV, Klein TE. <em>Clin Pharmacol Ther</em>, 2011. CPIC: Clinical Pharmacogenetics Implementation Consortium.</li>
<li>PharmGKB — pharmgkb.org (base de dados de farmacogenômica)</li>
</ul>"
            },

            // ───────────── 5 ─────────────
            new()
            {
                Id = "curated-005",
                Titulo = "Nanotecnologia Farmacêutica: Sistemas de Drug Delivery que Estão Transformando Tratamentos",
                Subtitulo = "Lipossomas, nanopartículas poliméricas e dendrímeros levam fármacos exatamente onde eles precisam agir",
                Resumo = "A nanotecnologia permite criar sistemas de liberação controlada que aumentam a eficácia, reduzem toxicidade e possibilitam terapias antes impossíveis. Conheça os principais sistemas e suas aplicações clínicas.",
                Autor = "Profa. Dra. Fernanda Lima",
                Fonte = "Portal Farmácia Estudos",
                ImagemUrl = Img + "photo-1576086213369-97a306d36557?w=900&h=450&fit=crop&auto=format&q=80",
                ImagemCredito = "Unsplash / Research Lab",
                Categoria = "Tecnologia Farmacêutica",
                Tags = new() { "nanotecnologia", "lipossomas", "drug delivery", "nanopartículas" },
                DataPublicacao = today.AddDays(-4),
                LeituraMinutos = 11,
                Conteudo = @"
<p class='lead'>Medicamentos nanoestruturados já são realidade clínica: Doxil® (doxorrubicina lipossomal), Abraxane® (paclitaxel em nanopartículas de albumina) e as vacinas de mRNA (LNPs) demonstram o poder da escala nanométrica na farmacoterapia.</p>

<h2>Principais Sistemas Nanoestruturados</h2>
<table>
<thead><tr><th>Sistema</th><th>Tamanho</th><th>Composição</th><th>Exemplo comercial</th></tr></thead>
<tbody>
<tr><td>Lipossomas</td><td>80-200 nm</td><td>Bicamada de fosfolipídios</td><td>Doxil® (doxorrubicina), AmBisome® (anfotericina B)</td></tr>
<tr><td>Nanopartículas poliméricas</td><td>100-300 nm</td><td>PLGA, PCL, quitosana</td><td>Eligard® (leuprorrelina)</td></tr>
<tr><td>Nanopartículas lipídicas sólidas (SLN)</td><td>50-1000 nm</td><td>Lipídios sólidos em temperatura ambiente</td><td>Formulações tópicas (em estudo)</td></tr>
<tr><td>Dendrímeros</td><td>1-10 nm</td><td>Polímeros ramificados</td><td>Vivagel® (antiviral tópico)</td></tr>
<tr><td>Micelas poliméricas</td><td>10-100 nm</td><td>Copolímeros anfifílicos</td><td>Genexol-PM® (paclitaxel)</td></tr>
<tr><td>LNPs (nanopartículas lipídicas)</td><td>60-100 nm</td><td>Lipídio ionizável + colesterol</td><td>Comirnaty® (vacina COVID mRNA)</td></tr>
</tbody>
</table>

<h2>Vantagens dos Nanossistemas</h2>
<ul>
<li><strong>Efeito EPR (Enhanced Permeability and Retention)</strong>: tumores sólidos possuem vasculatura defeituosa que permite acúmulo preferencial de nanopartículas.</li>
<li><strong>Liberação controlada</strong>: modulação do perfil de liberação (sustentada, pulsátil, responsiva ao pH).</li>
<li><strong>Proteção do fármaco</strong>: evita degradação no TGI ou sangue (proteínas, peptídeos, RNA).</li>
<li><strong>Direcionamento ativo</strong>: conjugação com ligantes (anticorpos, peptídeos RGD, ácido fólico) para reconhecimento celular específico.</li>
</ul>

<h2>Desafios e Perspectivas</h2>
<ul>
<li>Escalabilidade de produção: técnicas de microfluídica permitem lotes reprodutíveis.</li>
<li>Caracterização: DLS (tamanho), potencial zeta (estabilidade), cryo-TEM (morfologia), HPLC (eficiência de encapsulamento).</li>
<li>Regulamentação: a ANVISA e o FDA exigem estudos de segurança específicos para nanomedicamentos (toxicidade nano-específica).</li>
</ul>

<h2>Referências</h2>
<ul>
<li>Allen TM, Cullis PR. <em>Adv Drug Deliv Rev</em>, 2013. Liposomal drug delivery systems.</li>
<li>Anselmo AC, Mitragotri S. <em>Bioeng Transl Med</em>, 2019. Nanoparticles in the clinic: An update.</li>
</ul>"
            },

            // ───────────── 6 ─────────────
            new()
            {
                Id = "curated-006",
                Titulo = "Inteligência Artificial na Descoberta de Fármacos: Do Alvo Molecular ao Medicamento em Tempo Recorde",
                Subtitulo = "Machine learning, AlphaFold e virtual screening estão acelerando a P&D farmacêutica em anos",
                Resumo = "A IA reduziu o tempo de descoberta de candidatos a fármacos de 4-5 anos para meses. Veja como deep learning, docking molecular e modelos generativos estão revolucionando o pipeline farmacêutico.",
                Autor = "Prof. Dr. André Vieira",
                Fonte = "Portal Farmácia Estudos",
                ImagemUrl = Img + "photo-1677442136019-21780ecad995?w=900&h=450&fit=crop&auto=format&q=80",
                ImagemCredito = "Unsplash / AI Research",
                Categoria = "Inovação",
                Tags = new() { "inteligência artificial", "drug discovery", "AlphaFold", "machine learning" },
                DataPublicacao = today.AddDays(-5),
                LeituraMinutos = 10,
                Conteudo = @"
<p class='lead'>Em 2023, a Insilico Medicine levou o <strong>INS018_055</strong> — um fármaco inteiramente descoberto por IA para fibrose pulmonar idiopática — à Fase II clínica em apenas 30 meses e US$ 2,6 milhões, contra a média de 5+ anos e US$ 200+ milhões do processo tradicional.</p>

<h2>Etapas do Pipeline de IA em Drug Discovery</h2>
<ol>
<li><strong>Identificação de alvos</strong>: redes neurais analisam dados ômicos (genômica, proteômica, transcriptômica) para encontrar proteínas associadas à doença.</li>
<li><strong>Predição de estrutura</strong>: AlphaFold (DeepMind) prediz estrutura 3D de proteínas com precisão atômica, eliminando meses de cristalografia.</li>
<li><strong>Virtual screening</strong>: triagem computacional de milhões de compostos contra o alvo por docking molecular (AutoDock, Glide).</li>
<li><strong>Geração de moléculas</strong>: modelos generativos (GANs, VAEs, transformers) propõem moléculas novas otimizadas para afinidade, seletividade e ADMET.</li>
<li><strong>Otimização ADMET</strong>: modelos de ML preveem absorção, metabolismo, toxicidade e solubilidade — priorizando candidatos drug-like.</li>
<li><strong>Reposicionamento de fármacos</strong>: IA identifica novos usos para fármacos existentes (ex: baricitinib para COVID-19, sugerido por IA da BenevolentAI).</li>
</ol>

<h2>Ferramentas de IA no Cotidiano Farmacêutico</h2>
<ul>
<li><strong>AlphaFold Protein Structure Database</strong> — mais de 200 milhões de estruturas proteicas preditas, acesso gratuito.</li>
<li><strong>REINVENT</strong> (AstraZeneca) — modelo generativo de moléculas open-source.</li>
<li><strong>DeepChem</strong> — biblioteca Python de ML para ciências da vida.</li>
<li><strong>ADMETlab 2.0</strong> — predição ADMET online e gratuita.</li>
</ul>

<h2>Impacto na Formação Farmacêutica</h2>
<p>O farmacêutico do futuro precisa de letramento em dados: bioinformática, cheminformática e pensamento computacional estão se tornando competências-chave. Universidades como USP e UNICAMP já oferecem disciplinas de modelagem molecular e machine learning aplicado a fármacos.</p>

<h2>Referências</h2>
<ul>
<li>Jumper J et al. <em>Nature</em>, 2021. Highly accurate protein structure prediction with AlphaFold.</li>
<li>Ren F et al. <em>Nat Biotechnol</em>, 2024. Insilico Medicine AI-discovered drug enters Phase II.</li>
</ul>"
            },

            // ───────────── 7 ─────────────
            new()
            {
                Id = "curated-007",
                Titulo = "Sistema Nacional de Controle de Medicamentos (SNCM): Rastreabilidade de Ponta a Ponta",
                Subtitulo = "IUM, DataMatrix e blockchain farmacêutico — como o Brasil rastreia cada caixa de medicamento",
                Resumo = "O SNCM, previsto na Lei 11.903/2009, exige rastreabilidade total de medicamentos no Brasil. Entenda o Identificador Único de Medicamento (IUM), os prazos de implantação e o impacto para farmácias.",
                Autor = "Farm. Esp. Juliana Rocha",
                Fonte = "Portal Farmácia Estudos",
                ImagemUrl = Img + "photo-1587854692152-cbe660dbde88?w=900&h=450&fit=crop&auto=format&q=80",
                ImagemCredito = "Unsplash / Pharmacy",
                Categoria = "Legislação",
                Tags = new() { "SNCM", "rastreabilidade", "ANVISA", "IUM", "DataMatrix" },
                DataPublicacao = today.AddDays(-6),
                LeituraMinutos = 9,
                Conteudo = @"
<p class='lead'>O Sistema Nacional de Controle de Medicamentos (SNCM) é o mecanismo brasileiro de rastreabilidade de medicamentos ao longo de toda a cadeia: fabricante → distribuidora → farmácia → paciente. O objetivo é combater falsificação, roubo de carga e desvio de medicamentos.</p>

<h2>Identificador Único de Medicamento (IUM)</h2>
<p>Cada unidade de medicamento recebe um código único composto por:</p>
<ul>
<li><strong>GTIN</strong> (Global Trade Item Number) — identifica o produto.</li>
<li><strong>Número serial</strong> — identifica a unidade individual.</li>
<li><strong>Lote e validade</strong>.</li>
</ul>
<p>Esses dados são codificados em um código <strong>DataMatrix GS1</strong> impresso na embalagem, escaneável por qualquer leitor 2D.</p>

<h2>Marco Legal e Cronograma</h2>
<table>
<thead><tr><th>Norma</th><th>Ano</th><th>Determinação</th></tr></thead>
<tbody>
<tr><td>Lei 11.903</td><td>2009</td><td>Institui o SNCM</td></tr>
<tr><td>RDC 157</td><td>2017</td><td>Regulamenta tecnicamente o SNCM</td></tr>
<tr><td>IN 29</td><td>2020</td><td>Piloto com indústrias selecionadas</td></tr>
<tr><td>Cronograma ANVISA 2024-2026</td><td>2024</td><td>Implantação progressiva por categoria de medicamento</td></tr>
</tbody>
</table>

<h2>Impacto para Farmácias e Drogarias</h2>
<ul>
<li>Necessidade de leitores de código DataMatrix no balcão.</li>
<li>Software de gestão integrado ao SNCM para registro de entrada/saída.</li>
<li>Eventualmente, o paciente poderá verificar a autenticidade do medicamento pelo app <strong>""MeuMedicamento""</strong> da ANVISA.</li>
</ul>

<h2>Referências</h2>
<ul>
<li>ANVISA – Portal do SNCM: sncm.anvisa.gov.br</li>
<li>Lei nº 11.903/2009 — Rastreamento da produção e do consumo de medicamentos.</li>
</ul>"
            },

            // ───────────── 8 ─────────────
            new()
            {
                Id = "curated-008",
                Titulo = "Farmacovigilância: Como Reações Adversas São Monitoradas Após a Aprovação de Medicamentos",
                Subtitulo = "Da notificação espontânea ao big data — o sistema que continua avaliando a segurança dos medicamentos depois que chegam ao mercado",
                Resumo = "O sistema de farmacovigilância detectou problemas como Vioxx (rofecoxib) e talidomida. Entenda os métodos (notificação espontânea, estudos de coorte, data mining), a classificação de RAMs e o papel do farmacêutico na notificação.",
                Autor = "Prof. Dr. Carlos Eduardo",
                Fonte = "Portal Farmácia Estudos",
                ImagemUrl = Img + "photo-1576091160550-2173dba999ef?w=900&h=450&fit=crop&auto=format&q=80",
                ImagemCredito = "Unsplash / Medical",
                Categoria = "Farmácia Clínica",
                Tags = new() { "farmacovigilância", "RAM", "Notivisa", "segurança" },
                DataPublicacao = today.AddDays(-7),
                LeituraMinutos = 10,
                Conteudo = @"
<p class='lead'>Ensaios clínicos pré-registro envolvem milhares de pacientes — mas um medicamento pode ser usado por milhões. RAMs raras (1:10.000) só são detectadas na Fase IV, a farmacovigilância pós-comercialização.</p>

<h2>Classificação de Reações Adversas (Rawlins & Thompson)</h2>
<table>
<thead><tr><th>Tipo</th><th>Característica</th><th>Exemplo</th></tr></thead>
<tbody>
<tr><td>A (Augmented)</td><td>Dose-dependente, previsível, comum</td><td>Hipoglicemia por insulina, hemorragia por varfarina</td></tr>
<tr><td>B (Bizarre)</td><td>Dose-independente, imprevisível, rara</td><td>Anafilaxia por penicilina, Síndrome de Stevens-Johnson por alopurinol</td></tr>
<tr><td>C (Chronic)</td><td>Uso prolongado</td><td>Nefropatia por AINEs, osteoporose por corticoides</td></tr>
<tr><td>D (Delayed)</td><td>Tardia</td><td>Teratogenicidade (talidomida), carcinogênese</td></tr>
<tr><td>E (End of use)</td><td>Retirada</td><td>Crise adrenal por suspensão abrupta de corticoides</td></tr>
</tbody>
</table>

<h2>Métodos de Farmacovigilância</h2>
<ul>
<li><strong>Notificação espontânea</strong>: profissionais de saúde e pacientes reportam RAMs ao Notivisa (ANVISA) ou VigiMed (OMS/Uppsala Monitoring Centre).</li>
<li><strong>Estudos observacionais</strong>: coortes e caso-controle para quantificar risco.</li>
<li><strong>Data mining de bancos de dados</strong>: algoritmos (PRR, ROR, BCPNN) analisam desproporcionalidade de relatos — sinais estatísticos de segurança.</li>
<li><strong>Monitorização intensiva</strong>: hospitais sentinela acompanham pacientes em uso de medicamentos recém-lançados.</li>
</ul>

<h2>O Caso Vioxx (Rofecoxib)</h2>
<p>Em 2004, o Vioxx foi retirado do mercado após o estudo APPROVe demonstrar aumento de 3,5x no risco de infarto do miocárdio. Estima-se que causou entre 88.000 e 140.000 casos de doença coronariana nos EUA. Este caso reforçou a importância da farmacovigilância contínua e rigorosa.</p>

<h2>O Farmacêutico na Farmacovigilância</h2>
<ul>
<li>Farmacêuticos são os profissionais que mais notificam RAMs no Brasil (após médicos).</li>
<li>Na farmácia hospitalar: busca ativa de RAMs, reconciliação medicamentosa e trigger tools.</li>
<li>Na drogaria: orientação sobre efeitos adversos e estímulo ao relato pelo paciente.</li>
</ul>

<h2>Referências</h2>
<ul>
<li>ANVISA – Sistema Notivisa: notivisa.anvisa.gov.br</li>
<li>WHO Programme for International Drug Monitoring (Uppsala Monitoring Centre).</li>
<li>Rawlins MD, Thompson JW. Textbook of Adverse Drug Reactions, 1977.</li>
</ul>"
            },

            // ───────────── 9 ─────────────
            new()
            {
                Id = "curated-009",
                Titulo = "Bioquímica Clínica: Marcadores Hepáticos — Interpretação Completa para o Farmacêutico",
                Subtitulo = "ALT, AST, GGT, fosfatase alcalina, bilirrubinas — o que cada marcador revela sobre o fígado",
                Resumo = "Os testes de função hepática estão entre os mais solicitados na prática clínica. Aprenda a interpretar cada enzima, diferenciar padrão hepatocelular de colestático e correlacionar com hepatotoxicidade por fármacos.",
                Autor = "Profa. Dra. Patrícia Souza",
                Fonte = "Portal Farmácia Estudos",
                ImagemUrl = Img + "photo-1579154204601-01588f351e67?w=900&h=450&fit=crop&auto=format&q=80",
                ImagemCredito = "Unsplash / Lab Tests",
                Categoria = "Análises Clínicas",
                Tags = new() { "bioquímica", "hepatograma", "ALT", "AST", "fígado" },
                DataPublicacao = today.AddDays(-8),
                LeituraMinutos = 12,
                Conteudo = @"
<p class='lead'>O fígado é o principal órgão de metabolização de fármacos (Fases I e II do CYP450). Hepatotoxicidade por medicamentos (DILI — Drug-Induced Liver Injury) é a principal causa de retirada de fármacos do mercado. Saber interpretar marcadores hepáticos é essencial para o farmacêutico.</p>

<h2>Principais Marcadores e Significado</h2>
<table>
<thead><tr><th>Marcador</th><th>VR (adulto)</th><th>Elevação indica</th></tr></thead>
<tbody>
<tr><td>ALT (TGP)</td><td>7-56 U/L</td><td>Lesão hepatocelular (mais específica para fígado)</td></tr>
<tr><td>AST (TGO)</td><td>10-40 U/L</td><td>Lesão hepatocelular + muscular + cardíaca</td></tr>
<tr><td>GGT (γ-GT)</td><td>9-48 U/L</td><td>Colestase, etilismo, indução enzimática por fármacos</td></tr>
<tr><td>Fosfatase Alcalina (FA)</td><td>44-147 U/L</td><td>Colestase, doença óssea (Paget, metástases)</td></tr>
<tr><td>Bilirrubina Total</td><td>0,1-1,2 mg/dL</td><td>Icterícia se > 2,5 mg/dL</td></tr>
<tr><td>Albumina</td><td>3,5-5,0 g/dL</td><td>↓ em doença hepática crônica (síntese reduzida)</td></tr>
<tr><td>INR / TP</td><td>INR ~1,0</td><td>↑ em insuficiência hepática (fígado produz fatores de coagulação)</td></tr>
</tbody>
</table>

<h2>Padrão Hepatocelular vs Colestático</h2>
<ul>
<li><strong>Hepatocelular</strong>: ALT e AST >> FA e GGT. Relação R = (ALT/VR) / (FA/VR) > 5. Ex: hepatite viral, DILI por paracetamol.</li>
<li><strong>Colestático</strong>: FA e GGT >> ALT e AST. R < 2. Ex: obstrução biliar, DILI por amoxicilina-clavulanato.</li>
<li><strong>Misto</strong>: R entre 2-5. Ex: DILI por fenitoína.</li>
</ul>

<h2>DILI — Hepatotoxicidade por Fármacos</h2>
<p>Principais causadores:</p>
<ul>
<li><strong>Paracetamol</strong> — dose-dependente (>4g/dia ou >150 mg/kg); necrose centrolobular. Tratamento: N-acetilcisteína.</li>
<li><strong>Amoxicilina-clavulanato</strong> — principal antibiótico causador de DILI; padrão colestático.</li>
<li><strong>Isoniazida</strong> — hepatocelular; monitorar ALT mensalmente nos primeiros 3 meses.</li>
<li><strong>Estatinas</strong> — elevação leve de ALT (<3x LSN) é comum e geralmente não requer suspensão.</li>
<li><strong>Metotrexato</strong> — fibrose hepática acumulativa; monitorar com FibroScan ou biópsia.</li>
</ul>

<h2>O Farmacêutico na Monitorização Hepática</h2>
<p>O farmacêutico clínico deve solicitar e interpretar exames laboratoriais (Lei 13.021/2014 e Resolução CFF 585/2013), especialmente para pacientes em uso crônico de medicamentos hepatotóxicos — ajustando doses ou sugerindo troca terapêutica quando necessário.</p>

<h2>Referências</h2>
<ul>
<li>Kaplowitz N. <em>Nat Rev Drug Discov</em>, 2005. Idiosyncratic drug hepatotoxicity.</li>
<li>Reuben A et al. <em>Hepatology</em>, 2010. Drug-Induced Acute Liver Failure.</li>
<li>EASL Clinical Practice Guidelines: Drug-induced liver injury, 2019.</li>
</ul>"
            },

            // ───────────── 10 ─────────────
            new()
            {
                Id = "curated-010",
                Titulo = "Cuidados Paliativos e o Farmacêutico: Manejo da Dor Oncológica e Sintomas de Fim de Vida",
                Subtitulo = "Escada analgésica da OMS, titulação de opioides, e o papel farmacêutico na equipe multidisciplinar",
                Resumo = "Cuidados paliativos são um direito do paciente e uma área crescente para farmacêuticos. Aprenda sobre a escada analgésica, rotação de opioides, adjuvantes e controle de sintomas como dispneia, náusea e delírio.",
                Autor = "Prof. Dr. Henrique Moraes",
                Fonte = "Portal Farmácia Estudos",
                ImagemUrl = Img + "photo-1559757175-0eb30cd8c063?w=900&h=450&fit=crop&auto=format&q=80",
                ImagemCredito = "Unsplash / Healthcare",
                Categoria = "Farmácia Clínica",
                Tags = new() { "cuidados paliativos", "opioides", "dor", "oncologia" },
                DataPublicacao = today.AddDays(-9),
                LeituraMinutos = 11,
                Conteudo = @"
<p class='lead'>Segundo a OMS, cuidados paliativos melhoram a qualidade de vida de pacientes com doenças graves através da prevenção e alívio do sofrimento. O farmacêutico é essencial na otimização da farmacoterapia analgésica e no manejo de sintomas complexos.</p>

<h2>Escada Analgésica da OMS (Modificada)</h2>
<table>
<thead><tr><th>Degrau</th><th>Intensidade da Dor</th><th>Fármacos</th></tr></thead>
<tbody>
<tr><td>1</td><td>Leve (EVA 1-3)</td><td>Paracetamol, dipirona, AINEs ± adjuvantes</td></tr>
<tr><td>2</td><td>Moderada (EVA 4-6)</td><td>Opioides fracos (codeína, tramadol) + degrau 1 ± adjuvantes</td></tr>
<tr><td>3</td><td>Intensa (EVA 7-10)</td><td>Opioides fortes (morfina, oxicodona, fentanil, metadona) ± degrau 1 ± adjuvantes</td></tr>
<tr><td>4 (intervencionista)</td><td>Refratária</td><td>Bloqueios anestésicos, bombas intratecais, neurolíse</td></tr>
</tbody>
</table>

<h2>Rotação de Opioides</h2>
<p>Quando um opioide perde eficácia ou causa efeitos adversos intoleráveis, troca-se por outro usando tabelas de equianalgesia:</p>
<ul>
<li>Morfina oral 30 mg/dia ≈ Oxicodona oral 20 mg/dia ≈ Fentanil transdérmico 12 mcg/h</li>
<li>Na rotação, iniciar com 50-75% da dose equianalgésica calculada (tolerância cruzada incompleta).</li>
<li>A metadona exige cuidado especial: meia-vida longa e variável (8-59h), QTc prolongado, múltiplas interações.</li>
</ul>

<h2>Adjuvantes Analgésicos</h2>
<ul>
<li><strong>Dor neuropática</strong>: gabapentina (300-3600 mg/dia), pregabalina, duloxetina, amitriptilina.</li>
<li><strong>Dor óssea por metástases</strong>: bisfosfonatos (ácido zoledrônico), denosumab, corticoides, radioterapia.</li>
<li><strong>Dor visceral por espasmo</strong>: hioscina, octreotida (obstrução intestinal maligna).</li>
</ul>

<h2>Atuação Farmacêutica em Cuidados Paliativos</h2>
<ul>
<li>Titulação e conversão de opioides, verificando interações e ajuste renal/hepático.</li>
<li>Preparo de infusões contínuas subcutâneas (<strong>hipodermóclise</strong>) de morfina + midazolam + ondansetrona.</li>
<li>Orientação domiciliar sobre uso de medicamentos de resgate e manejo de constipação induzida por opioides (lactulose, PEG 3350, naloxegol).</li>
<li>Suporte à equipe na desprescrição de medicamentos fúteis no contexto de fim de vida.</li>
</ul>

<h2>Referências</h2>
<ul>
<li>OMS – WHO Guidelines for the Pharmacological and Radiotherapeutic Management of Cancer Pain in Adults and Adolescents, 2018.</li>
<li>ANCP – Manual de Cuidados Paliativos, 3ª edição, 2022.</li>
</ul>"
            },

            // ───────────── 11 ─────────────
            new()
            {
                Id = "curated-011",
                Titulo = "Hemograma Completo: Guia Prático de Interpretação para Estudantes de Farmácia",
                Subtitulo = "Eritrograma, leucograma e plaquetograma — valores de referência, artefatos e correlações clínicas",
                Resumo = "O hemograma é o exame laboratorial mais solicitado no mundo. Este guia cobre cada parâmetro do eritrograma, leucograma e plaquetograma com valores de referência, significado clínico e armadilhas comuns.",
                Autor = "Profa. Dra. Patrícia Souza",
                Fonte = "Portal Farmácia Estudos",
                ImagemUrl = Img + "photo-1582719508461-905c673771fd?w=900&h=450&fit=crop&auto=format&q=80",
                ImagemCredito = "Unsplash / Laboratory",
                Categoria = "Análises Clínicas",
                Tags = new() { "hemograma", "eritrograma", "leucograma", "anemia" },
                DataPublicacao = today.AddDays(-10),
                LeituraMinutos = 14,
                Conteudo = @"
<p class='lead'>O hemograma avalia quantitativa e qualitativamente as três linhagens celulares do sangue: eritrócitos (vermelhos), leucócitos (brancos) e plaquetas. Para o farmacêutico, é fundamental na avaliação de toxicidade hematológica por fármacos.</p>

<h2>Eritrograma</h2>
<table>
<thead><tr><th>Parâmetro</th><th>VR (adulto)</th><th>Significado clínico da alteração</th></tr></thead>
<tbody>
<tr><td>Hemácias</td><td>♂ 4,5-5,5 / ♀ 4,0-5,0 (×10⁶/µL)</td><td>↓ Anemia / ↑ Policitemia</td></tr>
<tr><td>Hemoglobina (Hb)</td><td>♂ 13-17 / ♀ 12-16 g/dL</td><td>Diagnóstico de anemia (OMS: ♂<13, ♀<12)</td></tr>
<tr><td>Hematócrito (Ht)</td><td>♂ 40-50% / ♀ 36-44%</td><td>↑ Desidratação, policitemia / ↓ Anemia</td></tr>
<tr><td>VCM</td><td>80-100 fL</td><td>Microcítica (<80): ferropriva | Macrocítica (>100): B12/folato</td></tr>
<tr><td>HCM</td><td>27-31 pg</td><td>Hipocrômica (<27): ferropriva</td></tr>
<tr><td>CHCM</td><td>32-36 g/dL</td><td>Hipercrômica (>36): esferocitose</td></tr>
<tr><td>RDW</td><td>11,5-14,5%</td><td>↑ Anisocitose — ex: ferropriva precoce</td></tr>
</tbody>
</table>

<h2>Leucograma</h2>
<table>
<thead><tr><th>Célula</th><th>VR (%)</th><th>Absoluto (/µL)</th><th>↑ Em</th></tr></thead>
<tbody>
<tr><td>Neutrófilos</td><td>40-70%</td><td>1.800-7.700</td><td>Infecção bacteriana, estresse, corticoides</td></tr>
<tr><td>Linfócitos</td><td>20-40%</td><td>1.000-4.800</td><td>Infecção viral, LLC</td></tr>
<tr><td>Monócitos</td><td>2-8%</td><td>100-800</td><td>Infecções crônicas (TB), recuperação medular</td></tr>
<tr><td>Eosinófilos</td><td>1-4%</td><td>50-500</td><td>Parasitoses, alergias, eosinofilia por fármacos (DRESS)</td></tr>
<tr><td>Basófilos</td><td>0-1%</td><td>0-100</td><td>LMC, hipersensibilidade</td></tr>
</tbody>
</table>

<h2>Toxicidade Hematológica por Fármacos</h2>
<ul>
<li><strong>Neutropenia</strong> (<1.500/µL): metotrexato, clozapina, quimioterápicos, ganciclovir. <em>Neutropenia febril é emergência oncológica.</em></li>
<li><strong>Trombocitopenia</strong> (<150.000/µL): heparina (HIT), valproato, linezolida, quimioterápicos.</li>
<li><strong>Anemia megaloblástica</strong>: metotrexato (antagonista de folato), trimetoprima.</li>
<li><strong>Anemia aplástica</strong>: cloranfenicol, carbamazepina (rara), metamizol (rara).</li>
</ul>

<h2>Referências</h2>
<ul>
<li>Failace R. <em>Hemograma: Manual de Interpretação</em>, 6ª ed., Artmed, 2015.</li>
<li>Hoffbrand AV. <em>Fundamentos em Hematologia de Hoffbrand</em>, 7ª ed., 2018.</li>
</ul>"
            },

            // ───────────── 12 ─────────────
            new()
            {
                Id = "curated-012",
                Titulo = "Interações Medicamentosas Críticas: As 15 que Todo Farmacêutico Precisa Conhecer de Cor",
                Subtitulo = "Mecanismos farmacocinéticos e farmacodinâmicos ilustrados com desfechos clínicos reais",
                Resumo = "Interações medicamentosas causam cerca de 7% das internações hospitalares. Este artigo detalha 15 interações potencialmente fatais, seus mecanismos e as condutas do farmacêutico para preveni-las.",
                Autor = "Prof. Dr. Lucas Mendes",
                Fonte = "Portal Farmácia Estudos",
                ImagemUrl = Img + "photo-1471864190281-a93a3070b6de?w=900&h=450&fit=crop&auto=format&q=80",
                ImagemCredito = "Unsplash / Medications",
                Categoria = "Farmacologia",
                Tags = new() { "interações", "CYP450", "farmacocinética", "segurança" },
                DataPublicacao = today.AddDays(-11),
                LeituraMinutos = 15,
                Conteudo = @"
<p class='lead'>O manejo seguro de interações medicamentosas é uma das competências mais valorizadas do farmacêutico clínico. A cada prescrição revisada, o profissional pode identificar combinações perigosas e evitar desfechos graves.</p>

<h2>Interações Farmacocinéticas (Absorção, Metabolismo, Excreção)</h2>

<h3>1. Varfarina + Fluconazol</h3>
<p><strong>Mecanismo</strong>: fluconazol inibe CYP2C9 → acúmulo de S-varfarina (enantiômero mais ativo). <strong>Risco</strong>: INR >10, hemorragia grave. <strong>Conduta</strong>: reduzir dose de varfarina em 50%, monitorar INR a cada 2-3 dias.</p>

<h3>2. Sinvastatina + Itraconazol</h3>
<p><strong>Mecanismo</strong>: itraconazol é inibidor potente do CYP3A4 → aumento de até 20x nos níveis de sinvastatina. <strong>Risco</strong>: rabdomiólise (destruição muscular, insuficiência renal). <strong>Conduta</strong>: suspender estatina durante o antifúngico ou trocar por pravastatina/rosuvastatina (menos dependentes do CYP3A4).</p>

<h3>3. Ciprofloxacino + Antiácidos com Al/Mg</h3>
<p><strong>Mecanismo</strong>: quelação no TGI → redução de 80-90% na absorção da quinolona. <strong>Conduta</strong>: administrar cipro 2h antes ou 6h após o antiácido.</p>

<h3>4. Metotrexato + AINEs</h3>
<p><strong>Mecanismo</strong>: AINEs reduzem perfusão renal → diminuição do clearance de metotrexato → toxicidade (pancitopenia, mucosite). <strong>Conduta</strong>: em dose alta de MTX, os AINEs são contraindicados; em dose baixa (artrite), cautela e monitoramento.</p>

<h3>5. Digoxina + Amiodarona</h3>
<p><strong>Mecanismo</strong>: amiodarona inibe a glicoproteína-P (P-gp) → aumento de 70-100% nos níveis de digoxina. <strong>Conduta</strong>: reduzir dose de digoxina pela metade ao iniciar amiodarona; monitorar digoxinemia (alvo 0,5-0,9 ng/mL).</p>

<h2>Interações Farmacodinâmicas (Efeito Aditivo, Sinergismo, Antagonismo)</h2>

<h3>6. IECA + Espironolactona + Suplemento de KCl</h3>
<p><strong>Mecanismo</strong>: tripla retenção de potássio → hipercalemia grave (arritmia, PCR). <strong>Conduta</strong>: monitorar K+ sérico; evitar suplementação de K+ se K+ >5,0 mEq/L.</p>

<h3>7. ISRS + Tramadol (Síndrome Serotoninérgica)</h3>
<p><strong>Mecanismo</strong>: ambos aumentam serotonina → toxicidade (agitação, hipertermia, mioclonias, coma). <strong>Conduta</strong>: evitar a combinação; se necessário opioide, preferir morfina.</p>

<h3>8. Metformina + Contraste Iodado</h3>
<p><strong>Mecanismo</strong>: contraste pode causar IRA transitória → acúmulo de metformina → acidose lática. <strong>Conduta</strong>: suspender metformina 48h antes do exame; retomar após confirmar creatinina normal.</p>

<h3>9. Lítio + Diuréticos Tiazídicos</h3>
<p><strong>Mecanismo</strong>: tiazídicos aumentam reabsorção de Na+ e Li+ no túbulo proximal → acúmulo de lítio. <strong>Risco</strong>: tremor, ataxia, convulsão. <strong>Conduta</strong>: monitorar litemia; ajustar dose ou trocar diurético.</p>

<h3>10. Clopidogrel + Omeprazol</h3>
<p><strong>Mecanismo</strong>: omeprazol inibe CYP2C19 → reduz conversão do pró-fármaco clopidogrel ao metabólito ativo. <strong>Conduta</strong>: preferir pantoprazol (menor inibição do CYP2C19).</p>

<h2>Mais 5 Interações Essenciais</h2>
<ul>
<li><strong>11. Carbamazepina + Anticoncepcionais orais</strong> — indução CYP3A4 → reduz eficácia contraceptiva.</li>
<li><strong>12. Ciprofloxacino + Tizanidina</strong> — inibição CYP1A2 → hipotensão severa, sedação extrema.</li>
<li><strong>13. Linezolida + Alimentos ricos em tiramina</strong> — efeito IMAO → crise hipertensiva.</li>
<li><strong>14. Azitromicina + Amiodarona</strong> — prolongamento QTc aditivo → torsades de pointes.</li>
<li><strong>15. Claritromicina + Colchicina</strong> — inibição CYP3A4 e P-gp → toxicidade fatal por colchicina.</li>
</ul>

<h2>Referências</h2>
<ul>
<li>Hansten PD, Horn JR. <em>Drug Interactions Analysis and Management</em>, 2024.</li>
<li>Micromedex Drug Interactions database.</li>
<li>Stockley's Drug Interactions, 13th edition.</li>
</ul>"
            },

            // ───────────── 13 ─────────────
            new()
            {
                Id = "curated-013",
                Titulo = "Medicamentos Biológicos e Biossimilares: Entenda as Diferenças e o Impacto no SUS",
                Subtitulo = "Anticorpos monoclonais, proteínas de fusão e a regulamentação brasileira de biossimilares",
                Resumo = "O mercado de biológicos já representa mais de 30% do gasto farmacêutico global. Saiba como são produzidos, como biossimilares diferem de genéricos e como o SUS está ampliando acesso a essas terapias.",
                Autor = "Profa. Dra. Fernanda Lima",
                Fonte = "Portal Farmácia Estudos",
                ImagemUrl = Img + "photo-1516549655169-df83a0774514?w=900&h=450&fit=crop&auto=format&q=80",
                ImagemCredito = "Unsplash / Pharmaceutical",
                Categoria = "Tecnologia Farmacêutica",
                Tags = new() { "biológicos", "biossimilares", "anticorpos monoclonais", "SUS" },
                DataPublicacao = today.AddDays(-12),
                LeituraMinutos = 10,
                Conteudo = @"
<p class='lead'>Enquanto medicamentos sintéticos são moléculas pequenas (100-500 Da), biológicos são macromoléculas complexas (15.000-150.000 Da) produzidas por organismos vivos. Essa complexidade torna impossível uma ""cópia idêntica"" — daí o conceito de <strong>biossimilar</strong>.</p>

<h2>Tipos de Medicamentos Biológicos</h2>
<table>
<thead><tr><th>Tipo</th><th>Exemplo</th><th>Indicação</th></tr></thead>
<tbody>
<tr><td>Anticorpo monoclonal</td><td>Adalimumabe (Humira®)</td><td>Artrite reumatoide, Crohn, psoríase</td></tr>
<tr><td>Proteína de fusão</td><td>Etanercepte (Enbrel®)</td><td>Artrite reumatoide, espondilite</td></tr>
<tr><td>Hormônio recombinante</td><td>Insulina glargina (Lantus®)</td><td>Diabetes mellitus</td></tr>
<tr><td>Fator de crescimento</td><td>Filgrastima (Neupogen®)</td><td>Neutropenia pós-quimioterapia</td></tr>
<tr><td>Vacina</td><td>Vacinas recombinantes</td><td>Prevenção de doenças</td></tr>
</tbody>
</table>

<h2>Biossimilar ≠ Genérico</h2>
<ul>
<li><strong>Genérico</strong>: molécula pequena, síntese química reprodutível, bioequivalência demonstrada, intercambiável por padrão.</li>
<li><strong>Biossimilar</strong>: molécula grande, produção em biorreatores (condições diferentes = produto diferente), requer exercício de comparabilidade extenso (analítico, pré-clínico, clínico), intercambialidade precisa de regulamentação específica.</li>
</ul>

<h2>Regulamentação no Brasil (RDC 55/2010)</h2>
<ul>
<li><strong>Via de desenvolvimento individual</strong>: dossiê completo, como novo biológico.</li>
<li><strong>Via de desenvolvimento por comparabilidade</strong>: biossimilar demonstra similaridade ao produto de referência.</li>
<li>Extrapolação de indicações: se demonstrada similaridade em uma indicação, pode-se extrapolar para outras (decisão regulatória caso a caso).</li>
</ul>

<h2>Impacto Econômico e SUS</h2>
<p>Biossimilares podem custar <strong>30-50% menos</strong> que o biológico de referência. Com a expiração de patentes de blockbusters (adalimumabe, trastuzumabe, rituximabe, bevacizumabe), o SUS está incorporando biossimilares para ampliar acesso via CONITEC.</p>

<h2>Referências</h2>
<ul>
<li>ANVISA – RDC 55/2010 (registro de biológicos e biossimilares).</li>
<li>OMS – Guidelines on Evaluation of Biosimilars, 2022.</li>
</ul>"
            },

            // ───────────── 14 ─────────────
            new()
            {
                Id = "curated-014",
                Titulo = "Antibióticos: Mecanismos de Ação Completos — Do Alvo Molecular ao Espectro de Atividade",
                Subtitulo = "β-Lactâmicos, aminoglicosídeos, fluoroquinolonas, macrolídeos, glicopeptídeos e mais",
                Resumo = "Guia detalhado dos mecanismos de ação de todas as classes de antibióticos, seus alvos moleculares, espectro de ação e principais representantes — essencial para provas e prática clínica.",
                Autor = "Profa. Dra. Mariana Costa",
                Fonte = "Portal Farmácia Estudos",
                ImagemUrl = Img + "photo-1585435557343-3b092031a831?w=900&h=450&fit=crop&auto=format&q=80",
                ImagemCredito = "Unsplash / Medicine",
                Categoria = "Farmacologia",
                Tags = new() { "antibióticos", "β-lactâmicos", "quinolonas", "mecanismo de ação" },
                DataPublicacao = today.AddDays(-13),
                LeituraMinutos = 16,
                Conteudo = @"
<p class='lead'>Antibióticos são classificados por seu mecanismo de ação em cinco grandes grupos: <strong>inibidores da parede celular</strong>, <strong>inibidores da síntese proteica</strong>, <strong>inibidores de ácidos nucleicos</strong>, <strong>antimetabólitos</strong> e <strong>disruptores de membrana</strong>.</p>

<h2>1. Inibidores da Parede Celular</h2>
<h3>β-Lactâmicos</h3>
<p><strong>Alvo</strong>: PBPs (Penicillin-Binding Proteins) — enzimas transpeptidases que ligam as cadeias de peptidoglicano.</p>
<p><strong>Mecanismo</strong>: o anel β-lactâmico é análogo estrutural da D-ala-D-ala, ligando-se irreversivelmente às PBPs → inibição da síntese de parede → lise osmótica.</p>
<ul>
<li><strong>Penicilinas</strong>: amoxicilina, ampicilina, piperacilina. Amoxicilina + clavulanato (inibidor de β-lactamase).</li>
<li><strong>Cefalosporinas</strong>: 1ª geração (cefalexina, cefazolina), 3ª (ceftriaxona, ceftazidima), 4ª (cefepima), 5ª (ceftarolina — anti-MRSA).</li>
<li><strong>Carbapenêmicos</strong>: meropenem, imipenem-cilastatina, ertapenem. Espectro ultra-amplo, reservados para infecções graves por MDR.</li>
</ul>

<h3>Glicopeptídeos</h3>
<p><strong>Alvo</strong>: terminal D-ala-D-ala do pentapeptídeo nascente da parede celular.</p>
<p><strong>Mecanismo</strong>: vancomicina se liga ao substrato, impedindo a transpeptidação. Atua apenas em gram-positivos (não penetra a membrana externa de gram-negativos).</p>

<h2>2. Inibidores da Síntese Proteica</h2>
<table>
<thead><tr><th>Classe</th><th>Subunidade ribossonal</th><th>Efeito</th><th>Exemplos</th></tr></thead>
<tbody>
<tr><td>Aminoglicosídeos</td><td>30S</td><td>Bactericida (erro de leitura do mRNA)</td><td>Gentamicina, amicacina, tobramicina</td></tr>
<tr><td>Tetraciclinas</td><td>30S</td><td>Bacteriostático</td><td>Doxiciclina, minociclina, tigeciclina</td></tr>
<tr><td>Macrolídeos</td><td>50S</td><td>Bacteriostático</td><td>Azitromicina, claritromicina, eritromicina</td></tr>
<tr><td>Lincosamidas</td><td>50S</td><td>Bacteriostático</td><td>Clindamicina</td></tr>
<tr><td>Oxazolidinonas</td><td>50S (formação do complexo de iniciação)</td><td>Bacteriostático</td><td>Linezolida, tedizolida</td></tr>
</tbody>
</table>

<h2>3. Inibidores de Ácidos Nucleicos</h2>
<ul>
<li><strong>Fluoroquinolonas</strong> (ciprofloxacino, levofloxacino, moxifloxacino): inibem DNA girase (topoisomerase II, em gram-negativos) e topoisomerase IV (em gram-positivos) → quebra do DNA.</li>
<li><strong>Rifamicinas</strong> (rifampicina): inibem RNA polimerase DNA-dependente → bloqueio da transcrição. Principal uso: tuberculose.</li>
<li><strong>Metronidazol</strong>: radical nitro reduzido em anaeróbios → danos ao DNA. Ativo contra anaeróbios e protozoários.</li>
</ul>

<h2>4. Antimetabólitos</h2>
<p><strong>Sulfonamidas</strong> (sulfametoxazol) + <strong>Trimetoprima</strong>: bloqueio sequencial da via do folato (DHPS e DHFR, respectivamente). Bactericida em combinação. Uso: ITU, Pneumocystis jirovecii.</p>

<h2>5. Disruptores de Membrana</h2>
<p><strong>Polimixinas</strong> (colistina, polimixina B): interagem com LPS da membrana externa de gram-negativos → desestabilização → morte celular. Último recurso para Acinetobacter e Pseudomonas pan-resistentes.</p>

<h2>Referências</h2>
<ul>
<li>Brunton LL et al. <em>Goodman & Gilman's: The Pharmacological Basis of Therapeutics</em>, 14th ed., 2023.</li>
<li>Rang HP et al. <em>Rang & Dale's Pharmacology</em>, 10th ed., 2024.</li>
</ul>"
            },

            // ───────────── 15 ─────────────
            new()
            {
                Id = "curated-015",
                Titulo = "Assistência Farmacêutica no SUS: Do Ciclo Logístico à Farmácia Clínica na Atenção Primária",
                Subtitulo = "RENAME, REMUME, PNM, PNAF e as competências clínicas do farmacêutico no NASF e UBS",
                Resumo = "A Assistência Farmacêutica é uma das áreas mais cobradas em concursos públicos de farmácia. Este artigo aborda todo o ciclo (seleção→dispensação), a RENAME 2024, e o papel clínico do farmacêutico na atenção primária do SUS.",
                Autor = "Farm. Esp. Juliana Rocha",
                Fonte = "Portal Farmácia Estudos",
                ImagemUrl = Img + "photo-1530026405186-ed1f139313f8?w=900&h=450&fit=crop&auto=format&q=80",
                ImagemCredito = "Unsplash / Health System",
                Categoria = "Saúde Pública",
                Tags = new() { "SUS", "RENAME", "assistência farmacêutica", "atenção primária" },
                DataPublicacao = today.AddDays(-14),
                LeituraMinutos = 13,
                Conteudo = @"
<p class='lead'>O SUS é o maior sistema público de saúde do mundo, atendendo mais de 190 milhões de brasileiros. A Assistência Farmacêutica garante acesso a medicamentos essenciais e é um dos pilares da Política Nacional de Medicamentos (PNM, Portaria 3.916/1998).</p>

<h2>Políticas Nacionais</h2>
<ul>
<li><strong>PNM (1998)</strong>: garantir segurança, eficácia e qualidade dos medicamentos; promoção do uso racional.</li>
<li><strong>PNAF (2004 – Resolução CNS 338)</strong>: conjunto de ações voltadas à promoção, proteção e recuperação da saúde tendo o medicamento como insumo essencial.</li>
<li><strong>Componentes de financiamento</strong>: Básico (UBS), Estratégico (programas especiais — TB, HIV, hanseníase) e Especializado (alto custo — PCDT).</li>
</ul>

<h2>RENAME — Relação Nacional de Medicamentos Essenciais</h2>
<p>A RENAME 2024 contém mais de 900 itens farmacêuticos. É elaborada pela CONITEC (Comissão Nacional de Incorporação de Tecnologias no SUS) com base em:</p>
<ul>
<li>Evidência de eficácia e segurança.</li>
<li>Análise de custo-efetividade.</li>
<li>Impacto orçamentário.</li>
<li>Necessidade epidemiológica.</li>
</ul>
<p>Municípios elaboram suas REMUME (Relação Municipal) baseadas na RENAME, acrescidas de itens de relevância local.</p>

<h2>Ciclo da Assistência Farmacêutica</h2>
<table>
<thead><tr><th>Etapa</th><th>Atividades</th></tr></thead>
<tbody>
<tr><td>1. Seleção</td><td>Elaboração da REMUME; CFT (Comissão de Farmácia e Terapêutica)</td></tr>
<tr><td>2. Programação</td><td>Estimativa de demanda por método de consumo histórico ou perfil epidemiológico</td></tr>
<tr><td>3. Aquisição</td><td>Licitação (pregão eletrônico), ARP (Ata de Registro de Preços), compras emergenciais</td></tr>
<tr><td>4. Armazenamento</td><td>Boas práticas: controle de temperatura, FIFO/FEFO, segregação de controlados</td></tr>
<tr><td>5. Distribuição</td><td>Abastecimento das UBS, hospitais, CAPS; controle logístico</td></tr>
<tr><td>6. Dispensação</td><td>Entrega do medicamento + orientação farmacêutica ao paciente</td></tr>
</tbody>
</table>

<h2>O Farmacêutico Clínico no NASF e na ESF</h2>
<p>Com a Resolução CFF 585/2013, o farmacêutico pode legalmente:</p>
<ul>
<li>Solicitar exames laboratoriais para monitoramento farmacoterapêutico.</li>
<li>Realizar consulta farmacêutica (anamnese, plano de cuidado, agendamento de retorno).</li>
<li>Prescrever medicamentos isentos de prescrição (MIPs) e ajustar doses dentro de protocolos clínicos.</li>
<li>Reconciliar medicamentos em pacientes transferidos entre níveis de atenção.</li>
</ul>

<h2>Para Concursos: Pontos Mais Cobrados</h2>
<ol>
<li>Diferença entre PNM e PNAF.</li>
<li>Componentes de financiamento (básico, estratégico, especializado).</li>
<li>Etapas do ciclo da AF.</li>
<li>Papel do farmacêutico na ESF/NASF.</li>
<li>RENAME vs REMUME.</li>
</ol>

<h2>Referências</h2>
<ul>
<li>Brasil. Ministério da Saúde. Política Nacional de Medicamentos (Portaria 3.916/1998).</li>
<li>Brasil. Resolução CNS nº 338/2004 (PNAF).</li>
<li>RENAME 2024 — Ministério da Saúde.</li>
</ul>"
            },
        };
    }
}
