import axios from 'axios';

/**
 * Instância configurada do Axios.
 * - baseURL aponta para a API ASP.NET Core
 * - Interceptor adiciona automaticamente o token JWT em todas as requisições
 */
const api = axios.create({
  baseURL: 'http://localhost:5000/api',
});

// Interceptor de requisição: injeta o token JWT no header Authorization
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Interceptor de resposta: redireciona para login se token expirou (401)
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

export default api;
