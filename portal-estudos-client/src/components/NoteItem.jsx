import { useState } from 'react';
import { Edit3, Trash2, ChevronDown, ChevronUp, Clock } from 'lucide-react';

function formatDate(d) {
  return new Date(d).toLocaleDateString('pt-BR', { day: '2-digit', month: 'short', year: 'numeric', hour: '2-digit', minute: '2-digit' });
}

export default function NoteItem({ note, onEdit, onDelete, topicColor = '#6366f1' }) {
  const [expanded, setExpanded] = useState(false);
  const [deleting, setDeleting] = useState(false);

  const handleDelete = async () => {
    if (!window.confirm('Excluir esta anotação?')) return;
    setDeleting(true);
    try { await onDelete(note.id); }
    catch { setDeleting(false); }
  };

  return (
    <div className="group rounded-2xl bg-slate-800/50 border border-slate-700/50 hover:border-slate-600/50 transition overflow-hidden">
      <div className="h-0.5" style={{ backgroundColor: topicColor, opacity: 0.6 }} />
      <div className="p-3.5 md:p-4 lg:p-5">
        {/* Header */}
        <div className="flex items-start justify-between gap-2 lg:gap-4">
          <button onClick={() => setExpanded(!expanded)}
            className="flex-1 flex items-start gap-2 lg:gap-3 text-left cursor-pointer active:scale-[0.99]">
            <div className="mt-0.5 p-1.5 rounded-lg bg-slate-700/50 text-slate-400 shrink-0">
              {expanded ? <ChevronUp className="w-3.5 h-3.5 lg:w-4 lg:h-4" /> : <ChevronDown className="w-3.5 h-3.5 lg:w-4 lg:h-4" />}
            </div>
            <div className="flex-1 min-w-0">
              <h4 className="text-white font-medium text-sm lg:text-base truncate">{note.titulo}</h4>
              <div className="flex items-center gap-1 lg:gap-1.5 mt-0.5 lg:mt-1 text-[10px] lg:text-xs text-slate-500">
                <Clock className="w-3 h-3" />
                <span>{formatDate(note.dataAtualizacao || note.dataCriacao)}</span>
              </div>
            </div>
          </button>
          <div className="flex items-center gap-0.5 lg:gap-1 lg:opacity-0 lg:group-hover:opacity-100 transition">
            <button onClick={() => onEdit(note)} title="Editar"
              className="p-1.5 lg:p-2 rounded-lg text-slate-400 hover:text-indigo-400 active:bg-slate-700/70 hover:bg-slate-700/50 transition cursor-pointer active:scale-95">
              <Edit3 className="w-4 h-4" />
            </button>
            <button onClick={handleDelete} disabled={deleting} title="Excluir"
              className="p-1.5 lg:p-2 rounded-lg text-slate-400 hover:text-red-400 active:bg-slate-700/70 hover:bg-slate-700/50 transition cursor-pointer disabled:opacity-50 active:scale-95">
              <Trash2 className="w-4 h-4" />
            </button>
          </div>
        </div>
        {/* Content */}
        {expanded && (
          <div className="mt-3 lg:mt-4 pt-3 lg:pt-4 border-t border-slate-700/50">
            <div className="note-content prose prose-invert prose-sm max-w-none
              prose-headings:text-white prose-p:text-slate-300 prose-a:text-indigo-400
              prose-strong:text-white prose-code:text-indigo-300 prose-blockquote:border-indigo-500"
              dangerouslySetInnerHTML={{ __html: note.conteudo }} />
          </div>
        )}
      </div>
    </div>
  );
}
