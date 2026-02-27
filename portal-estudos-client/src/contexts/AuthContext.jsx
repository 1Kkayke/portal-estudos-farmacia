import { createContext, useContext, useState, useEffect } from 'react';
import api from '../services/api';

/**
 * Context de autenticação.
 * Gerencia o estado do usuário logado e fornece funções de login/register/logout
 * para todos os componentes filhos via useAuth().
 */
const AuthContext = createContext(null);

export function AuthProvider({ children }) {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  // Ao carregar a aplicação, verifica se há um usuário salvo no localStorage
  useEffect(() => {
    const savedUser = localStorage.getItem('user');
    const savedToken = localStorage.getItem('token');
    if (savedUser && savedToken) {
      setUser(JSON.parse(savedUser));
    }
    setLoading(false);
  }, []);

  /**
   * Registra um novo usuário.
   * Salva o token e dados do usuário no localStorage.
   */
  const register = async (nomeCompleto, email, password) => {
    const response = await api.post('/auth/register', {
      nomeCompleto,
      email,
      password,
    });
    const data = response.data;
    localStorage.setItem('token', data.token);
    localStorage.setItem('user', JSON.stringify(data));
    setUser(data);
    return data;
  };

  /**
   * Faz login com email e senha.
   * Salva o token e dados do usuário no localStorage.
   */
  const login = async (email, password) => {
    const response = await api.post('/auth/login', { email, password });
    const data = response.data;
    localStorage.setItem('token', data.token);
    localStorage.setItem('user', JSON.stringify(data));
    setUser(data);
    return data;
  };

  /**
   * Faz logout, limpando o localStorage e resetando o estado.
   */
  const logout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    setUser(null);
  };

  /**
   * Atualiza os dados do usuário logado.
   */
  const updateUser = (updatedData) => {
    const newUser = { ...user, ...updatedData };
    localStorage.setItem('user', JSON.stringify(newUser));
    setUser(newUser);
  };

  return (
    <AuthContext.Provider value={{ user, loading, login, register, logout, updateUser }}>
      {children}
    </AuthContext.Provider>
  );
}

/**
 * Hook personalizado para acessar o contexto de autenticação.
 * Uso: const { user, login, logout } = useAuth();
 */
export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth deve ser usado dentro de um AuthProvider');
  }
  return context;
}
