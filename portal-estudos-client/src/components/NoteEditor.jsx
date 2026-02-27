import { useState, useEffect } from 'react';
import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css';
import { Save, X } from 'lucide-react';

const quillModules = {
  toolbar: [
    [{ header: [1, 2, 3, false] }],
    ['bold', 'italic', 'underline', 'strike'],
    [{ list: 'ordered' }, { list: 'bullet' }],
    ['blockquote', 'code-block'],
    ['link'],
    ['clean'],
  ],
};

export default function NoteEditor({ note, onSave, onCancel, topicColor = '#6366f1' }) {
  const [titulo, setTitulo] = useState('');
  const [conteudo, setConteudo] = useState('');
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState('');

  useEffect(() => {
    if (note) { setTitulo(note.titulo); setConteudo(note.conteudo); }
    else { setTitulo(''); setConteudo(''); }
  }, [note]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    if (!titulo.trim()) { setError('Título obrigatório.'); return; }
    if (!conteudo.trim() || conteudo === '<p><br></p>') { setError('Conteúdo obrigatório.'); return; }
    setSaving(true);
    try { await onSave({ titulo: titulo.trim(), conteudo }); }
    catch { setError('Erro ao salvar.'); }
    finally { setSaving(false); }
  };

  return (
    <div className="rounded-2xl bg-slate-800/50 border border-slate-700/50 overflow-hidden">
      <div className="h-1" style={{ backgroundColor: topicColor }} />
      <form onSubmit={handleSubmit} className="p-4 md:p-5 lg:p-6 space-y-4 lg:space-y-5">
        <h3 className="text-base lg:text-lg font-semibold text-white">{note ? 'Editar Anotação' : 'Nova Anotação'}</h3>
        {error && <div className="bg-red-500/10 border border-red-500/20 text-red-400 text-xs lg:text-sm rounded-xl p-2.5 lg:p-3">{error}</div>}
        <div>
          <label className="block text-xs lg:text-sm font-medium text-slate-300 mb-1.5">Título</label>
          <input type="text" value={titulo} onChange={(e) => setTitulo(e.target.value)}
            className="w-full px-3 lg:px-4 py-2 lg:py-2.5 rounded-xl bg-slate-900/50 border border-slate-700 text-white placeholder-slate-500 focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 outline-none transition text-sm"
            placeholder="Ex: Resumo sobre farmacocinética..." maxLength={200} />
        </div>
        <div>
          <label className="block text-xs lg:text-sm font-medium text-slate-300 mb-1.5">Conteúdo</label>
          <ReactQuill theme="snow" value={conteudo} onChange={setConteudo} modules={quillModules}
            placeholder="Escreva sua anotação..." />
        </div>
        <div className="flex flex-col-reverse sm:flex-row items-stretch sm:items-center gap-2.5 lg:gap-3 sm:justify-end pt-2 border-t border-slate-700/50">
          <button type="button" onClick={onCancel}
            className="flex items-center justify-center gap-2 px-4 py-2.5 lg:py-2 text-sm text-slate-400 hover:text-white border border-slate-700 rounded-xl transition active:scale-95 cursor-pointer">
            <X className="w-4 h-4" /> Cancelar
          </button>
          <button type="submit" disabled={saving}
            className="flex items-center justify-center gap-2 px-5 py-2.5 lg:py-2 text-sm font-medium text-white rounded-xl transition disabled:opacity-50 active:scale-95 cursor-pointer"
            style={{ backgroundColor: topicColor }}>
            <Save className="w-4 h-4" /> {saving ? 'Salvando...' : 'Salvar'}
          </button>
        </div>
      </form>
    </div>
  );
}
