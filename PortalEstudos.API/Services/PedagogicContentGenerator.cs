namespace PortalEstudos.API.Services;

/// <summary>Gerador de conteúdo pedagógico único e profissional por disciplina</summary>
public static class PedagogicContentGenerator
{
    private record DisciplineSeed(string CoreConcept, string PracticalApplication, string CriticalRisk, string CasePrompt);

    private static readonly Dictionary<int, DisciplineSeed> _disciplineSeeds = new()
    {
        [11] = new("Relação estrutura-atividade e planejamento racional de fármacos", "Otimização de potência e seletividade por modificações químicas", "Metabólitos reativos e toxicidade idiossincrática", "Propor mudanças estruturais para reduzir hepatotoxicidade sem perder atividade"),
        [12] = new("Reatividade de funções orgânicas em síntese farmacêutica", "Escolha de rota sintética com melhor rendimento e pureza", "Formação de subprodutos e impurezas genotóxicas", "Comparar duas rotas de síntese para um intermediário farmacêutico"),
        [13] = new("Validação analítica: seletividade, linearidade, precisão e exatidão", "Quantificação de princípio ativo por HPLC-UV", "Resultados fora de especificação por erro de preparo", "Interpretar curva analítica e decidir aceitação do lote"),
        [14] = new("Sistema de qualidade farmacêutica e CAPA", "Liberação de lote com base em especificações físico-químicas", "Desvio crítico não investigado adequadamente", "Definir plano CAPA após falha de dissolução"),
        [15] = new("Triagem toxicológica e confirmação por métodos instrumentais", "Detecção de fármacos e drogas de abuso em matrizes biológicas", "Falso-positivo por reação cruzada em imunoensaio", "Montar fluxo confirmatório GC-MS após triagem positiva"),
        [16] = new("Vias metabólicas e regulação enzimática", "Interpretação de biomarcadores bioquímicos em doença crônica", "Interferentes pré-analíticos alterando exames", "Relacionar alteração de ALT/AST com hipótese clínica"),
        [17] = new("Equilíbrio químico, pKa e solubilidade", "Ajuste de formulação para melhorar dissolução de fármaco pouco solúvel", "Precipitação por variação de pH na mistura", "Definir faixa de pH para estabilidade de solução oral"),
        [18] = new("Identificação microbiológica e teste de sensibilidade", "Escolha de antibiótico guiada por antibiograma", "Resistência induzida por uso empírico inadequado", "Interpretar perfil de sensibilidade em infecção hospitalar"),
        [19] = new("Ciclo biológico de parasitas e diagnóstico laboratorial", "Escolha de método coproparasitológico conforme suspeita", "Subdiagnóstico por coleta inadequada", "Definir conduta para suspeita de helmintíase em comunidade"),
        [20] = new("Resposta imune inata e adaptativa", "Uso racional de imunobiológicos e vacinas", "Reações de hipersensibilidade graves", "Avaliar esquema vacinal em paciente imunossuprimido"),
        [21] = new("Hemostasia, coagulação e interpretação de hemograma", "Estratificação de risco trombótico e hemorrágico", "Interpretação incorreta de plaquetopenia", "Relacionar TP/INR alterado com ajuste terapêutico"),
        [22] = new("Homeostase e integração entre sistemas orgânicos", "Compreensão fisiológica para ajuste farmacoterapêutico", "Intervenção sem considerar compensações fisiológicas", "Explicar resposta cardiovascular ao uso de vasodilatador"),
        [23] = new("Organização tecidual e correlação morfofuncional", "Leitura de alterações histológicas associadas a doença", "Confusão entre padrão inflamatório agudo e crônico", "Relacionar achado histológico com manifestação clínica"),
        [24] = new("Mecanismos gerais de lesão celular e inflamação", "Interpretação patológica para decisão clínica", "Subestimar progressão de dano tecidual", "Identificar fases de inflamação em cenário clínico"),
        [25] = new("Expressão gênica, variantes e farmacogenética", "Personalização terapêutica por perfil genético", "Dose inadequada por ignorar polimorfismo metabólico", "Escolher terapia considerando metabolizador lento/rápido"),
        [26] = new("Taxonomia botânica e identificação de espécies medicinais", "Controle de autenticidade da droga vegetal", "Adulteração de matéria-prima botânica", "Criar checklist macroscópico e microscópico para identificação"),
        [27] = new("Indicadores epidemiológicos e vigilância em saúde", "Planejamento de intervenção com base em incidência e prevalência", "Viés na interpretação de associação causal", "Calcular risco relativo em cenário de surto"),
        [28] = new("Princípios do SUS: universalidade, integralidade e equidade", "Organização da assistência farmacêutica na atenção básica", "Descontinuidade de tratamento por falha logística", "Desenhar fluxo de dispensação para doença crônica"),
        [29] = new("Gestão de estoque, compras e indicadores de desempenho", "Planejamento de mix de produtos com curva ABC", "Ruptura de estoque em medicamentos essenciais", "Definir política de estoque mínimo e ponto de pedido"),
        [30] = new("Protocolos antineoplásicos e suporte ao paciente oncológico", "Validação de prescrição quimioterápica e prevenção de erros", "Extravasamento e toxicidade dose-limitante", "Avaliar ajuste de dose em neutropenia pós-ciclo"),
        [31] = new("Fisiopatologia cardiovascular e alvos farmacológicos", "Seleção de terapias para HAS, IC e doença coronariana", "Interações que aumentam risco de hipotensão/bradicardia", "Montar esquema para paciente com IC e múltiplas comorbidades"),
        [32] = new("Neurotransmissão e farmacologia do sistema nervoso central", "Manejo de depressão, epilepsia e transtornos psicóticos", "Sedação excessiva e comprometimento cognitivo", "Ajustar psicotrópicos com foco em segurança e adesão"),
        [33] = new("Barreira cutânea e farmacologia tópica", "Escolha de veículo dermatológico conforme lesão", "Uso prolongado de corticoide tópico e atrofia cutânea", "Definir conduta tópica em dermatite inflamatória"),
        [34] = new("Mecanismos de ação de antimicrobianos e resistência", "Terapia guiada por perfil microbiológico", "Seleção de resistência por uso inadequado", "Escolher esquema inicial e estratégia de descalonamento"),
        [35] = new("Eixos hormonais e modulação farmacológica", "Ajuste de terapia em diabetes e disfunções tireoidianas", "Hipoglicemia e eventos adversos hormonais", "Interpretar perfil glicêmico para ajuste terapêutico"),
        [36] = new("Fisiologia pulmonar e broncomodulação", "Uso escalonado de broncodilatadores e anti-inflamatórios", "Técnica inalatória incorreta reduzindo eficácia", "Revisar plano terapêutico em asma não controlada"),
        [37] = new("Relação dose-resposta tóxica e avaliação de exposição", "Conduta inicial em intoxicação aguda", "Atraso no reconhecimento de sinais de gravidade", "Definir medidas de descontaminação e suporte"),
        [38] = new("Evidência clínica em fitoterapia e padronização de extratos", "Indicação segura de plantas medicinais com base em diretrizes", "Interações fitoterápico-fármaco", "Avaliar uso concomitante de fitoterápico com anticoagulante"),
        [39] = new("Boas práticas de manipulação e controle magistral", "Desenvolvimento de fórmulas individualizadas", "Erro de pesagem e uniformidade de dose", "Elaborar rotina de dupla checagem em manipulação"),
        [40] = new("Biodisponibilidade, bioequivalência e fatores de absorção", "Comparação entre formulações referência e teste", "Falha terapêutica por diferença de desempenho in vivo", "Interpretar parâmetros Cmax, Tmax e AUC"),
        [41] = new("Modelagem PK/PD e ajuste posológico", "Predição de concentração-alvo em diferentes perfis de paciente", "Sub ou superdosagem por modelo inadequado", "Escolher regime de dose a partir de alvo PK/PD"),
        [42] = new("Diagnóstico micológico e terapêutica antifúngica", "Definição de tratamento conforme espécie e sítio de infecção", "Resistência antifúngica e toxicidade hepática", "Planejar tratamento para candidemia em paciente crítico")
    };

