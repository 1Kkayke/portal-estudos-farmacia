namespace PortalEstudos.API.Data;

public static partial class StudyContentSeeder
{
    static partial void GenerateTopics29to42(Dictionary<int, (DocDef[], QDef[])> data)
    {
        string[] docTitles = new string[20];
        for (int i = 0; i < 20; i++) docTitles[i] = $"Documento {i + 1}";

        // Topics 29-42 com term banks
        data[29] = (BuildDocs("Gestão Farmacêutica", docTitles), BuildFromTerms("Gestão Farmacêutica", new QDef[0], new[] {
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

        data[30] = (BuildDocs("Oncologia Farmacêutica", docTitles), BuildFromTerms("Oncologia Farmacêutica", new QDef[0], new[] {
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

        data[31] = (BuildDocs("Farmacologia Cardiovascular", docTitles), BuildFromTerms("Farmacologia Cardiovascular", new QDef[0], new[] {
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

        data[32] = (BuildDocs("Farmacologia do SNC", docTitles), BuildFromTerms("Farmacologia do SNC", new QDef[0], new[] {
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

        data[33] = (BuildDocs("Dermatologia Farmacêutica", docTitles), BuildFromTerms("Dermatologia Farmacêutica", new QDef[0], new[] {
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

        data[34] = (BuildDocs("Farmacologia Anti-infecciosa", docTitles), BuildFromTerms("Farmacologia Anti-infecciosa", new QDef[0], new[] {
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

        data[35] = (BuildDocs("Farmacologia Endócrina", docTitles), BuildFromTerms("Farmacologia Endócrina", new QDef[0], new[] {
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

        data[36] = (BuildDocs("Farmacologia Respiratória", docTitles), BuildFromTerms("Farmacologia Respiratória", new QDef[0], new[] {
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

        data[37] = (BuildDocs("Toxicologia", docTitles), BuildFromTerms("Toxicologia", new QDef[0], new[] {
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

        data[38] = (BuildDocs("Plantas Medicinais e Fitoterapia", docTitles), BuildFromTerms("Plantas Medicinais e Fitoterapia", new QDef[0], new[] {
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

        data[39] = (BuildDocs("Farmácia Magistral", docTitles), BuildFromTerms("Farmácia Magistral", new QDef[0], new[] {
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

        data[40] = (BuildDocs("Biofarmácia", docTitles), BuildFromTerms("Biofarmácia", new QDef[0], new[] {
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

        data[41] = (BuildDocs("Farmacometria", docTitles), BuildFromTerms("Farmacometria", new QDef[0], new[] {
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

        data[42] = (BuildDocs("Micologia Clínica", docTitles), BuildFromTerms("Micologia Clínica", new QDef[0], new[] {
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
