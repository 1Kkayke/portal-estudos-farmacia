import { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import api from '../services/api';
import { getIcon } from '../utils/icons';
import {
  FileText, Clock, BookOpen, ChevronRight, Search,
  ArrowLeft, Loader2, BarChart3, GraduationCap,
} from 'lucide-react';

const diffColor = {
  'Básico': 'bg-green-500/10 text-green-400 border-green-500/20',
  'Intermediário': 'bg-yellow-500/10 text-yellow-400 border-yellow-500/20',
  'Avançado': 'bg-red-500/10 text-red-400 border-red-500/20',
};

const FALLBACK_DOC = '/docs/templates/cover-1.svg';

export default function DocumentsPage() {
  const { topicId } = useParams();
  const [docs, setDocs] = useState([]);
  const [topic, setTopic] = useState(null);
  const [selectedDoc, setSelectedDoc] = useState(null);
  const [loading, setLoading] = useState(true);
  const [search, setSearch] = useState('');

  const apiBase = (api.defaults.baseURL || '').replace(/\/api$/, '');
  const apiOrigin = apiBase;
  const resolveDocUrl = (path) => {
    if (!path) return '';
    if (path.startsWith('http')) return path;
    if (path.startsWith('/api/')) return `${apiOrigin}${path}`;
    if (path.startsWith('/docs/')) return `${apiOrigin}${path}`;
    return `${apiBase}${path}`;
  };

  const handleDownloadPdf = async () => {
    if (!selectedDoc?.pdfUrl) return;
    try {
      const pdfPath = selectedDoc.pdfUrl.replace(/^\/api/, '');
      const res = await api.get(pdfPath, { responseType: 'blob' });
      const contentType = res.headers?.['content-type'] || '';
      const ext = contentType.includes('text/html') ? 'html' : 'pdf';
      const fileName = `documento-${selectedDoc.topicId}-${selectedDoc.id}.${ext}`;
      const url = window.URL.createObjectURL(new Blob([res.data], { type: contentType || 'application/octet-stream' }));
      const a = document.createElement('a');
      a.href = url;
      a.download = fileName;
      document.body.appendChild(a);
      a.click();
      a.remove();
      window.URL.revokeObjectURL(url);
    } catch (err) {
      console.error('Erro ao baixar PDF:', err);
    }
  };

  useEffect(() => {
    const load = async () => {
      try {
        const [docsRes, topicsRes] = await Promise.all([
          api.get(`/topics/${topicId}/documents`),
          api.get('/topics'),
        ]);
        setDocs(docsRes.data);
        setTopic(topicsRes.data.find(t => t.id === parseInt(topicId)));        
        // Registrar atividade (último acesso)
        api.post(`/usertopics/activity/${topicId}`).catch(console.error);      } catch (err) {
        console.error(err);
      } finally {
        setLoading(false);
      }
    };
    load();
  }, [topicId]);

  const loadDocument = async (docId) => {
    try {
      const res = await api.get(`/topics/${topicId}/documents/${docId}`);
      setSelectedDoc(res.data);
    } catch (err) {
      console.error(err);
    }
  };

  const filtered = docs.filter(d =>
    d.titulo.toLowerCase().includes(search.toLowerCase()) ||
    d.resumo.toLowerCase().includes(search.toLowerCase())
  );

  if (loading) {
    return (
      <div className="flex items-center justify-center min-h-screen">
        <Loader2 className="w-8 h-8 animate-spin text-indigo-400" />
      </div>
    );
  }

  // Reading view
  if (selectedDoc) {
    const youtubeQuery = encodeURIComponent(`${selectedDoc.titulo} ${topic?.nome ?? ''} farmacia aula`);
    const youtubeEmbed = `https://www.youtube.com/embed?listType=search&list=${youtubeQuery}`;
    const pdfLink = resolveDocUrl(selectedDoc.pdfUrl || '');
    return (
      <div className="p-4 md:p-6 max-w-4xl mx-auto animate-fadeIn">
        <button
          onClick={() => setSelectedDoc(null)}
          className="flex items-center gap-2 text-slate-400 hover:text-white mb-4 lg:mb-6 transition cursor-pointer active:scale-95"
        >
          <ArrowLeft className="w-4 h-4" /> Voltar aos documentos
        </button>

        <div className="bg-slate-800/50 rounded-xl lg:rounded-2xl border border-slate-700/50 p-4 md:p-6 lg:p-8">
          <div className="mb-6">
            <img
              src={resolveDocUrl(selectedDoc.capaUrl || FALLBACK_DOC)}
              alt="Capa do documento"
              className="w-full h-48 md:h-56 object-cover rounded-xl border border-slate-700/50"
              loading="lazy"
            />
          </div>

          <div className="flex flex-wrap items-center gap-2 lg:gap-3 mb-3 lg:mb-4">
            <span className={`px-2.5 lg:px-3 py-0.5 lg:py-1 text-[10px] lg:text-xs font-medium rounded-full border ${diffColor[selectedDoc.dificuldade] || diffColor['Intermediário']}`}>
              {selectedDoc.dificuldade}
            </span>
            <span className="flex items-center gap-1 text-[10px] lg:text-xs text-slate-500">
              <Clock className="w-3.5 h-3.5" /> {selectedDoc.leituraMinutos} min
            </span>
          </div>

          <h1 className="text-2xl font-bold text-white mb-2">{selectedDoc.titulo}</h1>
          <p className="text-slate-400 text-sm mb-8">{selectedDoc.resumo}</p>

          {pdfLink && (
            <div className="space-y-3 mb-6">
              <div className="flex gap-2 flex-wrap">
                <button
                  onClick={() => {
                    const apostilaUrl = `${apiOrigin}/api/topics/${topicId}/documents/${selectedDoc.id}/apostila`;
                    window.open(apostilaUrl, '_blank', 'noopener,noreferrer');
                  }}
                  className="inline-flex items-center gap-2 px-4 py-2 rounded-lg bg-indigo-600 text-white text-sm hover:bg-indigo-700 active:bg-indigo-800 transition"
                >
                  <BookOpen className="w-4 h-4" />
                  Abrir Apostila
                </button>
                <button
                  onClick={handleDownloadPdf}
                  className="inline-flex items-center gap-2 px-4 py-2 rounded-lg bg-slate-700 text-white text-sm hover:bg-slate-600 active:bg-slate-800 transition"
                >
                  <FileText className="w-4 h-4" />
                  Baixar
                </button>
              </div>
              <p className="text-slate-500 text-xs">Clique em "Abrir Apostila" para visualizar em nova aba ou "Baixar" para salvar o arquivo.</p>
            </div>
          )}

          <div
            className="prose prose-invert prose-indigo max-w-none"
            dangerouslySetInnerHTML={{ __html: selectedDoc.conteudo }}
          />
        </div>

        <div className="bg-slate-800/40 rounded-xl border border-slate-700/50 p-4 md:p-6 mt-6">
          <h2 className="text-lg font-semibold text-white mb-3">Videos relacionados</h2>
          <div className="aspect-video rounded-xl overflow-hidden border border-slate-700/50">
            <iframe
              title="Videos relacionados"
              src={youtubeEmbed}
              className="w-full h-full"
              allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
              allowFullScreen
            />
          </div>
          <a
            href={`https://www.youtube.com/results?search_query=${youtubeQuery}`}
            target="_blank"
            rel="noreferrer"
            className="inline-flex items-center gap-2 text-sm text-indigo-400 hover:text-indigo-300 mt-3"
          >
            Ver mais no YouTube
            <ChevronRight className="w-4 h-4" />
          </a>
        </div>

        {/* Navigation */}
        <div className="flex items-center justify-between mt-6">
          {(() => {
            const currentIdx = docs.findIndex(d => d.id === selectedDoc.id);
            const prev = currentIdx > 0 ? docs[currentIdx - 1] : null;
            const next = currentIdx < docs.length - 1 ? docs[currentIdx + 1] : null;
            return (
              <>
                {prev ? (
                  <button onClick={() => loadDocument(prev.id)} className="flex items-center gap-2 text-sm text-slate-400 hover:text-white transition cursor-pointer">
                    <ArrowLeft className="w-4 h-4" /> {prev.titulo}
                  </button>
                ) : <div />}
                {next ? (
                  <button onClick={() => loadDocument(next.id)} className="flex items-center gap-2 text-sm text-indigo-400 hover:text-indigo-300 transition cursor-pointer">
                    {next.titulo} <ChevronRight className="w-4 h-4" />
                  </button>
                ) : <div />}
              </>
            );
          })()}
        </div>
      </div>
    );
  }

  // List view
  const TopicIcon = topic ? getIcon(topic.icone) : FileText;

  return (
    <div className="p-6 max-w-6xl mx-auto animate-fadeIn">
      {/* Header */}
      <div className="flex items-center gap-3 mb-2">
        <Link to="/disciplinas" className="text-slate-400 hover:text-white transition">
          <ArrowLeft className="w-5 h-5" />
        </Link>
        <div className="flex items-center justify-center w-10 h-10 rounded-xl bg-indigo-500/10 border border-indigo-500/20">
          <TopicIcon className="w-5 h-5 text-indigo-400" />
        </div>
        <div>
          <h1 className="text-xl font-bold text-white">{topic?.nome}</h1>
          <p className="text-xs text-slate-500">Documentos de Estudo</p>
        </div>
      </div>

      {/* Stats */}
      <div className="grid grid-cols-3 gap-3 my-6">
        <div className="bg-slate-800/50 rounded-xl border border-slate-700/50 p-4 text-center">
          <FileText className="w-5 h-5 text-indigo-400 mx-auto mb-1" />
          <div className="text-lg font-bold text-white">{docs.length}</div>
          <div className="text-xs text-slate-500">Documentos</div>
        </div>
        <div className="bg-slate-800/50 rounded-xl border border-slate-700/50 p-4 text-center">
          <Clock className="w-5 h-5 text-emerald-400 mx-auto mb-1" />
          <div className="text-lg font-bold text-white">{docs.reduce((acc, d) => acc + d.leituraMinutos, 0)}</div>
          <div className="text-xs text-slate-500">Min. Leitura</div>
        </div>
        <div className="bg-slate-800/50 rounded-xl border border-slate-700/50 p-4 text-center">
          <BarChart3 className="w-5 h-5 text-purple-400 mx-auto mb-1" />
          <div className="text-lg font-bold text-white">3</div>
          <div className="text-xs text-slate-500">Níveis</div>
        </div>
      </div>

      {/* Search */}
      <div className="relative mb-6">
        <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-slate-500" />
        <input
          type="text"
          placeholder="Pesquisar documentos..."
          value={search}
          onChange={e => setSearch(e.target.value)}
          className="w-full pl-10 pr-4 py-2.5 bg-slate-800/50 rounded-xl border border-slate-700/50 text-sm text-white placeholder-slate-500 focus:outline-none focus:ring-2 focus:ring-indigo-500/50"
        />
      </div>

      {/* Quick links */}
      <div className="flex gap-2 mb-6 flex-wrap">
        <Link
          to={`/topics/${topicId}/questoes`}
          className="flex items-center gap-2 px-4 py-2 bg-amber-500/10 rounded-xl border border-amber-500/20 text-amber-400 text-sm hover:bg-amber-500/20 transition"
        >
          <BookOpen className="w-4 h-4" /> Questões
        </Link>
        <Link
          to={`/topics/${topicId}/prova`}
          className="flex items-center gap-2 px-4 py-2 bg-emerald-500/10 rounded-xl border border-emerald-500/20 text-emerald-400 text-sm hover:bg-emerald-500/20 transition"
        >
          <GraduationCap className="w-4 h-4" /> Fazer Prova
        </Link>
      </div>

      {/* Document list */}
      <div className="space-y-3">
        {filtered.map((doc, idx) => (
          <button
            key={doc.id}
            onClick={() => loadDocument(doc.id)}
            className="w-full flex items-center gap-4 p-4 bg-slate-800/50 rounded-xl border border-slate-700/50 hover:border-indigo-500/30 hover:bg-slate-800/80 transition text-left cursor-pointer group"
          >
            <div className="relative w-16 h-16 rounded-lg overflow-hidden border border-slate-700/50 shrink-0">
              <img
                src={resolveDocUrl(doc.capaUrl || FALLBACK_DOC)}
                alt="Capa"
                className="w-full h-full object-cover"
                loading="lazy"
              />
              <div className="absolute bottom-1 right-1 text-[10px] px-1.5 py-0.5 rounded bg-slate-900/80 text-slate-300">
                {String(idx + 1).padStart(2, '0')}
              </div>
            </div>
            <div className="flex-1 min-w-0">
              <h3 className="text-white font-medium text-sm group-hover:text-indigo-300 transition truncate">{doc.titulo}</h3>
              <p className="text-slate-500 text-xs mt-0.5 truncate">{doc.resumo}</p>
            </div>
            <div className="flex items-center gap-3 shrink-0">
              <span className={`px-2.5 py-0.5 text-[10px] font-medium rounded-full border ${diffColor[doc.dificuldade] || diffColor['Intermediário']}`}>
                {doc.dificuldade}
              </span>
              <span className="text-xs text-slate-500 hidden sm:block">{doc.leituraMinutos} min</span>
              <ChevronRight className="w-4 h-4 text-slate-600 group-hover:text-indigo-400 transition" />
            </div>
          </button>
        ))}
      </div>

      {filtered.length === 0 && (
        <div className="text-center py-12 text-slate-500">
          <FileText className="w-12 h-12 mx-auto mb-3 opacity-50" />
          <p>Nenhum documento encontrado.</p>
        </div>
      )}
    </div>
  );
}