    public static string GenerateDistinctiveContent(int topicId, string topicName, string documentTitle, int order)
    {
        return topicId switch
        {
            // FARMÁCIA
            1 => GeneratePharmacologyContent(documentTitle, order),
            2 => GenerateClinicalPharmacologyContent(documentTitle, order),
            3 => GeneratePharmacognosyContent(documentTitle, order),
            4 => GeneratePharmatechniqueContent(documentTitle, order),
            5 => GeneratePharmaceuticalTechContent(documentTitle, order),
            
            // EDUCAÇÃO & GESTÃO
            6 => GenerateHospitalPharmacyContent(documentTitle, order),
            7 => GenerateAttentionPharmacyContent(documentTitle, order),
            8 => GenerateVigilanceContent(documentTitle, order),
            9 => GenerateEpidemiologyContent(documentTitle, order),
            10 => GeneratePharmacotheraphyContent(documentTitle, order),
            11 or 12 or 13 or 14 or 15 or 16 or 17 or 18 or 19 or 20 or 21 or 22 or 23 or 24 or 25
                or 26 or 27 or 28 or 29 or 30 or 31 or 32 or 33 or 34 or 35 or 36 or 37 or 38 or 39
                or 40 or 41 or 42 => GenerateMappedDisciplineContent(topicId, topicName, documentTitle, order),
            
            _ => GenerateDefaultContetent(topicName, documentTitle, order)
        };
    }

    private static string GenerateMappedDisciplineContent(int topicId, string topicName, string documentTitle, int order)
    {
        if (!_disciplineSeeds.TryGetValue(topicId, out var seed))
        {
            return GenerateDefaultContetent(topicName, documentTitle, order);
        }

        var titleEncoded = System.Net.WebUtility.HtmlEncode(documentTitle);
        var topicEncoded = System.Net.WebUtility.HtmlEncode(topicName);
        var focusTag = order % 2 == 0 ? "aplicação clínica" : "fundamentação técnica";

        return $@"
        <section class=""content-section animate-fade-in"">
            <h2>{titleEncoded}</h2>
            <p>Este módulo de <strong>{topicEncoded}</strong> aborda o tema com profundidade acadêmica e orientação prática. O foco da unidade é <strong>{focusTag}</strong>, conectando teoria, tomada de decisão e segurança profissional.</p>

            <h3>1. Núcleo Conceitual</h3>
            <p>{seed.CoreConcept}. A compreensão desse eixo permite interpretar literatura técnica, protocolos institucionais e critérios de conduta com maior precisão.</p>

            <h3>2. Aplicação Profissional</h3>
            <p>{seed.PracticalApplication}. Em rotina assistencial e laboratorial, essa competência melhora a qualidade do cuidado e reduz retrabalho técnico.</p>

            <h3>3. Ponto Crítico de Segurança</h3>
            <div class=""callout warning"">
                <div class=""callout-title"">⚠️ Atenção Operacional</div>
                <p>{seed.CriticalRisk}. Por isso, o processo deve incluir dupla checagem, documentação rastreável e revisão por protocolo quando houver dúvida.</p>
            </div>

            <h3>4. Caso para Discussão</h3>
            <p>{seed.CasePrompt}. Estruture sua resposta em três blocos: avaliação inicial, decisão técnica e monitoramento de desfechos.</p>

            <h3>5. Checklist de Domínio</h3>
            <ul>
                <li>Defino os conceitos-chave do tema sem consulta?</li>
                <li>Aplico corretamente critérios técnicos em um caso real?</li>
                <li>Reconheço riscos e proponho estratégias de mitigação?</li>
                <li>Consigo justificar a conduta com base em evidências e diretrizes?</li>
            </ul>
        </section>";
    }

