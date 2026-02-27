import { useState, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import api from '../services/api';
import { Search, FileText, ArrowRight, ChevronDown, ChevronUp, BookOpen, GraduationCap, HelpCircle, Heart } from 'lucide-react';
import { getIcon } from '../utils/icons';

/**
 * Página que lista todas as 42 disciplinas agrupadas por categoria.
 * Inclui busca e colapso de categorias.
 */
export default function DisciplinasPage() {
  const [topics, setTopics] = useState([]);
  const [favorites, setFavorites] = useState(new Set());
  const [loading, setLoading] = useState(true);
  const [search, setSearch] = useState('');
  const [collapsed, setCollapsed] = useState({});
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [topicsRes, favoritesRes] = await Promise.all([
          api.get('/topics'),
          api.get('/usertopics/favorites')
        ]);
        setTopics(topicsRes.data);
        setFavorites(new Set(favoritesRes.data.map(f => f.id)));
      } catch (error) {
        console.error('Erro ao carregar dados:', error);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  const toggleFavorite = async (topicId, e) => {
    e.stopPropagation(); // Evita navegação ao clicar no coração
    
    try {
      console.log('Favoritando topicId:', topicId);
      const res = await api.post(`/usertopics/favorites/${topicId}`);
      console.log('Resposta do servidor:', res.data);
      
      setFavorites(prev => {
        const newFavorites = new Set(prev);
        if (newFavorites.has(topicId)) {
          newFavorites.delete(topicId);
        } else {
          newFavorites.add(topicId);
        }
        console.log('Novos favoritos:', Array.from(newFavorites));
        return newFavorites;
      });
    } catch (error) {
      console.error('Erro ao favoritar:', error.response?.status, error.message);
    }
  };

  // Filtra por busca
  const filtered = search
    ? topics.filter(t => t.nome.toLowerCase().includes(search.toLowerCase()) || t.descricao.toLowerCase().includes(search.toLowerCase()))
    : topics;

  // Agrupa por categoria
  const grouped = filtered.reduce((acc, t) => {
    const cat = t.categoria || 'Outros';
    if (!acc[cat]) acc[cat] = [];
    acc[cat].push(t);
    return acc;
  }, {});

  const toggleCat = (cat) => setCollapsed(prev => ({ ...prev, [cat]: !prev[cat] }));

  if (loading) {
    return <div className="flex items-center justify-center h-full py-20"><div className="animate-spin rounded-full h-6 w-6 border-b-2 border-indigo-500"></div></div>;
  }

  return (
    <div className="p-4 md:p-6 lg:p-8 max-w-6xl mx-auto">
      {/* Header */}
      <div className="flex flex-col gap-3 lg:gap-4 mb-6 lg:mb-8">
        <div>
          <h1 className="text-xl md:text-2xl font-bold text-white">Disciplinas</h1>
          <p className="text-slate-400 text-xs md:text-sm mt-1">{topics.length} tópicos disponíveis em {Object.keys(grouped).length} categorias</p>
        </div>
        <div className="relative w-full">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-slate-500" />
          <input type="text" value={search} onChange={(e) => setSearch(e.target.value)}
            className="w-full pl-10 pr-4 py-2.5 lg:py-3 rounded-xl bg-slate-800/50 border border-slate-700 text-white placeholder-slate-500 focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 outline-none transition text-sm"
            placeholder="Buscar disciplina..." />
        </div>
      </div>

      {/* Categorias */}
      <div className="space-y-4 lg:space-y-6">
        {Object.entries(grouped).map(([cat, catTopics]) => (
          <div key={cat} className="rounded-2xl bg-slate-800/30 border border-slate-700/50 overflow-hidden">
            <button onClick={() => toggleCat(cat)}
              className="w-full flex items-center justify-between px-4 lg:px-6 py-3.5 lg:py-4 text-left cursor-pointer hover:bg-slate-800/50 active:bg-slate-800/60 transition">
              <div>
                <h2 className="text-sm lg:text-base font-semibold text-white">{cat}</h2>
                <p className="text-[10px] lg:text-xs text-slate-500">{catTopics.length} disciplinas</p>
              </div>
              {collapsed[cat] ? <ChevronDown className="w-5 h-5 text-slate-500 shrink-0" /> : <ChevronUp className="w-5 h-5 text-slate-500 shrink-0" />}
            </button>

            {!collapsed[cat] && (
              <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-2.5 lg:gap-3 px-3 lg:px-4 pb-3 lg:pb-4">
                {catTopics.map((t, i) => {
                  const Icon = getIcon(t.icone);
                  const isFavorite = favorites.has(t.id);
                  return (
                    <div key={t.id} className="relative">
                      <button onClick={() => navigate(`/topics/${t.id}/documentos`)}
                        className="w-full flex items-start gap-2.5 lg:gap-3 p-3 lg:p-4 rounded-xl bg-slate-900/50 border border-slate-700/40 hover:border-slate-600 active:scale-[0.98] transition-all duration-200 text-left cursor-pointer group animate-fade-in"
                        style={{ animationDelay: `${i * 30}ms` }}>
                        <div className="shrink-0 w-9 h-9 lg:w-10 lg:h-10 rounded-lg flex items-center justify-center"
                          style={{ backgroundColor: t.cor + '18', color: t.cor }}>
                          <Icon className="w-4 h-4 lg:w-5 lg:h-5" />
                        </div>
                        <div className="flex-1 min-w-0">
                          <p className="text-xs lg:text-sm font-medium text-white group-hover:text-indigo-300 transition truncate pr-8">{t.nome}</p>
                          <p className="text-[10px] lg:text-xs text-slate-500 line-clamp-2 mt-0.5">{t.descricao}</p>
                          <div className="flex items-center gap-2 lg:gap-3 mt-1.5 lg:mt-2 text-[10px] lg:text-xs text-slate-600">
                            <span className="flex items-center gap-1"><FileText className="w-3 h-3" /> Docs</span>
                            <span className="flex items-center gap-1"><HelpCircle className="w-3 h-3" /> Questões</span>
                            <span className="flex items-center gap-1"><GraduationCap className="w-3 h-3" /> Prova</span>
                          </div>
                        </div>
                      </button>
                      
                      {/* Botão de Favorito */}
                      <button
                        onClick={(e) => toggleFavorite(t.id, e)}
                        className={`absolute top-2 right-2 p-2 rounded-lg transition-all duration-200 ${
                          isFavorite 
                            ? 'text-rose-400 hover:text-rose-500 bg-rose-500/10' 
                            : 'text-slate-600 hover:text-rose-400 hover:bg-slate-800/50'
                        } active:scale-95`}
                        title={isFavorite ? 'Remover dos favoritos' : 'Adicionar aos favoritos'}
                      >
                        <Heart 
                          className={`w-4 h-4 lg:w-5 lg:h-5 transition-all ${isFavorite ? 'fill-rose-400' : ''}`} 
                        />
                      </button>
                    </div>
                  );
                })}
              </div>
            )}
          </div>
        ))}
      </div>

      {filtered.length === 0 && (
        <div className="text-center py-12 lg:py-16 text-slate-500">
          <Search className="w-8 h-8 lg:w-10 lg:h-10 mx-auto mb-3 text-slate-600" />
          <p className="font-medium text-sm">Nenhuma disciplina encontrada</p>
          <p className="text-xs mt-1">Tente outro termo de busca</p>
        </div>
      )}
    </div>
  );
}
