import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import api from '../services/api';
import {
  CheckCircle2, ChevronRight, Heart, BookOpen, AlertCircle,
  Sparkles, ArrowRight, Loader
} from 'lucide-react';
import { getIcon } from '../utils/icons';

/**
 * Página de onboarding após registro
 * Permite ao usuário preencher dados de perfil e selecionar áreas de interesse
 */
export default function OnboardingPage() {
  const navigate = useNavigate();
  const { user, updateUser } = useAuth();
  const [step, setStep] = useState(1); // 1: Dados pessoais, 2: Interesses
  const [loading, setLoading] = useState(false);
  const [topics, setTopics] = useState([]);
  const [selectedInterests, setSelectedInterests] = useState(new Set());
  const [submitting, setSubmitting] = useState(false);

  // Dados do formulário
  const [formData, setFormData] = useState({
    nomeCompleto: user?.nomeCompleto || '',
    email: user?.email || '',
    telefone: user?.telefone || '',
    dataNascimento: user?.dataNascimento ? user.dataNascimento.split('T')[0] : '',
    bio: user?.bio || ''
  });

  useEffect(() => {
    const fetchTopics = async () => {
      try {
        setLoading(true);
        const res = await api.get('/topics');
        setTopics(res.data);
      } catch (error) {
        console.error('Erro ao carregar disciplinas:', error);
      } finally {
        setLoading(false);
      }
    };
    fetchTopics();
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleNext = () => {
    // Validação básica
    if (!formData.nomeCompleto.trim()) {
      alert('Por favor, informe seu nome completo');
      return;
    }
    if (!formData.email.trim()) {
      alert('Por favor, informe seu email');
      return;
    }
    setStep(2);
  };

  const toggleInterest = (topicId) => {
    setSelectedInterests(prev => {
      const newInterests = new Set(prev);
      if (newInterests.has(topicId)) {
        newInterests.delete(topicId);
      } else {
        newInterests.add(topicId);
      }
      return newInterests;
    });
  };

  const handleComplete = async () => {
    try {
      setSubmitting(true);

      // Salvar dados do perfil se houver alterações
      if (formData.nomeCompleto !== user?.nomeCompleto || 
          formData.telefone !== user?.telefone ||
          formData.dataNascimento !== (user?.dataNascimento?.split('T')[0] || '') ||
          formData.bio !== user?.bio) {
        
        const dto = new FormData();
        dto.append('nomeCompleto', formData.nomeCompleto);
        dto.append('email', formData.email);
        if (formData.telefone) dto.append('telefone', formData.telefone);
        if (formData.dataNascimento) dto.append('dataNascimento', formData.dataNascimento);
        if (formData.bio) dto.append('bio', formData.bio);

        console.log('Atualizando perfil com:', {
          nomeCompleto: formData.nomeCompleto,
          email: formData.email,
          telefone: formData.telefone,
          dataNascimento: formData.dataNascimento,
          bio: formData.bio
        });

        const profileRes = await api.put('/auth/perfil', dto);
        console.log('Perfil atualizado:', profileRes.data);
      }

      // Salvar interesses (favoritar disciplinas)
      for (const topicId of selectedInterests) {
        try {
          await api.post(`/usertopics/favorites/${topicId}`);
        } catch (error) {
          console.error(`Erro ao favoritar tópico ${topicId}:`, error);
        }
      }

      // Atualizar contexto de autenticação
      if (updateUser) {
        updateUser({
          ...user,
          nomeCompleto: formData.nomeCompleto,
          telefone: formData.telefone,
          dataNascimento: formData.dataNascimento,
          bio: formData.bio
        });
      }

      // Redirecionar para dashboard
      navigate('/');
    } catch (error) {
      console.error('Erro ao completar onboarding:', error.response?.data || error.message);
      alert('Erro ao salvar dados. Tente novamente.');
    } finally {
      setSubmitting(false);
    }
  };

  // Agrupar tópicos por categoria
  const topicsByCategory = topics.reduce((acc, topic) => {
    if (!acc[topic.categoria]) {
      acc[topic.categoria] = [];
    }
    acc[topic.categoria].push(topic);
    return acc;
  }, {});

  const categories = Object.keys(topicsByCategory).sort();

  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-900 via-purple-900 to-slate-900 p-4 md:p-6 flex items-center justify-center">
      <div className="w-full max-w-2xl">
        {/* Progress indicator */}
        <div className="mb-8">
          <div className="flex items-center gap-4">
            <div className={`w-10 h-10 rounded-full flex items-center justify-center font-bold text-sm transition ${
              step === 1 
                ? 'bg-indigo-600 text-white' 
                : 'bg-emerald-600 text-white'
            }`}>
              {step === 1 ? '1' : <CheckCircle2 className="w-5 h-5" />}
            </div>
            <div className={`flex-1 h-1 rounded ${step === 2 ? 'bg-indigo-600' : 'bg-slate-700'}`} />
            <div className={`w-10 h-10 rounded-full flex items-center justify-center font-bold text-sm ${
              step === 2 
                ? 'bg-indigo-600 text-white' 
                : 'bg-slate-700 text-slate-400'
            }`}>
              2
            </div>
          </div>
          <div className="flex justify-between mt-2 text-xs">
            <span className={step === 1 ? 'text-indigo-400 font-semibold' : 'text-slate-500'}>
              Dados Pessoais
            </span>
            <span className={step === 2 ? 'text-indigo-400 font-semibold' : 'text-slate-500'}>
              Áreas de Interesse
            </span>
          </div>
        </div>

        {/* Card */}
        <div className="relative rounded-2xl bg-slate-800/40 border border-slate-700/50 backdrop-blur-xl overflow-hidden">
          <div className="absolute -top-20 -right-20 w-40 h-40 bg-indigo-500/10 rounded-full blur-3xl" />
          <div className="absolute -bottom-20 -left-20 w-40 h-40 bg-purple-500/10 rounded-full blur-3xl" />

          <div className="relative p-6 md:p-8 z-10">
            {/* Passo 1: Dados Pessoais */}
            {step === 1 && (
              <div className="space-y-6">
                <div>
                  <div className="flex items-center gap-3 mb-2">
                    <Sparkles className="w-5 h-5 text-amber-400" />
                    <h2 className="text-2xl md:text-3xl font-bold text-white">
                      Bem-vindo ao Portal Estudos! 👋
                    </h2>
                  </div>
                  <p className="text-sm md:text-base text-slate-300">
                    Vamos completar seu perfil para personalizar sua experiência
                  </p>
                </div>

                <div className="space-y-4">
                  {/* Nome Completo */}
                  <div>
                    <label className="block text-xs font-semibold text-slate-300 mb-2">
                      Nome Completo *
                    </label>
                    <input
                      type="text"
                      name="nomeCompleto"
                      value={formData.nomeCompleto}
                      onChange={handleInputChange}
                      placeholder="Digite seu nome completo"
                      className="w-full px-4 py-2.5 rounded-lg bg-slate-700/50 border border-slate-600 text-white placeholder-slate-500 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-transparent transition"
                    />
                  </div>

                  {/* Email */}
                  <div>
                    <label className="block text-xs font-semibold text-slate-300 mb-2">
                      Email *
                    </label>
                    <input
                      type="email"
                      name="email"
                      value={formData.email}
                      disabled
                      className="w-full px-4 py-2.5 rounded-lg bg-slate-700/30 border border-slate-600 text-slate-400 cursor-not-allowed"
                    />
                    <p className="text-xs text-slate-400 mt-1">Email usado no registro (não pode ser alterado)</p>
                  </div>

                  {/* Telefone */}
                  <div>
                    <label className="block text-xs font-semibold text-slate-300 mb-2">
                      Telefone
                    </label>
                    <input
                      type="tel"
                      name="telefone"
                      value={formData.telefone}
                      onChange={handleInputChange}
                      placeholder="(11) 99999-9999"
                      className="w-full px-4 py-2.5 rounded-lg bg-slate-700/50 border border-slate-600 text-white placeholder-slate-500 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-transparent transition"
                    />
                  </div>

                  {/* Data de Nascimento */}
                  <div>
                    <label className="block text-xs font-semibold text-slate-300 mb-2">
                      Data de Nascimento
                    </label>
                    <input
                      type="date"
                      name="dataNascimento"
                      value={formData.dataNascimento}
                      onChange={handleInputChange}
                      className="w-full px-4 py-2.5 rounded-lg bg-slate-700/50 border border-slate-600 text-white focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-transparent transition"
                    />
                  </div>

                  {/* Bio */}
                  <div>
                    <label className="block text-xs font-semibold text-slate-300 mb-2">
                      Sobre você
                    </label>
                    <textarea
                      name="bio"
                      value={formData.bio}
                      onChange={handleInputChange}
                      placeholder="Conte um pouco sobre você (opcional)"
                      rows={3}
                      className="w-full px-4 py-2.5 rounded-lg bg-slate-700/50 border border-slate-600 text-white placeholder-slate-500 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-transparent transition resize-none"
                    />
                  </div>
                </div>

                {/* Botões */}
                <div className="flex gap-3 pt-4">
                  <button
                    onClick={() => navigate('/')}
                    className="flex-1 px-4 py-2.5 rounded-lg border border-slate-600 text-white font-medium hover:bg-slate-700/50 transition text-sm"
                  >
                    Pular por agora
                  </button>
                  <button
                    onClick={handleNext}
                    className="flex-1 flex items-center justify-center gap-2 px-4 py-2.5 rounded-lg bg-indigo-600 hover:bg-indigo-700 active:bg-indigo-800 text-white font-medium transition text-sm"
                  >
                    Próximo
                    <ChevronRight className="w-4 h-4" />
                  </button>
                </div>
              </div>
            )}

            {/* Passo 2: Interesses */}
            {step === 2 && (
              <div className="space-y-6">
                <div>
                  <div className="flex items-center gap-3 mb-2">
                    <Heart className="w-5 h-5 text-rose-400" />
                    <h2 className="text-2xl md:text-3xl font-bold text-white">
                      Áreas de Interesse
                    </h2>
                  </div>
                  <p className="text-sm md:text-base text-slate-300">
                    Selecione as áreas que mais te interessam em Farmácia (opcional)
                  </p>
                </div>

                {loading ? (
                  <div className="flex items-center justify-center py-12">
                    <Loader className="w-6 h-6 text-indigo-400 animate-spin" />
                  </div>
                ) : (
                  <div className="space-y-4 max-h-96 overflow-y-auto pr-2">
                    {categories.map(categoria => (
                      <div key={categoria}>
                        <h3 className="text-sm font-semibold text-slate-300 mb-3 flex items-center gap-2">
                          <BookOpen className="w-4 h-4 text-indigo-400" />
                          {categoria}
                        </h3>
                        <div className="space-y-2 pl-6">
                          {topicsByCategory[categoria].map(topic => (
                            <button
                              key={topic.id}
                              onClick={() => toggleInterest(topic.id)}
                              className={`w-full flex items-start gap-3 p-3 rounded-lg border-2 transition text-left ${
                                selectedInterests.has(topic.id)
                                  ? 'border-indigo-500 bg-indigo-500/10'
                                  : 'border-slate-600/50 bg-slate-700/20 hover:border-slate-500'
                              }`}
                            >
                              <div className={`w-5 h-5 rounded-md border-2 flex items-center justify-center flex-shrink-0 mt-0.5 transition ${
                                selectedInterests.has(topic.id)
                                  ? 'border-indigo-500 bg-indigo-600'
                                  : 'border-slate-500'
                              }`}>
                                {selectedInterests.has(topic.id) && (
                                  <CheckCircle2 className="w-4 h-4 text-white" />
                                )}
                              </div>
                              <div className="flex-1 min-w-0">
                                <p className="font-medium text-white text-sm">
                                  {topic.nome}
                                </p>
                                <p className="text-xs text-slate-400 line-clamp-2">
                                  {topic.descricao}
                                </p>
                              </div>
                            </button>
                          ))}
                        </div>
                      </div>
                    ))}
                  </div>
                )}

                {/* Info */}
                <div className="rounded-lg bg-blue-500/10 border border-blue-500/30 p-3 flex gap-2">
                  <AlertCircle className="w-4 h-4 text-blue-400 flex-shrink-0 mt-0.5" />
                  <p className="text-xs text-blue-300">
                    Você pode alterar suas áreas de interesse a qualquer momento na seção de Disciplinas
                  </p>
                </div>

                {/* Botões */}
                <div className="flex gap-3 pt-4">
                  <button
                    onClick={() => setStep(1)}
                    disabled={submitting}
                    className="flex-1 px-4 py-2.5 rounded-lg border border-slate-600 text-white font-medium hover:bg-slate-700/50 disabled:opacity-50 disabled:cursor-not-allowed transition text-sm"
                  >
                    Voltar
                  </button>
                  <button
                    onClick={handleComplete}
                    disabled={submitting}
                    className="flex-1 flex items-center justify-center gap-2 px-4 py-2.5 rounded-lg bg-emerald-600 hover:bg-emerald-700 active:bg-emerald-800 disabled:opacity-50 disabled:cursor-not-allowed text-white font-medium transition text-sm"
                  >
                    {submitting ? (
                      <>
                        <Loader className="w-4 h-4 animate-spin" />
                        Salvando...
                      </>
                    ) : (
                      <>
                        Começar a Estudar
                        <ArrowRight className="w-4 h-4" />
                      </>
                    )}
                  </button>
                </div>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