    private static string GeneratePharmacologyContent(string title, int order)
    {
        return $@"
        <section class=""content-section animate-fade-in"">
            <h2>Conceitos Fundamentais de Farmacologia</h2>
            <p>A <strong>farmacologia geral</strong> é a ciência que estuda as interações entre moléculas químicas (fármacos) e sistemas biológicos. Sua compreensão é essencial para qualquer profissional na área da saúde, permitindo interpretação segura de mecanismos terapêuticos e previsão de reações adversas.</p>
            
            <h3>1. Dinâmica da Ação Farmacológica</h3>
            <p>Os fármacos exercem seus efeitos através de <strong>interações moleculares específicas</strong> com proteínas receptoras em membranas celulares, citoplasma ou núcleo. Esta interação segue princípios de cinética química e termodinâmica.</p>
            
            <div class=""callout info"">
                <div class=""callout-title"">📌 Princípio Central</div>
                <p><strong>Correlação Estrutura-Atividade:</strong> Pequenas mudanças na estrutura química podem resultar em grandes alterações no efeito farmacológico. Por exemplo, a adição de um grupo metila ao core de uma molécula pode aumentar 10x sua potência ou eliminar completamente seu efeito.</p>
            </div>

            <h3>2. Farmacocinética: O que o Corpo faz ao Fármaco</h3>
            <p>Compreenda as quatro fases essenciais:</p>
            <ul>
                <li><strong>Absorção:</strong> Transporte do fármaco do local de administração para a circulação. Vias orais sofrem metabolismo de primeira passagem hepático.</li>
                <li><strong>Distribuição:</strong> Alocação do fármaco nos tecidos. Fármacos lipofílicos penetram melhor o SNC; hidrofílicos distribuem-se no espaço extracelular.</li>
                <li><strong>Metabolismo:</strong> Transformação química, predominantemente no fígado via citocromos P450. Cria metabólitos inativos (maioria) ou potencialmente ativos/tóxicos.</li>
                <li><strong>Excreção:</strong> Eliminação via rim (principal), bile, pulmões ou pele. Meia-vida (t½) determina frequência de dosagem.</li>
            </ul>

            <div class=""callout warning"">
                <div class=""callout-title"">⚠️ Conceito Crítico</div>
                <p><strong>Estado de Equilíbrio:</strong> Normalmente leva 5 meias-vidas para atingir estado de equilíbrio (100% da concentração esperada). Um fármaco com t½ de 24h atinge equilíbrio em ~5 dias. Este conceito é crucial para adequação de esquemas terapêuticos.</p>
            </div>

            <h3>3. Farmacodinâmica: O que o Fármaco faz ao Corpo</h3>
            <p>Mecanismos pelos quais o fármaco produz efeito:</p>
            
            <table>
                <thead>
                    <tr>
                        <th>Tipo de Interação</th>
                        <th>Mecanismo</th>
                        <th>Exemplo</th>
                        <th>Reversibilidade</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Agonista Completo</td>
                        <td>Liga ao receptor → 100% ativação</td>
                        <td>Epinefrina (α e β-adrenérgicos)</td>
                        <td>Reversivelmente ligado</td>
                    </tr>
                    <tr>
                        <td>Agonista Parcial</td>
                        <td>Liga ao receptor → 50% ativação máxima</td>
                        <td>Buprenorfina (opioide)</td>
                        <td>Teto terapêutico menor</td>
                    </tr>
                    <tr>
                        <td>Antagonista Competitivo</td>
                        <td>Bloqueia receptor reversível</td>
                        <td>Propranolol (β-bloqueador)</td>
                        <td>Deslocável por dose alta de agonista</td>
                    </tr>
                    <tr>
                        <td>Antagonista Irreversível</td>
                        <td>Liga covalentemente → bloqueio permanente</td>
                        <td>Aspirina (inibe COX irreversivelmente)</td>
                        <td>Requer síntese de novo de enzimas</td>
                    </tr>
                </tbody>
            </table>

            <h3>4. Variabilidade Farmacocinética e Farmacodinâmica</h3>
            <p>Nem todos respondem igualmente ao mesmo fármaco. Fatores determinantes:</p>
            <ul>
                <li><strong>Genéticos:</strong> Polimorfismos em CYP450 (metabolizadores lentos vs. rápidos) afetam 10-20x a concentração plasmática</li>
                <li><strong>Fisiológicos:</strong> Idade, peso, função renal/hepática modificam cinética</li>
                <li><strong>Ambientais:</strong> Alimentos, outros fármacos (indução/inibição de enzimas), álcool</li>
                <li><strong>Patológicos:</strong> Insuficiência renal/hepática reduz clearance dramaticamente</li>
            </ul>

            <div class=""callout success"">
                <div class=""callout-title"">✓ Aplicação Prática</div>
                <p><strong>Farmacogenômica:</strong> Testes genéticos permitem personalizar doses. Ex: pacientes com CYP2C19 polimorfo lento recebem doses reduzidas de clopidogrel. Omeprazol é inibidor forte de CYP2C19, reduzindo ativação do clopidogrel.</p>
            </div>

            <h3>5. Segurança Farmacológica: Índice Terapêutico</h3>
            <p>Razão entre dose tóxica (DL50) e dose efetiva (DE50):</p>
            <ul>
                <li><strong>Índice Alto (>10):</strong> Margem ampla entre terapia e toxicidade. Ex: Amoxicilina, Acetaminofeno</li>
                <li><strong>Índice Baixo (<2):</strong> Margem estreita. Requer monitoramento terapêutico. Ex: Digoxina, Varfarina, Lítio</li>
            </ul>

            <p>Durante formação profissional, compreender estes conceitos permite prever efeitos adversos e identificar interações medicamentosas <strong>antes</strong> de prescrevê-las a pacientes reais.</p>
        </section>";
    }

