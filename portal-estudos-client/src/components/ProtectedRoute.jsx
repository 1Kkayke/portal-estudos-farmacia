import { Navigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

/**
 * Componente de rota protegida.
 * Redireciona para /login se o usuário não estiver autenticado.
 * Exibe um spinner enquanto o estado de autenticação está carregando.
 */
export default function ProtectedRoute({ children }) {
  const { user, loading } = useAuth();

  // Exibe um loading enquanto verifica o localStorage
  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-indigo-600"></div>
      </div>
    );
  }

  // Se não há usuário logado, redireciona para login
  if (!user) {
    return <Navigate to="/login" replace />;
  }

  return children;
}
