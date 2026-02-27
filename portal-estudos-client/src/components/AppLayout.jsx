import { useState } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import {
  GraduationCap, LayoutDashboard, BookOpen, FileText,
  Timer, Layers, Link2, Newspaper, LogOut, Menu, X,
  ChevronDown, User, Sparkles,
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

  const isActive = (path) => {
    if (path === '/') return location.pathname === '/';
    return location.pathname.startsWith(path);
  };

  return (
    <div className="flex h-screen overflow-hidden bg-[#0f172a]">
      {/* ===== Overlay mobile ===== */}
      {sidebarOpen && (
        <div
          className="fixed inset-0 bg-black/60 z-40 lg:hidden backdrop-blur-sm"
          onClick={() => setSidebarOpen(false)}
        />
      )}

      {/* ===== Sidebar ===== */}
      <aside
        className={`
          fixed lg:static inset-y-0 left-0 z-50 w-72 bg-[#0f172a] border-r border-slate-800
          transform transition-transform duration-300 ease-in-out flex flex-col
          ${sidebarOpen ? 'translate-x-0' : '-translate-x-full lg:translate-x-0'}
        `}
      >
        {/* Logo */}
        <div className="flex items-center gap-3 px-6 h-16 border-b border-slate-800 shrink-0">
          <div className="flex items-center justify-center w-10 h-10 bg-gradient-to-br from-indigo-500 to-purple-600 rounded-xl shadow-lg shadow-indigo-500/20">
            <GraduationCap className="w-5 h-5 text-white" />
          </div>
          <div>
            <h1 className="text-base font-bold text-white tracking-tight">PharmStudy</h1>
            <p className="text-[10px] text-slate-500 font-medium uppercase tracking-widest">Portal de Farmácia</p>
          </div>
          <button onClick={() => setSidebarOpen(false)} className="ml-auto lg:hidden text-slate-400 cursor-pointer">
            <X className="w-5 h-5" />
          </button>
        </div>

        {/* Navegação */}
        <nav className="flex-1 px-3 py-4 space-y-1 overflow-y-auto">
          {navItems.map((item) => {
            const Icon = item.icon;
            const active = isActive(item.path);
            return (
              <Link
                key={item.path}
                to={item.path}
                onClick={() => setSidebarOpen(false)}
                className={`
                  flex items-center gap-3 px-3 py-2.5 rounded-xl text-sm font-medium transition-all duration-200
                  ${active
                    ? 'bg-indigo-500/10 text-indigo-400 border border-indigo-500/20'
                    : 'text-slate-400 hover:text-white hover:bg-slate-800/60 border border-transparent'
                  }
                `}
              >
                <Icon className={`w-[18px] h-[18px] ${active ? 'text-indigo-400' : ''}`} />
                {item.label}
                {active && <div className="ml-auto w-1.5 h-1.5 rounded-full bg-indigo-400" />}
              </Link>
            );
          })}
        </nav>

        {/* Card do usuário */}
        <div className="p-3 border-t border-slate-800 shrink-0">
          <div className="flex items-center gap-3 p-3 rounded-xl bg-slate-800/50">
            <div className="flex items-center justify-center w-9 h-9 rounded-lg bg-gradient-to-br from-indigo-500 to-purple-600 text-white text-sm font-bold">
              {user?.nomeCompleto?.charAt(0)?.toUpperCase() || 'U'}
            </div>
            <div className="flex-1 min-w-0">
              <p className="text-sm font-medium text-white truncate">{user?.nomeCompleto}</p>
              <p className="text-xs text-slate-500 truncate">{user?.email}</p>
            </div>
            <button
              onClick={handleLogout}
              className="p-1.5 text-slate-500 hover:text-red-400 transition cursor-pointer"
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
        <header className="flex items-center gap-3 px-4 h-14 bg-[#0f172a]/80 backdrop-blur-xl border-b border-slate-800 lg:hidden shrink-0">
          <button onClick={() => setSidebarOpen(true)} className="text-slate-400 cursor-pointer">
            <Menu className="w-5 h-5" />
          </button>
          <div className="flex items-center gap-2">
            <GraduationCap className="w-5 h-5 text-indigo-400" />
            <span className="text-sm font-bold text-white">PharmStudy</span>
          </div>
        </header>

        {/* Área de conteúdo com scroll */}
        <main className="flex-1 overflow-y-auto">
          <div className="animate-fade-in">
            {children}
          </div>
        </main>
      </div>
    </div>
  );
}