    private static string GenerateClinicalPharmacologyContent(string title, int order)
    {
        return $@"
        <section class=""content-section animate-fade-in"">
            <h2>Farmacologia Clínica Aplicada</h2>
            <p>Transformar conhecimento de mecanismos moleculares em decisões seguras e efetivas no tratamento de pacientes reais é o cerne da <strong>farmacologia clínica</strong>.</p>
            
            <h3>1. Medicina Preventiva através de Farmacologia</h3>
            <p>Compreender farmacodinâmica permite prevenção primária:</p>
            <ul>
                <li><strong>Estatinas:</strong> Inibem HMG-CoA redutase → redução de síntese de colesterol → prevenção de eventos cardiovasculares</li>
                <li><strong>IECA em diabéticos:</strong> Reduzem pressão glomerular → proteção renal independente de PA</li>
                <li><strong>AAS em prevenção:</strong> Antiagregante plaquetário → redução de trombos</li>
            </ul>

            <div class=""callout info"">
                <div class=""callout-title"">🔬 Evidência Clínica</div>
                <p><strong>Estudo 4S (1994):</strong> Simvastatina reduziu mortalidade coronariana em 30% em pacientes com coronariopatia prévia. NNT=20 para prevenir 1 morte cardiovascular em 5 anos.</p>
            </div>

            <h3>2. Ajuste de Dose em Insuficiência Renal</h3>
            <p>A maioria dos fármacos é eliminada renalmente. Insuficiência renal requer ajuste:</p>
            <table>
                <thead>
                    <tr>
                        <th>TFG (mL/min)</th>
                        <th>Classificação</th>
                        <th>Ajuste de Dose</th>
                        <th>Exemplos de Fármacos</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>>90</td>
                        <td>Normal</td>
                        <td>Sem ajuste</td>
                        <td>Maioria</td>
                    </tr>
                    <tr>
                        <td>60-89</td>
                        <td>Leve ↓</td>
                        <td>Sem ajuste geralmente</td>
                        <td>Maioria</td>
                    </tr>
                    <tr>
                        <td>30-59</td>
                        <td>Moderada ↓</td>
                        <td>↓↓ em alguns</td>
                        <td>Gentamicina, Aciclovir</td>
                    </tr>
                    <tr>
                        <td>15-29</td>
                        <td>Severa ↓</td>
                        <td>↓↓↓ frequência/dose</td>
                        <td>Aminoglicosídeos, ITRN</td>
                    </tr>
                    <tr>
                        <td><15</td>
                        <td>Falência</td>
                        <td>Ajuste individualizado</td>
                        <td>Necessita monitoramento</td>
                    </tr>
                </tbody>
            </table>

            <h3>3. Monitoramento Terapêutico de Fármacos</h3>
            <p>Alguns fármacos requerem dosagem sérica para otimizar terapia:</p>
            <ul>
                <li><strong>Digoxina:</strong> Janela terapêutica: 0,5-2 ng/mL; toxicidade >2 ng/mL com arritmias ventriculares</li>
                <li><strong>Varfarina:</strong> INR alvo 2-3 (>3 = hemorragia; <2 = falência terapêutica)</li>
                <li><strong>Lítio:</strong> Nível terapêutico: 0,6-1,2 mEq/L; acima = toxicidade neuropsiquiátrica/renal</li>
                <li><strong>Fenitoína:</strong> Nível terapêutico: 10-20 μg/mL; cinética não-linear (saturável)</li>
            </ul>

            <div class=""callout warning"">
                <div class=""callout-title"">⚠️ Interação Crítica</div>
                <p><strong>Varfarina + Aspirina:</strong> Risco sinérgico de hemorragia. Se necessário prescrever ambos, usar doses mínimas e monitorar INR mais frequentemente. Existem alternativas mais seguras (dabigatrana + paracetamol).</p>
            </div>

            <h3>4. Efeitos Adversos e Farmacovigilância</h3>
            <p>Classificação de severidade:</p>
            <ul>
                <li><strong>Reações Tipo A (Augmented):</strong> Extensão do efeito desejado. Ex: hipoglicemia com insulina. Previsíveis, dose-dependente.</li>
                <li><strong>Reações Tipo B (Bizarre):</strong> Não relacionadas ao efeito farmacológico. Imprevisíveis. Ex: Síndrome de Stevens-Johnson com antibióticos.</li>
                <li><strong>Reações Tipo C (Chronic):</strong> Dependentes de duração. Ex: Osteoporose com corticosteroides crônicos.</li>
            </ul>

            <div class=""callout success"">
                <div class=""callout-title"">✓ Dever Profissional</div>
                <p><strong>Notificar Farmacovigilância:</strong> Todo profissional tem obrigação legal de notificar reações adversas suspeitas. No Brasil: ANVISA (Sistema Vigilância Sanitária). Interface: www.anvisa.gov.br/farmacovigilancia</p>
            </div>
        </section>";
    }

    private static string GeneratePharmacognosyContent(string title, int order)
    {
        return $@"
        <section class=""content-section animate-fade-in"">
            <h2>Farmacognosia: Plantas Medicinais</h2>
            <p><strong>Farmacognosia</strong> estuda fármacos derivados de fontes naturais (plantas, animais, minerais). Aproximadamente <strong>25% de todos os fármacos modernos</strong> originam-se ou foram inspirados em compostos naturais.</p>
            
            <h3>1. Plantas Medicinais Relevantes na Prática</h3>
            <p>Exemplos com evidência científica:</p>
            <ul>
                <li><strong>Digitalis purpurea (Dedaleira):</strong> Contém glicosídeos cardíacos (digoxina, digitalina). Isolado: Digoxina comercial. Mecanismo: Inibição de Na+/K+-ATPase</li>
                <li><strong>Salix alba (Salgueiro Branco):</strong> Casca contém salicina (pré-fármaco). Metabolizado em ácido salicílico. Origem de aspirina</li>
                <li><strong>Cinchona officinalis (Quina):</strong> Alcaloide quinina. Antimalárico histórico. Hoje substituído por arteméter (derivado de Artemisia annua)</li>
                <li><strong>Catharanthus roseus (Vinca-de-Madagascar):</strong> Alcaloides vinca (vincristina, vinblastina). Antineoplásicos por inibição de microtúbulos</li>
            </ul>

            <h3>2. Desenvolvimento de Fármacos de Plantas</h3>
            <p>Processo etnofarmacológico:</p>
            <ol>
                <li><strong>Etnobotânica:</strong> Identificar plantas usadas tradicionalmente</li>
                <li><strong>Triagem:</strong> Testes in vitro para atividade biológica</li>
                <li><strong>Isolamento:</strong> Purificação do composto ativo via cromatografia</li>
                <li><strong>Elucidação:</strong> Determinação de estrutura por espectrometria</li>
                <li><strong>Ensaios Clínicos:</strong> Fases I, II, III em humanos</li>
                <li><strong>Síntese Total:</strong> Replicação sintética para ampla produção</li>
            </ol>

            <div class=""callout info"">
                <div class=""callout-title"">📊 Estatística</div>
                <p><strong>Taxa de Sucesso:</strong> De 5000 plantas triadas, ~50 alcançam fase clínica, ~5 potencialmente terapêuticas, ~1 aprovado. Taxa: 0,02%</p>
            </div>

            <h3>3. Qualidade e Segurança de Fitoterápicos</h3>
            <p>Desafios únicos:</p>
            <ul>
                <li><strong>Variabilidade sazonal:</strong> Teor de princípios ativos varia com colheita e armazenamento</li>
                <li><strong>Contaminantes:</strong> Pesticidas, metais pesados, microrganismos patogênicos</li>
                <li><strong>Adulterantes:</strong> Substituição por plantas similares (ex: efeito farmacológico diferente)</li>
                <li><strong>Polifarmacologia:</strong> Múltiplos compostos = múltiplos mecanismos e interações desconhecidas</li>
            </ul>

            <div class=""callout warning"">
                <div class=""callout-title"">⚠️ Mito vs. Realidade</div>
                <p><strong>Natural ≠ Seguro:</strong> Ricina (mamona) é letal em miligrama; Aconitina (acônito) causa arritmias fatais; Estricnina (árvore da noz-vômica) é neurotoxina. Orientação profissional é essencial.</p>
            </div>

            <h3>4. Fitoterápicos no Brasil</h3>
            <p>Regulamento ANVISA para Plantas Medicinais:</p>
            <table>
                <thead>
                    <tr>
                        <th>Categoria</th>
                        <th>Definição</th>
                        <th>Registro ANVISA</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Tradicional</td>
                        <td>Uso comprovado, sem evid. clínica</td>
                        <td>Notificação</td>
                    </tr>
                    <tr>
                        <td>Inovador</td>
                        <td>Nova indicação/forma</td>
                        <td>Registro + testes</td>
                    </tr>
                </tbody>
            </table>
        </section>";
    }

