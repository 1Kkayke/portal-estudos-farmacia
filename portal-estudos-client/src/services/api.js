import axios from 'axios'

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000/api'
const APP_ENV = import.meta.env.VITE_APP_ENV || import.meta.env.MODE
const normalizedBaseUrl = API_BASE_URL.replace(/\/+$/, '')

export const API_ORIGIN = normalizedBaseUrl.endsWith('/api')
  ? normalizedBaseUrl.slice(0, -4)
  : normalizedBaseUrl

// Configuração de segurança
if (APP_ENV === 'production' && !API_BASE_URL.startsWith('https://')) {
  console.warn('⚠️ AVISO DE SEGURANÇA: Configure VITE_API_BASE_URL com HTTPS no ambiente de produção')
}

const api = axios.create({
  baseURL: API_BASE_URL,
  timeout: 30000,
  headers: {
    'Content-Type': 'application/json',
    'X-Requested-With': 'XMLHttpRequest',
  },
})

// Request interceptor - adiciona token e metadata
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token')
    if (token) {
      config.headers.Authorization = `Bearer ${token}`
    }
    
    config.headers['X-Request-Time'] = new Date().toISOString()
    config.metadata = { startTime: Date.now() }
    
    return config
  },
  (error) => {
    console.error('Erro no request interceptor:', error)
    return Promise.reject(error)
  }
)

// Response interceptor - tratamento de erros e logging
api.interceptors.response.use(
  (response) => {
    // Log de performance em desenvolvimento
    if (APP_ENV === 'development' && response.config.metadata) {
      const duration = Date.now() - response.config.metadata.startTime
      console.log(`API Request: ${response.config.method?.toUpperCase()} ${response.config.url} - ${duration}ms`)
    }
    
    return response
  },
  async (error) => {
    const { config, response } = error
    const status = response?.status

    // Log de erros estruturado
    console.error('API Error:', {
      url: config?.url,
      method: config?.method,
      status,
      message: response?.data?.message || error.message
    })

    // Tratamento de diferentes tipos de erro
    switch (status) {
      case 401:
        handleUnauthorized()
        break
      case 403:
        error.userMessage = 'Acesso negado. Você não tem permissão para esta ação.'
        break
      case 404:
        error.userMessage = 'Recurso não encontrado.'
        break
      case 429:
        error.userMessage = 'Muitas requisições. Aguarde alguns instantes.'
        break
      case 500:
      case 502:
      case 503:
        error.userMessage = 'Erro interno no servidor. Tente novamente mais tarde.'
        break
      default:
        if (!response) {
          error.userMessage = 'Falha de conexão com o servidor.'
        } else {
          error.userMessage = response.data?.message || 'Ocorreu um erro inesperado.'
        }
    }

    return Promise.reject(error)
  }
)

function handleUnauthorized() {
  localStorage.removeItem('token')
  localStorage.removeItem('user')
  
  // Evita redirecionamento desnecessário se já estiver na página de login
  if (window.location.pathname !== '/login') {
    window.location.href = '/login'
  }
}

export default api
