import { useState } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import { API_ORIGIN } from '../services/api';
import {
  GraduationCap, LayoutDashboard, BookOpen, FileText,
  Timer, Layers, Link2, Newspaper, LogOut, Menu, X,
  ChevronDown, User, Sparkles, Settings,
} from 'lucide-react';

/**
 * Itens de navegação do sidebar.
 * Cada item tem rota, label e ícone.
 */
const navItems = [
  { path: '/', label: 'Dashboard', icon: LayoutDashboard },
  { path: '/disciplinas', label: 'Disciplinas', icon: BookOpen },
  { path: '/minhas-notas', label: 'Minhas Anotações', icon: FileText },
  { path: '/blog', label: 'Blog & Artigos', icon: Newspaper },
  { path: '/flashcards', label: 'Flashcards', icon: Layers },
  { path: '/pomodoro', label: 'Pomodoro', icon: Timer },
  { path: '/recursos', label: 'Recursos Úteis', icon: Link2 },
];

/**
 * Layout principal da aplicação com sidebar responsivo.
 * Usado como wrapper para todas as páginas protegidas.
 */
export default function AppLayout({ children }) {
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const { user, logout } = useAuth();
  const location = useLocation();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  const goToConfig =() => {
    navigate('/config')
  }

  const isActive = (path) => {
    if (path === '/') return location.pathname === '/';
    return location.pathname.startsWith(path);
  };

  const getProfileImageUrl = (url) => {
    if (!url) return null;
    if (url.startsWith('http://') || url.startsWith('https://')) return url;
    return `${API_ORIGIN}${url}`;
  };

  return (
    <div className="flex h-screen overflow-hidden bg-[#0f172a]">
      {/* ===== Overlay mobile ===== */}
      {sidebarOpen && (
        <div
          className="fixed inset-0 bg-black/70 z-40 lg:hidden backdrop-blur-sm animate-fade-in"
          onClick={() => setSidebarOpen(false)}
        />
      )}

      {/* ===== Sidebar ===== */}
      <aside
        className={`
          fixed lg:static inset-y-0 left-0 z-50 w-[280px] lg:w-72 bg-[#0f172a] border-r border-slate-800
          transform transition-transform duration-300 ease-in-out flex flex-col
          ${sidebarOpen ? 'translate-x-0' : '-translate-x-full lg:translate-x-0'}
        `}
      >
        {/* Logo */}
        <div className="flex items-center gap-3 px-4 lg:px-6 h-14 lg:h-16 border-b border-slate-800 shrink-0">
          <div className="flex items-center justify-center w-9 h-9 lg:w-10 lg:h-10 bg-gradient-to-br from-indigo-500 to-purple-600 rounded-xl shadow-lg shadow-indigo-500/20">
            <GraduationCap className="w-4 h-4 lg:w-5 lg:h-5 text-white" />
          </div>
          <div className="flex-1 min-w-0">
            <h1 className="text-sm lg:text-base font-bold text-white tracking-tight">PharmStudy</h1>
            <p className="text-[9px] lg:text-[10px] text-slate-500 font-medium uppercase tracking-widest">Portal de Farmácia</p>
          </div>
          <button 
            onClick={() => setSidebarOpen(false)} 
            className="lg:hidden text-slate-400 hover:text-white transition cursor-pointer p-1.5 rounded-lg hover:bg-slate-800/50"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        {/* Navegação */}
        <nav className="flex-1 px-2 lg:px-3 py-3 lg:py-4 space-y-1 overflow-y-auto">
          {navItems.map((item) => {
            const Icon = item.icon;
            const active = isActive(item.path);
            return (
              <Link
                key={item.path}
                to={item.path}
                onClick={() => setSidebarOpen(false)}
                className={`
                  flex items-center gap-3 px-3 lg:px-3 py-3 lg:py-2.5 rounded-xl text-sm font-medium transition-all duration-200
                  ${active
                    ? 'bg-indigo-500/10 text-indigo-400 border border-indigo-500/20'
                    : 'text-slate-400 hover:text-white hover:bg-slate-800/60 border border-transparent active:scale-95'
                  }
                `}
              >
                <Icon className={`w-[18px] h-[18px] shrink-0 ${active ? 'text-indigo-400' : ''}`} />
                <span className="truncate">{item.label}</span>
                {active && <div className="ml-auto w-1.5 h-1.5 rounded-full bg-indigo-400 shrink-0" />}
              </Link>
            );
          })}
        </nav>

        {/* Card do usuário */}
        <div className="p-2 lg:p-3 border-t border-slate-800 shrink-0">
          <div className="flex items-center gap-2 lg:gap-3 p-2.5 lg:p-3 rounded-xl bg-slate-800/50">
            {user?.fotoPerfilUrl ? (
              <img
                src={getProfileImageUrl(user.fotoPerfilUrl)}
                alt="Foto de perfil"
                className="w-8 h-8 lg:w-9 lg:h-9 rounded-lg object-cover shrink-0"
              />
            ) : (
              <div className="flex items-center justify-center w-8 h-8 lg:w-9 lg:h-9 rounded-lg bg-gradient-to-br from-indigo-500 to-purple-600 text-white text-xs lg:text-sm font-bold shrink-0">
                {user?.nomeCompleto?.charAt(0)?.toUpperCase() || 'U'}
              </div>
            )}
            <div className="flex-1 min-w-0">
              <p className="text-xs lg:text-sm font-medium text-white truncate">{user?.nomeCompleto}</p>
              <p className="text-[10px] lg:text-xs text-slate-500 truncate">{user?.email}</p>
            </div>
            <button
              onClick={goToConfig}
              className="p-1.5 text-slate-500 hover:text-indigo-400 transition cursor-pointer rounded-lg hover:bg-slate-700/50 active:scale-95 shrink-0"
              title="Configurações"
            >
              <Settings className="w-4 h-4" />
            </button>

            <button
              onClick={handleLogout}
              className="p-1.5 text-slate-500 hover:text-red-400 transition cursor-pointer rounded-lg hover:bg-slate-700/50 active:scale-95 shrink-0"
              title="Sair"
            >
              <LogOut className="w-4 h-4" />
            </button>
          </div>
        </div>
      </aside>

              {/* ===== Conteúdo principal ===== */}
      <div className="flex-1 flex flex-col overflow-hidden">
        {/* Top bar mobile */}
        <header className="flex items-center gap-3 px-3 lg:px-4 h-14 bg-[#0f172a]/95 backdrop-blur-xl border-b border-slate-800 lg:hidden shrink-0 sticky top-0 z-30">
          <button 
            onClick={() => setSidebarOpen(true)} 
            className="text-slate-400 hover:text-white transition cursor-pointer p-2 -ml-2 rounded-lg hover:bg-slate-800/50 active:scale-95"
          >
            <Menu className="w-5 h-5" />
          </button>
          <div className="flex items-center gap-2">
            <div className="flex items-center justify-center w-7 h-7 bg-gradient-to-br from-indigo-500 to-purple-600 rounded-lg">
              <GraduationCap className="w-4 h-4 text-white" />
            </div>
            <span className="text-sm font-bold text-white">PharmStudy</span>
          </div>
        </header>

        {/* Área de conteúdo com scroll */}
        <main className="flex-1 overflow-y-auto overflow-x-hidden">
          <div className="animate-fade-in">
            {children}
          </div>
        </main>
      </div>
    </div>
  );
}
