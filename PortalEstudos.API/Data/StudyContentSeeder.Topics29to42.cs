namespace PortalEstudos.API.Data;

public static partial class StudyContentSeeder
{
    static partial void GenerateTopics29to42(Dictionary<int, (DocDef[], QDef[])> data)
    {
        // Topic 29: Gestão Farmacêutica
        data[29] = (BuildDocs("Gestão Farmacêutica", new[] {
            "Introdução à Gestão Farmacêutica", "Administração de Farmácias e Drogarias", "Curva ABC na Gestão de Estoques",
            "Consumo Médio Mensal (CMM)", "Lead Time e Ponto de Pedido", "Estoque de Segurança",
            "Sistema PEPS", "Custo da Mercadoria Vendida (CMV)", "Inventário Rotativo",
            "Indicadores de Desempenho (KPIs)", "Fluxo de Caixa", "Preço de Venda e Markup",
            "Legislação Sanitária em Farmácias", "Responsabilidade Técnica", "Escrituração Farmacêutica",
            "Marketing Farmacêutico", "Gestão de Pessoas", "Controle de Medicamentos Controlados",
            "Boas Práticas Farmacêuticas", "Plano de Negócios"
        }), BuildFromTerms("Gestão Farmacêutica", new QDef[0], new[] {
            ("Curva ABC", "Classificação de itens em A (alto valor), B (médio), C (baixo) para priorização"),
            ("CMM", "Consumo Médio Mensal, usado para calcular estoque"),
            ("Lead time", "Tempo entre pedido e recebimento de produtos"),
            ("Ponto de pedido", "Nível de estoque que dispara nova compra considerando consumo e lead time"),
            ("Estoque de segurança", "Reserva para prevenir falta por variação de demanda ou atraso de entrega"),
            ("PEPS", "Primeiro que Entra, Primeiro que Sai, rotação por data de validade"),
            ("CMV", "Custo da Mercadoria Vendida, despesa com produtos comercializados"),
            ("Inventário rotativo", "Contagem periódica contínua de itens sem parar operação"),
            ("Tempo de reposição", "Intervalo para reabastecer item em falta"),
            ("Indicador de ruptura", "Métrica de falta de produto disponível para cliente")
        }));

        data[30] = (BuildDocs("Oncologia Farmacêutica", new[] {
            "Introdução à Oncologia Farmacêutica", "Bases da Quimioterapia", "Agentes Alquilantes",
            "Antimetabólitos", "Inibidores de Microtúbulos", "Agentes Hormonais", "Anticorpos Monoclonais",
            "Inibidores de Tirosina Quinase", "Imunoterapia Oncológica", "Efeitos Colaterais da Quimioterapia",
            "Neutropenia Febril", "Mucosite", "Nadir Hematológico", "Extravasamento de Quimioterápicos",
            "Manipulação Segura de HMD", "Reconstituição de Quimioterápicos", "Cuidados Farmacêuticos em Oncologia",
            "Suporte Nutricional em Oncologia", "Controle de Náuseas e Vômitos", "Cuidados Paliativos"
        }), BuildFromTerms("Oncologia Farmacêutica", new QDef[0], new[] {
            ("Metotrexato", "Antimetabólito que inibe di-hidrofolato redutase na síntese de DNA"),
            ("Ciclofosfamida", "Agente alquilante que causa crosslink em DNA"),
            ("Vincristina", "Alcaloide da vinca que inibe microtúbulos e divisão celular"),
            ("Trastuzumabe", "Anticorpo monoclonal anti-HER2 para câncer de mama HER2+"),
            ("Imunoterapia oncológica", "Terapia que estimula sistema imune contra células tumorais"),
            ("Neutropenia febril", "Emergência oncológica com neutrófilos baixos e febre"),
            ("Mucosite", "Inflamação de mucosa oral/GI, efeito colateral comum de quimioterapia"),
            ("Nadir", "Ponto de menor contagem de células sanguíneas após quimioterapia"),
            ("Extravasamento", "Vazamento de quimioterápico vesicante causando necrose tecidual"),
            ("Cuidados com HMD", "Manipulação segura de Medicamentos Perigosos com EPI e capela de fluxo")
        }));

        data[31] = (BuildDocs("Farmacologia Cardiovascular", new[] {
            "Introdução à Farmacologia Cardiovascular", "Inibidores da ECA (IECA)", "Bloqueadores do Receptor de Angiotensina (BRA)",
            "Betabloqueadores", "Bloqueadores dos Canais de Cálcio", "Diuréticos: Tiazídicos e Alaça",
            "Diuréticos Poupadores de Potássio", "Estatinas e Hipolipemiantes", "Fibratos",
            "Anticoagulantes: Varfarina", "Anticoagulantes Orais Diretos (DOACs)", "Antiplaquetaários: AAS e Clopidogrel",
            "Nitratos e Vasodilatadores", "Glicosídeos Cardíacos", "Inotrópicos",
            "Antiarrítmicos", "Farmacoterapia da Hipertensão", "Farmacoterapia da Insuficiência Cardíaca",
            "Farmacoterapia da Angina", "Farmacoterapia do Infarto Agudo do Miocárdio"
        }), BuildFromTerms("Farmacologia Cardiovascular", new QDef[0], new[] {
            ("IECA", "Inibidores da ECA reduzem angiotensina II, vasodilatação e proteção renal"),
            ("BRA", "Bloqueadores do receptor AT1 de angiotensina II"),
            ("Beta-bloqueador", "Antagonista beta-adrenérgico reduz FC, PA e mortalidade pós-IAM"),
            ("Diurético tiazídico", "Inibe reabsorção de Na+ no túbulo distal, anti-hipertensivo"),
            ("Espironolactona", "Diurético poupador de potássio, antagonista de aldosterona"),
            ("Estatina", "Inibidor de HMG-CoA redutase reduz colesterol LDL"),
            ("Varfarina", "Anticoagulante que inibe vitamina K epóxido redutase"),
            ("AAS", "Antiplaquetário que inibe COX-1 e agregação plaquetária"),
            ("Nitroglicerina", "Vasodilatador que libera óxido nítrico, alivia angina"),
            ("Digoxina", "Glicosídeo cardíaco inotrópico positivo, inibe bomba Na+/K+")
        }));

        data[32] = (BuildDocs("Farmacologia do SNC", new[] {
            "Introdução à Farmacologia do SNC", "Antidepressivos: ISRS", "Antidepressivos: Tricíclicos",
            "Antidepressivos: IMAO", "Antipsicóticos Típicos", "Antipsicóticos Atípicos",
            "Benzodiazepínicos", "Ansiolíticos não Benzodiazepínicos", "Estabilizadores de Humor",
            "Anticonvulsivantes", "Farmacologia da Doença de Parkinson", "Analgésicos Opioides",
            "Antagonistas Opioides", "Estimulantes do SNC", "Farmacologia do Sono",
            "Farmacoterapia da Depressão", "Farmacoterapia da Esquizofrenia", "Farmacoterapia do Transtorno Bipolar",
            "Farmacoterapia da Ansiedade", "Farmacoterapia da Epilepsia"
        }), BuildFromTerms("Farmacologia do SNC", new QDef[0], new[] {
            ("Fluoxetina", "ISRS inibidor seletivo de recaptação de serotonina, antidepressivo"),
            ("Amitriptilina", "Antidepressivo tricíclico inibe recaptação de serotonina e noradrenalina"),
            ("Haloperidol", "Antipsicótico típico antagonista D2 para esquizofrenia"),
            ("Clozapina", "Antipsicótico atípico, eficaz em esquizofrenia refratária, risco de agranulocitose"),
            ("Benzodiazepínico", "Ansiolítico que potencializa GABA-A, risco de dependência"),
            ("Levodopa", "Precursor de dopamina para tratamento de Parkinson"),
            ("Fenitoína", "Anticonvulsivante que bloqueia canais de sódio voltagem-dependentes"),
            ("Lítio", "Estabilizador de humor para transtorno bipolar, janela terapêutica estreita"),
            ("Naloxona", "Antagonista opioide para reversão de overdose"),
            ("IMAO", "Inibidor de monoamina oxidase, antidepressivo, risco de crise hipertensiva")
        }));

        data[33] = (BuildDocs("Dermatologia Farmacêutica", new[] {
            "Introdução à Dermatologia Farmacêutica", "Tratamento Farmacológico da Acne", "Retinóides Tópicos",
            "Corticosteroides Tópicos", "Antifúngicos Tópicos", "Antibióticos Tópicos",
            "Tratamento da Alopecia", "Fotoprotetores e Filtros Solares", "Despigmentantes",
            "Tratamento de Psoríase", "Tratamento de Dermatite Atópica", "Sistemas Transdermos",
            "Emolientes e Hidratantes", "Tratamento de Verrugas", "Queratolíticas",
            "Antissépticos e Desinfetantes", "Medicamentos para Queimaduras", "Cosméticos Dermofarmacêuticos",
            "Preparações Dermatológicas Magistrais", "Fotoquimioterapia"
        }), BuildFromTerms("Dermatologia Farmacêutica", new QDef[0], new[] {
            ("Tretinoína", "Retinoide tópico para acne, normaliza descamação folicular"),
            ("Peróxido de benzoíla", "Antimicrobiano oxidante para acne vulgaris"),
            ("Corticosteroide tópico", "Anti-inflamatório para dermatoses, classificado por potência"),
            ("Minoxidil", "Vasodilatador tópico para alopecia androgenética"),
            ("Imiquimode", "Imunomodulador tópico para verrugas e queratose actínica"),
            ("Antifúngico azólico", "Cetoconazol, miconazol inibem síntese de ergosterol fúngico"),
            ("Fotoprotetor", "Filtro UV previne danos actínicos e câncer de pele"),
            ("Hidroquinona", "Despigmentante inibe tirosinase, trata melasma"),
            ("Sistema transdérmico", "Adesivo que libera fármaco através da pele de forma controlada"),
            ("Emoliente", "Hidratante que amacia e retém água na pele")
        }));

        data[34] = (BuildDocs("Farmacologia Anti-infecciosa", new[] {
            "Introdução à Farmacologia Anti-infecciosa", "Penicilinas", "Cefalosporinas",
            "Carbapenêmicos e Monobactamas", "Aminoglicosídeos", "Macrolídeos",
            "Fluorquinolonas", "Glicopeptídeos: Vancomicina e Teicoplanina", "Sulfonamidas e Trimetoprima",
            "Rifampicina e Tratamento da Tuberculose", "Antivirais: Inibidores de DNA Polimerase",
            "Antivirais para HIV", "Antivirais para Influenza", "Antifúngicos: Azólicos",
            "Antifúngicos: Polienícicos", "Antifúngicos: Equinocandinas", "Antiparasitários",
            "Resistência Antimicrobiana", "Uso Racional de Antimicrobianos", "Profilaxia Antimicrobiana"
        }), BuildFromTerms("Farmacologia Anti-infecciosa", new QDef[0], new[] {
            ("Penicilina", "Beta-lactâmico que inibe síntese de parede celular bacteriana"),
            ("Cefalosporina", "Beta-lactâmico resistente a algumas beta-lactamases"),
            ("Vancomicina", "Glicopeptídeo para Gram-positivos resistentes, inibe síntese de parede"),
            ("Aminoglicosídeo", "Gentamicina, amicacina inibem síntese proteica 30S, nefro e ototóxico"),
            ("Fluorquinolona", "Ciprofloxacino inibe DNA girase bacteriana"),
            ("Rifampicina", "Inibidor de RNA polimerase, primeira linha TB"),
            ("Aciclovir", "Antiviral inibe DNA polimerase de herpesvírus"),
            ("Oseltamivir", "Inibidor de neuraminidase para influenza"),
            ("Anfotericina B", "Antifúngico poliênico liga-se a ergosterol, nefrotóxico"),
            ("Resistência antimicrobiana", "Capacidade de microrganismos sobreviverem a antimicrobianos")
        }));

        data[35] = (BuildDocs("Farmacologia Endócrina", new[] {
            "Introdução à Farmacologia Endócrina", "Insulinas", "Antidiabéticos Orais: Metformina",
            "Sulfonilureias e Glinidas", "Inibidores de DPP-4", "Agonistas de GLP-1",
            "Inibidores de SGLT2", "Hormônios Tireoidianos", "Antitireoidianos",
            "Glicocorticoides", "Mineralocorticoides", "Anticoncepcionais Hormonais",
            "Terapia de Reposição Hormonal", "Andrógenos e Esteroides Anabólicos",
            "Moduladores Seletivos de Receptor Estrogênico", "Inibidores de Aromatase",
            "Tratamento da Osteoporose", "Hormônio do Crescimento", "Ocitocina e Ergotínicos", "Prolactina e Agonistas Dopaminérgicos"
        }), BuildFromTerms("Farmacologia Endócrina", new QDef[0], new[] {
            ("Metformina", "Biguanida anti-hiperglicemiante que reduz gliconeogênese hepática"),
            ("Glibenclamida", "Sulfonilureia secretagogo de insulina em DM2"),
            ("Insulina NPH", "Insulina de ação intermediária com pico em 4-8h"),
            ("Levotiroxina", "T4 sintético para hipotireoidismo"),
            ("Propiltiouracil", "Tionamida que inibe síntese de hormônios tireoidianos"),
            ("Prednisona", "Glicocorticoide com efeito anti-inflamatório e imunossupressor"),
            ("Anticoncepcional oral combinado", "Estrogênio + progestina suprime ovulação"),
            ("Testosterona", "Andrógeno para reposição hormonal em hipogonadismo"),
            ("Tamoxifeno", "Modulador seletivo de receptor estrogênico para câncer de mama"),
            ("Semaglutida", "Agonista de GLP-1 para DM2 e perda de peso")
        }));

        data[36] = (BuildDocs("Farmacologia Respiratória", new[] {
            "Introdução à Farmacologia Respiratória", "Beta-2 Agonistas de Curta Ação", "Beta-2 Agonistas de Longa Ação",
            "Anticolinérgicos Inalaríveis", "Corticosteroides Inalatórios", "Antagonistas de Leucotrienos",
            "Terapia Biológica na Asma", "Teofilinas", "Mucolíticos e Expectorantes",
            "Antitussígenos", "Descongestionantes Nasais", "Dispositivos Inalatórios",
            "Técnica Inalatória", "Espaçadores", "Farmacoterapia da Asma",
            "Farmacoterapia da DPOC", "Exacerbações Respiratórias", "Tratamento da Rinite Alérgica",
            "Tratamento de Infecções Respiratórias", "Oxigenoterapia e Suporte Respiratório"
        }), BuildFromTerms("Farmacologia Respiratória", new QDef[0], new[] {
            ("Salbutamol", "Beta-2 agonista de curta ação broncodilatador para asma"),
            ("Formoterol", "Beta-2 agonista de longa ação, manutenção de asma"),
            ("Ipratrópio", "Anticolinérgico broncodilatador inibe receptores muscarínicos"),
            ("Corticoide inalatório", "Beclometasona, budesonida controlam inflamação em asma"),
            ("Montelucaste", "Antagonista de receptor de leucotrienos, controle de asma"),
            ("Omalizumabe", "Anti-IgE monoclonal para asma alérgica grave"),
            ("Acetilcisteína", "Mucolítico que rompe pontes dissulfeto de muco"),
            ("Dispositivo inalatório", "Nebulizador, inalador pressurizado, pó seco liberam fármaco nos pulmões"),
            ("Técnica inalatória", "Coordenação de disparo e inspiração profunda para eficácia"),
            ("Espaçador", "Câmara de expansão que facilita uso de aerossol e reduz deposição orofaríngea")
        }));

        data[37] = (BuildDocs("Toxicologia", new[] {
            "Introdução à Toxicologia", "DL50 e Margem de Segurança", "Intoxicação por Paracetamol",
            "Intoxicação por Salicilatos", "Síndrome Colinérgica", "Síndrome Anticolinérgica",
            "Intoxicação por Opioides", "Intoxicação por Benzodiazepínicos", "Intoxicação Alcoólica",
            "Intoxicação por Metais Pesados", "Quelação", "Intoxicação por Inseticidas Organofosforados",
            "Intoxicação por Raticidas", "Mordidas de Serpentes e Soros Antiofídicos",
            "Intoxicação por Plantas Tóxicas", "Antídotos e Antagonistas", "Lavagem Gástrica e Carvão Ativado",
            "Toxicologia Ocupacional", "Toxicologia Ambiental", "Toxicovigilância"
        }), BuildFromTerms("Toxicologia", new QDef[0], new[] {
            ("DL50", "Dose letal mediana que mata 50% dos animais testados"),
            ("Margem de segurança", "Diferença entre dose terapêutica e dose tóxica"),
            ("Intoxicação por paracetamol", "Overdose causa hepatotoxicidade, antídoto é N-acetilcisteína"),
            ("Síndrome colinérgica", "Intoxicação por inibidores de colinesterase com miose, sialorreia, bradicardia"),
            ("Quelação", "Ligação de metal pesado a agente quelante para eliminação (EDTA, DMSA)"),
            ("Carcinógeno", "Substância que induz câncer por mutação de DNA"),
            ("Teratógeno", "Substância que causa malformação em feto (talidomida, isotretinoína)"),
            ("Antídoto", "Substância que reverte efeito de toxicante (atropina, flumazenil)"),
            ("Período de carência", "Tempo após aplicação de agrotóxico para consumo seguro"),
            ("Chumbo", "Metal tóxico que causa saturnismo com anemia e neuropatia")
        }));

        data[38] = (BuildDocs("Plantas Medicinais e Fitoterapia", new[] {
            "Introdução à Fitoterapia", "Plantas Medicinais da Flora Brasileira", "Metabólitos Secundários",
            "Alcaloides de Plantas Medicinais", "Flavonoides e Atividade Antioxidante", "Terpenóides",
            "Óleos Essenciais e Aromaterapia", "Saponinas", "Taninos", "Glicosídeos Cardíacos",
            "Ginkgo biloba", "Hypericum perforatum (Hiperico)", "Panax ginseng", "Valeriana officinalis",
            "Passiflora incarnata (Maracujá)", "Echinacea purpurea", "Controle de Qualidade de Fitoterápicos",
            "Interações Fármaco-Fitoterápico", "Legislação de Fitoterápicos no Brasil", "Prescrição de Fitoterápicos"
        }), BuildFromTerms("Plantas Medicinais e Fitoterapia", new QDef[0], new[] {
            ("Cascara sagrada", "Laxativo antraquinônico de Rhamnus purshiana"),
            ("Guaco", "Mikania glomerata, expectorante e broncodilatador"),
            ("Espinheira-santa", "Maytenus ilicifolia para dispepsia e gastrite"),
            ("Ginkgo biloba", "Melhora cognição e circulação, inibe PAF"),
            ("Hypericum", "Erva-de-São-João, antidepressivo inibe recaptação de monoaminas"),
            ("Valeriana", "Valeriana officinalis, sedativo e ansiolítico"),
            ("Alcachofra", "Cynara scolymus, hepatoprotetor e colerético"),
            ("Equinácea", "Echinacea purpurea, imunoestimulante"),
            ("Silimarina", "Flavonoligna de Silybum marianum, hepatoprotetor"),
            ("RENISUS", "Relação Nacional de Plantas Medicinais de Interesse ao SUS com 71 espécies")
        }));

        data[39] = (BuildDocs("Farmácia Magistral", new[] {
            "Introdução à Farmácia Magistral", "Boas Práticas de Manipulação", "Formas Farmacêuticas Sólidas",
            "Cápsulas e Compridos Manipulados", "Formas Farmacêuticas Semisólidas", "Cremes e Pomadas",
            "Géis", "Formas Farmacêuticas Líquidas", "Soluções e Suspensões", "Xaropes e Elixires",
            "Emulsões", "Incompatibilidades Farmacêuticas", "Cálculos Farmacêuticos",
            "Padronização de Concentrações", "Controle de Qualidade na Manipulação",
            "Conservantes e Antioxidantes", "Aromatizantes e Edulcorantes", "Bases e Veículos",
            "Legislação da Farmácia Magistral", "Aviação de Receitas"
        }), BuildFromTerms("Farmácia Magistral", new QDef[0], new[] {
            ("RDC 67/2007", "Regulamento técnico de boas práticas de manipulação de medicamentos"),
            ("Ordem de manipulação", "Documento que orienta etapas de preparo de fórmula"),
            ("Tara", "Massa do recipiente vazio descontada na pesagem"),
            ("Levigação", "Incorporar substância insolúvel em base com agente umedecedor"),
            ("Trituraração", "Redução de tamanho de partículas por fricção no gral"),
            ("Controle de qualidade", "Ensaios de peso, pH, viscosidade, aspecto de manipulados"),
            ("POP", "Procedimento Operacional Padrão descreve etapas de processo"),
            ("Quarentena", "Período de retenção de matéria-prima aguardando liberação"),
            ("Rastreabilidade", "Capacidade de rastrear matéria-prima desde fornecedor até produto final"),
            ("Cápsula gelatinosa", "Invólucro de gelatina para pós e líquidos, tamanhos 00 a 5")
        }));

        data[40] = (BuildDocs("Biofarmácia", new[] {
            "Introdução à Biofarmácia", "Biodisponibilidade", "Bioequivalência",
            "Fatores que Afetam a Absorção", "Efeito de Primeira Passagem", "Vias de Administração",
            "Liberação de Fármacos", "Dissolução", "Permeabilidade", "Sistema de Classificação Biofarmacêutica (SCB)",
            "Medicamentos Genéricos e Similares", "Estudos de Bioequivalência", "Cmax e Tmax",
            "AUC (Area Under Curve)", "Janela Terapêutica", "Sistemas de Liberação Controlada",
            "Sistemas de Liberação Modificada", "Nanopartículas e Drug Delivery",
            "Formulações Orodispersíveis", "Sistemas Mucoadesivos"
        }), BuildFromTerms("Biofarmácia", new QDef[0], new[] {
            ("Biodisponibilidade", "Fração de dose que atinge circulação sistêmica inalterada"),
            ("Bioequivalência", "Produtos com biodisponibilidade equivalente sem diferença clínica"),
            ("Dissolução", "Velocidade de passagem de fármaco sólido para solução"),
            ("Classificação biofarmacêutica", "SCB divide fármacos por solubilidade e permeabilidade"),
            ("Efeito de primeira passagem", "Metabolismo antes de atingir sistêmica (hepático ou intestinal)"),
            ("Absorção", "Passagem de fármaco do local de administração para corrente sanguínea"),
            ("Permeabilidade", "Capacidade de fármaco atravessar membranas biológicas"),
            ("pKa e absorção", "pH do meio e pKa determinam ionização e absorção"),
            ("Velocidade de dissolução", "Equação de Noyes-Whitney depende de área, difusão e solubilidade"),
            ("Janela de absorção", "Região do TGI onde fármaco é absorvido eficientemente")
        }));

        data[41] = (BuildDocs("Farmacometria", new[] {
            "Introdução à Farmacometria", "Farmacocinética Clínica", "Modelo Monocompartimental",
            "Modelo Bicompartimental", "Clearance e Depuração", "Volume de Distribuição",
            "Meia-vida de Eliminação", "Estado de Equilíbrio", "Dose de Ataque e Manutenção",
            "Monitorização Terapêutica de Fármacos", "Farmacocinética de População",
            "Modelagem Farmacocinética", "Software para Farmacometria", "Ajuste de Dose Individual",
            "Farmacocinética na Insuficiência Renal", "Farmacocinética na Insuficiência Hepática",
            "Farmacocinética em Pediatria", "Farmacocinética em Geriatria", "Farmacocinética em Obesos",
            "Relação PK/PD"
        }), BuildFromTerms("Farmacometria", new QDef[0], new[] {
            ("Meia-vida", "Tempo para concentração plasmática reduzir pela metade"),
            ("Clearance", "Volume de plasma depurado de fármaco por unidade de tempo"),
            ("Volume de distribuição", "Volume aparente em que fármaco se distribui no corpo"),
            ("Estado de equilíbrio", "Concentração estável quando velocidade de entrada = saída"),
            ("Dose de ataque", "Dose elevada inicial para alcançar rapidamente concentração terapêutica"),
            ("Dose de manutenção", "Dose para manter concentração no estado de equilíbrio"),
            ("AUC", "Área sob a curva concentração-tempo, representa exposição total"),
            ("Modelo monocompartimental", "Corpo tratado como compartimento único homogêneo"),
            ("Cinética linear", "Clearance constante independente da dose"),
            ("TDM", "Monitorização terapêutica de fármacos com janela estreita (vancomicina, digoxina)")
        }));

        data[42] = (BuildDocs("Micologia Clínica", new[] {
            "Introdução à Micologia Clínica", "Classificação de Fungos", "Candida spp",
            "Candidíase Oral e Vaginal", "Cryptococcus neoformans", "Aspergillus spp",
            "Dermatófitos e Dermatomicoses", "Onicomicoses", "Esporotricose",
            "Paracoccidioidomicose", "Histoplasmose", "Cromoblastomicose",
            "Diagnóstico Micológico", "Exame Micológico Direto", "Cultura de Fungos",
            "Identificação de Fungos por Morfologia", "Testes de Sensibilidade a Antifúngicos",
            "Tratamento de Infecções Fúngicas", "Micoses Superficiais", "Micoses Profundas"
        }), BuildFromTerms("Micologia Clínica", new QDef[0], new[] {
            ("Candida albicans", "Levedura comensal oportunista causa candidíase oral, vaginal, sistêmica"),
            ("Dermatófitos", "Fungos que invadem queratina causam tinhas e onicomicose"),
            ("Criptococose", "Infecção por Cryptococcus neoformans causa meningite em imunossuprimidos"),
            ("Exame micológico direto", "KOH dissolve queratina para visualizar hifas e leveduras"),
            ("Cultura de fungos", "Ágar Sabouraud a 25-30°C isola fungos"),
            ("Teste de germ tube", "Identifica Candida albicans por formação de tubo germinativo"),
            ("Terbinafina", "Antifúngico oral inibe esqualeno epoxidase para onicomicose"),
            ("Fluconazol", "Azólico sistêmico para candidíase e criptococose"),
            ("Aspergillus", "Fungo filamentoso causa aspergilose invasiva em imunossuprimidos"),
            ("Histoplasmose", "Micose sistêmica por Histoplasma capsulatum endêmica em áreas rurais")
        }));
    }
}
