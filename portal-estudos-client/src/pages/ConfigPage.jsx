import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import api from '../services/api';
import {
  Settings, User, Mail, Lock, Camera, ArrowLeft, Save,
  Eye, EyeOff, AlertCircle, CheckCircle, Phone, Calendar,
} from 'lucide-react';

export default function ConfigPage() {
  const { user, updateUser } = useAuth();
  const navigate = useNavigate();

  // Estados para perfil
  const [nomeCompleto, setNomeCompleto] = useState(user?.nomeCompleto || '');
  const [email, setEmail] = useState(user?.email || '');
  const [telefone, setTelefone] = useState(user?.telefone || '');
  const [dataNascimento, setDataNascimento] = useState(user?.dataNascimento?.split('T')[0] || '');
  const [bio, setBio] = useState(user?.bio || '');
  const [fotoPerfil, setFotoPerfil] = useState(null);
  const [fotoPreview, setFotoPreview] = useState(
    user?.fotoPerfilUrl ? `http://localhost:5000${user.fotoPerfilUrl}` : null
  );

  // Estados para senha
  const [senhaAtual, setSenhaAtual] = useState('');
  const [novaSenha, setNovaSenha] = useState('');
  const [confirmarSenha, setConfirmarSenha] = useState('');
  const [mostrarSenhaAtual, setMostrarSenhaAtual] = useState(false);
  const [mostrarNovaSenha, setMostrarNovaSenha] = useState(false);
  const [mostrarConfirmarSenha, setMostrarConfirmarSenha] = useState(false);

  // Estados de UI
  const [salvandoPerfil, setSalvandoPerfil] = useState(false);
  const [salvandoSenha, setSalvandoSenha] = useState(false);
  const [enviandoRecuperacao, setEnviandoRecuperacao] = useState(false);
  const [mensagemPerfil, setMensagemPerfil] = useState(null);
  const [mensagemSenha, setMensagemSenha] = useState(null);
  const [mensagemRecuperacao, setMensagemRecuperacao] = useState(null);

  const handleFotoChange = (e) => {
    const file = e.target.files[0];
    if (file) {
      if (file.size > 5 * 1024 * 1024) {
        setMensagemPerfil({ tipo: 'erro', texto: 'Imagem muito grande. Máximo 5MB.' });
        return;
      }
      setFotoPerfil(file);
      const reader = new FileReader();
      reader.onloadend = () => setFotoPreview(reader.result);
      reader.readAsDataURL(file);
    }
  };

  const handleSalvarPerfil = async (e) => {
    e.preventDefault();
    setMensagemPerfil(null);
    setSalvandoPerfil(true);

    try {
      const formData = new FormData();
      formData.append('nomeCompleto', nomeCompleto);
      formData.append('email', email);
      formData.append('telefone', telefone || '');
      formData.append('dataNascimento', dataNascimento || '');
      formData.append('bio', bio || '');
      if (fotoPerfil) formData.append('fotoPerfil', fotoPerfil);

      const res = await api.put('/auth/perfil', formData, {
        headers: { 'Content-Type': 'multipart/form-data' }
      });

      updateUser(res.data);
      setMensagemPerfil({ tipo: 'sucesso', texto: 'Perfil atualizado com sucesso!' });
    } catch (error) {
      setMensagemPerfil({ 
        tipo: 'erro', 
        texto: error.response?.data?.message || 'Erro ao atualizar perfil.' 
      });
    } finally {
      setSalvandoPerfil(false);
    }
  };

  const handleAlterarSenha = async (e) => {
    e.preventDefault();
    setMensagemSenha(null);

    if (!senhaAtual.trim()) {
      setMensagemSenha({ tipo: 'erro', texto: 'Digite a senha atual.' });
      return;
    }

    if (novaSenha.length < 6) {
      setMensagemSenha({ tipo: 'erro', texto: 'A nova senha deve ter no mínimo 6 caracteres.' });
      return;
    }

    if (novaSenha !== confirmarSenha) {
      setMensagemSenha({ tipo: 'erro', texto: 'As senhas não coincidem.' });
      return;
    }

    setSalvandoSenha(true);

    try {
      await api.put('/auth/alterar-senha', {
        senhaAtual,
        novaSenha,
      });

      setMensagemSenha({ tipo: 'sucesso', texto: 'Senha alterada com sucesso!' });
      setSenhaAtual('');
      setNovaSenha('');
      setConfirmarSenha('');
    } catch (error) {
      if (error.response?.status === 401) {
        setMensagemSenha({ tipo: 'erro', texto: 'Senha atual incorreta.' });
      } else {
        setMensagemSenha({ 
          tipo: 'erro', 
          texto: error.response?.data?.message || 'Erro ao alterar senha.' 
        });
      }
    } finally {
      setSalvandoSenha(false);
    }
  };

  const handleEsqueciSenha = async () => {
    setMensagemRecuperacao(null);
    setEnviandoRecuperacao(true);

    try {
      await api.post('/auth/recuperar-senha', { email: user.email });
      setMensagemRecuperacao({ 
        tipo: 'sucesso', 
        texto: 'Email de recuperação enviado! Verifique sua caixa de entrada.' 
      });
    } catch (error) {
      setMensagemRecuperacao({ 
        tipo: 'erro', 
        texto: 'Erro ao enviar email de recuperação.' 
      });
    } finally {
      setEnviandoRecuperacao(false);
    }
  };

  return (
    <div className="p-4 md:p-6 lg:p-8 max-w-4xl mx-auto space-y-6">
      {/* Header */}
      <div className="flex items-center gap-3 lg:gap-4">
        <button
          onClick={() => navigate('/')}
          className="p-2 rounded-xl text-slate-400 hover:text-white hover:bg-slate-800/50 active:scale-95 transition"
        >
          <ArrowLeft className="w-5 h-5" />
        </button>
        <div>
          <h1 className="text-xl md:text-2xl font-bold text-white flex items-center gap-3">
            <Settings className="w-6 h-6 lg:w-7 lg:h-7 text-indigo-400" /> Configurações
          </h1>
          <p className="text-slate-400 text-xs md:text-sm mt-1">Gerencie suas informações pessoais e segurança</p>
        </div>
      </div>

      {/* Seção: Perfil */}
      <form onSubmit={handleSalvarPerfil} className="bg-slate-800/50 rounded-2xl border border-slate-700/50 overflow-hidden">
        <div className="p-4 md:p-6 border-b border-slate-700/50">
          <h2 className="text-lg font-semibold text-white flex items-center gap-2">
            <User className="w-5 h-5 text-indigo-400" /> Informações do Perfil
          </h2>
        </div>

        <div className="p-4 md:p-6 space-y-5">
          {/* Foto de Perfil */}
          <div className="flex flex-col sm:flex-row items-center gap-4">
            <div className="relative">
              <div className="w-24 h-24 rounded-full overflow-hidden bg-gradient-to-br from-indigo-500 to-purple-600 flex items-center justify-center">
                {fotoPreview ? (
                  <img src={fotoPreview} alt="Perfil" className="w-full h-full object-cover" />
                ) : (
                  <span className="text-4xl font-bold text-white">
                    {user?.nomeCompleto?.charAt(0)?.toUpperCase() || 'U'}
                  </span>
                )}
              </div>
              <label className="absolute bottom-0 right-0 p-2 bg-indigo-500 rounded-full cursor-pointer hover:bg-indigo-600 transition active:scale-95">
                <Camera className="w-4 h-4 text-white" />
                <input type="file" accept="image/*" onChange={handleFotoChange} className="hidden" />
              </label>
            </div>
            <div className="text-center sm:text-left">
              <p className="text-sm font-medium text-white">Foto de Perfil</p>
              <p className="text-xs text-slate-500 mt-1">JPG, PNG ou GIF. Máximo 5MB.</p>
            </div>
          </div>

          {/* Nome Completo */}
          <div>
            <label className="block text-sm font-medium text-slate-300 mb-1.5">Nome Completo *</label>
            <input
              type="text"
              value={nomeCompleto}
              onChange={(e) => setNomeCompleto(e.target.value)}
              required
              className="w-full px-4 py-2.5 rounded-xl bg-slate-900/50 border border-slate-700 text-white placeholder-slate-500 focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 outline-none transition text-sm"
              placeholder="Seu nome completo"
            />
          </div>

          {/* Email */}
          <div>
            <label className="block text-sm font-medium text-slate-300 mb-1.5">Email *</label>
            <div className="relative">
              <Mail className="absolute left-3 top-1/2 -translate-y-1/2 w-5 h-5 text-slate-500" />
              <input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
                className="w-full pl-11 pr-4 py-2.5 rounded-xl bg-slate-900/50 border border-slate-700 text-white placeholder-slate-500 focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 outline-none transition text-sm"
                placeholder="seu@email.com"
              />
            </div>
          </div>

          {/* Telefone */}
          <div>
            <label className="block text-sm font-medium text-slate-300 mb-1.5">Telefone</label>
            <div className="relative">
              <Phone className="absolute left-3 top-1/2 -translate-y-1/2 w-5 h-5 text-slate-500" />
              <input
                type="tel"
                value={telefone}
                onChange={(e) => setTelefone(e.target.value)}
                className="w-full pl-11 pr-4 py-2.5 rounded-xl bg-slate-900/50 border border-slate-700 text-white placeholder-slate-500 focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 outline-none transition text-sm"
                placeholder="(00) 00000-0000"
              />
            </div>
          </div>

          {/* Data de Nascimento */}
          <div>
            <label className="block text-sm font-medium text-slate-300 mb-1.5">Data de Nascimento</label>
            <div className="relative">
              <Calendar className="absolute left-3 top-1/2 -translate-y-1/2 w-5 h-5 text-slate-500" />
              <input
                type="date"
                value={dataNascimento}
                onChange={(e) => setDataNascimento(e.target.value)}
                className="w-full pl-11 pr-4 py-2.5 rounded-xl bg-slate-900/50 border border-slate-700 text-white placeholder-slate-500 focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 outline-none transition text-sm"
              />
            </div>
          </div>

          {/* Bio */}
          <div>
            <label className="block text-sm font-medium text-slate-300 mb-1.5">Biografia</label>
            <textarea
              value={bio}
              onChange={(e) => setBio(e.target.value)}
              rows="3"
              maxLength="200"
              className="w-full px-4 py-2.5 rounded-xl bg-slate-900/50 border border-slate-700 text-white placeholder-slate-500 focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 outline-none transition text-sm resize-none"
              placeholder="Conte um pouco sobre você..."
            />
            <p className="text-xs text-slate-500 mt-1">{bio.length}/200 caracteres</p>
          </div>

          {/* Mensagem de Sucesso/Erro */}
          {mensagemPerfil && (
            <div className={`flex items-center gap-2 p-3 rounded-xl text-sm ${
              mensagemPerfil.tipo === 'sucesso' 
                ? 'bg-green-500/10 border border-green-500/20 text-green-400' 
                : 'bg-red-500/10 border border-red-500/20 text-red-400'
            }`}>
              {mensagemPerfil.tipo === 'sucesso' ? <CheckCircle className="w-5 h-5" /> : <AlertCircle className="w-5 h-5" />}
              {mensagemPerfil.texto}
            </div>
          )}

          {/* Botão Salvar */}
          <button
            type="submit"
            disabled={salvandoPerfil}
            className="w-full flex items-center justify-center gap-2 px-5 py-3 bg-indigo-500 hover:bg-indigo-600 active:bg-indigo-700 text-white rounded-xl font-medium transition disabled:opacity-50 active:scale-[0.99]"
          >
            <Save className="w-5 h-5" />
            {salvandoPerfil ? 'Salvando...' : 'Salvar Alterações'}
          </button>
        </div>
      </form>

      {/* Seção: Alterar Senha */}
      <form onSubmit={handleAlterarSenha} className="bg-slate-800/50 rounded-2xl border border-slate-700/50 overflow-hidden">
        <div className="p-4 md:p-6 border-b border-slate-700/50">
          <h2 className="text-lg font-semibold text-white flex items-center gap-2">
            <Lock className="w-5 h-5 text-indigo-400" /> Segurança
          </h2>
        </div>

        <div className="p-4 md:p-6 space-y-5">
          {/* Senha Atual */}
          <div>
            <label className="block text-sm font-medium text-slate-300 mb-1.5">Senha Atual *</label>
            <div className="relative">
              <Lock className="absolute left-3 top-1/2 -translate-y-1/2 w-5 h-5 text-slate-500" />
              <input
                type={mostrarSenhaAtual ? 'text' : 'password'}
                value={senhaAtual}
                onChange={(e) => setSenhaAtual(e.target.value)}
                className="w-full pl-11 pr-12 py-2.5 rounded-xl bg-slate-900/50 border border-slate-700 text-white placeholder-slate-500 focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 outline-none transition text-sm"
                placeholder="Digite sua senha atual"
              />
              <button
                type="button"
                onClick={() => setMostrarSenhaAtual(!mostrarSenhaAtual)}
                className="absolute right-3 top-1/2 -translate-y-1/2 text-slate-500 hover:text-white transition"
              >
                {mostrarSenhaAtual ? <EyeOff className="w-5 h-5" /> : <Eye className="w-5 h-5" />}
              </button>
            </div>
            <button
              type="button"
              onClick={handleEsqueciSenha}
              disabled={enviandoRecuperacao}
              className="text-xs text-indigo-400 hover:text-indigo-300 mt-1.5 transition disabled:opacity-50"
            >
              {enviandoRecuperacao ? 'Enviando...' : 'Esqueci a senha'}
            </button>
          </div>

          {/* Nova Senha */}
          <div>
            <label className="block text-sm font-medium text-slate-300 mb-1.5">Nova Senha *</label>
            <div className="relative">
              <Lock className="absolute left-3 top-1/2 -translate-y-1/2 w-5 h-5 text-slate-500" />
              <input
                type={mostrarNovaSenha ? 'text' : 'password'}
                value={novaSenha}
                onChange={(e) => setNovaSenha(e.target.value)}
                className="w-full pl-11 pr-12 py-2.5 rounded-xl bg-slate-900/50 border border-slate-700 text-white placeholder-slate-500 focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 outline-none transition text-sm"
                placeholder="Mínimo 6 caracteres"
              />
              <button
                type="button"
                onClick={() => setMostrarNovaSenha(!mostrarNovaSenha)}
                className="absolute right-3 top-1/2 -translate-y-1/2 text-slate-500 hover:text-white transition"
              >
                {mostrarNovaSenha ? <EyeOff className="w-5 h-5" /> : <Eye className="w-5 h-5" />}
              </button>
            </div>
          </div>

          {/* Confirmar Nova Senha */}
          <div>
            <label className="block text-sm font-medium text-slate-300 mb-1.5">Confirmar Nova Senha *</label>
            <div className="relative">
              <Lock className="absolute left-3 top-1/2 -translate-y-1/2 w-5 h-5 text-slate-500" />
              <input
                type={mostrarConfirmarSenha ? 'text' : 'password'}
                value={confirmarSenha}
                onChange={(e) => setConfirmarSenha(e.target.value)}
                className="w-full pl-11 pr-12 py-2.5 rounded-xl bg-slate-900/50 border border-slate-700 text-white placeholder-slate-500 focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 outline-none transition text-sm"
                placeholder="Digite novamente a nova senha"
              />
              <button
                type="button"
                onClick={() => setMostrarConfirmarSenha(!mostrarConfirmarSenha)}
                className="absolute right-3 top-1/2 -translate-y-1/2 text-slate-500 hover:text-white transition"
              >
                {mostrarConfirmarSenha ? <EyeOff className="w-5 h-5" /> : <Eye className="w-5 h-5" />}
              </button>
            </div>
          </div>

          {/* Mensagem de Recuperação */}
          {mensagemRecuperacao && (
            <div className={`flex items-center gap-2 p-3 rounded-xl text-sm ${
              mensagemRecuperacao.tipo === 'sucesso' 
                ? 'bg-green-500/10 border border-green-500/20 text-green-400' 
                : 'bg-red-500/10 border border-red-500/20 text-red-400'
            }`}>
              {mensagemRecuperacao.tipo === 'sucesso' ? <CheckCircle className="w-5 h-5" /> : <AlertCircle className="w-5 h-5" />}
              {mensagemRecuperacao.texto}
            </div>
          )}

          {/* Mensagem de Sucesso/Erro Senha */}
          {mensagemSenha && (
            <div className={`flex items-center gap-2 p-3 rounded-xl text-sm ${
              mensagemSenha.tipo === 'sucesso' 
                ? 'bg-green-500/10 border border-green-500/20 text-green-400' 
                : 'bg-red-500/10 border border-red-500/20 text-red-400'
            }`}>
              {mensagemSenha.tipo === 'sucesso' ? <CheckCircle className="w-5 h-5" /> : <AlertCircle className="w-5 h-5" />}
              {mensagemSenha.texto}
            </div>
          )}

          {/* Botão Alterar Senha */}
          <button
            type="submit"
            disabled={salvandoSenha}
            className="w-full flex items-center justify-center gap-2 px-5 py-3 bg-purple-500 hover:bg-purple-600 active:bg-purple-700 text-white rounded-xl font-medium transition disabled:opacity-50 active:scale-[0.99]"
          >
            <Lock className="w-5 h-5" />
            {salvandoSenha ? 'Alterando...' : 'Alterar Senha'}
          </button>
        </div>
      </form>
    </div>
  );
}
