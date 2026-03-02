import { Routes, Route, Navigate } from 'react-router-dom'
import { lazy } from 'react'
import ProtectedRoute from './components/ProtectedRoute'
import AppLayout from './components/AppLayout'

// Lazy loading para melhor performance
const LoginPage = lazy(() => import('./pages/LoginPage'))
const RegisterPage = lazy(() => import('./pages/RegisterPage'))
const ResetPasswordPage = lazy(() => import('./pages/ResetPasswordPage'))
const OnboardingPage = lazy(() => import('./pages/OnboardingPage'))
const DashboardPage = lazy(() => import('./pages/DashboardPage'))
const DisciplinasPage = lazy(() => import('./pages/DisciplinasPage'))
const NotesPage = lazy(() => import('./pages/NotesPage'))
const MyNotesPage = lazy(() => import('./pages/MyNotesPage'))
const BlogPage = lazy(() => import('./pages/BlogPage'))
const FlashcardsPage = lazy(() => import('./pages/FlashcardsPage'))
const PomodoroPage = lazy(() => import('./pages/PomodoroPage'))
const RecursosPage = lazy(() => import('./pages/RecursosPage'))
const DocumentsPage = lazy(() => import('./pages/DocumentsPage'))
const QuestionsPage = lazy(() => import('./pages/QuestionsPage'))
const ExamPage = lazy(() => import('./pages/ExamPage'))
const ConfigPage = lazy(() => import('./pages/ConfigPage'))

const WithLayout = ({ children }) => (
  <ProtectedRoute>
    <AppLayout>{children}</AppLayout>
  </ProtectedRoute>
)

const publicRoutes = [
  { path: '/login', component: LoginPage },
  { path: '/register', component: RegisterPage },
  { path: '/redefinir-senha', component: ResetPasswordPage }
]

const protectedRoutes = [
  { path: '/', component: DashboardPage },
  { path: '/disciplinas', component: DisciplinasPage },
  { path: '/topics/:topicId/notes', component: NotesPage },
  { path: '/topics/:topicId/documentos', component: DocumentsPage },
  { path: '/topics/:topicId/questoes', component: QuestionsPage },
  { path: '/topics/:topicId/prova', component: ExamPage },
  { path: '/minhas-notas', component: MyNotesPage },
  { path: '/blog', component: BlogPage },
  { path: '/flashcards', component: FlashcardsPage },
  { path: '/pomodoro', component: PomodoroPage },
  { path: '/recursos', component: RecursosPage },
  { path: '/config', component: ConfigPage }
]

function App() {
  return (
    <Routes>
      {/* Rotas públicas */}
      {publicRoutes.map(({ path, component: Component }) => (
        <Route key={path} path={path} element={<Component />} />
      ))}
      
      {/* Onboarding precisa de proteção mas não layout */}
      <Route 
        path="/onboarding" 
        element={
          <ProtectedRoute>
            <OnboardingPage />
          </ProtectedRoute>
        } 
      />

      {/* Rotas protegidas com layout */}
      {protectedRoutes.map(({ path, component: Component }) => (
        <Route
          key={path}
          path={path}
          element={
            <WithLayout>
              <Component />
            </WithLayout>
          }
        />
      ))}

      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  )
}

export default App
