import { createContext, useContext, useState, useEffect, useCallback } from 'react'
import api from '../services/api'

const AuthContext = createContext(null)

const STORAGE_KEYS = {
  TOKEN: 'token',
  USER: 'user'
}

export function AuthProvider({ children }) {
  const [user, setUser] = useState(null)
  const [loading, setLoading] = useState(true)

  const clearAuthData = useCallback(() => {
    Object.values(STORAGE_KEYS).forEach(key => localStorage.removeItem(key))
    setUser(null)
  }, [])

  const isTokenExpired = useCallback((token) => {
    if (!token) return true
    try {
      const payload = JSON.parse(atob(token.split('.')[1]))
      return payload.exp * 1000 < Date.now()
    } catch {
      return true
    }
  }, [])

  const saveAuthData = useCallback((authData) => {
    localStorage.setItem(STORAGE_KEYS.TOKEN, authData.token)
    localStorage.setItem(STORAGE_KEYS.USER, JSON.stringify(authData))
    setUser(authData)
  }, [])

  useEffect(() => {
    const initializeAuth = async () => {
      try {
        const savedUser = localStorage.getItem(STORAGE_KEYS.USER)
        const savedToken = localStorage.getItem(STORAGE_KEYS.TOKEN)
        
        if (savedUser && savedToken && !isTokenExpired(savedToken)) {
          setUser(JSON.parse(savedUser))
        } else {
          clearAuthData()
        }
      } catch (error) {
        console.error('Erro ao inicializar autenticação:', error)
        clearAuthData()
      } finally {
        setLoading(false)
      }
    }

    initializeAuth()
  }, [clearAuthData, isTokenExpired])

  const register = async (nomeCompleto, email, password) => {
    try {
      const response = await api.post('/auth/register', {
        nomeCompleto,
        email,
        password,
      })
      
      const authData = response.data
      saveAuthData(authData)
      return { success: true, data: authData }
    } catch (error) {
      const message = error.response?.data?.message || 'Erro ao registrar usuário'
      return { success: false, error: message }
    }
  }

  const login = async (email, password) => {
    try {
      const response = await api.post('/auth/login', { email, password })
      
      const authData = response.data
      saveAuthData(authData)
      return { success: true, data: authData }
    } catch (error) {
      const message = error.response?.data?.message || 'Credenciais inválidas'
      return { success: false, error: message }
    }
  }

  const logout = useCallback(() => {
    clearAuthData()
  }, [clearAuthData])

  const updateUser = useCallback((updatedData) => {
    if (!user) return

    const newUser = { ...user, ...updatedData }
    localStorage.setItem(STORAGE_KEYS.USER, JSON.stringify(newUser))
    setUser(newUser)
  }, [user])

  const contextValue = {
    user,
    loading,
    login,
    register,
    logout,
    updateUser,
    isAuthenticated: !!user
  }

  return (
    <AuthContext.Provider value={contextValue}>
      {children}
    </AuthContext.Provider>
  )
}

export function useAuth() {
  const context = useContext(AuthContext)
  if (!context) {
    throw new Error('useAuth deve ser usado dentro de um AuthProvider')
  }
  return context
}
