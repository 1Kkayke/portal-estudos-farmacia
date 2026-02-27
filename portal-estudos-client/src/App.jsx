import { Routes, Route, Navigate } from 'react-router-dom'
import LoginPage from './pages/LoginPage'
import RegisterPage from './pages/RegisterPage'
import ResetPasswordPage from './pages/ResetPasswordPage'
import OnboardingPage from './pages/OnboardingPage'
import DashboardPage from './pages/DashboardPage'
import DisciplinasPage from './pages/DisciplinasPage'
import NotesPage from './pages/NotesPage'
import MyNotesPage from './pages/MyNotesPage'
import BlogPage from './pages/BlogPage'
import FlashcardsPage from './pages/FlashcardsPage'
import PomodoroPage from './pages/PomodoroPage'
import RecursosPage from './pages/RecursosPage'
import DocumentsPage from './pages/DocumentsPage'
import QuestionsPage from './pages/QuestionsPage'
import ExamPage from './pages/ExamPage'
import ConfigPage from './pages/ConfigPage'
import ProtectedRoute from './components/ProtectedRoute'
import AppLayout from './components/AppLayout'

/** Wrapper que aplica layout + proteção de rota */
function Protected({ children }) {
  return (
    <ProtectedRoute>
      <AppLayout>{children}</AppLayout>
    </ProtectedRoute>
  )
}

function App() {
  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />
      <Route path="/redefinir-senha" element={<ResetPasswordPage />} />
      <Route path="/onboarding" element={<ProtectedRoute><OnboardingPage /></ProtectedRoute>} />

      <Route path="/" element={<Protected><DashboardPage /></Protected>} />
      <Route path="/disciplinas" element={<Protected><DisciplinasPage /></Protected>} />
      <Route path="/topics/:topicId/notes" element={<Protected><NotesPage /></Protected>} />
      <Route path="/topics/:topicId/documentos" element={<Protected><DocumentsPage /></Protected>} />
      <Route path="/topics/:topicId/questoes" element={<Protected><QuestionsPage /></Protected>} />
      <Route path="/topics/:topicId/prova" element={<Protected><ExamPage /></Protected>} />
      <Route path="/minhas-notas" element={<Protected><MyNotesPage /></Protected>} />
      <Route path="/blog" element={<Protected><BlogPage /></Protected>} />
      <Route path="/flashcards" element={<Protected><FlashcardsPage /></Protected>} />
      <Route path="/pomodoro" element={<Protected><PomodoroPage /></Protected>} />
      <Route path="/recursos" element={<Protected><RecursosPage /></Protected>} />
      <Route path="/config" element={<Protected><ConfigPage /></Protected>} />

      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  )
}

export default App
