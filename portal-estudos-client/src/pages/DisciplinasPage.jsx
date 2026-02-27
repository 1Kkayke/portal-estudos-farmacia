import { useState, useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import api from '../services/api';
import { Search, FileText, ArrowRight, ChevronDown, ChevronUp, BookOpen, GraduationCap, HelpCircle } from 'lucide-react';
import { getIcon } from '../utils/icons';

/**
 * Página que lista todas as 42 disciplinas agrupadas por categoria.
 * Inclui busca e colapso de categorias.
 */
export default function DisciplinasPage() {
  const [topics, setTopics] = useState([]);
  const [loading, setLoading] = useState(true);
  const [search, setSearch] = useState('');
  const [collapsed, setCollapsed] = useState({});
  const navigate = useNavigate();

  useEffect(() => {
    api.get('/topics').then(r => setTopics(r.data)).catch(console.error).finally(() => setLoading(false));
  }, []);

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
    <div className="p-6 lg:p-8 max-w-6xl mx-auto">
      {/* Header */}
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4 mb-8">
        <div>
          <h1 className="text-2xl font-bold text-white">Disciplinas</h1>
          <p className="text-slate-400 text-sm mt-1">{topics.length} tópicos disponíveis em {Object.keys(grouped).length} categorias</p>
        </div>
        <div className="relative w-full sm:w-72">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-slate-500" />
          <input type="text" value={search} onChange={(e) => setSearch(e.target.value)}
            className="w-full pl-10 pr-4 py-2.5 rounded-xl bg-slate-800/50 border border-slate-700 text-white placeholder-slate-500 focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 outline-none transition text-sm"
            placeholder="Buscar disciplina..." />
        </div>
      </div>

      {/* Categorias */}
      <div className="space-y-6">
        {Object.entries(grouped).map(([cat, catTopics]) => (
          <div key={cat} className="rounded-2xl bg-slate-800/30 border border-slate-700/50 overflow-hidden">
            <button onClick={() => toggleCat(cat)}
              className="w-full flex items-center justify-between px-6 py-4 text-left cursor-pointer hover:bg-slate-800/50 transition">
              <div>
                <h2 className="text-base font-semibold text-white">{cat}</h2>
                <p className="text-xs text-slate-500">{catTopics.length} disciplinas</p>
              </div>
              {collapsed[cat] ? <ChevronDown className="w-5 h-5 text-slate-500" /> : <ChevronUp className="w-5 h-5 text-slate-500" />}
            </button>

            {!collapsed[cat] && (
              <div className="grid sm:grid-cols-2 lg:grid-cols-3 gap-3 px-4 pb-4">
                {catTopics.map((t, i) => {
                  const Icon = getIcon(t.icone);
                  return (
                    <button key={t.id} onClick={() => navigate(`/topics/${t.id}/documentos`)}
                      className="flex items-start gap-3 p-4 rounded-xl bg-slate-900/50 border border-slate-700/40 hover:border-slate-600 transition-all duration-200 text-left cursor-pointer group animate-fade-in"
                      style={{ animationDelay: `${i * 30}ms` }}>
                      <div className="shrink-0 w-10 h-10 rounded-lg flex items-center justify-center"
                        style={{ backgroundColor: t.cor + '18', color: t.cor }}>
                        <Icon className="w-5 h-5" />
                      </div>
                      <div className="flex-1 min-w-0">
                        <p className="text-sm font-medium text-white group-hover:text-indigo-300 transition truncate">{t.nome}</p>
                        <p className="text-xs text-slate-500 line-clamp-2 mt-0.5">{t.descricao}</p>
                        <div className="flex items-center gap-3 mt-2 text-xs text-slate-600">
                          <span className="flex items-center gap-1"><FileText className="w-3 h-3" /> Docs</span>
                          <span className="flex items-center gap-1"><HelpCircle className="w-3 h-3" /> Questões</span>
                          <span className="flex items-center gap-1"><GraduationCap className="w-3 h-3" /> Prova</span>
                        </div>
                      </div>
                      <ArrowRight className="w-4 h-4 text-slate-700 group-hover:text-indigo-400 transition shrink-0 mt-1" />
                    </button>
                  );
                })}
              </div>
            )}
          </div>
        ))}
      </div>

      {filtered.length === 0 && (
        <div className="text-center py-16 text-slate-500">
          <Search className="w-10 h-10 mx-auto mb-3 text-slate-600" />
          <p className="font-medium">Nenhuma disciplina encontrada</p>
          <p className="text-xs mt-1">Tente outro termo de busca</p>
        </div>
      )}
    </div>
  );
}
