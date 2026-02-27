import { useState, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import api from '../services/api';
import {
  BookOpen, FileText, Timer, Layers, Newspaper,
  TrendingUp, Clock, ArrowRight, Sparkles, Zap, Heart, History,
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
  const [favorites, setFavorites] = useState([]);
  const [recentStudies, setRecentStudies] = useState([]);
  const [loading, setLoading] = useState(true);
  const { user } = useAuth();
  const navigate = useNavigate();
  const [tip] = useState(() => tips[Math.floor(Math.random() * tips.length)]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [topicsRes, favoritesRes, recentRes] = await Promise.all([
          api.get('/topics'),
          api.get('/usertopics/favorites'),
          api.get('/usertopics/recent')
        ]);
        console.log('Topics:', topicsRes.data);
        console.log('Favorites:', favoritesRes.data);
        console.log('Recent:', recentRes.data);
        setTopics(topicsRes.data);
        setFavorites(favoritesRes.data);
        setRecentStudies(recentRes.data);
      } catch (error) {
        console.error('Erro ao carregar dados:', error.response?.status, error.message);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  const totalNotas = topics.reduce((sum, t) => sum + t.totalNotas, 0);

  /** Cards de acesso rápido */
  const quickActions = [
    { icon: BookOpen, label: 'Disciplinas', desc: `${topics.length} tópicos`, path: '/disciplinas', color: 'from-indigo-500 to-blue-600' },
    { icon: FileText, label: 'Anotações', desc: `${totalNotas} notas`, path: '/minhas-notas', color: 'from-emerald-500 to-teal-600' },
    { icon: Layers, label: 'Flashcards', desc: 'Revisão ativa', path: '/flashcards', color: 'from-amber-500 to-orange-600' },
    { icon: Timer, label: 'Pomodoro', desc: 'Foco total', path: '/pomodoro', color: 'from-rose-500 to-pink-600' },
    { icon: Newspaper, label: 'Blog', desc: 'Artigos úteis', path: '/blog', color: 'from-violet-500 to-purple-600' },
    { icon: Zap, label: 'Recursos', desc: 'Links e mais', path: '/recursos', color: 'from-cyan-500 to-blue-600' },
  ];

  const formatLastAccess = (date) => {
    const now = new Date();
    const accessed = new Date(date);
    const diffMs = now - accessed;
    const diffMins = Math.floor(diffMs / 60000);
    const diffHours = Math.floor(diffMs / 3600000);
    const diffDays = Math.floor(diffMs / 86400000);

    if (diffMins < 1) return 'Agora mesmo';
    if (diffMins < 60) return `${diffMins} min atrás`;
    if (diffHours < 24) return `${diffHours}h atrás`;
    if (diffDays === 1) return 'Ontem';
    if (diffDays < 7) return `${diffDays} dias atrás`;
    return accessed.toLocaleDateString('pt-BR', { day: '2-digit', month: 'short' });
  };

  return (
    <div className="p-4 md:p-6 lg:p-8 max-w-7xl mx-auto space-y-6 lg:space-y-8">
      {/* Hero / Welcome */}
      <div className="relative overflow-hidden rounded-2xl bg-gradient-to-r from-indigo-600 via-purple-600 to-indigo-700 p-6 md:p-8 lg:p-10">
        <div className="absolute inset-0 opacity-10">
          <div className="absolute -top-10 -right-10 w-64 h-64 bg-white rounded-full blur-3xl" />
          <div className="absolute -bottom-10 -left-10 w-48 h-48 bg-purple-300 rounded-full blur-3xl" />
        </div>
        <div className="relative z-10">
          <div className="flex items-center gap-2 mb-2 lg:mb-3">
            <Sparkles className="w-4 h-4 lg:w-5 lg:h-5 text-amber-300" />
            <span className="text-[10px] lg:text-xs font-semibold text-indigo-200 uppercase tracking-widest">Dica do dia</span>
          </div>
          <h1 className="text-xl md:text-2xl lg:text-3xl font-extrabold text-white mb-2">
            Olá, {user?.nomeCompleto?.split(' ')[0]}! 👋
          </h1>
          <p className="text-indigo-100 text-xs md:text-sm lg:text-base max-w-2xl leading-relaxed mb-3 lg:mb-4">
            {tip}
          </p>
          <Link to="/disciplinas"
            className="inline-flex items-center gap-2 px-4 lg:px-5 py-2 lg:py-2.5 bg-white/15 hover:bg-white/25 active:bg-white/30 backdrop-blur rounded-xl text-xs md:text-sm font-medium text-white transition border border-white/10 active:scale-95">
            Estudar agora <ArrowRight className="w-3.5 h-3.5 lg:w-4 lg:h-4" />
          </Link>
        </div>
      </div>

      {/* Quick Actions Grid */}
      <div>
        <h2 className="text-base lg:text-lg font-bold text-white mb-3 lg:mb-4 flex items-center gap-2">
          <Zap className="w-4 h-4 lg:w-5 lg:h-5 text-amber-400" /> Acesso Rápido
        </h2>
        <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-6 gap-3 lg:gap-4">
          {quickActions.map((item) => {
            const Icon = item.icon;
            return (
              <button key={item.label} onClick={() => navigate(item.path)}
                className="group flex flex-col items-center gap-2.5 lg:gap-3 p-4 lg:p-5 rounded-2xl bg-slate-800/50 border border-slate-700/50 hover:border-slate-600 transition-all duration-300 hover:-translate-y-1 active:scale-95 cursor-pointer">
                <div className={`flex items-center justify-center w-11 h-11 lg:w-12 lg:h-12 rounded-xl bg-gradient-to-br ${item.color} shadow-lg`}>
                  <Icon className="w-5 h-5 lg:w-6 lg:h-6 text-white" />
                </div>
                <div className="text-center">
                  <p className="text-xs lg:text-sm font-semibold text-white">{item.label}</p>
                  <p className="text-[10px] lg:text-xs text-slate-500 truncate max-w-[100px]">{item.desc}</p>
                </div>
              </button>
            );
          })}
        </div>
      </div>

      {/* Favorites & Recent Studies */}
      <div className="grid lg:grid-cols-2 gap-6">
        {/* Matérias Favoritas */}
        <div>
          <div className="flex items-center justify-between mb-4">
            <h3 className="text-sm lg:text-base font-bold text-white flex items-center gap-2">
              <Heart className="w-4 h-4 lg:w-5 lg:h-5 text-rose-400 fill-rose-400" />
              Matérias de Interesse
            </h3>
            <Link to="/disciplinas" className="text-xs text-indigo-400 hover:text-indigo-300 font-medium">
              Ver todas →
            </Link>
          </div>
          
          {loading ? (
            <div className="flex items-center justify-center py-12">
              <div className="animate-spin rounded-full h-6 w-6 border-b-2 border-indigo-500"></div>
            </div>
          ) : favorites.length === 0 ? (
            <div className="flex flex-col items-center justify-center py-12 px-4 rounded-xl bg-slate-800/30 border border-slate-700/30">
              <Heart className="w-12 h-12 text-slate-600 mb-3" />
              <p className="text-sm text-slate-400 text-center mb-2">Nenhuma matéria favorita ainda</p>
              <p className="text-xs text-slate-500 text-center">Adicione matérias aos favoritos na página de Disciplinas</p>
            </div>
          ) : (
            <div className="space-y-2">
              {favorites.slice(0, 5).map((topic, i) => (
                <button key={topic.id} onClick={() => navigate(`/topics/${topic.id}/documentos`)}
                  className="w-full flex items-center gap-3 p-3 rounded-xl bg-slate-800/50 border border-slate-700/50 hover:border-slate-600 active:scale-[0.99] transition text-left cursor-pointer group">
                  <div className="w-10 h-10 rounded-lg flex items-center justify-center text-base font-bold text-white shrink-0"
                    style={{ backgroundColor: topic.cor + '22', color: topic.cor }}>
                    {topic.nome.charAt(0)}
                  </div>
                  <div className="flex-1 min-w-0">
                    <p className="text-sm font-medium text-white truncate group-hover:text-indigo-300 transition">{topic.nome}</p>
                    <p className="text-xs text-slate-500 truncate">{topic.categoria}</p>
                  </div>
                  <Heart className="w-4 h-4 text-rose-400 fill-rose-400 shrink-0" />
                </button>
              ))}
            </div>
          )}
        </div>

        {/* Ultimamente Estudado */}
        <div>
          <div className="flex items-center justify-between mb-4">
            <h3 className="text-sm lg:text-base font-bold text-white flex items-center gap-2">
              <History className="w-4 h-4 lg:w-5 lg:h-5 text-indigo-400" />
              Estudado Recentemente
            </h3>
          </div>
          
          {loading ? (
            <div className="flex items-center justify-center py-12">
              <div className="animate-spin rounded-full h-6 w-6 border-b-2 border-indigo-500"></div>
            </div>
          ) : recentStudies.length === 0 ? (
            <div className="flex flex-col items-center justify-center py-12 px-4 rounded-xl bg-slate-800/30 border border-slate-700/30">
              <Clock className="w-12 h-12 text-slate-600 mb-3" />
              <p className="text-sm text-slate-400 text-center mb-2">Nenhum histórico de estudo</p>
              <p className="text-xs text-slate-500 text-center">Comece a estudar para ver seu histórico aqui</p>
            </div>
          ) : (
            <div className="space-y-2">
              {recentStudies.map((activity) => (
                <button key={activity.topicId} onClick={() => navigate(`/topics/${activity.topicId}/documentos`)}
                  className="w-full flex items-center gap-3 p-3 rounded-xl bg-slate-800/50 border border-slate-700/50 hover:border-slate-600 active:scale-[0.99] transition text-left cursor-pointer group">
                  <div className="w-10 h-10 rounded-lg flex items-center justify-center text-base font-bold text-white shrink-0"
                    style={{ backgroundColor: activity.topicCor + '22', color: activity.topicCor }}>
                    {activity.topicNome.charAt(0)}
                  </div>
                  <div className="flex-1 min-w-0">
                    <p className="text-sm font-medium text-white truncate group-hover:text-indigo-300 transition">{activity.topicNome}</p>
                    <div className="flex items-center gap-2 text-xs text-slate-500">
                      <Clock className="w-3 h-3" />
                      <span>{formatLastAccess(activity.ultimoAcesso)}</span>
                      <span>•</span>
                      <span>{activity.totalAcessos} {activity.totalAcessos === 1 ? 'acesso' : 'acessos'}</span>
                    </div>
                  </div>
                  <ArrowRight className="w-4 h-4 text-slate-600 group-hover:text-indigo-400 transition shrink-0" />
                </button>
              ))}
            </div>
          )}
        </div>
      </div>

      {/* Stats */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {[
          { label: 'Total de Disciplinas', value: topics.length, icon: BookOpen, color: 'text-indigo-400', bg: 'bg-indigo-500/10' },
          { label: 'Anotações Criadas', value: totalNotas, icon: FileText, color: 'text-emerald-400', bg: 'bg-emerald-500/10' },
          { label: 'Matérias Favoritas', value: favorites.length, icon: Heart, color: 'text-rose-400', bg: 'bg-rose-500/10' },
        ].map((s) => (
          <div key={s.label} className={`flex items-center gap-4 p-4 lg:p-5 rounded-xl ${s.bg} border border-slate-700/50`}>
            <div className={`p-3 rounded-xl bg-slate-800/50`}>
              <s.icon className={`w-6 h-6 ${s.color}`} />
            </div>
            <div className="flex-1 min-w-0">
              <p className="text-xs text-slate-500 truncate">{s.label}</p>
              <p className="text-2xl font-bold text-white">{s.value}</p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