    private static string GeneratePharmatechniqueContent(string title, int order)
    {
        return $@"
        <section class=""content-section animate-fade-in"">
            <h2>Farmacotécnica: Tecnologia de Formulações</h2>
            <p><strong>Farmacotécnica</strong> é a ciência de transformar fármacos em medicamentos seguros, eficazes, estáveis e convenientes para uso. Um mesmo fármaco pode ter eficácia vastamente diferente conforme formulação.</p>
            
            <h3>1. Formas Farmacêuticas Principais</h3>
            <table>
                <thead>
                    <tr>
                        <th>Forma</th>
                        <th>Via</th>
                        <th>Vantagens</th>
                        <th>Desvantagens</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Comprimido</td>
                        <td>Oral</td>
                        <td>Estável, barato, fácil de usar</td>
                        <td>Disfagia, fármaco degradado gastrintestinal</td>
                    </tr>
                    <tr>
                        <td>Cápsula</td>
                        <td>Oral</td>
                        <td>Máscara sabor, proteção ácida possível</td>
                        <td>Custo maior, pode abrir prematuramente</td>
                    </tr>
                    <tr>
                        <td>Solução oral</td>
                        <td>Oral</td>
                        <td>Rápida absorção, pediatria</td>
                        <td>Instável, sabor ruim, menor adesão</td>
                    </tr>
                    <tr>
                        <td>Injeção IV</td>
                        <td>Intravenosa</td>
                        <td>100% biodisponibilidade, efeito imediato</td>
                        <td>Invasivo, risco de flebite, requer treinamento</td>
                    </tr>
                    <tr>
                        <td>Transdérmico</td>
                        <td>Percutânea</td>
                        <td>Evita metabolismo 1ª passagem, adesão</td>
                        <td>Absorção lenta, reação alérgica local</td>
                    </tr>
                </tbody>
            </table>

            <h3>2. Libração Modificada (Dosagem Sustentada)</h3>
            <p>Estratégias para manter níveis terapêuticos:</p>
            <ul>
                <li><strong>Comprimido de ação prolongada:</strong> Encapsulação de microsferas + dissolução gradual</li>
                <li><strong>Gel de silicone:</strong> Reservatório libera fármaco através de membrana</li>
                <li><strong>Adesivos transdérmicos:</strong> Ex: Nitroglicerina, Nicotina, Fentanila</li>
                <li><strong>Nanotecnologia:</strong> Nanopartículas com recapeamento progressivo</li>
            </ul>

            <div class=""callout success"">
                <div class=""callout-title"">✓ Impacto Clínico</div>
                <p><strong>Adesão Medicamentosa:</strong> Pacientes tomam menos doses = maior adesão. Ex: De 2x/dia para 1x/dia aumenta adesão de ~70% para ~90%. Fármaco de curta ação = picos/vales; versão sustentada = níveis estáveis.</p>
            </div>

            <h3>3. Estabilidade Farmacêutica</h3>
            <p>Fatores que degradam medicamentos:</p>
            <ul>
                <li><strong>Luz:</strong> Reações fotoquímicas. Solução: frasco âmbar ou alumínio</li>
                <li><strong>Temperatura:</strong> Acelera hidrólise e isomerização. Armazenar <25°C ou refrigerado</li>
                <li><strong>Umidade:</strong> Absorção de água → degradação, caking. Usar dessecante</li>
                <li><strong>pH:</strong> Ésteres hidrolisam em meio básico; bases deprotonizam em pH ácido</li>
                <li><strong>Oxigênio:</strong> Oxidação de antioxidáveis. Uso de antioxidantes (BHA, Vit E)</li>
            </ul>

            <p><strong>Teste de Estabilidade Acelerada:</strong> 40°C/75% umidade por 6 meses = equivalente a 3 anos em condições normais</p>
        </section>";
    }

    private static string GeneratePharmaceuticalTechContent(string title, int order)
    {
        return $@"
        <section class=""content-section animate-fade-in"">
            <h2>Tecnologia Farmacêutica Industrial</h2>
            <p>Escalar formulação de lote piloto (<100g) para produção industrial (toneladas) requer conhecimento avançado de engenharia de processos, validação e compliance regulatória.</p>
            
            <h3>1. Processo de Fabricação (GMP - Good Manufacturing Practice)</h3>
            <ol>
                <li><strong>Pesagem e Dispensação:</strong> Reagentes em proporções exatas, rastreabilidade</li>
                <li><strong>Mistura:</strong> Homogeneização em misturadores de alta capacidade (V ≥ 100L)</li>
                <li><strong>Granulação:</strong> Compactação via úmida ou seca para melhor fluidez</li>
                <li><strong>Secagem:</strong> Redução de umidade (<3%) em estufa ou liofilizador</li>
                <li><strong>Aglomeração:</strong> Compactação em prensa (toneladas) para formar comprimidos</li>
                <li><strong>Puncionamento:</strong> Produção de milhões de unidades/hora em máquina rotativa</li>
                <li><strong>Empacotamento:</strong> Blister, frasco, sachê com código de rastreamento</li>
                <li><strong>Controle de Qualidade:</strong> Uniformidade, dureza, friabilidade, dissolução in vitro</li>
            </ol>

            <div class=""callout warning"">
                <div class=""callout-title"">⚠️ Validação Crítica</div>
                <p><strong>Dissolução:</strong> Não é suficiente saber que um comprimido contém 500mg de fármaco. Deve liberar 80-120% em 30-60 min em pH 6.8. Falha = biodisponibilidade reduzida mesmo com teor correto.</p>
            </div>

            <h3>2. Validação de Processos</h3>
            <p>Demonstrar que um processo produz consistentemente produtos de qualidade:</p>
            <ul>
                <li><strong>PV Prospectiva:</strong> Antes da implementação (cálculos teóricos)</li>
                <li><strong>PV Concorrente:</strong> Durante a introdução (monitoramento real)</li>
                <li><strong>PV Retrospectiva:</strong> Análise de dados históricos (até 1 ano)</li>
            </ul>

            <p><strong>Requisito:</strong> Mínimo 3 lotes de tamanho comercial devem cumprir especificações em testes de estabilidade.</p>
        </section>";
    }

