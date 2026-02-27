import axios from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000/api';
const APP_ENV = import.meta.env.VITE_APP_ENV || import.meta.env.MODE;
const normalizedBaseUrl = API_BASE_URL.replace(/\/+$/, '');

export const API_ORIGIN = normalizedBaseUrl.endsWith('/api')
  ? normalizedBaseUrl.slice(0, -4)
  : normalizedBaseUrl;

// Aviso de segurança (não quebra o app)
if (APP_ENV === 'production' && !API_BASE_URL.startsWith('https://')) {
  console.warn('⚠️ AVISO DE SEGURANÇA: Configure VITE_API_BASE_URL com HTTPS no Netlify Dashboard');
}

const api = axios.create({
  baseURL: API_BASE_URL,
  timeout: 30000,
  headers: {
    'Content-Type': 'application/json',
    'X-Requested-With': 'XMLHttpRequest',
  },
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  config.headers['X-Request-Time'] = new Date().toISOString();
  return config;
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    const status = error.response?.status;

    if (status === 401) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      if (window.location.pathname !== '/login') {
        window.location.href = '/login';
      }
    }

    if (status === 429) {
      error.userMessage = 'Muitas requisições. Tente novamente em instantes.';
    } else if (status >= 500) {
      error.userMessage = 'Erro interno no servidor. Tente novamente mais tarde.';
    } else if (!error.response) {
      error.userMessage = 'Falha de conexão com o servidor.';
    }

    return Promise.reject(error);
  }
);

export default api;
