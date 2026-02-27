import { useState, useEffect, useCallback } from 'react';
import { useParams, Link } from 'react-router-dom';
import { Plus, ArrowLeft, BookOpen, FileText, Search } from 'lucide-react';
import api from '../services/api';
import NoteEditor from '../components/NoteEditor';
import NoteItem from '../components/NoteItem';
import { getIcon } from '../utils/icons';

export default function NotesPage() {
  const { topicId } = useParams();
  const [topic, setTopic] = useState(null);
  const [notes, setNotes] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showEditor, setShowEditor] = useState(false);
  const [editingNote, setEditingNote] = useState(null);
  const [search, setSearch] = useState('');

  const load = useCallback(async () => {
    setLoading(true);
    try {
      const [tRes, nRes] = await Promise.all([
        api.get(`/topics/${topicId}`),
        api.get(`/notes?topicId=${topicId}`),
      ]);
      setTopic(tRes.data);
      setNotes(nRes.data);
      
      // Registrar atividade (último acesso)
      api.post(`/usertopics/activity/${topicId}`).catch(console.error);
    } catch (err) { console.error(err); }
    finally { setLoading(false); }
  }, [topicId]);

  useEffect(() => { load(); }, [load]);

  const handleSave = async (data) => {
    if (editingNote) {
      await api.put(`/notes/${editingNote.id}`, data);
    } else {
      await api.post('/notes', { ...data, topicId: Number(topicId) });
    }
    setShowEditor(false);
    setEditingNote(null);
    load();
  };

  const handleEdit = (note) => { setEditingNote(note); setShowEditor(true); };
  const handleDelete = async (id) => { await api.delete(`/notes/${id}`); load(); };
  const handleCancel = () => { setShowEditor(false); setEditingNote(null); };

  const filtered = notes.filter(n =>
    n.titulo.toLowerCase().includes(search.toLowerCase())
  );

  const topicColor = topic?.cor || '#6366f1';
  const IconComp = topic ? getIcon(topic.icone) : BookOpen;

  if (loading) {
    return (
      <div className="flex items-center justify-center h-full">
        <div className="w-8 h-8 border-2 border-indigo-500 border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  if (!topic) {
    return (
      <div className="p-8 text-center">
        <p className="text-slate-400">Tópico não encontrado.</p>
        <Link to="/disciplinas" className="text-indigo-400 hover:underline mt-2 inline-block">Voltar</Link>
      </div>
    );
  }

  return (
    <div className="p-4 md:p-6 lg:p-8 max-w-5xl mx-auto space-y-4 lg:space-y-6">
      {/* Header */}
      <div className="flex items-start gap-3 lg:gap-4">
        <Link to="/disciplinas"
          className="mt-0.5 lg:mt-1 p-2 rounded-xl text-slate-400 hover:text-white hover:bg-slate-800/50 active:scale-95 transition">
          <ArrowLeft className="w-4 h-4 lg:w-5 lg:h-5" />
        </Link>
        <div className="flex-1 min-w-0">
          <div className="flex items-center gap-2 lg:gap-3 mb-1">
            <div className="p-2 lg:p-2.5 rounded-xl" style={{ backgroundColor: topicColor + '22', color: topicColor }}>
              <IconComp className="w-5 h-5 lg:w-6 lg:h-6" />
            </div>
            <div className="flex-1 min-w-0">
              <span className="text-[10px] lg:text-xs font-medium px-2 lg:px-2.5 py-0.5 rounded-full"
                style={{ backgroundColor: topicColor + '22', color: topicColor }}>
                {topic.categoria}
              </span>
              <h1 className="text-lg md:text-xl lg:text-2xl font-bold text-white mt-0.5 lg:mt-1 truncate">{topic.nome}</h1>
            </div>
          </div>
          <p className="text-slate-400 text-xs md:text-sm mt-1 ml-0 lg:ml-14 line-clamp-2">{topic.descricao}</p>
        </div>
      </div>

      {/* Toolbar */}
      <div className="flex flex-col gap-2.5 lg:gap-3">
        <div className="flex items-center gap-2 text-xs lg:text-sm text-slate-400">
          <FileText className="w-3.5 h-3.5 lg:w-4 lg:h-4" />
          <span>{notes.length} anotação{notes.length !== 1 ? 'ões' : ''}</span>
        </div>
        <div className="flex flex-col sm:flex-row items-stretch sm:items-center gap-2.5 lg:gap-3">
          <div className="relative flex-1">
            <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-slate-500" />
            <input type="text" placeholder="Buscar anotações..."
              value={search} onChange={(e) => setSearch(e.target.value)}
              className="w-full pl-10 pr-4 py-2.5 lg:py-3 rounded-xl bg-slate-800/50 border border-slate-700/50 text-white placeholder-slate-500 text-sm outline-none focus:ring-2 focus:ring-indigo-500 transition" />
          </div>
          <button onClick={() => { setEditingNote(null); setShowEditor(true); }}
            className="flex items-center justify-center gap-2 px-4 py-2.5 lg:py-3 text-sm font-medium text-white rounded-xl transition active:scale-95 cursor-pointer"
            style={{ backgroundColor: topicColor }}>
            <Plus className="w-4 h-4" /> <span>Nova Anotação</span>
          </button>
        </div>
      </div>

      {/* Editor */}
      {showEditor && (
        <NoteEditor note={editingNote} onSave={handleSave} onCancel={handleCancel} topicColor={topicColor} />
      )}

      {/* Notes List */}
      {filtered.length === 0 ? (
        <div className="text-center py-12 lg:py-16">
          <BookOpen className="w-10 h-10 lg:w-12 lg:h-12 text-slate-600 mx-auto mb-4" />
          <p className="text-slate-400 mb-1 text-sm">
            {search ? 'Nenhuma anotação encontrada.' : 'Nenhuma anotação ainda.'}
          </p>
          {!search && (
            <p className="text-slate-500 text-xs lg:text-sm">Clique em "Nova Anotação" para começar.</p>
          )}
        </div>
      ) : (
        <div className="space-y-2.5 lg:space-y-3">
          {filtered.map((n) => (
            <NoteItem key={n.id} note={n} onEdit={handleEdit} onDelete={handleDelete} topicColor={topicColor} />
          ))}
        </div>
      )}
    </div>
  );
}
