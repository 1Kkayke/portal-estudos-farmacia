namespace PortalEstudos.API.Data;

public static partial class StudyContentSeeder
{
    static partial void GenerateTopics15to28(Dictionary<int, (DocDef[], QDef[])> data)
    {
        // Topic 15: Análises Toxicológicas
        data[15] = (BuildDocs("Análises Toxicológicas", new[] {
            "Introdução à Toxicologia Analítica", "Cadeia de Custódia em Análises Forenses", "Screening Toxicológico",
            "Imunoensaios em Toxicologia", "Cromatografia Gasosa Acoplada a Espectrometria de Massas (GC-MS)",
            "Análise de Drogas de Abuso", "Dosagem de Etanol", "Biomarcadores de Exposição",
            "Intoxicação por Paracetamol e N-acetilcisteína", "Análise de Organofosforados e Carbamatos",
            "Colinesterase Eritrocitária", "Intoxicação por Monóxido de Carbono", "Análise de Metais Pesados",
            "Absorção Atômica em Toxicologia", "Análise de Benzodiazepínicos", "Análise de Opioides",
            "Análise de Canabinoides", "Análise de Cocaína e Metabólitos", "Análise de Anfetaminas",
            "Interpretação de Resultados Toxicológicos"
        }), BuildFromTerms("Análises Toxicológicas", new QDef[0], new[] {
            ("Cadeia de custódia", "Registro documentado de posse de amostra forense garantindo rastreabilidade"),
            ("Screening toxicológico", "Triagem inicial qualitativa de drogas por imunoensaio"),
            ("Cromatografia forense", "Técnica confirmatória de identificação de drogas por CG-MS ou LC-MS"),
            ("Biomarcador de exposição", "Indicador biológico de contato com toxicante"),
            ("N-acetilcisteína", "Antídoto para intoxicação por paracetamol, repõe glutationa"),
            ("Colinesterase eritrocitária", "Marcador de exposição a organofosforados e carbamatos"),
            ("Dosagem de etanol", "Quantificação de álcool em sangue/ar exalado por cromatografia gasosa"),
            ("Imunoensaio", "Técnica de triagem baseada em reação antígeno-anticorpo"),
            ("Monóxido de carbono", "Tóxico que forma carboxihemoglobina, medida por co-oximetria"),
            ("Metais pesados", "Chumbo, mercúrio, arsênio quantificados por absorção atômica")
        }));

        data[16] = (BuildDocs("Bioquímica", new[] {
            "Introdução à Bioquímica", "Estrutura e Função de Proteínas", "Enzimas e Cinética Enzimática",
            "Glicólise e Metabolismo de Carboidratos", "Ciclo de Krebs", "Fosforilação Oxidativa e Cadeia Respiratória",
            "Metabolismo de Lipídeos e Beta-oxidação", "Gliconeogênese", "Via das Pentoses-Fosfato",
            "Metabolismo de Aminoácidos", "Ciclo da Ureia", "Metabolismo do Nitrogênio",
            "Bioquímica Clínica: Marcadores Cardíacos", "Troponinas e Mioglobina", "Transaminases Hepáticas",
            "Bilirrubinas e Função Hepática", "Ureia e Creatinina", "Glicemia e Hemoglobina Glicada",
            "Perfil Lipídico", "Hormônios e Função Endócrina"
        }), BuildFromTerms("Bioquímica", new QDef[0], new[] {
            ("Glicólise", "Via citosólica que converte glicose em 2 piruvatos gerando 2 ATP"),
            ("Ciclo de Krebs", "Ciclo mitocondrial que oxida acetil-CoA gerando NADH e FADH2"),
            ("Fosforilação oxidativa", "Geração de ATP na cadeia respiratória mitocondrial"),
            ("Beta-oxidação", "Degradação de ácidos graxos em acetil-CoA na mitocôndria"),
            ("Gliconeogênese", "Síntese de glicose a partir de precursores não glicídicos"),
            ("Ciclo da ureia", "Via hepática que converte amônia tóxica em ureia excretável"),
            ("Via das pentoses", "Via citosólica que gera NADPH e ribose-5-fosfato"),
            ("Troponina I", "Marcador cardíaco específico de necrose miocárdica"),
            ("Transaminases", "ALT e AST, enzimas indicadoras de lesão hepatocelular"),
            ("Cinética enzimática", "Estudo de velocidade de reação catalisada por enzima")
        }));

        data[17] = (BuildDocs("Físico-Química", new[] {
            "Fundamentos de Físico-Química Farmacêutica", "Equação de Henderson-Hasselbalch", "Polimorfismo e Pseudopolimorfismo",
            "Cinética Química: Ordem Zero e Primeira Ordem", "Coeficiente de Partição (Log P)",
            "Solubilidade e Fatores que a Afetam", "Sistemas Coloidais", "Potencial Zeta",
            "Isotonia e Tonicidade", "Equação de Arrhenius", "Estabilidade de Medicamentos",
            "Solubilização Micelar", "Tensão Superficial", "Emulsões e Sistemas Emulsionados",
            "Cristalização e Recristalização", "Estados da Matéria Aplicados", "Análise Térmica",
            "Cinética de Degradação", "Prazo de Validade e Shelf Life", "Fatores Físico-Químicos na Formulação"
        }), BuildFromTerms("Físico-Química", new QDef[0], new[] {
            ("Henderson-Hasselbalch", "Equação que relaciona pH, pKa e razão forma não ionizada/ionizada"),
            ("Polimorfismo", "Diferentes arranjos cristalinos de mesma molécula com propriedades distintas"),
            ("Cinética de primeira ordem", "Velocidade proporcional à concentração do reagente"),
            ("Cinética de ordem zero", "Velocidade constante independente da concentração"),
            ("Log P", "Coeficiente de partição octanol/água que prediz lipofilicidade"),
            ("Coloide", "Dispersão com partículas entre 1 nm e 1 µm"),
            ("Potencial zeta", "Carga elétrica na superfície de partícula coloidal"),
            ("Isotonia", "Pressão osmótica igual à dos fluidos corporais (0,9% NaCl equivalente)"),
            ("Equação de Arrhenius", "Relaciona constante de velocidade com temperatura e energia de ativação"),
            ("Solubilização micelar", "Solubilização de substância lipofílica em núcleo de micela tensoativa")
        }));

        data[18] = (BuildDocs("Microbiologia Clínica", new[] {
            "Introdução à Microbiologia Clínica", "Coloração de Gram", "Características de Bactérias Gram-Positivas",
            "Características de Bactérias Gram-Negativas", "Staphylococcus aureus e MRSA", "Streptococcus",
            "Enterobactérias", "Pseudomonas aeruginosa", "Hemocultura e Diagnóstico de Bacteremia",
            "Urocultura e Infecções Urinárias", "Antibiograma e Teste de Sensibilidade",
            "Coloração de Ziehl-Neelsen", "Mycobacterium tuberculosis", "IRAS - Infecções Relacionadas à Assistência",
            "Esterilização e Desinfecção", "Meios de Cultura Seletivos", "Virologia Clínica e Diagnóstico Molecular",
            "PCR em Microbiologia", "Biossegurança Laboratorial", "Controle de Qualidade em Microbiologia"
        }), BuildFromTerms("Microbiologia Clínica", new QDef[0], new[] {
            ("Coloração de Gram", "Diferencia bactérias em Gram-positivas (roxas) e Gram-negativas (rosas)"),
            ("MRSA", "Staphylococcus aureus resistente à meticilina por gene mecA"),
            ("Hemocultura", "Cultura de sangue para diagnóstico de bacteremia e sepse"),
            ("Antibiograma", "Teste de sensibilidade a antimicrobianos por disco-difusão ou MIC"),
            ("Ziehl-Neelsen", "Coloração para bacilos álcool-ácido resistentes como Mycobacterium"),
            ("IRAS", "Infecções Relacionadas à Assistência à Saúde adquiridas no ambiente hospitalar"),
            ("Esterilização", "Eliminação completa de microrganismos viáveis por calor, radiação ou químicos"),
            ("Meio seletivo", "Meio de cultura que favorece crescimento de microrganismos específicos"),
            ("Virologia clínica", "Diagnóstico de infecções virais por PCR, sorologia ou cultura"),
            ("Biossegurança laboratorial", "Práticas e equipamentos para proteção em laboratório microbiológico")
        }));

        data[19] = (BuildDocs("Parasitologia", new[] {
            "Introdução à Parasitologia", "Protozoários Intestinais", "Giardia lamblia", "Entamoeba histolytica",
            "Plasmodium e Malária", "Toxoplasma gondii", "Trypanosoma cruzi e Doença de Chagas",
            "Leishmania e Leishmanioses", "Helmintos Intestinais", "Ascaris lumbricoides",
            "Ancilostomídeos", "Enterobius vermicularis", "Taenia solium e Taenia saginata",
            "Schistosoma mansoni", "Métodos de Diagnóstico Parasitológico", "Exame Parasitológico de Fezes",
            "Técnicas de Concentração", "Diagnóstico Sorológico", "Hemoparasitas", "Controle e Profilaxia"
        }), BuildFromTerms("Parasitologia", new QDef[0], new[] {
            ("Anopheles", "Mosquito vetor da malária, transmite Plasmodium"),
            ("Triatoma", "Barbeiro, inseto vetor da doença de Chagas (Trypanosoma cruzi)"),
            ("Schistosoma mansoni", "Trematódeo causador da esquistossomose intestinal"),
            ("Método de Lutz", "Sedimentação espontânea para diagnóstico de helmintos nas fezes"),
            ("Giardia lamblia", "Protozoário flagelado que causa giardíase com diarreia"),
            ("Entamoeba histolytica", "Protozoário causador de amebíase intestinal e extraintestinal"),
            ("Plasmodium", "Protozoário causador da malária, transmitido por Anopheles"),
            ("Praziquantel", "Anti-helmíntico para esquistossomose, teníase e cisticercose"),
            ("Albendazol", "Anti-helmíntico de amplo espectro para nematelmintos")
        }));

        data[20] = (BuildDocs("Imunologia", new[] {
            "Introdução ao Sistema Imunológico", "Imunidade Inata e Adaptativa", "Antígenos e Anticorpos",
            "Estrutura de Imunoglobulinas", "Classes de Imunoglobulinas (IgG, IgM, IgA, IgE, IgD)",
            "Resposta Imune Humoral", "Resposta Imune Celular", "Células T e Células B",
            "Complexo Principal de Histocompatibilidade (MHC)", "Hipersensibilidade Tipo I (Alérgica)",
            "Hipersensibilidade Tipo II (Citotóxica)", "Hipersensibilidade Tipo III (Imunocomplexos)",
            "Hipersensibilidade Tipo IV (Tardia)", "Autoimunidade", "Imunodeficiências",
            "Vacinas e Imunização", "Imunologia de Transplantes", "Diagnóstico Sorológico",
            "ELISA e Testes Imunoenzimáticos", "Imunofluorescência"
        }), BuildFromTerms("Imunologia", new QDef[0], new[] {
            ("IgE", "Imunoglobulina envolvida em hipersensibilidade tipo I e alergia"),
            ("MHC classe I", "Complexo de histocompatibilidade que apresenta antígenos para LT CD8+"),
            ("Hipersensibilidade tipo I", "Reação alérgica imediata mediada por IgE e mastócitos"),
            ("BCG", "Vacina viva atenuada contra tuberculose com Bacillus Calmette-Guérin"),
            ("ELISA", "Ensaio imunoenzimático para detectar antígenos ou anticorpos"),
            ("Sistema complemento", "Cascata de proteínas que auxilia eliminação de patógenos"),
            ("Linfócito T CD4", "Linfócito auxiliar que coordena resposta imune adaptativa"),
            ("Linfócito B", "Célula que produz anticorpos na resposta imune humoral"),
            ("Autoimunidade", "Resposta imune contra antígenos próprios do organismo"),
            ("Citocina", "Proteína sinalizadora que regula resposta imune e inflamação")
        }));

        data[21] = (BuildDocs("Hematologia", new[] {
            "Introdução à Hematologia", "Eritropoese e Série Vermelha", "Anemias: Classificação e Diagnóstico",
            "Anemia Ferropriva", "Anemia Megaloblástica", "Anemia Hemolítica", "Anemia Falciforme",
            "Leucopoese e Série Branca", "Leucemias", "Linfomas", "Hemostasia Primária",
            "Hemostasia Secundária", "Coagulação Sanguínea", "Tempo de Protrombina (TP/INR)",
            "Tempo de Tromboplastina Parcial Ativada (TTPA)", "Plaquetas e Trombocitopenias",
            "Hemograma Completo", "Reticulocitos", "VHS e Proteína C Reativa", "Eletroforese de Hemoglobina"
        }), BuildFromTerms("Hematologia", new QDef[0], new[] {
            ("Anemia ferropriva", "Anemia microcítica hipocrômica por deficiência de ferro"),
            ("VCM", "Volume Corpuscular Médio classifica anemias em micro, normo ou macrocítica"),
            ("Desvio à esquerda", "Aumento de neutrófilos jovens (bastões) indicando infecção aguda"),
            ("Hemofilia A", "Coagulopatia hereditária por deficiência do fator VIII"),
            ("Prova de Coombs direta", "Detecta anticorpos ou complemento aderidos a hemácias in vivo"),
            ("Leucemia", "Neoplasia hematológica com proliferação clonal de leucócitos"),
            ("Plaquetopenia", "Redução de plaquetas abaixo de 150.000/mm³"),
            ("Anemia megaloblástica", "Anemia macrocítica por deficiência de B12 ou folato"),
            ("CIVD", "Coagulação Intravascular Disseminada com coagulação e fibrinólise simultâneas"),
            ("Hemoglobinopatia", "Alteração estrutural ou síntese de hemoglobina como falciforme e talassemia")
        }));

        data[22] = (BuildDocs("Fisiologia Humana", new[] {
            "Introdução à Fisiologia", "Fisiologia Cardiovascular", "Fisiologia Respiratória",
            "Fisiologia Renal", "Fisiologia Gastrintestinal", "Fisiologia do Sistema Nervoso",
            "Fisiologia Endócrina", "Fisiologia Muscular", "Fisiologia do Exercício",
            "Homeostase e Controle Homeostático", "Equilíbrio Ácido-Base", "Equilíbrio Hidro-eletrolítico",
            "Transporte Através de Membranas", "Potencial de Ação", "Sinapses e Neurotransmissão",
            "Ciclo Cardíaco", "Pressão Arterial e Regulação", "Ventilação Pulmonar",
            "Troca Gasosa e Transporte de Gases", "Filtracão Glomerular"
        }), BuildFromTerms("Fisiologia Humana", new QDef[0], new[] {
            ("Débito cardíaco", "Volume de sangue bombeado pelo coração por minuto (FC × VS)"),
            ("TFG", "Taxa de Filtração Glomerular, medida de função renal (~125 mL/min)"),
            ("Surfactante pulmonar", "Fosfolipídio produzido por pneumócitos II que reduz tensão superficial"),
            ("Insulina", "Hormônio pancreático produzido por células beta que reduz glicemia"),
            ("pH sanguíneo", "Faixa normal 7,35-7,45 mantida por tampões, pulmões e rins"),
            ("Potencial de ação", "Despolarização rápida da membrana neuronal por influxo de Na+"),
            ("Neurotransmissor", "Molécula sinalizadora liberada na sinapse (dopamina, serotonina, GABA)"),
            ("Homeostase", "Manutenção do equilíbrio do meio interno"),
            ("Aldosterona", "Hormônio mineralocorticoide que retém sódio e excreta potássio")
        }));

        data[23] = (BuildDocs("Anatomia e Histologia", new[] {
            "Introdução à Anatomia Humana", "Planos e Eixos Anatômicos", "Sistema Esquelético",
            "Sistema Muscular", "Sistema Cardiovascular", "Sistema Respiratório", "Sistema Digestório",
            "Sistema Urinário", "Sistema Nervoso Central", "Sistema Nervoso Periférico",
            "Sistema Endócrino", "Histologia: Tecido Epitelial", "Histologia: Tecido Conjuntivo",
            "Histologia: Tecido Muscular", "Histologia: Tecido Nervoso", "Histologia: Sistema Circulatório",
            "Histologia: Sistema Respiratório", "Histologia: Sistema Digestório", "Histologia: Sistema Urinário",
            "Histologia: Sistema Reprodutor"
        }), BuildFromTerms("Anatomia e Histologia", new QDef[0], new[] {
            ("Néfron", "Unidade funcional do rim composta por glomérulo e túbulos"),
            ("Epitélio pseudoestratificado ciliado", "Reveste traqueia e brônquios, com cílios que movem muco"),
            ("Hepatócito", "Célula parenquimatosa do fígado com funções metabólicas"),
            ("Tecido muscular cardíaco", "Músculo estriado involuntário com discos intercalares"),
            ("Peritônio", "Membrana serosa que reveste cavidade abdominal"),
            ("Tecido epitelial", "Tecido de revestimento e glândulas com células justapostas"),
            ("Tecido conjuntivo", "Tecido de sustentação e preenchimento com matriz extracelular"),
            ("Tecido nervoso", "Neurônios e células da glia no SNC e SNP"),
            ("Histologia renal", "Córtex com glomérulos e medula com alças de Henle")
        }));

        data[24] = (BuildDocs("Patologia Geral", new[] {
            "Introdução à Patologia", "Lesão e Adaptação Celular", "Necrose e Apoptose",
            "Inflamação Aguda", "Inflamação Crônica", "Reparo Tecidual e Cicatrização",
            "Distúrbios da Circulação", "Trombose e Embolia", "Isquemia e Infarto",
            "Neoplasias Benignas e Malignas", "Carcinogênese", "Mecanismos de Invasão e Metástase",
            "Imunopatologia", "Doenças Infecciosas", "Doenças Genéticas", "Distúrbios do Crescimento Celular",
            "Degenerações", "Calcificações Patológicas", "Pigmentações Anormais", "Cicatrização e Queloides"
        }), BuildFromTerms("Patologia Geral", new QDef[0], new[] {
            ("Necrose coagulativa", "Morte celular com preservação da arquitetura, comum em isquemia"),
            ("Apoptose", "Morte celular programada com fragmentação nuclear e corpos apoptóticos"),
            ("Metaplasia", "Substituição reversível de um tipo celular diferenciado por outro"),
            ("Inflamação aguda", "Resposta vascular e celular rápida com neutrófilos"),
            ("Trombose", "Formação de coágulo no interior de vaso sanguíneo"),
            ("Neoplasia maligna", "Tumor com invasão, metástase e perda de diferenciação"),
            ("Oncogene", "Gene mutado que promove proliferação celular descontrolada (ex: RAS)"),
            ("Gene supressor", "Gene que inibe crescimento celular quando ativo (ex: p53)"),
            ("Estadiamento TNM", "Classificação de câncer por tamanho do tumor, linfonodos e metástases"),
            ("Metástase", "Disseminação de células tumorais a órgãos distantes")
        }));

        data[25] = (BuildDocs("Genética e Biologia Molecular", new[] {
            "Introdução à Genética", "Estrutura do DNA e RNA", "Replicação do DNA",
            "Transcrição", "Tradução e Síntese Proteica", "Código Genético", "Mutações Genéticas",
            "Herança Mendeliana", "Herança Ligada ao Sexo", "Doenças Genéticas Autossômicas",
            "Farmacogenética", "Polimorfismos Genéticos", "PCR e Amplificação de DNA",
            "Sequenciamento de DNA", "Eletroforese de DNA", "Northern, Southern e Western Blot",
            "CRISPR e Edição Gênica", "Terapia Gênica", "Diagnóstico Molecular", "Biobancos e Ética em Genética"
        }), BuildFromTerms("Genética e Biologia Molecular", new QDef[0], new[] {
            ("PCR", "Reação em Cadeia da Polimerase amplifica DNA usando primers e Taq polimerase"),
            ("Farmacogenômica", "Estudo de como variações genéticas afetam resposta a fármacos"),
            ("CRISPR-Cas9", "Ferramenta de edição gênica que corta DNA em local específico"),
            ("Síndrome de Klinefelter", "Cariótipo 47,XXY com hipogonadismo e ginecomastia"),
            ("Transcrição", "Síntese de RNA a partir de molde de DNA pela RNA polimerase"),
            ("Tradução", "Síntese de proteína a partir de mRNA nos ribossomos"),
            ("Mutação gênica", "Alteração permanente na sequência de DNA"),
            ("Herança ligada ao X", "Padrão de herança de genes no cromossomo X (hemofilia, daltonismo)"),
            ("Epigenética", "Modificações hereditárias na expressão gênica sem alterar DNA"),
            ("Sequenciamento NGS", "Sequenciamento de nova geração de alto rendimento paralelo")
        }));

        data[26] = (BuildDocs("Botânica Farmacêutica", new[] {
            "Introdução à Botânica Farmacêutica", "Morfologia Vegetal", "Anatomia Vegetal",
            "Classificação Taxonômica de Plantas Medicinais", "Metabólitos Secundários", "Alcaloides",
            "Flavonoides", "Terpenoides", "Glicosídeos Cardíacos", "Óleos Essenciais",
            "Taninos", "Saponinas", "Coleta e Secagem de Plantas", "Identificação Botânica",
            "Controle de Qualidade de Drogas Vegetais", "Análise Macroscópica e Microscópica",
            "Ensaios Físico-Químicos em Fitoterápicos", "Extracão de Princípios Ativos",
            "Padronização de Extratos Vegetais", "Legislação de Fitoterápicos"
        }), BuildFromTerms("Botânica Farmacêutica", new QDef[0], new[] {
            ("Fotossíntese", "Processo cloroplástico que converte CO2 e H2O em glicose usando luz"),
            ("Nomenclatura binomial", "Sistema de Lineu com gênero e espécie (Plantago major)"),
            ("Lamiaceae", "Família com folhas aromáticas (hortelã, alecrim, manjericão)"),
            ("Asteraceae", "Família com capítulo floral (camomila, arnica, calêndula)"),
            ("Droga vegetal", "Parte da planta com princípios ativos (folha, casca, raiz)"),
            ("Microscopia vegetal", "Identifica estruturas anatômicas para autenticidade de droga"),
            ("Herbário", "Coleção de plantas desidratadas e prensadas para referência taxonômica"),
            ("Etnobotânica", "Estudo do uso tradicional de plantas por populações"),
            ("Morfologia floral", "Estudo de cálice, corola, estames e pistilos"),
            ("Secagem de plantas", "Remove umidade preservando princípios ativos")
        }));

        data[27] = (BuildDocs("Epidemiologia", new[] {
            "Introdução à Epidemiologia", "História Natural da Doença", "Níveis de Prevenção",
            "Medidas de Frequência: Incidência e Prevalência", "Medidas de Associação: Risco Relativo e Odds Ratio",
            "Estudos Epidemiológicos: Desenhos", "Estudos Transversais", "Estudos de Coorte",
            "Estudos Caso-Controle", "Ensaios Clínicos Randomizados", "Revisões Sistemáticas e Meta-análises",
            "Vieses em Estudos Epidemiológicos", "Fatores de Confusão", "Causalidade em Epidemiologia",
            "Critérios de Bradford Hill", "Vigilância Epidemiológica", "Investigação de Surtos",
            "Transição Epidemiológica", "Doenças Transmissíveis", "Doenças Crônicas Não Transmissíveis"
        }), BuildFromTerms("Epidemiologia", new QDef[0], new[] {
            ("Prevalência", "Proporção de casos existentes em população em determinado momento"),
            ("Incidência", "Taxa de novos casos em população em risco em período definido"),
            ("Risco relativo", "Usado em coorte, razão de incidência entre expostos e não expostos"),
            ("Vigilância epidemiológica", "Coleta e análise de dados para detectar surtos e controlar doenças"),
            ("Imunidade de rebanho", "Proteção indireta quando alta proporção de população é imune"),
            ("SINAN", "Sistema de Informação de Agravos de Notificação do Ministério da Saúde"),
            ("Notificação compulsória", "Doenças de notificação obrigatória às autoridades sanitárias"),
            ("Curva epidêmica", "Gráfico de casos ao longo do tempo para caracterizar surto"),
            ("Estudo transversal", "Mede prevalência em população em momento único"),
            ("Viés epidemiológico", "Erro sistemático que distorce associação entre exposição e desfecho")
        }));

        data[28] = (BuildDocs("Saúde Pública e SUS", new[] {
            "Introdução à Saúde Pública", "História do Sistema Único de Saúde (SUS)", "Princípios do SUS",
            "Diretrizes do SUS", "Atencão Primária à Saúde", "Estratégia Saúde da Família",
            "Assistência Farmacêutica no SUS", "Política Nacional de Medicamentos", "RENAME",
            "Política Nacional de Assistência Farmacêutica", "Vigilância Sanitária", "Vigilância Epidemiológica",
            "Programa Nacional de Imunizações", "Controle de Endemias", "Saúde do Trabalhador",
            "Saúde Mental no SUS", "Humanização da Assistência", "Participação Social no SUS",
            "Financiamento do SUS", "Judicialização da Saúde"
        }), BuildFromTerms("Saúde Pública e SUS", new QDef[0], new[] {
            ("Universalidade", "Princípio do SUS que garante saúde como direito de todos"),
            ("Integralidade", "Princípio do SUS de atenção completa preventiva e curativa"),
            ("Equidade", "Princípio do SUS de tratar desigualmente os desiguais"),
            ("RENAME", "Relação Nacional de Medicamentos Essenciais do SUS"),
            ("ESF", "Estratégia Saúde da Família prioriza atenção primária comunitária"),
            ("ANVISA", "Agência Nacional de Vigilância Sanitária regulamenta medicamentos e alimentos"),
            ("Programa Farmácia Popular", "Fornece medicamentos com subsídio governamental em farmácias conveniadas"),
            ("Determinantes sociais", "Condições socioeconômicas que influenciam saúde além de fatores biológicos"),
            ("Promoção da saúde", "Ações que aumentam controle da população sobre sua saúde"),
            ("Financiamento tripartite", "SUS financiado por União, Estados e Municípios")
        }));
    }
}