    private static string GenerateHospitalPharmacyContent(string title, int order)
    {
        return $@"
        <section class=""content-section animate-fade-in"">
            <h2>Farmácia Hospitalar e Clínica</h2>
            <p>Ambiente hospitalar exige farmacêutico clínico atuante em <strong>equipe multiprofissional</strong>, garantindo farmacoterapia apropriada, segura e custo-efetiva.</p>
            
            <h3>1. Funções do Farmacêutico Hospitalar</h3>
            <ul>
                <li><strong>Seleção e Aquisição:</strong> Avaliação farmacoeconômica de alternativas</li>
                <li><strong>Armazenamento:</strong> Gerenciamento de estoques, controle de validade, storage apropriado</li>
                <li><strong>Distribuição:</strong> Sistema de dose unitária ou distribuição direta segura</li>
                <li><strong>Farmacovigilância:</strong> Relato de RAM e interações em tempo real</li>
                <li><strong>Farmacodisponibilização:</strong> Preparação de medicações em farmácia (anti-sépticos, vacinas, citotóxicos)</li>
                <li><strong>Consultoria Clínica:</strong> Recomendações a médicos sobre dosagem, via, alternativas</li>
            </ul>

            <h3>2. Sistema de Distribuição de Medicamentos</h3>
            <p><strong>Dose Unitária:</strong> Cada paciente recebe medicação pré-preparada, reduzindo erros:</p>
            <ul>
                <li>Erro com dose unitária: ~2 a 5 por 1000</li>
                <li>Erro com distribuição tradicional: ~50 por 1000</li>
                <li>Diferença: ~90% redução de erros</li>
            </ul>

            <div class=""callout success"">
                <div class=""callout-title"">✓ Segurança do Paciente</div>
                <p><strong>Seis Certos:</strong> Paciente certo, medicamento certo, dose certa, via certa, hora certa, documentação certa. Farmacêutico é última barreira antes da administração.</p>
            </div>

            <h3>3. Antibioticoterapia Apropriada</h3>
            <p>Principal responsabilidade clínica para reduzir resistência bacteriana:</p>
            <ul>
                <li><strong>Diagnóstico rápido:</strong> Hemocultura antes de iniciar cobertura empírica</li>
                <li><strong>Desescalação:</strong> Trocar ampla cobertura após suscetibilidade conhecida</li>
                <li><strong>Duração apropriada:</strong> 5-7 dias para maioria das infecções (não 14 dias desnecessários)</li>
                <li><strong>Dose terapêutica:</strong> Alcançar concentração mínima inibitória (CMI)</li>
                <li><strong>Monitoramento:</strong> Função renal, hepática; TDM (Terapêutic Drug Monitoring) se necessário</li>
            </ul>
        </section>";
    }

    private static string GenerateAttentionPharmacyContent(string title, int order)
    {
        return $@"
        <section class=""content-section animate-fade-in"">
            <h2>Atenção Farmacêutica e Seguimento Farmacoterapêutico</h2>
            <p><strong>Atenção Farmacêutica (AF)</strong> é prática centrada no paciente, visando garantir eficácia e segurança da farmacoterapia, melhorando qualidade de vida.</p>
            
            <h3>1. Processo de Atenção Farmacêutica</h3>
            <ol>
                <li><strong>Identificação:</strong> Localizar pacientes em risco (idade avançada, polifarmácia, doença crônica)</li>
                <li><strong>Coleta de Informação:</strong> Entrevista farmacêutica detalhada</li>
                <li><strong>Avaliação:</strong> Identificar problemas relacionados a medicamentos (PRM)</li>
                <li><strong>Planejamento:</strong> Definir metas de tratamento com paciente</li>
                <li><strong>Implementação:</strong> Orientações, ajustes de esquema, encaminhamentos</li>
                <li><strong>Avaliação de Resultado:</strong> Acompanhamento periódico de desfechos</li>
                <li><strong>Documentação:</strong> Registro legível e assinado</li>
            </ol>

            <h3>2. Classificação de Problemas Relacionados a Medicamentos</h3>
            <table>
                <thead>
                    <tr>
                        <th>Categoria</th>
                        <th>Definição</th>
                        <th>Exemplo</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Desabastecimento</td>
                        <td>Paciente sem medicação apropriada</td>
                        <td>Diabético sem metformina = hiperglicemia não controlada</td>
                    </tr>
                    <tr>
                        <td>Farmáco Inapropriado</td>
                        <td>Fármaco escolhido não é ideal</td>
                        <td>Sedentário com simvastatina 80mg = risco alto de rabdomiolise</td>
                    </tr>
                    <tr>
                        <td>Dose Inadequada</td>
                        <td>Dose insuficiente ou excessiva</td>
                        <td>IDOSO com lisinopril 20mg = hipotensão, queda</td>
                    </tr>
                    <tr>
                        <td>Reação Adversa</td>
                        <td>Efeito indesejado presente</td>
                        <td>Tosse seca com IECA = trocar para ARA II</td>
                    </tr>
                    <tr>
                        <td>Interação</td>
                        <td>Medicamentos interferem entre si</td>
                        <td>Warfarina + Aspirina = risco sinérgico de hemorragia</td>
                    </tr>
                    <tr>
                        <td>Inefetividade</td>
                        <td>Medicação não produz efeito esperado</td>
                        <td>Paciente em PA 150/100 mmHg com losartana 50mg = ajuste recomendado</td>
                    </tr>
                </tbody>
            </table>

            <div class=""callout info"">
                <div class=""callout-title"">📋 Prática Comum</div>
                <p><strong>Idoso Polifarmacopático:</strong> Paciente 80 anos com CHF, DM2, HAS, FA. Medicações: IECA, betabloqueador, diurético, DAOP, Varfarina, Hipoglicemiante. Revise: duplicatas? Contraindicações? Interações?</p>
            </div>

            <h3>3. Conciliação Medicamentosa</h3>
            <p>Processo crítico em transição de cuidados (admissão hospitalar, alta, transferência):</p>
            <ul>
                <li>Listar todos os medicamentos que paciente toma em casa (prescritos + OTC + fitoterápicos)</li>
                <li>Comparar com prescrição hospitalar</li>
                <li>Detectar omissões, duplicatas, doses inadequadas</li>
                <li>Resolver discrepâncias com equipe médica</li>
                <li>Documentar conclusões</li>
            </ul>

            <p><strong>Erro frequente:</strong> Paciente em casa toma simvastatina 40mg, prescrição hospitalar omite. Risco: não-controle lipídico pós-alta.</p>
        </section>";
    }

