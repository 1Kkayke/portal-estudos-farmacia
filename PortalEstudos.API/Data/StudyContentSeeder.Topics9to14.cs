namespace PortalEstudos.API.Data;

public static partial class StudyContentSeeder
{
    static partial void GenerateTopics9to14(Dictionary<int, (DocDef[], QDef[])> data)
    {
        // Topics 9-14: Fast generation with term banks
        string[] docTitles = new string[20];
        for (int i = 0; i < 20; i++) docTitles[i] = $"Documento {i + 1}";

        // Topic 9: Farmacoepidemiologia
        data[9] = (BuildDocs("Farmacoepidemiologia", docTitles), BuildFromTerms("Farmacoepidemiologia", new QDef[0], new[] {
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
        data[10] = (BuildDocs("Farmacoterapia", docTitles), BuildFromTerms("Farmacoterapia", new QDef[0], new[] {
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
        data[11] = (BuildDocs("Química Farmacêutica", docTitles), BuildFromTerms("Química Farmacêutica", new QDef[0], new[] {
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
        data[12] = (BuildDocs("Química Orgânica", docTitles), BuildFromTerms("Química Orgânica", new QDef[0], new[] {
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
        data[13] = (BuildDocs("Química Analítica", docTitles), BuildFromTerms("Química Analítica", new QDef[0], new[] {
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
        data[14] = (BuildDocs("Controle de Qualidade", docTitles), BuildFromTerms("Controle de Qualidade", new QDef[0], new[] {
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
