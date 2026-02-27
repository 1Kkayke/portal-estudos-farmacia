namespace PortalEstudos.API.Data;

public static partial class StudyContentSeeder
{
    static partial void GenerateTopics9to14(Dictionary<int, (DocDef[], QDef[])> data)
    {
        // Topic 9: Farmacoepidemiologia
        data[9] = (BuildDocs("Farmacoepidemiologia", new[] {
            "Introdução à Farmacoepidemiologia", "Estudos de Utilização de Medicamentos", "Dose Diária Definida (DDD)", "Classificação ATC",
            "Farmacoeconomia e Avaliação de Tecnologias", "Análise de Custo-Efetividade", "QALY e Carga de Doença",
            "Ensaios Clínicos Pragmáticos", "Incidência e Prevalência de Doenças", "Risco Relativo e Odds Ratio",
            "Estudos de Coorte", "Estudos Caso-Controle", "Vieses em Estudos Epidemiológicos", "Confundimento e Causalidade",
            "Indicadores da OMS para Uso Racional", "Polifarmácia e Prescrição Inadequada", "Farmacovigilância Populacional",
            "CONITEC e Incorporação de Tecnologias", "Estudos de Fase IV", "Farmacoepidemiologia no SUS"
        }), BuildFromTerms("Farmacoepidemiologia", new QDef[0], new[] {
            ("DDD", "Dose Diária Definida: dose média de manutenção assumida por dia para um fármaco"),
            ("ATC", "Sistema de classificação anatômica, terapêutica e química de medicamentos"),
            ("CONITEC", "Comissão Nacional de Incorporação de Tecnologias no SUS"),
            ("Farmacoeconomia", "Análise de custos e consequências de medicamentos e terapias"),
            ("Custo-efetividade", "Razão entre custo adicional e benefício clínico adicional de uma intervenção"),
            ("QALY", "Anos de vida ajustados por qualidade, medida de carga de doença"),
            ("Ensaio pragmático", "Ensaio clínico que avalia efetividade em contexto real"),
            ("Incidência", "Número de novos casos de doença em população em risco em período definido"),
            ("Prevalência", "Proporção de indivíduos com doença em determinado momento"),
            ("Risco relativo", "Razão entre incidência em expostos e não expostos"),
            ("Odds ratio", "Razão de chances em estudos caso-controle"),
            ("Coorte prospectiva", "Estudo que acompanha expostos e não expostos no tempo para medir desfechos"),
            ("Estudo caso-controle", "Compara exposição entre casos (doentes) e controles (sadios)"),
            ("Viés de seleção", "Erro sistemático na seleção de participantes que distorce resultados"),
            ("Viés de informação", "Erro sistemático na coleta ou medição de dados"),
            ("Confundimento", "Variável associada a exposição e desfecho que distorce associação causal"),
            ("EUM", "Estudo de Utilização de Medicamentos para descrever padrões de uso"),
            ("Indicadores OMS", "Métricas para avaliar uso racional de medicamentos segundo OMS"),
            ("Polifarmácia", "Uso concomitante de múltiplos medicamentos, geralmente ≥5")
        }));

        // Topic 10: Farmacoterapia
        data[10] = (BuildDocs("Farmacoterapia", new[] {
            "Princípios da Farmacoterapia", "Uso Racional de Medicamentos", "Farmacoterapia Baseada em Evidências",
            "Farmacoterapia da Hipertensão Arterial", "Farmacoterapia do Diabetes Mellitus", "Farmacoterapia da Dislipidemia",
            "Farmacoterapia da Insuficiência Cardíaca", "Farmacoterapia da Doença Arterial Coronariana",
            "Farmacoterapia da Asma e DPOC", "Farmacoterapia da Úlcera Péptica", "Farmacoterapia da Doença Inflamatória Intestinal",
            "Farmacoterapia das Infecções Respiratórias", "Farmacoterapia das Infecções Urinárias",
            "Farmacoterapia da Dor Aguda e Crônica", "Farmacoterapia dos Distúrbios da Tireoide",
            "Farmacoterapia da Osteoporose", "Farmacoterapia em Geriatria", "Farmacoterapia Pediátrica",
            "Farmacoterapia na Gestação e Lactação", "Conciliação Medicamentosa e Seguimento Farmacoterapêutico"
        }), BuildFromTerms("Farmacoterapia", new QDef[0], new[] {
            ("Terapia sequencial", "Substituição de antibiótico IV por oral após estabilização clínica"),
            ("IECA", "Inibidores da enzima conversora de angiotensina para HAS e IC"),
            ("BRA", "Bloqueadores do receptor de angiotensina II para HAS"),
            ("Estatina", "Inibidores da HMG-CoA redutase para reduzir colesterol"),
            ("RIPE", "Rifampicina, Isoniazida, Pirazinamida, Etambutol para tuberculose"),
            ("PEP", "Profilaxia Pós-Exposição para HIV após exposição de risco"),
            ("TARV", "Terapia Antirretroviral para HIV/AIDS"),
            ("GINA", "Iniciativa Global para Asma, diretriz de tratamento escalonado"),
            ("GOLD", "Classificação de DPOC com tratamento escalonado"),
            ("Inibidor de SGLT2", "Gliflozinas que bloqueiam reabsorção renal de glicose"),
            ("Agonista GLP-1", "Análogos de incretina que estimulam insulina e suprimem glucagon"),
            ("DOAC", "Anticoagulantes orais diretos como dabigatrana, rivaroxabana"),
            ("AAS", "Ácido acetilsalicílico, antiagregante plaquetário irreversível"),
            ("AINE", "Anti-inflamatório não esteroidal, inibidor de COX"),
            ("IBP", "Inibidor da Bomba de Prótons, suprime ácido gástrico"),
            ("ITU", "Infecção do Trato Urinário tratada com antibióticos"),
            ("Esquema RIPE", "Tratamento padrão de tuberculose por 6 meses"),
            ("Terapia tripla", "TARV com 2 ITRN + 1 ITRNN ou IP/r"),
            ("Step-up", "Intensificação do tratamento por controle inadequado"),
            ("Step-down", "Redução escalonada após controle sustentado")
        }));

        // Topic 11: Química Farmacêutica
        data[11] = (BuildDocs("Química Farmacêutica", new[] {
            "Introdução à Química Medicinal", "Relação Estrutura-Atividade (SAR)", "Bioisosterismo", "Grupos Farmacofóricos",
            "Planejamento de Fármacos", "Drug Design Racional", "Química Combinatória", "High-Throughput Screening",
            "Fármacos Agonistas e Antagonistas", "Pró-fármacos e Latenciação", "Estereoquímica de Fármacos",
            "Metabolismo de Fármacos e Metabólitos Ativos", "Beta-Lactâmicos: Química e Mecanismos",
            "Inibidores de Enzimas", "Receptores e Interações Ligante-Receptor", "Química dos Antivirais",
            "Química dos Antineoplásicos", "Química dos Anti-inflamatórios", "Química dos Antidepressivos",
            "Química dos Fármacos Cardiovasculares"
        }), BuildFromTerms("Química Farmacêutica", new QDef[0], new[] {
            ("REA", "Relação Estrutura-Atividade que correlaciona grupo químico e efeito farmacológico"),
            ("QSAR", "Relação Quantitativa Estrutura-Atividade usando modelos matemáticos"),
            ("Bioisosterismo", "Substituição de átomo/grupo por outro similar que mantém atividade"),
            ("Pró-fármaco", "Fármaco inativo que biotransforma-se em ativo no organismo"),
            ("Latenciação", "Modificação temporária para melhorar biodisponibilidade ou direcionamento"),
            ("Farmacóforo", "Arranjo espacial de átomos/grupos essenciais para atividade biológica"),
            ("Drug design", "Desenho racional de fármacos com base em alvo molecular"),
            ("Docking molecular", "Simulação computacional de ligação fármaco-receptor"),
            ("Screening virtual", "Busca in silico de candidatos em bibliotecas de moléculas"),
            ("Inibidor de quinase", "Fármaco que bloqueia enzimas quinases em câncer"),
            ("Anticorpo monoclonal", "Proteína produzida por clone celular que reconhece antígeno específico"),
            ("Inibidor de protease", "Classe de antirretroviral que bloqueia maturação viral"),
            ("Peptideomimético", "Molécula não-peptídica que mimetiza estrutura/função de peptídeo")
        }));

        // Topic 12: Química Orgânica
        data[12] = (BuildDocs("Química Orgânica", new[] {
            "Estrutura e Ligações Químicas", "Hidrocarbonetos: Alcanos, Alcenos e Alcinos", "Compostos Aromáticos e Aromaticidade",
            "Álcoois, Fenóis e Éteres", "Aldeídos e Cetonas", "Ácidos Carboxílicos e Derivados",
            "Aminas e Compostos Nitrogenados", "Estereoquímica: Quiralidade e Enantiômeros",
            "Isomeria Geométrica e Conformacional", "Reações de Substituição Nucleofílica",
            "Reações de Eliminação", "Reações de Adição em Alcenos", "Reações de Oxidação e Redução",
            "Mecanismos de Reação Orgânica", "Ressonância e Estabilidade de Intermediários",
            "Compostos Heterocíclicos", "Química dos Carboidratos", "Química dos Lipídeos",
            "Química dos Aminoácidos e Proteínas", "Espectroscopia para Identificação de Compostos"
        }), BuildFromTerms("Química Orgânica", new QDef[0], new[] {
            ("Hibridização sp3", "Geometria tetraédrica, ângulo 109,5°, carbono saturado"),
            ("Carbono quiral", "Carbono assimétrico ligado a 4 grupos diferentes, gerando enantiômeros"),
            ("Enantiômeros", "Isômeros ópticos que são imagens especulares não sobreponíveis"),
            ("Reação SN2", "Substituição nucleofílica bimolecular com inversão de configuração"),
            ("Reação E2", "Eliminação bimolecular anti-periplanar formando alceno"),
            ("Adição eletrofílica", "Adição de eletrófilo a alceno via carbocátion intermediário"),
            ("Oxidação de álcool", "Álcool primário → aldeído → ácido; secundário → cetona"),
            ("Grupamentos funcionais", "Hidroxila, carbonila, carboxila, amina, amida, éster"),
            ("Composto aromático", "Sistema cíclico conjugado com 4n+2 elétrons pi (regra de Hückel)"),
            ("Aminoácido", "Molécula com grupo amina e carboxila, unidade de proteínas"),
            ("Ligação peptídica", "Ligação amida formada entre carboxila e amina de aminoácidos"),
            ("Carboidrato", "Poliidroxialdeído ou poliidroxicetona, fórmula (CH2O)n"),
            ("Lipídio", "Molécula hidrofóbica: ácidos graxos, triglicerídeos, esteroides"),
            ("Anel heterocíclico", "Anel contendo heteroátomo (N, O, S), comum em fármacos"),
            ("RMN", "Ressonância Magnética Nuclear identifica estrutura por deslocamento químico"),
            ("Infravermelho", "Espectroscopia IV identifica grupos funcionais por vibração molecular"),
            ("Espectrometria de massas", "Identifica massa molecular e fragmentação de molécula ionizada")
        }));

        // Topic 13: Química Analítica
        data[13] = (BuildDocs("Química Analítica", new[] {
            "Introdução à Química Analítica", "Análise Qualitativa e Quantitativa", "Equilíbrio Químico em Análise",
            "Titulações Ácido-Base", "Titulações de Precipitação", "Titulações Complexométricas",
            "Titulações de Oxirredução", "Gravimetria", "Espectrofotometria UV-Visível", "Lei de Beer-Lambert",
            "Espectroscopia de Absorção Atômica", "Cromatografia Líquida de Alta Eficiência (HPLC)",
            "Cromatografia Gasosa (GC)", "Cromatografia em Camada Delgada (TLC)",
            "Espectrometria de Massas", "Ressonância Magnética Nuclear (RMN)", "Validação de Métodos Analíticos",
            "Linearidade, Precisão e Exatidão", "Limite de Detecção e Quantificação", "Controle de Qualidade Analítico"
        }), BuildFromTerms("Química Analítica", new QDef[0], new[] {
            ("Lei de Lambert-Beer", "A = εbc relaciona absorbância com concentração e caminho óptico"),
            ("Titulação ácido-base", "Determinação de concentração por reação de neutralização"),
            ("Titulação complexométrica", "Usa EDTA para determinar metais por formação de complexo"),
            ("CLAE", "Cromatografia Líquida de Alta Eficiência separa compostos por coluna"),
            ("Cromatografia gasosa", "Separa compostos voláteis por fase gasosa móvel"),
            ("Espectrofotometria UV-Vis", "Mede absorbância de luz UV/visível para quantificar substâncias"),
            ("Potenciometria", "Medição de potencial elétrico para determinar pH ou concentração"),
            ("Fluorimetria", "Detecção de fluorescência após excitação para análise sensível"),
            ("Eletroforese", "Separação de moléculas carregadas em campo elétrico"),
            ("Absorção atômica", "Quantifica elementos por absorção de luz característica"),
            ("Gravimetria", "Determinação de massa de analito por precipitação e pesagem"),
            ("Linearidade", "Proporcionalidade entre resposta analítica e concentração"),
            ("Exatidão", "Proximidade entre valor medido e valor verdadeiro"),
            ("Precisão", "Concordância entre medidas repetidas, avaliada por DPR"),
            ("Limite de detecção", "Menor concentração detectável com confiança estatística"),
            ("Limite de quantificação", "Menor concentração quantificável com precisão e exatidão"),
            ("Validação analítica", "Demonstração que método é adequado para uso pretendido"),
            ("Curva de calibração", "Gráfico resposta vs concentração para quantificar analito")
        }));

        // Topic 14: Controle de Qualidade
        data[14] = (BuildDocs("Controle de Qualidade", new[] {
            "Introdução ao Controle de Qualidade Farmacêutico", "Boas Práticas de Fabricação (BPF)",
            "Farmacopeias e Compêndios Oficiais", "Ensaios Físico-Químicos", "Ensaios de Identificação",
            "Ensaios de Doseamento", "Testes de Dissolução", "Uniformidade de Conteúdo",
            "Peso Médio e Variação de Peso", "Desintegração e Friabilidade", "Teor de Água e Resíduo de Incineração",
            "Controle Microbiológico", "Testes de Esterilidade", "Endotoxinas Bacterianas",
            "Contagem Microbiana", "Validação de Processos", "Qualificação de Equipamentos",
            "Amostragem e Inspeção", "Gráficos de Controle", "Auditoria e Certificação de Qualidade"
        }), BuildFromTerms("Controle de Qualidade", new QDef[0], new[] {
            ("BPF", "Boas Práticas de Fabricação para garantir qualidade de medicamentos"),
            ("Garantia da Qualidade", "Sistema que assegura qualidade em todas as etapas do produto"),
            ("Teste de dissolução", "Avalia velocidade de liberação do fármaco da forma farmacêutica"),
            ("Teste de desintegração", "Tempo para comprimido fragmentar em partículas menores"),
            ("Uniformidade de conteúdo", "Verifica se cada unidade contém teor de ativo dentro de limites"),
            ("Teste de friabilidade", "Avalia resistência ao desgaste por atrito em friabilômetro"),
            ("Teste de dureza", "Força necessária para romper comprimido diametralmente"),
            ("Teste LAL", "Limulus Amebocyte Lysate detecta endotoxinas bacterianas em injetáveis"),
            ("Teste de esterilidade", "Verifica ausência de microrganismos viáveis em produto estéril"),
            ("Estabilidade ICH Q1A", "Estudo de degradação em condições definidas para prazo de validade"),
            ("Fotoestabilidade", "Avalia degradação por exposição à luz segundo ICH Q1B"),
            ("Validação analítica ICH Q2", "Demonstra método analítico é adequado ao propósito"),
            ("Água purificada", "Água com condutividade ≤1,3 µS/cm para formulações não parenterais"),
            ("Água para injetáveis", "WFI produzida por destilação, <0,25 EU/mL de endotoxina"),
            ("Qualificação IQ", "Instalação: verifica equipamento instalado conforme especificação"),
            ("Qualificação OQ", "Operação: verifica equipamento opera dentro de limites especificados"),
            ("Qualificação PQ", "Desempenho: verifica equipamento produz resultados consistentes"),
            ("CAPA", "Ações Corretivas e Preventivas para desvios e não conformidades"),
            ("Desvio de qualidade", "Afastamento de padrão/especificação que requer investigação"),
            ("Recall", "Recolhimento de lote por risco à saúde determinado pela ANVISA")
        }));
    }
}