    private static string GenerateVigilanceContent(string title, int order)
    {
        return $@"
        <section class=""content-section animate-fade-in"">
            <h2>Farmacovigilância: Vigilância de Segurança</h2>
            <p><strong>Farmacovigilância</strong> monitora a ocorrência e avaliação de reações adversas a medicamentos após aprovação. Fase IV de farmacokinética clínica em milhões de pacientes reais.</p>
            
            <h3>1. Diferenças Ensaio Clínico vs. Pós-Comercialização</h3>
            <ul>
                <li><strong>Amostra:</strong> ECT: 3000-5000 pacientes; Pós-comercialização: milhões</li>
                <li><strong>Seleção:</strong> ECT: critérios rígidos; Pós: população irrestrita (idosos, gestantes, polifarmacopáticos)</li>
                <li><strong>Duração:</strong> ECT: semanas-meses; Pós: anos-décadas</li>
                <li><strong>Eventos Raros:</strong> ECT pode não detectar RAM com freq. 1:10.000</li>
            </ul>

            <div class=""callout warning"">
                <div class=""callout-title"">⚠️ Histórico</div>
                <p><strong>Talidomida (1955-1961):</strong> Aprovada para enjoo gestacional. Causa: Teratogenia severa (focomelia). 10.000+ crianças afetadas antes da retirada. Lição: aprovação baseada em testes inadequados em gestantes.</p>
            </div>

            <h3>2. Notificação de RAM</h3>
            <p><strong>Obrigação Legal:</strong> Todo profissional de saúde deve notificar reações adversas suspeitas.</p>
            <p><strong>Sistema Brasileiro:</strong></p>
            <ul>
                <li><strong>ANVISA:</strong> Sistema Nacional de Vigilância Sanitária (www.anvisa.gov.br)</li>
                <li><strong>Formulário:</strong> Notificado de forma eletrônica através de sistema específico</li>
                <li><strong>Confidencialidade:</strong> Dados do paciente protegidos</li>
                <li><strong>Feedback:</strong> ANVISA analisa e divulga alertas se necessário</li>
            </ul>

            <h3>3. Causalidade em RAM</h3>
            <p>Algoritmo de Naranjo (0-13 pontos):</p>
            <table>
                <thead>
                    <tr>
                        <th>Pontuação</th>
                        <th>Categoria</th>
                        <th>Interpretação</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>9-13</td>
                        <td>Definida</td>
                        <td>Forte evidência causal</td>
                    </tr>
                    <tr>
                        <td>5-8</td>
                        <td>Provável</td>
                        <td>Relação causa-efeito provável</td>
                    </tr>
                    <tr>
                        <td>1-4</td>
                        <td>Possível</td>
                        <td>Relação não pode ser excluída</td>
                    </tr>
                    <tr>
                        <td>0 ou minus</td>
                        <td>Improvável</td>
                        <td>Outra explicação mais provável</td>
                    </tr>
                </tbody>
            </table>

            <p><strong>Exemplo:</strong> Paciente inicia atorvastatina → 2 semanas depois apresenta mialgia (dor muscular) → CK elevado. Score Naranjo = 7 (provável relatado estatina).</p>
        </section>";
    }

    private static string GenerateEpidemiologyContent(string title, int order)
    {
        return $@"
        <section class=""content-section animate-fade-in"">
            <h2>Farmacoepidemiologia: Medicamentos em Populações</h2>
            <p><strong>Farmacoepidemiologia</strong> aplica metodologia epidemiológica para estudar uso, efetividade e segurança de medicamentos em grandes populações.</p>
            
            <h3>1. Delineamentos de Estudo</h3>
            <ul>
                <li><strong>Transversal:</strong> “Fotografia” em um momento. Ex: Qual % da população usa estatinas?</li>
                <li><strong>Caso-Controle:</strong> Comparação retrospectiva. Ex: Usuários de ACO têm maior risco de tromboembolismo? (Raro)</li>
                <li><strong>Coorte:</strong> Acompanhamento prospectivo. Ex: Seguir 10.000 usuários de estatinas por 5 anos para eventos CV</li>
                <li><strong>Ensaio Clínico Randomizado:</strong> Gold standard. Ex: ALLHAT (14.000 hipertensos randomizados a diferentes anti-HAS)</li>
            </ul>

            <h3>2. Utilização Irracional de Medicamentos</h3>
            <p>Problemas globais identificados por OMS (2019):</p>
            <ul>
                <li><strong>Uso excessivo:</strong> Prescrita dose > que necessária. Ex: Antiácidos para azia ocasional</li>
                <li><strong>Subutilização:</strong> Paciente adequado recebe dose inadequada. Ex: Diabético com A1c=10% em dose baixa metformina</li>
                <li><strong>Uso inapropriado:</strong> Medicação para indicação não apropriada. Ex: Antibiótico para resfriado (90% viral)</li>
                <li><strong>Falta de uso:</strong> Paciente não começa medicação essencial. Ex: Recusa estatina por medo</li>
            </ul>

            <div class=""callout info"">
                <div class=""callout-title"">📊 Resistência Bacteriana</div>
                <p><strong>Custo Global:</strong> 700.000-1.000.000 mortes/ano por infecções resistentes. Uso irracional de antibióticos é principal driver. Estratégia: Prescrever apenas quando indicado, duração apropriada, desescalar conforme possível.</p>
            </div>

            <h3>3. Estudos Observacionais Pós-comercialização</h3>
            <p>Usando dados administrativos/eletrônicos:</p>
            <ul>
                <li><strong>Análise de Claims:</strong> Banco de dados de seguros. Milhões de pacientes, baixo custo investigativo</li>
                <li><strong>Registros Eletrônicos:</strong> EHR de hospitais/clínicas. Riqueza de dados clínicos</li>
                <li><strong>Ensaios Pragmáticos:</strong> Ensaio clínico em prática real (menos critérios exclusão) → maior validade externa</li>
            </ul>

            <p><strong>Vantagem:</strong> Efeitos em subpopulações raras ou idosos (frequentemente exclusos de ECT) podem ser detectados.</p>
        </section>";
    }

