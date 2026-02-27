import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { FileText, Search, BookOpen, Clock, Tag, Trash2, Plus, X, Loader2 } from 'lucide-react';
import ReactQuill from 'react-quill';
import api from '../services/api';
import 'react-quill/dist/quill.snow.css';

function formatDate(d) {
  return new Date(d).toLocaleDateString('pt-BR', { day: '2-digit', month: 'short', year: 'numeric', hour: '2-digit', minute: '2-digit' });
}

export default function MyNotesPage() {
  const [notes, setNotes] = useState([]);
  const [topics, setTopics] = useState([]);
  const [loading, setLoading] = useState(true);
  const [search, setSearch] = useState('');
  const [filterTopic, setFilterTopic] = useState('');
  
  // Modal state
  const [showModal, setShowModal] = useState(false);
  const [newNote, setNewNote] = useState({ topicId: '', titulo: '', conteudo: '' });
  const [creating, setCreating] = useState(false);

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

  const handleCreateNote = async () => {
    if (!newNote.topicId.trim()) {
      alert('Selecione uma disciplina');
      return;
    }
    if (!newNote.titulo.trim()) {
      alert('Digite um título para a anotação');
      return;
    }
    if (!newNote.conteudo.trim()) {
      alert('Digite o conteúdo da anotação');
      return;
    }

    try {
      setCreating(true);
      const res = await api.post('/notes', {
        topicId: Number(newNote.topicId),
        titulo: newNote.titulo,
        conteudo: newNote.conteudo
      });
      
      setNotes((prev) => [res.data, ...prev]);
      setShowModal(false);
      setNewNote({ topicId: '', titulo: '', conteudo: '' });
    } catch (error) {
      console.error('Erro ao criar anotação:', error);
      alert('Erro ao criar anotação. Tente novamente.');
    } finally {
      setCreating(false);
    }
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
    <div className="p-4 md:p-6 lg:p-8 max-w-5xl mx-auto space-y-4 lg:space-y-6">
      {/* Header */}
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-xl md:text-2xl font-bold text-white flex items-center gap-2 lg:gap-3">
            <FileText className="w-6 h-6 lg:w-7 lg:h-7 text-indigo-400" /> Minhas Anotações
          </h1>
          <p className="text-slate-400 text-xs md:text-sm mt-1">Todas as suas anotações em um só lugar.</p>
        </div>
        <button
          onClick={() => setShowModal(true)}
          className="flex items-center gap-2 px-4 py-2.5 rounded-lg bg-indigo-600 hover:bg-indigo-700 active:bg-indigo-800 text-white font-medium transition text-sm"
        >
          <Plus className="w-4 h-4" />
          Nova Anotação
        </button>
      </div>

      {/* Filters */}
      <div className="flex flex-col gap-2.5 lg:gap-3">
        <div className="relative flex-1">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-slate-500" />
          <input type="text" placeholder="Buscar por título..."
            value={search} onChange={(e) => setSearch(e.target.value)}
            className="w-full pl-10 pr-4 py-2.5 lg:py-3 rounded-xl bg-slate-800/50 border border-slate-700/50 text-white placeholder-slate-500 text-sm outline-none focus:ring-2 focus:ring-indigo-500 transition" />
        </div>
        <select value={filterTopic} onChange={(e) => setFilterTopic(e.target.value)}
          className="px-4 py-2.5 lg:py-3 rounded-xl bg-slate-800/50 border border-slate-700/50 text-white text-sm outline-none focus:ring-2 focus:ring-indigo-500 transition w-full">
          <option value="">Todas as disciplinas</option>
          {usedTopics.map((tid) => (
            <option key={tid} value={tid}>{topicMap[tid]?.nome || `Tópico ${tid}`}</option>
          ))}
        </select>
      </div>

      {/* Stats */}
      <div className="grid grid-cols-2 md:grid-cols-3 gap-2.5 lg:gap-3">
        <div className="bg-slate-800/50 border border-slate-700/50 rounded-xl p-3 lg:p-4 text-center">
          <p className="text-xl lg:text-2xl font-bold text-white">{notes.length}</p>
          <p className="text-[10px] lg:text-xs text-slate-400 mt-0.5 lg:mt-1">Total de Anotações</p>
        </div>
        <div className="bg-slate-800/50 border border-slate-700/50 rounded-xl p-3 lg:p-4 text-center">
          <p className="text-xl lg:text-2xl font-bold text-indigo-400">{usedTopics.length}</p>
          <p className="text-[10px] lg:text-xs text-slate-400 mt-0.5 lg:mt-1">Disciplinas Usadas</p>
        </div>
        <div className="bg-slate-800/50 border border-slate-700/50 rounded-xl p-3 lg:p-4 text-center col-span-2 md:col-span-1">
          <p className="text-xl lg:text-2xl font-bold text-purple-400">{filtered.length}</p>
          <p className="text-[10px] lg:text-xs text-slate-400 mt-0.5 lg:mt-1">Resultados</p>
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
            <button onClick={() => setShowModal(true)} className="text-indigo-400 hover:underline text-sm mt-2">
              Criar sua primeira anotação
            </button>
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

      {/* Modal para criar nova anotação */}
      {showModal && (
        <div className="fixed inset-0 bg-black/50 backdrop-blur-sm flex items-center justify-center p-4 z-50">
          <div className="bg-slate-800 border border-slate-700 rounded-2xl w-full max-w-2xl max-h-[90vh] overflow-y-auto">
            {/* Header */}
            <div className="sticky top-0 flex items-center justify-between p-6 border-b border-slate-700 bg-slate-800">
              <h2 className="text-xl font-bold text-white">Nova Anotação</h2>
              <button
                onClick={() => setShowModal(false)}
                className="p-2 rounded-lg hover:bg-slate-700 transition text-slate-400"
              >
                <X className="w-5 h-5" />
              </button>
            </div>

            {/* Conteúdo */}
            <div className="p-6 space-y-4">
              {/* Disciplina */}
              <div>
                <label className="block text-sm font-medium text-slate-300 mb-2">
                  Disciplina *
                </label>
                <select
                  value={newNote.topicId}
                  onChange={(e) => setNewNote({ ...newNote, topicId: e.target.value })}
                  className="w-full px-4 py-2.5 rounded-lg bg-slate-700/50 border border-slate-600 text-white text-sm outline-none focus:ring-2 focus:ring-indigo-500 transition"
                >
                  <option value="">Selecione uma disciplina...</option>
                  {topics.map(t => (
                    <option key={t.id} value={t.id}>{t.nome}</option>
                  ))}
                </select>
              </div>

              {/* Título */}
              <div>
                <label className="block text-sm font-medium text-slate-300 mb-2">
                  Título *
                </label>
                <input
                  type="text"
                  value={newNote.titulo}
                  onChange={(e) => setNewNote({ ...newNote, titulo: e.target.value })}
                  placeholder="Ex: Conceitos de Farmacocinética"
                  className="w-full px-4 py-2.5 rounded-lg bg-slate-700/50 border border-slate-600 text-white placeholder-slate-500 text-sm outline-none focus:ring-2 focus:ring-indigo-500 transition"
                />
              </div>

              {/* Conteúdo */}
              <div>
                <label className="block text-sm font-medium text-slate-300 mb-2">
                  Conteúdo *
                </label>
                <div className="bg-slate-700/50 rounded-lg border border-slate-600 overflow-hidden">
                  <ReactQuill
                    theme="snow"
                    value={newNote.conteudo}
                    onChange={(value) => setNewNote({ ...newNote, conteudo: value })}
                    placeholder="Escreva sua anotação aqui..."
                    modules={{
                      toolbar: [
                        [{ 'header': [1, 2, 3, false] }],
                        ['bold', 'italic', 'underline', 'strike'],
                        [{ 'list': 'ordered'}, { 'list': 'bullet' }],
                        ['blockquote', 'code-block'],
                        ['link'],
                        ['clean']
                      ]
                    }}
                    style={{ minHeight: '300px', fontSize: '14px' }}
                  />
                </div>
              </div>
            </div>

            {/* Footer */}
            <div className="sticky bottom-0 flex gap-3 p-6 border-t border-slate-700 bg-slate-800">
              <button
                onClick={() => setShowModal(false)}
                disabled={creating}
                className="flex-1 px-4 py-2.5 rounded-lg border border-slate-600 text-white font-medium hover:bg-slate-700 disabled:opacity-50 disabled:cursor-not-allowed transition"
              >
                Cancelar
              </button>
              <button
                onClick={handleCreateNote}
                disabled={creating}
                className="flex-1 flex items-center justify-center gap-2 px-4 py-2.5 rounded-lg bg-indigo-600 hover:bg-indigo-700 active:bg-indigo-800 disabled:opacity-50 disabled:cursor-not-allowed text-white font-medium transition"
              >
                {creating ? (
                  <>
                    <Loader2 className="w-4 h-4 animate-spin" />
                    Salvando...
                  </>
                ) : (
                  <>
                    <Plus className="w-4 h-4" />
                    Criar Anotação
                  </>
                )}
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
