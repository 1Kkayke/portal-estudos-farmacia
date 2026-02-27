import { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import api from '../services/api';
import { getIcon } from '../utils/icons';
import {
  CheckCircle2, XCircle, ArrowLeft, Loader2, ChevronRight,
  BookOpen, GraduationCap, HelpCircle, Search, RotateCcw,
  Eye, EyeOff, Award, Filter,
} from 'lucide-react';

const diffColor = {
  'Fácil': 'bg-green-500/10 text-green-400 border-green-500/20',
  'Médio': 'bg-yellow-500/10 text-yellow-400 border-yellow-500/20',
  'Difícil': 'bg-red-500/10 text-red-400 border-red-500/20',
};

const optionLabels = ['A', 'B', 'C', 'D'];

export default function QuestionsPage() {
  const { topicId } = useParams();
  const [questions, setQuestions] = useState([]);
  const [topic, setTopic] = useState(null);
  const [loading, setLoading] = useState(true);
  const [currentIdx, setCurrentIdx] = useState(0);
  const [answers, setAnswers] = useState({});
  const [revealed, setRevealed] = useState({});
  const [mode, setMode] = useState('practice'); // 'practice' | 'review'
  const [filterDiff, setFilterDiff] = useState('all');

  useEffect(() => {
    const load = async () => {
      try {
        const [qRes, tRes] = await Promise.all([
          api.get(`/topics/${topicId}/questions/review`),
          api.get('/topics'),
        ]);
        setQuestions(qRes.data);
        setTopic(tRes.data.find(t => t.id === parseInt(topicId)));
      } catch (err) {
        console.error(err);
      } finally {
        setLoading(false);
      }
    };
    load();
  }, [topicId]);

  const filtered = filterDiff === 'all'
    ? questions
    : questions.filter(q => q.dificuldade === filterDiff);

  const current = filtered[currentIdx];
  const totalAnswered = Object.keys(answers).length;
  const totalCorrect = Object.values(answers).filter(a => a.correct).length;

  const handleAnswer = async (questionId, optionIdx) => {
    if (answers[questionId]) return; // already answered
    try {
      const res = await api.post(`/topics/${topicId}/questions/${questionId}/check`, {
        respostaEscolhida: optionIdx,
      });
      setAnswers(prev => ({
        ...prev,
        [questionId]: { chosen: optionIdx, ...res.data },
      }));
      setRevealed(prev => ({ ...prev, [questionId]: true }));
    } catch (err) {
      console.error(err);
    }
  };

  const reset = () => {
    setAnswers({});
    setRevealed({});
    setCurrentIdx(0);
  };

  if (loading) {
    return (
      <div className="flex items-center justify-center min-h-screen">
        <Loader2 className="w-8 h-8 animate-spin text-indigo-400" />
      </div>
    );
  }

  const TopicIcon = topic ? getIcon(topic.icone) : HelpCircle;

  return (
    <div className="p-6 max-w-4xl mx-auto animate-fadeIn">
      {/* Header */}
      <div className="flex items-center gap-3 mb-6">
        <Link to={`/topics/${topicId}/documentos`} className="text-slate-400 hover:text-white transition">
          <ArrowLeft className="w-5 h-5" />
        </Link>
        <div className="flex items-center justify-center w-10 h-10 rounded-xl bg-amber-500/10 border border-amber-500/20">
          <TopicIcon className="w-5 h-5 text-amber-400" />
        </div>
        <div>
          <h1 className="text-xl font-bold text-white">{topic?.nome}</h1>
          <p className="text-xs text-slate-500">Questões de Estudo — {filtered.length} questões</p>
        </div>
      </div>

      {/* Stats bar */}
      <div className="flex items-center gap-4 mb-6 flex-wrap">
        <div className="flex items-center gap-2 px-3 py-1.5 bg-slate-800/50 rounded-lg border border-slate-700/50 text-xs">
          <BookOpen className="w-3.5 h-3.5 text-indigo-400" />
          <span className="text-slate-400">Respondidas:</span>
          <span className="text-white font-bold">{totalAnswered}/{filtered.length}</span>
        </div>
        <div className="flex items-center gap-2 px-3 py-1.5 bg-slate-800/50 rounded-lg border border-slate-700/50 text-xs">
          <CheckCircle2 className="w-3.5 h-3.5 text-green-400" />
          <span className="text-slate-400">Acertos:</span>
          <span className="text-green-400 font-bold">{totalCorrect}</span>
        </div>
        <div className="flex items-center gap-2 px-3 py-1.5 bg-slate-800/50 rounded-lg border border-slate-700/50 text-xs">
          <XCircle className="w-3.5 h-3.5 text-red-400" />
          <span className="text-slate-400">Erros:</span>
          <span className="text-red-400 font-bold">{totalAnswered - totalCorrect}</span>
        </div>

        {/* Filter */}
        <select
          value={filterDiff}
          onChange={e => { setFilterDiff(e.target.value); setCurrentIdx(0); }}
          className="ml-auto px-3 py-1.5 bg-slate-800/50 rounded-lg border border-slate-700/50 text-xs text-white focus:outline-none cursor-pointer"
        >
          <option value="all">Todas</option>
          <option value="Fácil">Fácil</option>
          <option value="Médio">Médio</option>
          <option value="Difícil">Difícil</option>
        </select>

        <button onClick={reset} className="flex items-center gap-1.5 px-3 py-1.5 bg-slate-800/50 rounded-lg border border-slate-700/50 text-xs text-slate-400 hover:text-white transition cursor-pointer">
          <RotateCcw className="w-3.5 h-3.5" /> Reiniciar
        </button>
      </div>

      {/* Quick links */}
      <div className="flex gap-2 mb-6 flex-wrap">
        <Link
          to={`/topics/${topicId}/documentos`}
          className="flex items-center gap-2 px-4 py-2 bg-indigo-500/10 rounded-xl border border-indigo-500/20 text-indigo-400 text-sm hover:bg-indigo-500/20 transition"
        >
          <BookOpen className="w-4 h-4" /> Documentos
        </Link>
        <Link
          to={`/topics/${topicId}/prova`}
          className="flex items-center gap-2 px-4 py-2 bg-emerald-500/10 rounded-xl border border-emerald-500/20 text-emerald-400 text-sm hover:bg-emerald-500/20 transition"
        >
          <GraduationCap className="w-4 h-4" /> Fazer Prova
        </Link>
      </div>

      {/* Progress bar */}
      <div className="w-full h-1.5 bg-slate-800 rounded-full mb-6 overflow-hidden">
        <div
          className="h-full bg-gradient-to-r from-indigo-500 to-purple-500 rounded-full transition-all duration-500"
          style={{ width: `${filtered.length > 0 ? ((currentIdx + 1) / filtered.length) * 100 : 0}%` }}
        />
      </div>

      {/* Question card */}
      {current ? (
        <div className="bg-slate-800/50 rounded-2xl border border-slate-700/50 p-6 md:p-8">
          {/* Question header */}
          <div className="flex items-center justify-between mb-4">
            <span className="text-xs text-slate-500">Questão {currentIdx + 1} de {filtered.length}</span>
            <span className={`px-2.5 py-0.5 text-[10px] font-medium rounded-full border ${diffColor[current.dificuldade] || diffColor['Médio']}`}>
              {current.dificuldade}
            </span>
          </div>

          {/* Enunciado */}
          <h2 className="text-white font-medium text-base leading-relaxed mb-6">
            {current.enunciado}
          </h2>

          {/* Options */}
          <div className="space-y-3">
            {current.opcoes?.map((opt, i) => {
              const answered = answers[current.id];
              const isChosen = answered?.chosen === opt.indice;
              const isCorrect = answered && opt.indice === answered.respostaCorreta;
              const showResult = revealed[current.id];

              let borderClass = 'border-slate-700/50 hover:border-indigo-500/30';
              let bgClass = 'bg-slate-800/30 hover:bg-slate-800/60';

              if (showResult && isCorrect) {
                borderClass = 'border-green-500/50';
                bgClass = 'bg-green-500/10';
              } else if (showResult && isChosen && !answered.correta) {
                borderClass = 'border-red-500/50';
                bgClass = 'bg-red-500/10';
              }

              return (
                <button
                  key={opt.id}
                  onClick={() => handleAnswer(current.id, opt.indice)}
                  disabled={!!answered}
                  className={`w-full flex items-center gap-3 p-4 rounded-xl border transition text-left cursor-pointer ${borderClass} ${bgClass} ${answered ? 'cursor-default' : ''}`}
                >
                  <div className={`flex items-center justify-center w-8 h-8 rounded-lg text-sm font-bold shrink-0 ${
                    showResult && isCorrect ? 'bg-green-500/20 text-green-400' :
                    showResult && isChosen ? 'bg-red-500/20 text-red-400' :
                    'bg-slate-700/50 text-slate-400'
                  }`}>
                    {optionLabels[i]}
                  </div>
                  <span className={`text-sm ${
                    showResult && isCorrect ? 'text-green-300' :
                    showResult && isChosen ? 'text-red-300' :
                    'text-slate-300'
                  }`}>
                    {opt.texto}
                  </span>
                  {showResult && isCorrect && <CheckCircle2 className="w-5 h-5 text-green-400 ml-auto shrink-0" />}
                  {showResult && isChosen && !answered.correta && <XCircle className="w-5 h-5 text-red-400 ml-auto shrink-0" />}
                </button>
              );
            })}
          </div>

          {/* Explanation */}
          {revealed[current.id] && answers[current.id] && (
            <div className="mt-6 p-4 bg-indigo-500/5 rounded-xl border border-indigo-500/20">
              <div className="flex items-center gap-2 mb-2">
                <Award className="w-4 h-4 text-indigo-400" />
                <span className="text-sm font-medium text-indigo-400">Explicação</span>
              </div>
              <p className="text-slate-300 text-sm leading-relaxed">
                {answers[current.id].explicacao}
              </p>
            </div>
          )}

          {/* Navigation */}
          <div className="flex items-center justify-between mt-6 pt-4 border-t border-slate-700/50">
            <button
              onClick={() => setCurrentIdx(Math.max(0, currentIdx - 1))}
              disabled={currentIdx === 0}
              className="flex items-center gap-1.5 text-sm text-slate-400 hover:text-white disabled:opacity-30 disabled:hover:text-slate-400 transition cursor-pointer"
            >
              <ArrowLeft className="w-4 h-4" /> Anterior
            </button>
            <span className="text-xs text-slate-500">{currentIdx + 1}/{filtered.length}</span>
            <button
              onClick={() => setCurrentIdx(Math.min(filtered.length - 1, currentIdx + 1))}
              disabled={currentIdx === filtered.length - 1}
              className="flex items-center gap-1.5 text-sm text-indigo-400 hover:text-indigo-300 disabled:opacity-30 transition cursor-pointer"
            >
              Próxima <ChevronRight className="w-4 h-4" />
            </button>
          </div>
        </div>
      ) : (
        <div className="text-center py-12 text-slate-500">
          <HelpCircle className="w-12 h-12 mx-auto mb-3 opacity-50" />
          <p>Nenhuma questão encontrada para este filtro.</p>
        </div>
      )}

      {/* Question navigator */}
      <div className="mt-6 bg-slate-800/50 rounded-2xl border border-slate-700/50 p-4">
        <h3 className="text-xs font-medium text-slate-500 mb-3 uppercase tracking-wider">Navegador de Questões</h3>
        <div className="flex flex-wrap gap-2">
          {filtered.map((q, i) => {
            const answered = answers[q.id];
            const isCurrent = i === currentIdx;
            let cls = 'bg-slate-700/50 text-slate-400 border-slate-600/50';
            if (answered?.correta) cls = 'bg-green-500/20 text-green-400 border-green-500/30';
            else if (answered && !answered.correta) cls = 'bg-red-500/20 text-red-400 border-red-500/30';
            if (isCurrent) cls += ' ring-2 ring-indigo-500/50';
            return (
              <button
                key={q.id}
                onClick={() => setCurrentIdx(i)}
                className={`w-9 h-9 rounded-lg border text-xs font-bold transition cursor-pointer ${cls}`}
              >
                {i + 1}
              </button>
            );
          })}
        </div>
      </div>
    </div>
  );
}