    private static string GeneratePharmacotheraphyContent(string title, int order)
    {
        return $@"
        <section class=""content-section animate-fade-in"">
            <h2>Farmacoterapia: Uso Racional de Medicamentos</h2>
            <p><strong>Farmacoterapia</strong> é aplicação sistemática de conhecimento farmacológico para tratamento de doenças. Requer integração de Farmacologia + Patologia + Clínica.</p>
            
            <h3>1. Processo de Seleção de Fármaco</h3>
            <ol>
                <li><strong>Definição do problema:</strong> Diagnóstico preciso (não sintomas genéricos)</li>
                <li><strong>Estabelecer objetivo:</strong> Cura? Sintomático? Preventivo?</li>
                <li><strong>Terapêutica não-farmacológica:</strong> Exercício, dieta, psicoterapia sempre primeiro</li>
                <li><strong>Seleção de classe de fármaco:</strong> Baseada em mecanismo de ação e evidência</li>
                <li><strong>Seleção de fármaco específico:</strong> Considerando:
                    <ul>
                        <li>Potência e eficácia</li>
                        <li>Perfil de segurança</li>
                        <li>Farmacocinética (meia-vida, eliminação)</li>
                        <li>Interações drug-drug e drug-food</li>
                        <li>Custo-benefício</li>
                        <li>Preferência e adesão do paciente</li>
                    </ul>
                </li>
                <li><strong>Determinação de dose:</strong> Usual, ajustada por peso/função renal</li>
                <li><strong>Determinação de frequência:</strong> Baseada na meia-vida</li>
                <li><strong>Duração:</strong> Mínima necessária para objetivo terapêutico</li>
                <li><strong>Monitoramento:</strong> Eficácia clínica + RAM + aderência</li>
            </ol>

            <div class=""callout success"">
                <div class=""callout-title"">✓ Exemplo: Hipertensão Arterial Sistêmica</div>
                <p><strong>Paciente:</strong> Homem 55a, PA=150/95, CLcr=70, sem DM. Sem comorbidades. <br>
                <strong>Decisão:</strong> (1) Modificações de estilo de vida (se falhar) → (2) Monoterapia com IECA/ARA-II (cardoprotetores em qualquer hipertenso) → (3) Se PA persistir >140, adicionar diurético tiazídico → (4) Se <30% resposta, trocar por BCC → (5) Reavalia a cada 4 semanas até controle (PA <130/80)</p>
            </div>

            <h3>2. Ajustes Especiais de Dose</h3>
            <p><strong>Insuficiência Renal:</strong> Fármacos podem acumular. Ajuste por eGFR (Chronic Kidney Disease-EPI):</p>
            <ul>
                <li>eGFR >60: Sem ajuste</li>
                <li>eGFR 45-59: Ajuste em alguns fármacos</li>
                <li>eGFR 30-44: Ajuste em maioria</li>
                <li>eGFR <30: Ajuste substancial ou contraindicado</li>
            </ul>

            <p><strong>Insuficiência Hepática:</strong> Redução de síntese de albumina e enzimas → reduzir dose</p>

            <p><strong>Idosos:</strong> Reduzir dose inicial em ~25-50% (metabolism lento, comorbidades, polifarmácia)</p>

            <h3>3. Adesão à Farmacoterapia</h3>
            <p>Problema global: Apenas ~30% de pacientes crônicos aderem perfeitamente:</p>
            <ul>
                <li><strong>Estratégias:</strong>
                    <ul>
                        <li>Educação sobre importância da medicação</li>
                        <li>Simplificar esquema (fewer pills, dosing frequency)</li>
                        <li>Formulários OD em vez BID/TID</li>
                        <li>Blister com memória do dia/hora</li>
                        <li>Aplicativos lembradores</li>
                        <li>Reembolso por plano de saúde (remove barreira financeira)</li>
                    </ul>
                </li>
                <li><strong>Monitoramento:</strong> Perguntar frequentemente se está tomando; contar comprimidos restantes</li>
            </ul>
        </section>";
    }

    private static string GenerateDefaultContetent(string topicName, string documentTitle, int order)
    {
        return $@"
        <section class=""content-section animate-fade-in"">
            <h2>{System.Net.WebUtility.HtmlEncode(documentTitle)}</h2>
            <p>Bem-vindo ao módulo de aprendizado sobre <strong>{System.Net.WebUtility.HtmlEncode(documentTitle.ToLower())}</strong> em {System.Net.WebUtility.HtmlEncode(topicName)}.</p>
            
            <h3>1. Introdução Conceitual</h3>
            <p>Este tópico introduz conceitos fundamentais para formação acadêmica e profissional na área. Você aprenderá definições operacionais, aplicações práticas e conexões com outros campos do conhecimento.</p>

            <h3>2. Estrutura do Aprendizado</h3>
            <p>Este módulo está organizado para progressão gradual:</p>
            <ul>
                <li><strong>Conceitos Básicos:</strong> Definições e terminologia essencial</li>
                <li><strong>Desenvolvimento Teórico:</strong> Aprofundamento com exemplos práticos</li>
                <li><strong>Aplicações:</strong> Como usar este conhecimento em contexto real</li>
                <li><strong>Síntese:</strong> Resumo e pontos-chave para retenção</li>
            </ul>

            <h3>3. Objetivos de Aprendizado</h3>
            <p>Ao final deste módulo, você será capaz de:</p>
            <ul>
                <li>Definir corretamente termos-chave relacionados ao tema</li>
                <li>Explicar mecanismos e processos fundamentais</li>
                <li>Aplicar conceitos em cenários práticos</li>
                <li>Integrar conhecimento com áreas relacionadas</li>
                <li>Avaliar criticamente informações e fontes</li>
            </ul>

            <div class=""callout info"">
                <div class=""callout-title"">💡 Dica de Estudo</div>
                <p>Tome notas enquanto lê, destacando conceitos principais e conexões. Revise frequentemente. O SRS (Spaced Repetition System) melhora retenção de longa duração.</p>
            </div>

            <h3>4. Recursos Complementares</h3>
            <p>Para aprofundamento, consulte:</p>
            <ul>
                <li>Livros-referência da disciplina</li>
                <li>Artigos científicos atualizados</li>
                <li>Vídeos educacionais de fontes confiáveis</li>
                <li>Discussões em fórum com colegas e professores</li>
            </ul>

            <h3>5. Avaliação do Conhecimento</h3>
            <p>Ao final, teste seu conhecimento através de:</p>
            <ul>
                <li>Questões de múltipla escolha</li>
                <li>Discussão de casos</li>
                <li>Projetos de investigação</li>
                <li>Apresentações</li>
            </ul>

            <div class=""callout success"">
                <div class=""callout-title"">✓ Sucesso Garantido</div>
                <p>Estude sistematicamente, revise regularmente, procure ajuda quando necessário. Seu comprometimento com a aprendizagem resultar em compreensão profunda e durável.</p>
            </div>
        </section>";
    }
}
