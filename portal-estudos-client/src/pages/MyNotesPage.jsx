import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { FileText, Search, BookOpen, Clock, Tag, Trash2 } from 'lucide-react';
import api from '../services/api';

function formatDate(d) {
  return new Date(d).toLocaleDateString('pt-BR', { day: '2-digit', month: 'short', year: 'numeric', hour: '2-digit', minute: '2-digit' });
}

export default function MyNotesPage() {
  const [notes, setNotes] = useState([]);
  const [topics, setTopics] = useState([]);
  const [loading, setLoading] = useState(true);
  const [search, setSearch] = useState('');
  const [filterTopic, setFilterTopic] = useState('');

  useEffect(() => {
    (async () => {
      try {
        const [nRes, tRes] = await Promise.all([api.get('/notes'), api.get('/topics')]);
        setNotes(nRes.data);
        setTopics(tRes.data);
      } catch (e) { console.error(e); }
      finally { setLoading(false); }
    })();
  }, []);

  const handleDelete = async (id) => {
    if (!window.confirm('Excluir esta anotação?')) return;
    await api.delete(`/notes/${id}`);
    setNotes((prev) => prev.filter((n) => n.id !== id));
  };

  const topicMap = Object.fromEntries(topics.map((t) => [t.id, t]));

  const filtered = notes.filter((n) => {
    const matchSearch = n.titulo.toLowerCase().includes(search.toLowerCase());
    const matchTopic = filterTopic ? n.topicId === Number(filterTopic) : true;
    return matchSearch && matchTopic;
  });

  // Group by topic
  const grouped = {};
  filtered.forEach((n) => {
    const key = n.topicId;
    if (!grouped[key]) grouped[key] = [];
    grouped[key].push(n);
  });

  const usedTopics = [...new Set(notes.map((n) => n.topicId))];

  if (loading) {
    return (
      <div className="flex items-center justify-center h-full">
        <div className="w-8 h-8 border-2 border-indigo-500 border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  return (
    <div className="p-6 lg:p-8 max-w-5xl mx-auto space-y-6">
      {/* Header */}
      <div>
        <h1 className="text-2xl font-bold text-white flex items-center gap-3">
          <FileText className="w-7 h-7 text-indigo-400" /> Minhas Anotações
        </h1>
        <p className="text-slate-400 text-sm mt-1">Todas as suas anotações em um só lugar.</p>
      </div>

      {/* Filters */}
      <div className="flex flex-col sm:flex-row gap-3">
        <div className="relative flex-1">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-slate-500" />
          <input type="text" placeholder="Buscar por título..."
            value={search} onChange={(e) => setSearch(e.target.value)}
            className="w-full pl-10 pr-4 py-2.5 rounded-xl bg-slate-800/50 border border-slate-700/50 text-white placeholder-slate-500 text-sm outline-none focus:ring-2 focus:ring-indigo-500 transition" />
        </div>
        <select value={filterTopic} onChange={(e) => setFilterTopic(e.target.value)}
          className="px-4 py-2.5 rounded-xl bg-slate-800/50 border border-slate-700/50 text-white text-sm outline-none focus:ring-2 focus:ring-indigo-500 transition min-w-[200px]">
          <option value="">Todas as disciplinas</option>
          {usedTopics.map((tid) => (
            <option key={tid} value={tid}>{topicMap[tid]?.nome || `Tópico ${tid}`}</option>
          ))}
        </select>
      </div>

      {/* Stats */}
      <div className="grid grid-cols-2 sm:grid-cols-3 gap-3">
        <div className="bg-slate-800/50 border border-slate-700/50 rounded-xl p-4 text-center">
          <p className="text-2xl font-bold text-white">{notes.length}</p>
          <p className="text-xs text-slate-400 mt-1">Total de Anotações</p>
        </div>
        <div className="bg-slate-800/50 border border-slate-700/50 rounded-xl p-4 text-center">
          <p className="text-2xl font-bold text-indigo-400">{usedTopics.length}</p>
          <p className="text-xs text-slate-400 mt-1">Disciplinas Usadas</p>
        </div>
        <div className="bg-slate-800/50 border border-slate-700/50 rounded-xl p-4 text-center hidden sm:block">
          <p className="text-2xl font-bold text-purple-400">{filtered.length}</p>
          <p className="text-xs text-slate-400 mt-1">Resultados</p>
        </div>
      </div>

      {/* Notes grouped by topic */}
      {filtered.length === 0 ? (
        <div className="text-center py-16">
          <BookOpen className="w-12 h-12 text-slate-600 mx-auto mb-4" />
          <p className="text-slate-400 mb-1">
            {notes.length === 0 ? 'Você ainda não tem anotações.' : 'Nenhuma anotação encontrada.'}
          </p>
          {notes.length === 0 && (
            <Link to="/disciplinas" className="text-indigo-400 hover:underline text-sm">
              Explorar disciplinas para começar
            </Link>
          )}
        </div>
      ) : (
        <div className="space-y-6">
          {Object.entries(grouped).map(([tid, items]) => {
            const t = topicMap[Number(tid)];
            return (
              <div key={tid}>
                <div className="flex items-center gap-2 mb-3">
                  <Tag className="w-4 h-4" style={{ color: t?.cor || '#6366f1' }} />
                  <Link to={`/topics/${tid}/notes`}
                    className="text-sm font-semibold hover:underline" style={{ color: t?.cor || '#6366f1' }}>
                    {t?.nome || `Tópico ${tid}`}
                  </Link>
                  <span className="text-xs text-slate-500">({items.length})</span>
                </div>
                <div className="space-y-2 pl-6 border-l-2" style={{ borderColor: (t?.cor || '#6366f1') + '33' }}>
                  {items.map((n) => (
                    <div key={n.id}
                      className="group flex items-start justify-between bg-slate-800/40 border border-slate-700/40 rounded-xl p-4 hover:border-slate-600/50 transition">
                      <Link to={`/topics/${n.topicId}/notes`} className="flex-1 min-w-0">
                        <h4 className="text-white font-medium truncate">{n.titulo}</h4>
                        <div className="flex items-center gap-1.5 mt-1 text-xs text-slate-500">
                          <Clock className="w-3 h-3" />
                          <span>{formatDate(n.dataAtualizacao || n.dataCriacao)}</span>
                        </div>
                      </Link>
                      <button onClick={() => handleDelete(n.id)}
                        className="p-2 rounded-lg text-slate-500 hover:text-red-400 hover:bg-slate-700/50 opacity-0 group-hover:opacity-100 transition cursor-pointer">
                        <Trash2 className="w-4 h-4" />
                      </button>
                    </div>
                  ))}
                </div>
              </div>
            );
          })}
        </div>
      )}
    </div>
  );
}
