import axios from 'axios';
import DOMPurify from 'dompurify';

// ═══════════════════════════════════════════════════════════
// CONFIGURAÇÃO SEGURA DA API
// ═══════════════════════════════════════════════════════════

// Valida se a URL da API é segura
const validateApiUrl = (url) => {
  try {
    const apiUrl = new URL(url);
    // Apenas HTTPS em produção
    if (import.meta.env.VITE_APP_ENV === 'production' && apiUrl.protocol !== 'https:') {
      throw new Error('API URL deve usar HTTPS em produção');
    }
    return url;
  } catch (error) {
    console.error('URL da API inválida:', error);
    throw error;
  }
};

// URL segura da API
const API_BASE_URL = validateApiUrl(
  import.meta.env.VITE_API_BASE_URL || 'https://api.seu-dominio.com.br'
);

// ═══════════════════════════════════════════════════════════
// INSTÂNCIA AXIOS COM SEGURANÇA
// ═══════════════════════════════════════════════════════════

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  timeout: 10000, // Timeout de 10s para evitar hanging requests
  headers: {
    'Content-Type': 'application/json',
    'X-Requested-With': 'XMLHttpRequest', // Proteção contra CSRF
  },
  withCredentials: true, // Envia cookies seguros
});

// ═══════════════════════════════════════════════════════════
// INTERCEPTOR DE REQUEST - SEGURANÇA
// ═══════════════════════════════════════════════════════════

apiClient.interceptors.request.use(
  (config) => {
    // Adiciona token JWT do localStorage
    const token = localStorage.getItem('token');
    if (token) {
      // Valida token antes de enviar
      if (!isTokenValid(token)) {
        localStorage.removeItem('token');
        window.location.href = '/login';
        return Promise.reject(new Error('Token inválido ou expirado'));
      }
      config.headers.Authorization = `Bearer ${token}`;
    }

    // Sanitiza query parameters
    if (config.params) {
      config.params = sanitizeObject(config.params);
    }

    // Adiciona timestamp para prevenir cache indesejado
    config.headers['X-Request-ID'] = generateRequestId();
    config.headers['X-Timestamp'] = new Date().toISOString();

    return config;
  },
  (error) => {
    console.error('Erro na requisição:', error);
    return Promise.reject(error);
  }
);

// ═══════════════════════════════════════════════════════════
// INTERCEPTOR DE RESPONSE - SEGURANÇA
// ═══════════════════════════════════════════════════════════

apiClient.interceptors.response.use(
  (response) => {
    // Sanitiza dados retornados
    if (response.data && typeof response.data === 'object') {
      response.data = sanitizeObject(response.data);
    }
    return response;
  },
  (error) => {
    // Tratamento seguro de erros
    if (error.response) {
      const status = error.response.status;

      switch (status) {
        case 401:
          // Token expirado ou inválido
          localStorage.removeItem('token');
          window.location.href = '/login';
          break;

        case 403:
          // Acesso proibido
          console.error('Acesso proibido');
          break;

        case 429:
          // Rate limited
          console.error('Muitas requisições. Tente novamente mais tarde.');
          break;

        case 500:
        case 502:
        case 503:
          // Erro no servidor
          console.error('Erro no servidor. Tente novamente mais tarde.');
          break;

        default:
          console.error('Erro na requisição:', error.message);
      }

      // Não expõe detalhes técnicos ao usuário
      error.userMessage = 'Erro ao processar requisição. Por favor, tente novamente.';
    } else if (error.request) {
      console.error('Sem resposta do servidor');
      error.userMessage = 'Sem conexão com o servidor. Verifique sua internet.';
    } else {
      console.error('Erro:', error.message);
      error.userMessage = 'Erro desconhecido. Por favor, tente novamente.';
    }

    return Promise.reject(error);
  }
);

// ═══════════════════════════════════════════════════════════
// FUNÇÕES DE SEGURANÇA
// ═══════════════════════════════════════════════════════════

/**
 * Valida se o token JWT é válido
 */
function isTokenValid(token) {
  try {
    // Extrai o payload do JWT (segunda parte)
    const parts = token.split('.');
    if (parts.length !== 3) return false;

    // Decodifica o payload (sem validação de assinatura no frontend)
    const payload = JSON.parse(atob(parts[1]));

    // Verifica se está expirado
    if (payload.exp) {
      const now = Math.floor(Date.now() / 1000);
      if (payload.exp < now) return false;
    }

    return true;
  } catch (error) {
    console.error('Token validation error:', error);
    return false;
  }
}

/**
 * Gera um ID único para requisição (para logging)
 */
function generateRequestId() {
  return `${Date.now()}-${Math.random().toString(36).substr(2, 9)}`;
}

/**
 * Sanitiza objetos recursivamente usando DOMPurify
 */
function sanitizeObject(obj) {
  if (Array.isArray(obj)) {
    return obj.map(item => sanitizeObject(item));
  }

  if (obj !== null && typeof obj === 'object') {
    const sanitized = {};
    for (const key in obj) {
      if (Object.prototype.hasOwnProperty.call(obj, key)) {
        sanitized[key] = sanitizeObject(obj[key]);
      }
    }
    return sanitized;
  }

  if (typeof obj === 'string') {
    // Sanitiza HTML/scripts
    return DOMPurify.sanitize(obj, { ALLOWED_TAGS: [], ALLOWED_ATTR: [] });
  }

  return obj;
}

/**
 * Valida e sanitiza email
 */
export function validateEmail(email) {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  const sanitized = DOMPurify.sanitize(email, { ALLOWED_TAGS: [], ALLOWED_ATTR: [] });
  return emailRegex.test(sanitized);
}

/**
 * Valida entrada de usuário
 */
export function validateUserInput(input, maxLength = 500) {
  if (typeof input !== 'string') return '';
  
  const sanitized = DOMPurify.sanitize(input, { ALLOWED_TAGS: [], ALLOWED_ATTR: [] });
  return sanitized.substring(0, maxLength);
}

/**
 * Valida senha (força)
 */
export function validatePassword(password) {
  // Mínimo 8 caracteres, maiúscula, minúscula, número e caractere especial
  const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
  return passwordRegex.test(password);
}

// ═══════════════════════════════════════════════════════════
// EXPORT
// ═══════════════════════════════════════════════════════════

export default apiClient;
export { API_BASE_URL, sanitizeObject, isTokenValid };
