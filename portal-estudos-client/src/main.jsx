import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom'
import { AuthProvider } from './contexts/AuthContext'
import './index.css'
import App from './App.jsx'

/**
 * Ponto de entrada da aplicação React.
 * Envolve o App com:
 * - BrowserRouter: habilita navegação por rotas
 * - AuthProvider: fornece contexto de autenticação global
 */
createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter>
      <AuthProvider>
        <App />
      </AuthProvider>
    </BrowserRouter>
  </StrictMode>,
)
