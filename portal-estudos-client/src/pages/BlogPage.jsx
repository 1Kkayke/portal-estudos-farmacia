import { useState, useEffect, useCallback } from 'react';
import {
  Newspaper, Search, Clock, User, Calendar, ExternalLink,
  Globe, ChevronLeft, ChevronRight, RefreshCw, ArrowLeft, Loader2
} from 'lucide-react';
import api from '../services/api';

const FALLBACK_IMG = 'https://images.unsplash.com/photo-1532187863486-abf9dbad1b69?w=800&h=400&fit=crop&auto=format&q=80';

function formatDate(d) {
  return new Date(d).toLocaleDateString('pt-BR', { day: '2-digit', month: 'short', year: 'numeric' });
}

function handleImgError(e) {
  if (e.target.src !== FALLBACK_IMG) {
    e.target.src = FALLBACK_IMG;
  }
}

export default function BlogPage() {
  const [articles, setArticles] = useState([]);
  const [categories, setCategories] = useState([]);
  const [page, setPage] = useState(1);
  const [pageSize] = useState(12);
  const [totalPages, setTotalPages] = useState(0);
  const [search, setSearch] = useState('');
  const [catFilter, setCatFilter] = useState('');
  const [loading, setLoading] = useState(true);
  const [refreshing, setRefreshing] = useState(false);
  const [selected, setSelected] = useState(null);
  const [searching, setSearching] = useState(false);

  const loadArticles = useCallback(async (pageNum = 1) => {
    try {
      setLoading(pageNum === 1);
      const params = {
        page: pageNum,
        pageSize,
        ...(catFilter && { categoria: catFilter }),
        ...(search && { search })
      };
      const res = await api.get('/blog/paginated', { params });
      setArticles(res.data.items);
      setTotalPages(res.data.totalPages);
      setPage(pageNum);
      window.scrollTo({ top: 0, behavior: 'smooth' });
    } catch (e) {
      console.error('Blog load error:', e);
      setArticles([]);
    } finally {
      setLoading(false);
      setRefreshing(false);
      setSearching(false);
    }
  }, [pageSize, catFilter, search]);

  const loadCategories = useCallback(async () => {
    try {
      const res = await api.get('/blog/categories');
      setCategories(res.data || []);
    } catch (e) {
      console.error('Categories load error:', e);
    }
  }, []);

  useEffect(() => {
    loadCategories();
  }, [loadCategories]);

  useEffect(() => {
    setPage(1);
    loadArticles(1);
  }, [catFilter, search, loadArticles]);

  const handleSearch = (value) => {
    setSearching(true);
    setSearch(value);
  };

  const refresh = async () => {
    setRefreshing(true);
    await loadArticles(page);
  };

  if (selected) {
    return (
      <div className="min-h-screen bg-gradient-to-b from-slate-900 to-slate-900">
        <div className="p-4 md:p-6 lg:p-8 max-w-4xl mx-auto">
          <button
            onClick={() => setSelected(null)}
            className="flex items-center gap-2 text-indigo-400 hover:text-indigo-300 text-sm mb-6 transition group"
          >
            <ArrowLeft className="w-4 h-4 group-hover:-translate-x-1 transition" />
            Voltar aos artigos
          </button>

          <article>
            {selected.imagemUrl && (
              <div className="relative rounded-2xl overflow-hidden mb-6 aspect-video">
                <img
                  src={selected.imagemUrl}
                  alt={selected.titulo}
                  className="w-full h-full object-cover"
                  onError={handleImgError}
                />
                <div className="absolute inset-0 bg-gradient-to-t from-slate-900/80 to-transparent" />
                {selected.imagemCredito && (
                  <span className="absolute bottom-3 right-4 text-xs text-slate-400">
                    {selected.imagemCredito}
                  </span>
                )}
              </div>
            )}

            <div className="flex flex-wrap items-center gap-2 mb-4">
              <span className="text-xs font-semibold px-3 py-1 rounded-full bg-indigo-500/10 text-indigo-400">
                {selected.categoria}
              </span>
              {selected.isExterno && (
                <span className="text-xs font-semibold px-3 py-1 rounded-full bg-amber-500/10 text-amber-400 flex items-center gap-1.5">
                  <Globe className="w-3 h-3" /> Fonte Externa
                </span>
              )}
              {selected.leituraMinutos && (
                <span className="text-xs font-semibold px-3 py-1 rounded-full bg-slate-700/50 text-slate-300 flex items-center gap-1.5">
                  <Clock className="w-3 h-3" /> {selected.leituraMinutos} min
                </span>
              )}
            </div>

            <h1 className="text-3xl md:text-4xl font-bold text-white leading-tight mb-4">
              {selected.titulo}
            </h1>
            {selected.subtitulo && (
              <p className="text-lg text-slate-400 mb-6">{selected.subtitulo}</p>
            )}

            <div className="flex flex-wrap items-center gap-4 text-sm text-slate-400 pb-6 mb-6 border-b border-slate-700/50">
              {selected.autor && (
                <span className="flex items-center gap-2">
                  <User className="w-4 h-4" /> {selected.autor}
                </span>
              )}
              {selected.fonte && (
                <span className="flex items-center gap-2">
                  <Newspaper className="w-4 h-4" /> {selected.fonte}
                </span>
              )}
              <span className="flex items-center gap-2">
                <Calendar className="w-4 h-4" /> {formatDate(selected.dataPublicacao)}
              </span>
            </div>

            {selected.conteudo && (
              <div className="prose prose-invert max-w-none mb-8">
                <div
                  className="text-slate-300 leading-relaxed space-y-4"
                  dangerouslySetInnerHTML={{ __html: selected.conteudo }}
                />
              </div>
            )}

            {selected.resumo && !selected.conteudo && (
              <div className="text-slate-300 leading-relaxed mb-8">
                {selected.resumo}
              </div>
            )}

            {selected.isExterno && selected.linkExterno && (
              <a
                href={selected.linkExterno}
                target="_blank"
                rel="noopener noreferrer"
                className="inline-flex items-center gap-3 px-6 py-3 rounded-lg bg-indigo-600 hover:bg-indigo-700 active:bg-indigo-800 text-white font-medium transition group active:scale-95"
              >
                Ler no site original
                <ExternalLink className="w-4 h-4 group-hover:translate-x-0.5 transition" />
              </a>
            )}

            {selected.tags && selected.tags.length > 0 && (
              <div className="mt-8 pt-8 border-t border-slate-700/50">
                <div className="flex flex-wrap gap-2">
                  {selected.tags.map((tag, i) => (
                    <span
                      key={i}
                      className="text-xs px-3 py-1.5 rounded-full bg-slate-800 text-slate-300 border border-slate-700"
                    >
                      #{tag}
                    </span>
                  ))}
                </div>
              </div>
            )}
          </article>
        </div>
      </div>
    );
  }

  return (
    <div className="p-4 md:p-6 lg:p-8 max-w-7xl mx-auto space-y-6">
      <div>
        <h1 className="text-3xl md:text-4xl font-bold text-white flex items-center gap-3">
          <Newspaper className="w-8 h-8 md:w-10 md:h-10 text-indigo-400" />
          Blog & Notícias
        </h1>
        <p className="text-slate-400 mt-2">
          {articles.length === 0
            ? 'Carregando artigos...'
            : `Mostrando 12 de ${totalPages * pageSize}+ artigos atualizados diariamente`}
        </p>
      </div>

      <div className="flex flex-col gap-4">
        <div className="relative">
          <Search className="absolute left-4 top-1/2 -translate-y-1/2 w-5 h-5 text-slate-500" />
          <input
            type="text"
            placeholder="Buscar artigos..."
            value={search}
            onChange={(e) => handleSearch(e.target.value)}
            className="w-full pl-12 pr-4 py-3 rounded-xl bg-slate-800/50 border border-slate-700/50 text-white placeholder-slate-500 outline-none focus:ring-2 focus:ring-indigo-500 transition"
          />
          {searching && <Loader2 className="absolute right-4 top-1/2 -translate-y-1/2 w-5 h-5 text-indigo-500 animate-spin" />}
        </div>

        <div className="flex flex-wrap gap-2">
          <button
            onClick={() => setCatFilter('')}
            className={`px-4 py-2 rounded-lg font-medium transition text-sm ${
              !catFilter
                ? 'bg-indigo-600 text-white'
                : 'bg-slate-800/50 border border-slate-700 text-slate-300 hover:border-slate-600'
            }`}
          >
            Todos
          </button>
          {categories.map((cat) => (
            <button
              key={cat}
              onClick={() => setCatFilter(cat)}
              className={`px-4 py-2 rounded-lg font-medium transition text-sm whitespace-nowrap ${
                catFilter === cat
                  ? 'bg-indigo-600 text-white'
                  : 'bg-slate-800/50 border border-slate-700 text-slate-300 hover:border-slate-600'
              }`}
            >
              {cat}
            </button>
          ))}
        </div>
      </div>

      <button
        onClick={refresh}
        disabled={refreshing}
        className="flex items-center gap-2 px-4 py-2 rounded-lg border border-slate-700 text-slate-300 hover:border-slate-600 hover:text-white transition disabled:opacity-50 disabled:cursor-not-allowed text-sm"
      >
        <RefreshCw className={`w-4 h-4 ${refreshing ? 'animate-spin' : ''}`} />
        {refreshing ? 'Atualizando...' : 'Atualizar'}
      </button>

      {loading ? (
        <div className="flex items-center justify-center py-20">
          <div className="flex flex-col items-center gap-4">
            <Loader2 className="w-8 h-8 text-indigo-400 animate-spin" />
            <p className="text-slate-400">Carregando artigos...</p>
          </div>
        </div>
      ) : articles.length === 0 ? (
        <div className="flex items-center justify-center py-20">
          <div className="text-center">
            <Newspaper className="w-12 h-12 text-slate-600 mx-auto mb-4" />
            <p className="text-slate-400 mb-2">Nenhum artigo encontrado.</p>
            <p className="text-slate-500 text-sm">Tente ajustar seus filtros de busca.</p>
          </div>
        </div>
      ) : (
        <>
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {articles.map((article) => (
              <button
                key={article.id}
                onClick={() => setSelected(article)}
                className="group text-left overflow-hidden rounded-xl border border-slate-700/50 hover:border-slate-600 bg-slate-800/30 hover:bg-slate-800/50 transition active:scale-[0.98]"
              >
                <div className="relative overflow-hidden bg-slate-900 aspect-video">
                  <img
                    src={article.imagemUrl || FALLBACK_IMG}
                    alt={article.titulo}
                    className="w-full h-full object-cover group-hover:scale-105 transition duration-300"
                    onError={handleImgError}
                  />
                  <div className="absolute inset-0 bg-gradient-to-t from-slate-900/60 to-transparent" />

                  <div className="absolute top-3 left-3 flex gap-2">
                    <span className="text-xs font-semibold px-2.5 py-1 rounded-full bg-indigo-600 text-white">
                      {article.categoria}
                    </span>
                    {article.isExterno && (
                      <span className="text-xs font-semibold px-2.5 py-1 rounded-full bg-amber-500 text-white flex items-center gap-1">
                        <Globe className="w-3 h-3" />
                      </span>
                    )}
                  </div>
                </div>

                <div className="p-4">
                  <h3 className="font-bold text-white line-clamp-2 group-hover:text-indigo-400 transition mb-2">
                    {article.titulo}
                  </h3>
                  <p className="text-sm text-slate-400 line-clamp-2 mb-3">
                    {article.subtitulo || article.resumo}
                  </p>

                  <div className="flex items-center justify-between text-xs text-slate-500 border-t border-slate-700/50 pt-3">
                    <div className="flex items-center gap-3">
                      {article.autor && (
                        <span className="flex items-center gap-1">
                          <User className="w-3 h-3" />
                          {article.autor.split(' ')[0]}
                        </span>
                      )}
                      {article.leituraMinutos && (
                        <span className="flex items-center gap-1">
                          <Clock className="w-3 h-3" />
                          {article.leituraMinutos} min
                        </span>
                      )}
                    </div>
                    <span className="text-[10px]">
                      {formatDate(article.dataPublicacao)}
                    </span>
                  </div>
                </div>
              </button>
            ))}
          </div>

          {totalPages > 1 && (
            <div className="flex items-center justify-center gap-2 mt-12">
              <button
                onClick={() => loadArticles(page - 1)}
                disabled={page === 1}
                className="flex items-center gap-2 px-4 py-2 rounded-lg border border-slate-700 text-slate-300 hover:border-slate-600 disabled:opacity-50 disabled:cursor-not-allowed transition"
              >
                <ChevronLeft className="w-4 h-4" />
                Anterior
              </button>

              <div className="flex items-center gap-1">
                {Array.from({ length: Math.min(totalPages, 5) }, (_, i) => {
                  let pageNum;
                  if (totalPages <= 5) {
                    pageNum = i + 1;
                  } else if (page <= 3) {
                    pageNum = i + 1;
                  } else if (page >= totalPages - 2) {
                    pageNum = totalPages - 4 + i;
                  } else {
                    pageNum = page - 2 + i;
                  }
                  return (
                    <button
                      key={pageNum}
                      onClick={() => loadArticles(pageNum)}
                      className={`w-10 h-10 rounded-lg font-medium transition text-sm ${
                        page === pageNum
                          ? 'bg-indigo-600 text-white'
                          : 'bg-slate-800/50 border border-slate-700 text-slate-300 hover:border-slate-600'
                      }`}
                    >
                      {pageNum}
                    </button>
                  );
                })}
              </div>

              <button
                onClick={() => loadArticles(page + 1)}
                disabled={page === totalPages}
                className="flex items-center gap-2 px-4 py-2 rounded-lg border border-slate-700 text-slate-300 hover:border-slate-600 disabled:opacity-50 disabled:cursor-not-allowed transition"
              >
                Próximo
                <ChevronRight className="w-4 h-4" />
              </button>
            </div>
          )}
        </>
      )}
    </div>
  );
}
