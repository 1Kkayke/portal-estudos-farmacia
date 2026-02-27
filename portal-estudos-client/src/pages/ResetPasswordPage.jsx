import { useState, useEffect } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { GraduationCap, Lock, Eye, EyeOff, AlertCircle, CheckCircle } from 'lucide-react';
import api from '../services/api';

export default function ResetPasswordPage() {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  
  const [email, setEmail] = useState('');
  const [token, setToken] = useState('');
  const [novaSenha, setNovaSenha] = useState('');
  const [confirmarSenha, setConfirmarSenha] = useState('');
  const [mostrarNovaSenha, setMostrarNovaSenha] = useState(false);
  const [mostrarConfirmarSenha, setMostrarConfirmarSenha] = useState(false);
  const [loading, setLoading] = useState(false);
  const [mensagem, setMensagem] = useState(null);

  useEffect(() => {
    const emailParam = searchParams.get('email');
    const tokenParam = searchParams.get('token');
    
    if (!emailParam || !tokenParam) {
      setMensagem({ tipo: 'erro', texto: 'Link inválido ou expirado.' });
    } else {
      setEmail(emailParam);
      setToken(tokenParam);
    }
  }, [searchParams]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMensagem(null);

    if (novaSenha !== confirmarSenha) {
      setMensagem({ tipo: 'erro', texto: 'As senhas não coincidem.' });
      return;
    }

    if (novaSenha.length < 6) {
      setMensagem({ tipo: 'erro', texto: 'A senha deve ter no mínimo 6 caracteres.' });
      return;
    }

    setLoading(true);

    try {
      await api.put('/auth/redefinir-senha', {
        email,
        token,
        novaSenha
      });

      setMensagem({ tipo: 'sucesso', texto: 'Senha redefinida com sucesso! Redirecionando...' });
      
      setTimeout(() => {
        navigate('/login');
      }, 2000);
    } catch (error) {
      const errorData = error.response?.data;
      
      // Mensagens específicas baseadas no código de erro
      if (errorData?.code === 'TOKEN_EXPIRED_OR_USED') {
        setMensagem({
          tipo: 'erro',
          texto: 'Este link de recuperação expirou (válido por 24 horas) ou já foi utilizado. Por favor, solicite um novo link de recuperação.'
        });
      } else if (errorData?.code === 'USER_NOT_FOUND') {
        setMensagem({
          tipo: 'erro',
          texto: 'Usuário não encontrado. Verifique o email e tente novamente.'
        });
      } else {
        setMensagem({
          tipo: 'erro',
          texto: errorData?.message || 'Erro ao redefinir senha. Tente novamente mais tarde.'
        });
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-900 via-purple-900 to-slate-900 flex items-center justify-center p-4">
      <div className="w-full max-w-md">
        {/* Logo */}
        <div className="text-center mb-8 animate-fade-in">
          <div className="inline-flex items-center justify-center w-16 h-16 bg-gradient-to-br from-indigo-500 to-purple-600 rounded-2xl shadow-lg shadow-indigo-500/50 mb-4">
            <GraduationCap className="w-8 h-8 text-white" />
          </div>
          <h1 className="text-3xl font-bold text-white mb-2">PharmStudy</h1>
          <p className="text-slate-400">Redefinir Senha</p>
        </div>

        {/* Card */}
        <div className="bg-slate-800/50 backdrop-blur-xl border border-slate-700 rounded-2xl p-8 shadow-2xl">
          {!email || !token ? (
            <div className="text-center py-4">
              <AlertCircle className="w-12 h-12 text-red-400 mx-auto mb-3" />
              <p className="text-slate-300">Link inválido ou expirado.</p>
              <button
                onClick={() => navigate('/login')}
                className="mt-4 text-indigo-400 hover:text-indigo-300 text-sm font-medium"
              >
                Voltar para o login
              </button>
            </div>
          ) : (
            <form onSubmit={handleSubmit} className="space-y-5">
              {/* Email (readonly) */}
              <div>
                <label className="block text-sm font-medium text-slate-300 mb-2">
                  Email
                </label>
                <input
                  type="email"
                  value={email}
                  readOnly
                  className="w-full px-4 py-3 bg-slate-900/50 border border-slate-600 rounded-xl text-white text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-transparent cursor-not-allowed opacity-75"
                />
              </div>

              {/* Nova senha */}
              <div>
                <label className="block text-sm font-medium text-slate-300 mb-2">
                  Nova Senha
                </label>
                <div className="relative">
                  <Lock className="absolute left-3 top-1/2 -translate-y-1/2 w-5 h-5 text-slate-500" />
                  <input
                    type={mostrarNovaSenha ? 'text' : 'password'}
                    value={novaSenha}
                    onChange={(e) => setNovaSenha(e.target.value)}
                    placeholder="Mínimo 6 caracteres"
                    required
                    className="w-full pl-11 pr-11 py-3 bg-slate-900/50 border border-slate-600 rounded-xl text-white placeholder:text-slate-500 text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-transparent transition"
                  />
                  <button
                    type="button"
                    onClick={() => setMostrarNovaSenha(!mostrarNovaSenha)}
                    className="absolute right-3 top-1/2 -translate-y-1/2 text-slate-500 hover:text-slate-300 transition"
                  >
                    {mostrarNovaSenha ? <EyeOff className="w-5 h-5" /> : <Eye className="w-5 h-5" />}
                  </button>
                </div>
              </div>

              {/* Confirmar senha */}
              <div>
                <label className="block text-sm font-medium text-slate-300 mb-2">
                  Confirmar Nova Senha
                </label>
                <div className="relative">
                  <Lock className="absolute left-3 top-1/2 -translate-y-1/2 w-5 h-5 text-slate-500" />
                  <input
                    type={mostrarConfirmarSenha ? 'text' : 'password'}
                    value={confirmarSenha}
                    onChange={(e) => setConfirmarSenha(e.target.value)}
                    placeholder="Digite a senha novamente"
                    required
                    className="w-full pl-11 pr-11 py-3 bg-slate-900/50 border border-slate-600 rounded-xl text-white placeholder:text-slate-500 text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-transparent transition"
                  />
                  <button
                    type="button"
                    onClick={() => setMostrarConfirmarSenha(!mostrarConfirmarSenha)}
                    className="absolute right-3 top-1/2 -translate-y-1/2 text-slate-500 hover:text-slate-300 transition"
                  >
                    {mostrarConfirmarSenha ? <EyeOff className="w-5 h-5" /> : <Eye className="w-5 h-5" />}
                  </button>
                </div>
              </div>

              {/* Mensagem */}
              {mensagem && (
                <div className={`flex items-center gap-2 p-3 rounded-xl text-sm ${
                  mensagem.tipo === 'erro'
                    ? 'bg-red-500/10 border border-red-500/20 text-red-400'
                    : 'bg-green-500/10 border border-green-500/20 text-green-400'
                }`}>
                  {mensagem.tipo === 'erro' ? (
                    <AlertCircle className="w-5 h-5 shrink-0" />
                  ) : (
                    <CheckCircle className="w-5 h-5 shrink-0" />
                  )}
                  <span>{mensagem.texto}</span>
                </div>
              )}

              {/* Botão */}
              <button
                type="submit"
                disabled={loading || !novaSenha || !confirmarSenha}
                className="w-full py-3 bg-gradient-to-r from-indigo-500 to-purple-600 text-white font-semibold rounded-xl hover:from-indigo-600 hover:to-purple-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 focus:ring-offset-slate-900 transition shadow-lg shadow-indigo-500/30 disabled:opacity-50 disabled:cursor-not-allowed active:scale-95"
              >
                {loading ? 'Redefinindo...' : 'Redefinir Senha'}
              </button>

              {/* Link voltar */}
              <div className="text-center pt-2">
                <button
                  type="button"
                  onClick={() => navigate('/login')}
                  className="text-sm text-slate-400 hover:text-indigo-400 transition"
                >
                  Voltar para o login
                </button>
              </div>
            </form>
          )}
        </div>
      </div>
    </div>
  );
}
