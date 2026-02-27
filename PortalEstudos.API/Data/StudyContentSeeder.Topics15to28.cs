namespace PortalEstudos.API.Data;

public static partial class StudyContentSeeder
{
    static partial void GenerateTopics15to28(Dictionary<int, (DocDef[], QDef[])> data)
    {
        string[] docTitles = new string[20];
        for (int i = 0; i < 20; i++) docTitles[i] = $"Documento {i + 1}";

        // Topics 15-28 com term banks
        data[15] = (BuildDocs("Análises Toxicológicas", docTitles), BuildFromTerms("Análises Toxicológicas", new QDef[0], new[] {
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

        data[16] = (BuildDocs("Bioquímica", docTitles), BuildFromTerms("Bioquímica", new QDef[0], new[] {
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

        data[17] = (BuildDocs("Físico-Química", docTitles), BuildFromTerms("Físico-Química", new QDef[0], new[] {
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

        data[18] = (BuildDocs("Microbiologia Clínica", docTitles), BuildFromTerms("Microbiologia Clínica", new QDef[0], new[] {
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

        data[19] = (BuildDocs("Parasitologia", docTitles), BuildFromTerms("Parasitologia", new QDef[0], new[] {
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

        data[20] = (BuildDocs("Imunologia", docTitles), BuildFromTerms("Imunologia", new QDef[0], new[] {
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

        data[21] = (BuildDocs("Hematologia", docTitles), BuildFromTerms("Hematologia", new QDef[0], new[] {
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

        data[22] = (BuildDocs("Fisiologia Humana", docTitles), BuildFromTerms("Fisiologia Humana", new QDef[0], new[] {
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

        data[23] = (BuildDocs("Anatomia e Histologia", docTitles), BuildFromTerms("Anatomia e Histologia", new QDef[0], new[] {
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

        data[24] = (BuildDocs("Patologia Geral", docTitles), BuildFromTerms("Patologia Geral", new QDef[0], new[] {
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

        data[25] = (BuildDocs("Genética e Biologia Molecular", docTitles), BuildFromTerms("Genética e Biologia Molecular", new QDef[0], new[] {
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

        data[26] = (BuildDocs("Botânica Farmacêutica", docTitles), BuildFromTerms("Botânica Farmacêutica", new QDef[0], new[] {
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

        data[27] = (BuildDocs("Epidemiologia", docTitles), BuildFromTerms("Epidemiologia", new QDef[0], new[] {
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

        data[28] = (BuildDocs("Saúde Pública e SUS", docTitles), BuildFromTerms("Saúde Pública e SUS", new QDef[0], new[] {
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
