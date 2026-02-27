import { useState, useCallback } from 'react';
import { RotateCcw, ChevronLeft, ChevronRight, Shuffle, BookOpen, Check, X } from 'lucide-react';

const DECKS = [
  {
    nome: 'Farmacologia Básica',
    cor: '#6366f1',
    cards: [
      { frente: 'O que é biodisponibilidade?', verso: 'Fração do fármaco administrado que atinge a circulação sistêmica na forma inalterada. Por via IV, a biodisponibilidade é 100%.' },
      { frente: 'O que é meia-vida (t½)?', verso: 'Tempo necessário para que a concentração plasmática do fármaco se reduza à metade. Determina o intervalo posológico.' },
      { frente: 'Diferença entre agonista e antagonista?', verso: 'Agonista: liga-se ao receptor e ativa resposta. Antagonista: liga-se ao receptor e bloqueia a ação do agonista, sem gerar resposta.' },
      { frente: 'O que é efeito de primeira passagem?', verso: 'Metabolização do fármaco pelo fígado (e intestino) antes de atingir a circulação sistêmica, reduzindo a biodisponibilidade oral.' },
      { frente: 'Cite 3 fármacos inibidores do CYP3A4.', verso: 'Cetoconazol, eritromicina e ritonavir. Eles aumentam os níveis plasmáticos de fármacos metabolizados por esta enzima.' },
      { frente: 'O que é índice terapêutico?', verso: 'Relação entre DL50/DE50. Quanto maior o índice, mais seguro é o fármaco. Fármacos com IT baixo (ex: digoxina, lítio) requerem monitorização.' },
    ],
  },
  {
    nome: 'Química Farmacêutica',
    cor: '#8b5cf6',
    cards: [
      { frente: 'O que é um farmacóforo?', verso: 'Conjunto mínimo de características moleculares necessárias para o reconhecimento molecular por um alvo biológico e atividade farmacológica.' },
      { frente: 'O que é bioisosterismo?', verso: 'Substituição de grupos químicos por outros com propriedades eletrônicas/estéricas semelhantes, mantendo atividade biológica. Ex: -COOH ↔ tetrazol.' },
      { frente: 'Diferença entre pró-fármaco e fármaco latente?', verso: 'Pró-fármaco: forma inativa que é convertida in vivo no fármaco ativo (ex: enalapril → enalaprilato). São termos geralmente sinônimos.' },
      { frente: 'O que é SAR?', verso: 'Structure-Activity Relationship: estudo da relação entre a estrutura química e a atividade biológica, essencial para otimização de fármacos.' },
    ],
  },
  {
    nome: 'Farmácia Clínica',
    cor: '#ec4899',
    cards: [
      { frente: 'O que é reconciliação medicamentosa?', verso: 'Processo de obter a lista completa de medicamentos do paciente em cada transição de cuidado para evitar discrepâncias (omissão, duplicidade, dose errada).' },
      { frente: 'O que é PRM (Problema Relacionado a Medicamento)?', verso: 'Evento indesejado do paciente relacionado à farmacoterapia, que interfere nos resultados desejados. Ex: reação adversa, interação, dose inadequada.' },
      { frente: 'Diferença entre RAM tipo A e tipo B?', verso: 'Tipo A (aumentada): dose-dependente, previsível, ex: hipoglicemia por insulina. Tipo B (bizarra): dose-independente, imprevisível, ex: anafilaxia por penicilina.' },
      { frente: 'O que é Atenção Farmacêutica?', verso: 'Provisão responsável da farmacoterapia com o propósito de alcançar resultados definidos que melhorem a qualidade de vida do paciente (Hepler & Strand, 1990).' },
      { frente: 'Cite 3 medicamentos de alta vigilância.', verso: 'Insulinas, anticoagulantes (heparina/varfarina) e eletrólitos concentrados (KCl IV). Requerem protocolos especiais de segurança.' },
    ],
  },
  {
    nome: 'Legislação Farmacêutica',
    cor: '#f59e0b',
    cards: [
      { frente: 'Qual a diferença entre receita branca e azul?', verso: 'Receita Branca: medicamentos de controle especial (lista C1). Receita Azul (Notificação B): benzodiazepínicos e análogos (lista B1/B2).' },
      { frente: 'O que é a Notificação de Receita Amarela?', verso: 'Exigida para medicamentos entorpecentes (lista A1/A2) e psicotrópicos (lista A3). Válida por 30 dias e para tratamento máximo de 30 dias.' },
      { frente: 'O que é o SNGPC?', verso: 'Sistema Nacional de Gerenciamento de Produtos Controlados. Farmácias enviam eletronicamente à ANVISA dados de movimentação de psicotrópicos e antimicrobianos.' },
      { frente: 'Quanto tempo vale a receita de antimicrobiano?', verso: '10 dias a partir da data de prescrição (RDC 20/2011). A dispensação deve reter a segunda via.' },
    ],
  },
];

