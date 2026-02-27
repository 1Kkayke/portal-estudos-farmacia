import { useState, useEffect, useRef, useCallback } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import api from '../services/api';
import { getIcon } from '../utils/icons';
import {
  GraduationCap, Clock, CheckCircle2, XCircle, ArrowLeft,
  Loader2, Award, AlertTriangle, Play, Send, RotateCcw,
  BookOpen, FileText, Trophy, Target, BarChart3,
} from 'lucide-react';

const optionLabels = ['A', 'B', 'C', 'D'];

export default function ExamPage() {
  const { topicId } = useParams();
  const navigate = useNavigate();
  const [topic, setTopic] = useState(null);
  const [examState, setExamState] = useState('setup'); // 'setup' | 'exam' | 'result' | 'history'
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  // Setup
  const [totalQ, setTotalQ] = useState(20);

  // Exam state
  const [attempt, setAttempt] = useState(null);
  const [questions, setQuestions] = useState([]);
  const [currentIdx, setCurrentIdx] = useState(0);
  const [selectedAnswers, setSelectedAnswers] = useState({});
  const [timeLeft, setTimeLeft] = useState(0);
  const timerRef = useRef(null);

  // Result
  const [result, setResult] = useState(null);

  // History
  const [history, setHistory] = useState([]);

  useEffect(() => {
    const load = async () => {
      try {
        const [topicsRes, historyRes] = await Promise.all([
          api.get('/topics'),
          api.get(`/exams/history?topicId=${topicId}`),
        ]);
        setTopic(topicsRes.data.find(t => t.id === parseInt(topicId)));
        setHistory(historyRes.data);
      } catch (err) {
        console.error(err);
      }
    };
    load();
  }, [topicId]);

  // Timer
  useEffect(() => {
    if (examState === 'exam' && timeLeft > 0) {
      timerRef.current = setInterval(() => {
        setTimeLeft(prev => {
          if (prev <= 1) {
            clearInterval(timerRef.current);
            handleSubmit();
            return 0;
          }
          return prev - 1;
        });
      }, 1000);
      return () => clearInterval(timerRef.current);
    }
  }, [examState, timeLeft > 0]);

  const formatTime = (secs) => {
    const m = Math.floor(secs / 60);
    const s = secs % 60;
    return `${String(m).padStart(2, '0')}:${String(s).padStart(2, '0')}`;
  };

  const startExam = async () => {
    setLoading(true);
    setError('');
    try {
      const res = await api.post('/exams/start', {
        topicId: parseInt(topicId),
        totalQuestoes: totalQ,
      });
      setAttempt(res.data);
      setQuestions(res.data.questoes);
      setTimeLeft(res.data.tempoMinutos * 60);
      setSelectedAnswers({});
      setCurrentIdx(0);
      setExamState('exam');
    } catch (err) {
      setError('Erro ao iniciar prova. Tente novamente.');
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = useCallback(async () => {
    if (timerRef.current) clearInterval(timerRef.current);
    setLoading(true);
    try {
      const respostas = questions.map(q => ({
        questionId: q.id,
        respostaEscolhida: selectedAnswers[q.id] ?? -1,
      }));
      const res = await api.post('/exams/submit', {
        attemptId: attempt.attemptId,
        respostas,
      });
      setResult(res.data);
      setExamState('result');
      // Refresh history
      const histRes = await api.get(`/exams/history?topicId=${topicId}`);
      setHistory(histRes.data);
    } catch (err) {
      setError('Erro ao submeter prova.');
    } finally {
      setLoading(false);
    }
  }, [questions, selectedAnswers, attempt, topicId]);

  const selectOption = (questionId, optIdx) => {
    setSelectedAnswers(prev => ({ ...prev, [questionId]: optIdx }));
  };

  const TopicIcon = topic ? getIcon(topic?.icone) : GraduationCap;

  // ── SETUP VIEW ──
  if (examState === 'setup') {
    return (
      <div className="p-6 max-w-4xl mx-auto animate-fadeIn">
        <div className="flex items-center gap-3 mb-6">
          <Link to={`/topics/${topicId}/documentos`} className="text-slate-400 hover:text-white transition">
            <ArrowLeft className="w-5 h-5" />
          </Link>
          <div className="flex items-center justify-center w-10 h-10 rounded-xl bg-emerald-500/10 border border-emerald-500/20">
            <TopicIcon className="w-5 h-5 text-emerald-400" />
          </div>
          <div>
            <h1 className="text-xl font-bold text-white">{topic?.nome}</h1>
            <p className="text-xs text-slate-500">Sistema de Provas Online</p>
          </div>
        </div>

        {/* Quick links */}
        <div className="flex gap-2 mb-6 flex-wrap">
          <Link
            to={`/topics/${topicId}/documentos`}
            className="flex items-center gap-2 px-4 py-2 bg-indigo-500/10 rounded-xl border border-indigo-500/20 text-indigo-400 text-sm hover:bg-indigo-500/20 transition"
          >
            <FileText className="w-4 h-4" /> Documentos
          </Link>
          <Link
            to={`/topics/${topicId}/questoes`}
            className="flex items-center gap-2 px-4 py-2 bg-amber-500/10 rounded-xl border border-amber-500/20 text-amber-400 text-sm hover:bg-amber-500/20 transition"
          >
            <BookOpen className="w-4 h-4" /> Questões
          </Link>
        </div>

        {/* Exam config card */}
        <div className="bg-slate-800/50 rounded-2xl border border-slate-700/50 p-8 mb-6">
          <div className="flex items-center gap-3 mb-6">
            <div className="flex items-center justify-center w-12 h-12 rounded-xl bg-gradient-to-br from-emerald-500 to-teal-600 shadow-lg">
              <GraduationCap className="w-6 h-6 text-white" />
            </div>
            <div>
              <h2 className="text-lg font-bold text-white">Configurar Prova</h2>
              <p className="text-sm text-slate-400">Configure e inicie a prova de {topic?.nome}</p>
            </div>
          </div>

          <div className="space-y-4">
            <div>
              <label className="text-sm text-slate-400 block mb-2">Quantidade de Questões</label>
              <div className="flex gap-2 flex-wrap">
                {[10, 20, 30, 40].map(n => (
                  <button
                    key={n}
                    onClick={() => setTotalQ(n)}
                    className={`px-4 py-2 rounded-xl border text-sm font-medium transition cursor-pointer ${
                      totalQ === n
                        ? 'bg-emerald-500/10 text-emerald-400 border-emerald-500/30'
                        : 'bg-slate-800/50 text-slate-400 border-slate-700/50 hover:border-slate-600'
                    }`}
                  >
                    {n} questões
                  </button>
                ))}
              </div>
            </div>

            <div className="flex items-center gap-4 text-sm text-slate-400 pt-2">
              <span className="flex items-center gap-1.5">
                <Clock className="w-4 h-4" /> Tempo: 60 min
              </span>
              <span className="flex items-center gap-1.5">
                <Target className="w-4 h-4" /> Aprovação: 60%
              </span>
            </div>
          </div>

          {error && (
            <div className="mt-4 flex items-center gap-2 text-red-400 text-sm">
              <AlertTriangle className="w-4 h-4" /> {error}
            </div>
          )}

          <button
            onClick={startExam}
            disabled={loading}
            className="mt-6 w-full flex items-center justify-center gap-2 py-3 bg-gradient-to-r from-emerald-500 to-teal-600 text-white font-medium rounded-xl hover:opacity-90 transition disabled:opacity-50 cursor-pointer"
          >
            {loading ? <Loader2 className="w-5 h-5 animate-spin" /> : <Play className="w-5 h-5" />}
            {loading ? 'Gerando prova...' : 'Iniciar Prova'}
          </button>
        </div>

        {/* History */}
        {history.length > 0 && (
          <div className="bg-slate-800/50 rounded-2xl border border-slate-700/50 p-6">
            <h3 className="text-sm font-medium text-slate-400 mb-4 flex items-center gap-2">
              <BarChart3 className="w-4 h-4" /> Histórico de Provas
            </h3>
            <div className="space-y-2">
              {history.map(h => (
                <div key={h.id} className="flex items-center gap-4 p-3 bg-slate-800/50 rounded-xl border border-slate-700/30">
                  <div className={`flex items-center justify-center w-10 h-10 rounded-lg font-bold text-sm ${
                    h.nota >= 60 ? 'bg-green-500/10 text-green-400' : 'bg-red-500/10 text-red-400'
                  }`}>
                    {Math.round(h.nota)}%
                  </div>
                  <div className="flex-1 min-w-0">
                    <p className="text-sm text-white">{h.acertos}/{h.totalQuestoes} acertos</p>
                    <p className="text-xs text-slate-500">
                      {new Date(h.dataFim).toLocaleDateString('pt-BR')} às {new Date(h.dataFim).toLocaleTimeString('pt-BR', { hour: '2-digit', minute: '2-digit' })}
                    </p>
                  </div>
                  {h.nota >= 60 ? (
                    <Trophy className="w-5 h-5 text-yellow-400" />
                  ) : (
                    <XCircle className="w-5 h-5 text-red-400 opacity-50" />
                  )}
                </div>
              ))}
            </div>
          </div>
        )}
      </div>
    );
  }

  // ── EXAM VIEW ──
  if (examState === 'exam') {
    const current = questions[currentIdx];
    const answeredCount = Object.keys(selectedAnswers).length;
    const timePercent = attempt?.tempoMinutos ? (timeLeft / (attempt.tempoMinutos * 60)) * 100 : 100;

    return (
      <div className="p-6 max-w-4xl mx-auto animate-fadeIn">
        {/* Top bar */}
        <div className="flex items-center justify-between mb-6 p-4 bg-slate-800/50 rounded-xl border border-slate-700/50">
          <div className="flex items-center gap-3">
            <GraduationCap className="w-5 h-5 text-emerald-400" />
            <span className="text-sm font-medium text-white">{topic?.nome}</span>
          </div>
          <div className="flex items-center gap-4">
            <span className="text-xs text-slate-400">{answeredCount}/{questions.length} respondidas</span>
            <div className={`flex items-center gap-1.5 px-3 py-1 rounded-lg text-sm font-mono font-bold ${
              timeLeft < 300 ? 'bg-red-500/10 text-red-400 border border-red-500/20' : 'bg-slate-700/50 text-white'
            }`}>
              <Clock className="w-4 h-4" /> {formatTime(timeLeft)}
            </div>
          </div>
        </div>

        {/* Time bar */}
        <div className="w-full h-1 bg-slate-800 rounded-full mb-6 overflow-hidden">
          <div
            className={`h-full rounded-full transition-all duration-1000 ${timePercent < 20 ? 'bg-red-500' : 'bg-emerald-500'}`}
            style={{ width: `${timePercent}%` }}
          />
        </div>

        {/* Question card */}
        {current && (
          <div className="bg-slate-800/50 rounded-2xl border border-slate-700/50 p-6 md:p-8">
            <div className="flex items-center justify-between mb-4">
              <span className="text-xs text-slate-500">Questão {currentIdx + 1} de {questions.length}</span>
              <span className={`px-2.5 py-0.5 text-[10px] font-medium rounded-full border ${
                current.dificuldade === 'Fácil' ? 'bg-green-500/10 text-green-400 border-green-500/20' :
                current.dificuldade === 'Difícil' ? 'bg-red-500/10 text-red-400 border-red-500/20' :
                'bg-yellow-500/10 text-yellow-400 border-yellow-500/20'
              }`}>
                {current.dificuldade}
              </span>
            </div>

            <h2 className="text-white font-medium text-base leading-relaxed mb-6">{current.enunciado}</h2>

            <div className="space-y-3">
              {current.opcoes?.map((opt, i) => {
                const isSelected = selectedAnswers[current.id] === opt.indice;
                return (
                  <button
                    key={opt.id}
                    onClick={() => selectOption(current.id, opt.indice)}
                    className={`w-full flex items-center gap-3 p-4 rounded-xl border transition text-left cursor-pointer ${
                      isSelected
                        ? 'border-emerald-500/50 bg-emerald-500/10'
                        : 'border-slate-700/50 bg-slate-800/30 hover:border-slate-600 hover:bg-slate-800/60'
                    }`}
                  >
                    <div className={`flex items-center justify-center w-8 h-8 rounded-lg text-sm font-bold shrink-0 ${
                      isSelected ? 'bg-emerald-500/20 text-emerald-400' : 'bg-slate-700/50 text-slate-400'
                    }`}>
                      {optionLabels[i]}
                    </div>
                    <span className={`text-sm ${isSelected ? 'text-emerald-300' : 'text-slate-300'}`}>
                      {opt.texto}
                    </span>
                  </button>
                );
              })}
            </div>

            {/* Nav */}
            <div className="flex items-center justify-between mt-6 pt-4 border-t border-slate-700/50">
              <button
                onClick={() => setCurrentIdx(Math.max(0, currentIdx - 1))}
                disabled={currentIdx === 0}
                className="flex items-center gap-1.5 text-sm text-slate-400 hover:text-white disabled:opacity-30 transition cursor-pointer"
              >
                <ArrowLeft className="w-4 h-4" /> Anterior
              </button>

              {currentIdx === questions.length - 1 ? (
                <button
                  onClick={handleSubmit}
                  disabled={loading}
                  className="flex items-center gap-2 px-5 py-2 bg-gradient-to-r from-emerald-500 to-teal-600 text-white text-sm font-medium rounded-xl hover:opacity-90 transition cursor-pointer"
                >
                  {loading ? <Loader2 className="w-4 h-4 animate-spin" /> : <Send className="w-4 h-4" />}
                  Finalizar Prova
                </button>
              ) : (
                <button
                  onClick={() => setCurrentIdx(currentIdx + 1)}
                  className="flex items-center gap-1.5 text-sm text-emerald-400 hover:text-emerald-300 transition cursor-pointer"
                >
                  Próxima <span className="text-lg">→</span>
                </button>
              )}
            </div>
          </div>
        )}

        {/* Question nav */}
        <div className="mt-6 bg-slate-800/50 rounded-2xl border border-slate-700/50 p-4">
          <div className="flex items-center justify-between mb-3">
            <h3 className="text-xs font-medium text-slate-500 uppercase tracking-wider">Navegador</h3>
            <button
              onClick={handleSubmit}
              disabled={loading}
              className="text-xs text-emerald-400 hover:text-emerald-300 transition cursor-pointer"
            >
              Finalizar Prova →
            </button>
          </div>
          <div className="flex flex-wrap gap-2">
            {questions.map((q, i) => {
              const isAnswered = selectedAnswers[q.id] != null;
              const isCurrent = i === currentIdx;
              let cls = 'bg-slate-700/50 text-slate-400 border-slate-600/50';
              if (isAnswered) cls = 'bg-emerald-500/20 text-emerald-400 border-emerald-500/30';
              if (isCurrent) cls += ' ring-2 ring-emerald-500/50';
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

        {/* Submit button at bottom */}
        <button
          onClick={handleSubmit}
          disabled={loading}
          className="mt-6 w-full flex items-center justify-center gap-2 py-3 bg-gradient-to-r from-emerald-500 to-teal-600 text-white font-medium rounded-xl hover:opacity-90 transition disabled:opacity-50 cursor-pointer"
        >
          {loading ? <Loader2 className="w-5 h-5 animate-spin" /> : <Send className="w-5 h-5" />}
          Finalizar e Enviar Prova ({Object.keys(selectedAnswers).length}/{questions.length} respondidas)
        </button>
      </div>
    );
  }

  // ── RESULT VIEW ──
  if (examState === 'result' && result) {
    const passed = result.nota >= 60;
    return (
      <div className="p-6 max-w-4xl mx-auto animate-fadeIn">
        {/* Result header */}
        <div className={`rounded-2xl border p-8 mb-6 text-center ${
          passed ? 'bg-green-500/5 border-green-500/20' : 'bg-red-500/5 border-red-500/20'
        }`}>
          <div className={`inline-flex items-center justify-center w-20 h-20 rounded-full mb-4 ${
            passed ? 'bg-green-500/10' : 'bg-red-500/10'
          }`}>
            {passed ? (
              <Trophy className="w-10 h-10 text-yellow-400" />
            ) : (
              <AlertTriangle className="w-10 h-10 text-red-400" />
            )}
          </div>
          <h2 className="text-2xl font-bold text-white mb-1">
            {passed ? 'Aprovado!' : 'Não Aprovado'}
          </h2>
          <p className="text-slate-400 text-sm mb-4">{result.topicNome}</p>

          <div className={`text-5xl font-bold mb-2 ${passed ? 'text-green-400' : 'text-red-400'}`}>
            {Math.round(result.nota)}%
          </div>

          <div className="flex items-center justify-center gap-6 mt-4">
            <div className="text-center">
              <div className="text-lg font-bold text-green-400">{result.acertos}</div>
              <div className="text-xs text-slate-500">Acertos</div>
            </div>
            <div className="w-px h-8 bg-slate-700" />
            <div className="text-center">
              <div className="text-lg font-bold text-red-400">{result.erros}</div>
              <div className="text-xs text-slate-500">Erros</div>
            </div>
            <div className="w-px h-8 bg-slate-700" />
            <div className="text-center">
              <div className="text-lg font-bold text-white">{result.totalQuestoes}</div>
              <div className="text-xs text-slate-500">Total</div>
            </div>
          </div>
        </div>

        {/* Action buttons */}
        <div className="flex gap-3 mb-6 flex-wrap">
          <button
            onClick={() => { setExamState('setup'); setResult(null); }}
            className="flex items-center gap-2 px-5 py-2.5 bg-emerald-500/10 rounded-xl border border-emerald-500/20 text-emerald-400 text-sm hover:bg-emerald-500/20 transition cursor-pointer"
          >
            <RotateCcw className="w-4 h-4" /> Nova Prova
          </button>
          <Link
            to={`/topics/${topicId}/questoes`}
            className="flex items-center gap-2 px-5 py-2.5 bg-amber-500/10 rounded-xl border border-amber-500/20 text-amber-400 text-sm hover:bg-amber-500/20 transition"
          >
            <BookOpen className="w-4 h-4" /> Revisar Questões
          </Link>
          <Link
            to={`/topics/${topicId}/documentos`}
            className="flex items-center gap-2 px-5 py-2.5 bg-indigo-500/10 rounded-xl border border-indigo-500/20 text-indigo-400 text-sm hover:bg-indigo-500/20 transition"
          >
            <FileText className="w-4 h-4" /> Estudar Documentos
          </Link>
        </div>

        {/* Answers review */}
        <div className="bg-slate-800/50 rounded-2xl border border-slate-700/50 p-6">
          <h3 className="text-sm font-medium text-slate-400 mb-4">Revisão das Respostas</h3>
          <div className="space-y-4">
            {result.respostas?.map((r, i) => (
              <div key={i} className={`p-4 rounded-xl border ${r.correta ? 'border-green-500/20 bg-green-500/5' : 'border-red-500/20 bg-red-500/5'}`}>
                <div className="flex items-start gap-3">
                  <div className={`flex items-center justify-center w-7 h-7 rounded-lg text-xs font-bold shrink-0 mt-0.5 ${
                    r.correta ? 'bg-green-500/20 text-green-400' : 'bg-red-500/20 text-red-400'
                  }`}>
                    {r.correta ? <CheckCircle2 className="w-4 h-4" /> : <XCircle className="w-4 h-4" />}
                  </div>
                  <div className="flex-1 min-w-0">
                    <p className="text-sm text-white mb-2">{r.enunciado}</p>
                    <div className="space-y-1">
                      {r.opcoes?.map((opt, j) => {
                        const isCorrect = opt.indice === r.respostaCorreta;
                        const isChosen = opt.indice === r.respostaEscolhida;
                        let color = 'text-slate-500';
                        if (isCorrect) color = 'text-green-400 font-medium';
                        if (isChosen && !isCorrect) color = 'text-red-400 line-through';
                        return (
                          <p key={j} className={`text-xs ${color}`}>
                            {optionLabels[j]}) {opt.texto}
                            {isCorrect && ' ✓'}
                            {isChosen && !isCorrect && ' (sua resposta)'}
                          </p>
                        );
                      })}
                    </div>
                    {r.explicacao && (
                      <p className="mt-2 text-xs text-indigo-300 bg-indigo-500/5 p-2 rounded-lg">
                        💡 {r.explicacao}
                      </p>
                    )}
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    );
  }

  return null;
}
