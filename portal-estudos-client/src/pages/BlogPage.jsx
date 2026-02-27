import { useState, useEffect, useCallback } from 'react';
import { BookOpen, Clock, User, ChevronRight, Search, Tag, ExternalLink, RefreshCw, Globe, Newspaper, ArrowLeft, Calendar, Link2 } from 'lucide-react';
import api from '../services/api';

const FALLBACK_IMG = 'https://images.unsplash.com/photo-1532187863486-abf9dbad1b69?w=800&h=400&fit=crop&auto=format&q=80';

function formatDate(d) {
  return new Date(d).toLocaleDateString('pt-BR', { day: '2-digit', month: 'long', year: 'numeric' });
}

function handleImgError(e) {
  if (e.target.src !== FALLBACK_IMG) {
    e.target.src = FALLBACK_IMG;
  }
}

export default function BlogPage() {
  const [articles, setArticles] = useState([]);
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [selected, setSelected] = useState(null);
  const [search, setSearch] = useState('');
  const [catFilter, setCatFilter] = useState('');
  const [refreshing, setRefreshing] = useState(false);

  const load = useCallback(async () => {
    try {
      const params = {};
      if (catFilter) params.categoria = catFilter;
      if (search) params.search = search;
      const [artRes, catRes] = await Promise.all([
        api.get('/blog', { params }),
        api.get('/blog/categories'),
      ]);
      setArticles(artRes.data);
      setCategories(catRes.data);
    } catch (e) { console.error('Blog load error:', e); }
    finally { setLoading(false); setRefreshing(false); }
  }, [catFilter, search]);

  useEffect(() => { load(); }, [load]);

  const refresh = () => { setRefreshing(true); load(); };

  // ─── Article reader view ───
  if (selected) {
    return (
      <div className="p-6 lg:p-8 max-w-4xl mx-auto animate-fadeIn">
        <button onClick={() => setSelected(null)}
          className="flex items-center gap-2 text-indigo-400 hover:text-indigo-300 text-sm mb-6 cursor-pointer transition">
          <ArrowLeft className="w-4 h-4" /> Voltar aos artigos
        </button>

        <article>
          {/* Hero image */}
          {selected.imagemUrl && (
            <div className="relative rounded-2xl overflow-hidden mb-6 aspect-[2/1]">
              <img src={selected.imagemUrl} alt={selected.titulo}
                className="w-full h-full object-cover" loading="lazy"
                onError={handleImgError} />
              <div className="absolute inset-0 bg-gradient-to-t from-slate-900/80 to-transparent" />
              {selected.imagemCredito && (
                <span className="absolute bottom-3 right-4 text-xs text-slate-400">{selected.imagemCredito}</span>
              )}
            </div>
          )}

          {/* Meta */}
          <div className="flex flex-wrap items-center gap-2 mb-3">
            <span className="text-xs font-medium px-2.5 py-1 rounded-full bg-indigo-500/10 text-indigo-400">
              {selected.categoria}
            </span>
            {selected.isExterno && (
              <span className="text-xs font-medium px-2.5 py-1 rounded-full bg-amber-500/10 text-amber-400 flex items-center gap-1">
                <Globe className="w-3 h-3" /> Fonte externa
              </span>
            )}
          </div>

          <h1 className="text-3xl font-bold text-white mt-2 leading-tight">{selected.titulo}</h1>
          {selected.subtitulo && (
            <p className="text-lg text-slate-400 mt-2 leading-relaxed">{selected.subtitulo}</p>
          )}

          <div className="flex flex-wrap items-center gap-4 text-sm text-slate-400 mt-4 pb-6 border-b border-slate-700/50">
            <span className="flex items-center gap-1.5"><User className="w-4 h-4" />{selected.autor}</span>
            <span className="flex items-center gap-1.5"><Clock className="w-4 h-4" />{selected.leituraMinutos} min de leitura</span>
            <span className="flex items-center gap-1.5"><Calendar className="w-4 h-4" />{formatDate(selected.dataPublicacao)}</span>
            {selected.fonte && (
              <span className="flex items-center gap-1.5"><Link2 className="w-4 h-4" />{selected.fonte}</span>
            )}
          </div>

          {/* External link banner */}
          {selected.isExterno && selected.linkExterno && (
            <a href={selected.linkExterno} target="_blank" rel="noopener noreferrer"
              className="flex items-center gap-3 mt-6 p-4 rounded-xl bg-indigo-500/10 border border-indigo-500/20 hover:border-indigo-500/40 transition group">
              <ExternalLink className="w-5 h-5 text-indigo-400" />
              <div className="flex-1">
                <p className="text-sm font-medium text-white group-hover:text-indigo-400 transition">Ler artigo completo na fonte original</p>
                <p className="text-xs text-slate-500 truncate">{selected.linkExterno}</p>
              </div>
              <ChevronRight className="w-4 h-4 text-slate-600 group-hover:text-indigo-400 transition" />
            </a>
          )}

          {/* Tags */}
          {selected.tags?.length > 0 && (
            <div className="flex flex-wrap gap-2 mt-6">
              {selected.tags.map((t, i) => (
                <span key={i} className="text-xs px-2.5 py-1 rounded-full bg-slate-800/80 text-slate-400 border border-slate-700/50">
                  #{t}
                </span>
              ))}
            </div>
          )}

          {/* Content */}
          <div className="mt-8 prose prose-invert prose-lg max-w-none
            prose-headings:text-white prose-headings:font-bold
            prose-h2:text-2xl prose-h2:mt-10 prose-h2:mb-4
            prose-h3:text-xl prose-h3:mt-8 prose-h3:mb-3
            prose-p:text-slate-300 prose-p:leading-relaxed
            prose-a:text-indigo-400 prose-a:no-underline hover:prose-a:underline
            prose-strong:text-white
            prose-li:text-slate-300
            prose-table:border-slate-700
            prose-th:bg-slate-800/80 prose-th:text-white prose-th:py-3 prose-th:px-4 prose-th:text-left prose-th:border-slate-700
            prose-td:py-2.5 prose-td:px-4 prose-td:border-slate-700 prose-td:text-slate-300
            prose-blockquote:border-indigo-500 prose-blockquote:text-slate-400
            prose-code:text-indigo-300"
            dangerouslySetInnerHTML={{ __html: selected.conteudo }} />

          {/* Source reference */}
          {(selected.fonte || selected.fonteUrl) && (
            <div className="mt-10 pt-6 border-t border-slate-700/50">
              <p className="text-sm text-slate-500">
                Fonte: {selected.fonteUrl ? (
                  <a href={selected.fonteUrl} target="_blank" rel="noopener noreferrer" className="text-indigo-400 hover:underline">
                    {selected.fonte || selected.fonteUrl}
                  </a>
                ) : selected.fonte}
              </p>
            </div>
          )}
        </article>
      </div>
    );
  }

  // ─── Listing view ───
  if (loading) {
    return (
      <div className="flex items-center justify-center h-full">
        <div className="w-8 h-8 border-2 border-indigo-500 border-t-transparent rounded-full animate-spin" />
      </div>
    );
  }

  const externalCount = articles.filter(a => a.isExterno).length;
  const curatedCount = articles.filter(a => !a.isExterno).length;
  const featured = !search && !catFilter ? articles[0] : null;
  const listArticles = featured ? articles.slice(1) : articles;

  return (
    <div className="p-6 lg:p-8 max-w-6xl mx-auto space-y-6 animate-fadeIn">
      {/* Header */}
      <div className="flex items-start justify-between gap-4">
        <div>
          <h1 className="text-2xl font-bold text-white flex items-center gap-3">
            <Newspaper className="w-7 h-7 text-indigo-400" /> Blog & Notícias
          </h1>
          <p className="text-slate-400 text-sm mt-1">
            Artigos detalhados + notícias de fontes reais atualizadas diariamente.
          </p>
        </div>
        <button onClick={refresh} disabled={refreshing}
          className="flex items-center gap-2 px-4 py-2 text-sm border border-slate-700/50 rounded-xl text-slate-400 hover:text-white transition cursor-pointer disabled:opacity-50">
          <RefreshCw className={`w-4 h-4 ${refreshing ? 'animate-spin' : ''}`} />
          Atualizar
        </button>
      </div>

      {/* Stats */}
      <div className="flex gap-3 flex-wrap">
        <div className="bg-slate-800/50 border border-slate-700/50 rounded-xl px-4 py-2 text-sm flex items-center gap-2">
          <BookOpen className="w-4 h-4 text-indigo-400" />
          <span className="text-white font-semibold">{curatedCount}</span>
          <span className="text-slate-400">artigos editoriais</span>
        </div>
        <div className="bg-slate-800/50 border border-slate-700/50 rounded-xl px-4 py-2 text-sm flex items-center gap-2">
          <Globe className="w-4 h-4 text-green-400" />
          <span className="text-white font-semibold">{externalCount}</span>
          <span className="text-slate-400">fontes externas (PubMed, OMS)</span>
        </div>
      </div>

      {/* Filters */}
      <div className="flex flex-col sm:flex-row gap-3">
        <div className="relative flex-1">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-slate-500" />
          <input type="text" placeholder="Buscar artigos, tags..."
            value={search} onChange={(e) => setSearch(e.target.value)}
            className="w-full pl-10 pr-4 py-2.5 rounded-xl bg-slate-800/50 border border-slate-700/50 text-white placeholder-slate-500 text-sm outline-none focus:ring-2 focus:ring-indigo-500 transition" />
        </div>
        <div className="flex gap-2 flex-wrap">
          <button onClick={() => setCatFilter('')}
            className={`px-3 py-1.5 rounded-lg text-xs font-medium transition cursor-pointer ${!catFilter ? 'bg-indigo-500 text-white' : 'bg-slate-800/50 text-slate-400 hover:text-white border border-slate-700/50'}`}>
            Todos
          </button>
          {categories.map((c) => (
            <button key={c} onClick={() => setCatFilter(c)}
              className={`px-3 py-1.5 rounded-lg text-xs font-medium transition cursor-pointer ${catFilter === c ? 'bg-indigo-500 text-white' : 'bg-slate-800/50 text-slate-400 hover:text-white border border-slate-700/50'}`}>
              {c}
            </button>
          ))}
        </div>
      </div>

      {/* Featured article */}
      {featured && (
        <button onClick={() => setSelected(featured)}
          className="w-full text-left block rounded-2xl overflow-hidden border border-slate-700/50 hover:border-indigo-500/30 transition group cursor-pointer">
          {featured.imagemUrl && (
            <div className="relative h-56 sm:h-72 overflow-hidden">
              <img src={featured.imagemUrl} alt={featured.titulo}
                className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-500" loading="lazy"
                onError={handleImgError} />
              <div className="absolute inset-0 bg-gradient-to-t from-slate-900 via-slate-900/60 to-transparent" />
              <div className="absolute bottom-0 left-0 right-0 p-6">
                <div className="flex items-center gap-2 mb-2">
                  <span className="text-xs font-medium px-2.5 py-1 rounded-full bg-indigo-500/20 text-indigo-400">Destaque</span>
                  <span className="text-xs font-medium px-2.5 py-1 rounded-full bg-slate-800/80 text-slate-300">{featured.categoria}</span>
                  {featured.isExterno && (
                    <span className="text-xs font-medium px-2.5 py-1 rounded-full bg-amber-500/10 text-amber-400 flex items-center gap-1">
                      <Globe className="w-3 h-3" /> Externa
                    </span>
                  )}
                </div>
                <h2 className="text-xl sm:text-2xl font-bold text-white group-hover:text-indigo-400 transition leading-tight">{featured.titulo}</h2>
                <p className="text-slate-400 text-sm mt-2 line-clamp-2">{featured.resumo}</p>
                <div className="flex items-center gap-3 text-xs text-slate-500 mt-3">
                  <span className="flex items-center gap-1"><Clock className="w-3 h-3" />{featured.leituraMinutos} min</span>
                  <span>{featured.autor}</span>
                  <span>{formatDate(featured.dataPublicacao)}</span>
                </div>
              </div>
            </div>
          )}
          {!featured.imagemUrl && (
            <div className="p-6 bg-gradient-to-r from-indigo-500/10 to-purple-500/10">
              <span className="text-xs font-medium px-2.5 py-1 rounded-full bg-indigo-500/20 text-indigo-400">Destaque</span>
              <h2 className="text-xl font-bold text-white mt-3">{featured.titulo}</h2>
              <p className="text-slate-400 text-sm mt-2">{featured.resumo}</p>
            </div>
          )}
        </button>
      )}

      {/* Articles Grid */}
      <div className="grid sm:grid-cols-2 lg:grid-cols-3 gap-4">
        {listArticles.map((a) => (
          <button key={a.id} onClick={() => setSelected(a)}
            className="w-full text-left rounded-2xl bg-slate-800/50 border border-slate-700/50 hover:border-slate-600/50 transition group overflow-hidden cursor-pointer flex flex-col">
            {/* Thumbnail */}
            {a.imagemUrl && (
              <div className="h-40 overflow-hidden">
                <img src={a.imagemUrl} alt="" className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-500" loading="lazy"
                  onError={handleImgError} />
              </div>
            )}
            <div className="p-5 flex-1 flex flex-col">
              <div className="flex items-center gap-2 mb-2 flex-wrap">
                <span className="flex items-center gap-1 text-xs text-indigo-400 font-medium">
                  <Tag className="w-3 h-3" />{a.categoria}
                </span>
                {a.isExterno && (
                  <span className="flex items-center gap-1 text-xs text-amber-400/70">
                    <Globe className="w-3 h-3" /> {a.fonte}
                  </span>
                )}
              </div>
              <h3 className="text-white font-semibold group-hover:text-indigo-400 transition line-clamp-2 leading-snug">
                {a.titulo}
              </h3>
              <p className="text-slate-400 text-sm mt-2 line-clamp-2 flex-1">{a.resumo}</p>
              <div className="flex items-center justify-between mt-4 text-xs text-slate-500">
                <div className="flex items-center gap-3">
                  <span className="flex items-center gap-1"><Clock className="w-3 h-3" />{a.leituraMinutos} min</span>
                  <span>{formatDate(a.dataPublicacao)}</span>
                </div>
                <ChevronRight className="w-4 h-4 text-slate-600 group-hover:text-indigo-400 transition" />
              </div>
            </div>
          </button>
        ))}
      </div>

      {articles.length === 0 && (
        <div className="text-center py-12">
          <BookOpen className="w-10 h-10 text-slate-600 mx-auto mb-3" />
          <p className="text-slate-400">Nenhum artigo encontrado.</p>
        </div>
      )}

      {/* Footer note */}
      <div className="text-center text-xs text-slate-600 pt-4 border-t border-slate-800/50">
        Artigos editoriais escritos pela equipe + notícias de fontes verificadas (PubMed/NCBI, OMS).
        Atualizado automaticamente a cada 30 minutos.
      </div>
    </div>
  );
}
