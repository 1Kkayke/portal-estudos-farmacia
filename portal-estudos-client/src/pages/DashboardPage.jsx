import { useState, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import api from '../services/api';
import {
  BookOpen, FileText, Timer, Layers, Newspaper,
  TrendingUp, Clock, ArrowRight, Sparkles, Zap,
} from 'lucide-react';

/** Dicas diárias de farmácia */
const tips = [
  "A biodisponibilidade de um fármaco oral depende da absorção e do efeito de primeira passagem hepática.",
  "O pH gástrico (~1.5-3.5) favorece a absorção de ácidos fracos no estômago.",
  "Fármacos com alta ligação a proteínas plasmáticas possuem menor fração livre ativa.",
  "A meia-vida de eliminação determina o intervalo entre doses no estado de equilíbrio.",
  "A CIM (Concentração Inibitória Mínima) é fundamental para dosagem de antibióticos.",
  "Fitoterápicos como Ginkgo biloba podem interagir com anticoagulantes.",
  "O sistema CYP450, especialmente CYP3A4, metaboliza ~50% dos fármacos comercializados.",
  "A Atenção Farmacêutica visa resultados terapêuticos definidos na qualidade de vida do paciente.",
];

export default function DashboardPage() {
  const [topics, setTopics] = useState([]);
  const [loading, setLoading] = useState(true);
  const { user } = useAuth();
  const navigate = useNavigate();
  const [tip] = useState(() => tips[Math.floor(Math.random() * tips.length)]);

  useEffect(() => {
    api.get('/topics').then(r => setTopics(r.data)).catch(console.error).finally(() => setLoading(false));
  }, []);

  const totalNotas = topics.reduce((sum, t) => sum + t.totalNotas, 0);
  const recentTopics = [...topics].sort((a, b) => b.totalNotas - a.totalNotas).slice(0, 5);

  /** Cards de acesso rápido */
  const quickActions = [
    { icon: BookOpen, label: 'Disciplinas', desc: `${topics.length} tópicos`, path: '/disciplinas', color: 'from-indigo-500 to-blue-600' },
    { icon: FileText, label: 'Anotações', desc: `${totalNotas} notas`, path: '/minhas-notas', color: 'from-emerald-500 to-teal-600' },
    { icon: Layers, label: 'Flashcards', desc: 'Revisão ativa', path: '/flashcards', color: 'from-amber-500 to-orange-600' },
    { icon: Timer, label: 'Pomodoro', desc: 'Foco total', path: '/pomodoro', color: 'from-rose-500 to-pink-600' },
    { icon: Newspaper, label: 'Blog', desc: 'Artigos úteis', path: '/blog', color: 'from-violet-500 to-purple-600' },
    { icon: Zap, label: 'Recursos', desc: 'Links e mais', path: '/recursos', color: 'from-cyan-500 to-blue-600' },
  ];

  return (
    <div className="p-6 lg:p-8 max-w-7xl mx-auto space-y-8">
      {/* Hero / Welcome */}
      <div className="relative overflow-hidden rounded-2xl bg-gradient-to-r from-indigo-600 via-purple-600 to-indigo-700 p-8 lg:p-10">
        <div className="absolute inset-0 opacity-10">
          <div className="absolute -top-10 -right-10 w-64 h-64 bg-white rounded-full blur-3xl" />
          <div className="absolute -bottom-10 -left-10 w-48 h-48 bg-purple-300 rounded-full blur-3xl" />
        </div>
        <div className="relative z-10">
          <div className="flex items-center gap-2 mb-3">
            <Sparkles className="w-5 h-5 text-amber-300" />
            <span className="text-xs font-semibold text-indigo-200 uppercase tracking-widest">Dica do dia</span>
          </div>
          <h1 className="text-2xl lg:text-3xl font-extrabold text-white mb-2">
            Olá, {user?.nomeCompleto?.split(' ')[0]}! 👋
          </h1>
          <p className="text-indigo-100 text-sm lg:text-base max-w-2xl leading-relaxed mb-4">
            {tip}
          </p>
          <Link to="/disciplinas"
            className="inline-flex items-center gap-2 px-5 py-2.5 bg-white/15 hover:bg-white/25 backdrop-blur rounded-xl text-sm font-medium text-white transition border border-white/10">
            Estudar agora <ArrowRight className="w-4 h-4" />
          </Link>
        </div>
      </div>

      {/* Quick Actions Grid */}
      <div>
        <h2 className="text-lg font-bold text-white mb-4 flex items-center gap-2">
          <Zap className="w-5 h-5 text-amber-400" /> Acesso Rápido
        </h2>
        <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-6 gap-4">
          {quickActions.map((item) => {
            const Icon = item.icon;
            return (
              <button key={item.label} onClick={() => navigate(item.path)}
                className="group flex flex-col items-center gap-3 p-5 rounded-2xl bg-slate-800/50 border border-slate-700/50 hover:border-slate-600 transition-all duration-300 hover:-translate-y-1 cursor-pointer">
                <div className={`flex items-center justify-center w-12 h-12 rounded-xl bg-gradient-to-br ${item.color} shadow-lg`}>
                  <Icon className="w-6 h-6 text-white" />
                </div>
                <div className="text-center">
                  <p className="text-sm font-semibold text-white">{item.label}</p>
                  <p className="text-xs text-slate-500">{item.desc}</p>
                </div>
              </button>
            );
          })}
        </div>
      </div>

      {/* Stats + Top Topics */}
      <div className="grid lg:grid-cols-3 gap-6">
        {/* Stats */}
        <div className="lg:col-span-1 space-y-4">
          <h3 className="text-sm font-semibold text-slate-400 uppercase tracking-wider">Seus Números</h3>
          <div className="space-y-3">
            {[
              { label: 'Total de Disciplinas', value: topics.length, icon: BookOpen, color: 'text-indigo-400' },
              { label: 'Anotações Criadas', value: totalNotas, icon: FileText, color: 'text-emerald-400' },
              { label: 'Com Anotações', value: topics.filter(t => t.totalNotas > 0).length, icon: TrendingUp, color: 'text-amber-400' },
            ].map((s) => (
              <div key={s.label} className="flex items-center gap-4 p-4 rounded-xl bg-slate-800/50 border border-slate-700/50">
                <s.icon className={`w-5 h-5 ${s.color}`} />
                <div className="flex-1">
                  <p className="text-xs text-slate-500">{s.label}</p>
                  <p className="text-xl font-bold text-white">{s.value}</p>
                </div>
              </div>
            ))}
          </div>
        </div>

        {/* Top Topics */}
        <div className="lg:col-span-2">
          <div className="flex items-center justify-between mb-4">
            <h3 className="text-sm font-semibold text-slate-400 uppercase tracking-wider">Disciplinas Mais Anotadas</h3>
            <Link to="/disciplinas" className="text-xs text-indigo-400 hover:text-indigo-300 font-medium">Ver todas →</Link>
          </div>
          {loading ? (
            <div className="flex items-center justify-center py-12">
              <div className="animate-spin rounded-full h-6 w-6 border-b-2 border-indigo-500"></div>
            </div>
          ) : recentTopics.length === 0 ? (
            <div className="text-center py-12 text-slate-500 text-sm">Nenhum tópico encontrado.</div>
          ) : (
            <div className="space-y-2">
              {recentTopics.map((t, i) => (
                <button key={t.id} onClick={() => navigate(`/topics/${t.id}/notes`)}
                  className="w-full flex items-center gap-4 p-4 rounded-xl bg-slate-800/50 border border-slate-700/50 hover:border-slate-600 transition text-left cursor-pointer group animate-slide-in"
                  style={{ animationDelay: `${i * 60}ms` }}>
                  <div className="w-10 h-10 rounded-lg flex items-center justify-center text-base font-bold text-white shrink-0"
                    style={{ backgroundColor: t.cor + '22', color: t.cor }}>
                    {t.nome.charAt(0)}
                  </div>
                  <div className="flex-1 min-w-0">
                    <p className="text-sm font-medium text-white truncate group-hover:text-indigo-300 transition">{t.nome}</p>
                    <p className="text-xs text-slate-500 truncate">{t.categoria}</p>
                  </div>
                  <div className="flex items-center gap-1 text-xs text-slate-500">
                    <FileText className="w-3 h-3" />
                    {t.totalNotas}
                  </div>
                  <ArrowRight className="w-4 h-4 text-slate-600 group-hover:text-indigo-400 transition" />
                </button>
              ))}
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