export default function FlashcardsPage() {
  const [deckIdx, setDeckIdx] = useState(null);
  const [cardIdx, setCardIdx] = useState(0);
  const [flipped, setFlipped] = useState(false);
  const [score, setScore] = useState({ acertos: 0, erros: 0 });
  const [finished, setFinished] = useState(false);
  const [shuffled, setShuffled] = useState([]);

  const startDeck = useCallback((idx) => {
    setDeckIdx(idx);
    setCardIdx(0);
    setFlipped(false);
    setScore({ acertos: 0, erros: 0 });
    setFinished(false);
    const cards = [...DECKS[idx].cards];
    for (let i = cards.length - 1; i > 0; i--) { const j = Math.floor(Math.random() * (i + 1)); [cards[i], cards[j]] = [cards[j], cards[i]]; }
    setShuffled(cards);
  }, []);

  const advance = (acertou) => {
    setScore((s) => acertou ? { ...s, acertos: s.acertos + 1 } : { ...s, erros: s.erros + 1 });
    if (cardIdx + 1 >= shuffled.length) { setFinished(true); }
    else { setCardIdx((i) => i + 1); setFlipped(false); }
  };

  // Deck selection
  if (deckIdx === null) {
    return (
      <div className="p-6 lg:p-8 max-w-4xl mx-auto space-y-6">
        <div>
          <h1 className="text-2xl font-bold text-white flex items-center gap-3">
            <RotateCcw className="w-7 h-7 text-indigo-400" /> Flashcards
          </h1>
          <p className="text-slate-400 text-sm mt-1">Escolha um baralho para revisar com repetição ativa.</p>
        </div>
        <div className="grid sm:grid-cols-2 gap-4">
          {DECKS.map((d, i) => (
            <button key={i} onClick={() => startDeck(i)}
              className="text-left rounded-2xl bg-slate-800/50 border border-slate-700/50 p-6 hover:border-slate-600/50 transition group cursor-pointer">
              <div className="w-10 h-10 rounded-xl flex items-center justify-center mb-3" style={{ backgroundColor: d.cor + '22' }}>
                <BookOpen className="w-5 h-5" style={{ color: d.cor }} />
              </div>
              <h3 className="text-white font-semibold group-hover:text-indigo-400 transition">{d.nome}</h3>
              <p className="text-slate-500 text-sm mt-1">{d.cards.length} cards</p>
            </button>
          ))}
        </div>
      </div>
    );
  }

  const deck = DECKS[deckIdx];
  const card = shuffled[cardIdx];
  const progress = ((cardIdx + (finished ? 1 : 0)) / shuffled.length) * 100;

  // Finished
  if (finished) {
    const total = score.acertos + score.erros;
    const pct = Math.round((score.acertos / total) * 100);
    return (
      <div className="p-6 lg:p-8 max-w-2xl mx-auto space-y-6 text-center">
        <h2 className="text-2xl font-bold text-white">Resultado</h2>
        <p className="text-slate-400">{deck.nome}</p>
        <div className="text-6xl font-bold mt-4" style={{ color: pct >= 70 ? '#22c55e' : pct >= 40 ? '#f59e0b' : '#ef4444' }}>
          {pct}%
        </div>
        <div className="flex justify-center gap-8 mt-4">
          <div className="text-center">
            <p className="text-3xl font-bold text-green-400">{score.acertos}</p>
            <p className="text-xs text-slate-500">Acertos</p>
          </div>
          <div className="text-center">
            <p className="text-3xl font-bold text-red-400">{score.erros}</p>
            <p className="text-xs text-slate-500">Erros</p>
          </div>
        </div>
        <div className="flex justify-center gap-3 mt-6">
          <button onClick={() => startDeck(deckIdx)}
            className="flex items-center gap-2 px-5 py-2.5 bg-indigo-500 text-white rounded-xl font-medium transition hover:bg-indigo-600 cursor-pointer">
            <Shuffle className="w-4 h-4" /> Repetir
          </button>
          <button onClick={() => setDeckIdx(null)}
            className="px-5 py-2.5 border border-slate-700 text-slate-300 rounded-xl transition hover:text-white cursor-pointer">
            Outros Baralhos
          </button>
        </div>
      </div>
    );
  }

  // Card view
  return (
    <div className="p-6 lg:p-8 max-w-2xl mx-auto space-y-6">
      {/* Top bar */}
      <div className="flex items-center justify-between">
        <button onClick={() => setDeckIdx(null)} className="text-slate-400 hover:text-white text-sm cursor-pointer">← Baralhos</button>
        <span className="text-sm text-slate-400">{cardIdx + 1} / {shuffled.length}</span>
      </div>

      {/* Progress */}
      <div className="w-full bg-slate-800 rounded-full h-1.5">
        <div className="h-1.5 rounded-full transition-all duration-500" style={{ width: `${progress}%`, backgroundColor: deck.cor }} />
      </div>

      {/* Card */}
      <button onClick={() => setFlipped(!flipped)}
        className="w-full min-h-[300px] rounded-2xl border border-slate-700/50 p-8 flex flex-col items-center justify-center text-center transition-all duration-300 cursor-pointer"
        style={{ backgroundColor: flipped ? deck.cor + '11' : 'rgb(30 41 59 / 0.5)', borderColor: flipped ? deck.cor + '44' : undefined }}>
        <span className="text-xs font-medium mb-4 px-3 py-1 rounded-full"
          style={{ backgroundColor: deck.cor + '22', color: deck.cor }}>
          {flipped ? 'Resposta' : 'Pergunta'}
        </span>
        <p className={`text-lg leading-relaxed ${flipped ? 'text-slate-200' : 'text-white font-medium'}`}>
          {flipped ? card.verso : card.frente}
        </p>
        {!flipped && <p className="text-xs text-slate-500 mt-6">Clique para ver a resposta</p>}
      </button>

      {/* Actions */}
      {flipped && (
        <div className="flex justify-center gap-4">
          <button onClick={() => advance(false)}
            className="flex items-center gap-2 px-6 py-3 bg-red-500/10 text-red-400 border border-red-500/20 rounded-xl font-medium hover:bg-red-500/20 transition cursor-pointer">
            <X className="w-5 h-5" /> Errei
          </button>
          <button onClick={() => advance(true)}
            className="flex items-center gap-2 px-6 py-3 bg-green-500/10 text-green-400 border border-green-500/20 rounded-xl font-medium hover:bg-green-500/20 transition cursor-pointer">
            <Check className="w-5 h-5" /> Acertei
          </button>
        </div>
      )}

      {/* Score */}
      <div className="flex justify-center gap-6 text-sm text-slate-500">
        <span className="text-green-400">{score.acertos} acertos</span>
        <span className="text-red-400">{score.erros} erros</span>
      </div>
    </div>
  );
}
