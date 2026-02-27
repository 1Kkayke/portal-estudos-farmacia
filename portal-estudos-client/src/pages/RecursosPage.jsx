import { ExternalLink, BookOpen, FlaskConical, Scale, Globe, FileText, GraduationCap, Calculator, Microscope } from 'lucide-react';

const SECTIONS = [
  {
    titulo: 'Órgãos Reguladores',
    icone: Scale,
    cor: '#6366f1',
    links: [
      { nome: 'ANVISA', desc: 'Agência Nacional de Vigilância Sanitária – legislação, consultas públicas e registros.', url: 'https://www.gov.br/anvisa' },
      { nome: 'CFF – Conselho Federal de Farmácia', desc: 'Resoluções, código de ética e registro profissional.', url: 'https://www.cff.org.br' },
      { nome: 'Ministério da Saúde', desc: 'Políticas de saúde, RENAME, protocolos clínicos e diretrizes terapêuticas.', url: 'https://www.gov.br/saude' },
    ],
  },
  {
    titulo: 'Bulários e Informações de Medicamentos',
    icone: FileText,
    cor: '#8b5cf6',
    links: [
      { nome: 'Bulário Eletrônico ANVISA', desc: 'Bulas de todos os medicamentos registrados no Brasil.', url: 'https://consultas.anvisa.gov.br/#/bulario/' },
      { nome: 'Micromedex (via CAPES)', desc: 'Base de dados de evidências sobre medicamentos, interações e toxicologia.', url: 'https://www.micromedexsolutions.com' },
      { nome: 'UpToDate', desc: 'Referência clínica baseada em evidências para decisões de saúde.', url: 'https://www.uptodate.com' },
      { nome: 'Drugs.com', desc: 'Informações sobre medicamentos, interações e identificação de comprimidos (inglês).', url: 'https://www.drugs.com' },
    ],
  },
  {
    titulo: 'Periódicos e Pesquisa Científica',
    icone: Microscope,
    cor: '#ec4899',
    links: [
      { nome: 'PubMed', desc: 'Maior base de dados de artigos biomédicos do mundo (NCBI/NIH).', url: 'https://pubmed.ncbi.nlm.nih.gov' },
      { nome: 'SciELO', desc: 'Biblioteca eletrônica com periódicos científicos brasileiros e latino-americanos.', url: 'https://www.scielo.br' },
      { nome: 'Portal de Periódicos CAPES', desc: 'Acesso gratuito a milhares de periódicos para estudantes de instituições públicas.', url: 'https://www.periodicos.capes.gov.br' },
      { nome: 'Google Scholar', desc: 'Busca de artigos acadêmicos, teses e livros.', url: 'https://scholar.google.com.br' },
      { nome: 'Revista Brasileira de Farmácia', desc: 'Publicação científica oficial da Associação Brasileira de Farmacêuticos.', url: 'http://www.rbfarma.org.br' },
    ],
  },
  {
    titulo: 'Calculadoras e Ferramentas',
    icone: Calculator,
    cor: '#f59e0b',
    links: [
      { nome: 'MedCalc', desc: 'Calculadoras médicas: clearance de creatinina (Cockcroft-Gault), IMC, doses pediátricas.', url: 'https://www.mdcalc.com' },
      { nome: 'GlobalRPh', desc: 'Calculadoras farmacêuticas para farmacocinética, diluições e compatibilidades IV.', url: 'https://www.globalrph.com' },
      { nome: 'Tabela Periódica Interativa', desc: 'Tabela periódica online com propriedades detalhadas dos elementos.', url: 'https://ptable.com/?lang=pt' },
    ],
  },
  {
    titulo: 'Química e Laboratório',
    icone: FlaskConical,
    cor: '#22c55e',
    links: [
      { nome: 'Farmacopeia Brasileira', desc: 'Monografias oficiais de insumos e medicamentos (acesso via ANVISA).', url: 'https://www.gov.br/anvisa/pt-br/assuntos/farmacopeia' },
      { nome: 'PubChem', desc: 'Base de dados de estruturas químicas, propriedades e atividades biológicas.', url: 'https://pubchem.ncbi.nlm.nih.gov' },
      { nome: 'ChemDraw Online', desc: 'Desenho de estruturas moleculares online.', url: 'https://chemdrawdirect.perkinelmer.cloud/js/sample/index.html' },
    ],
  },
  {
    titulo: 'Estudo e Preparação para Concursos',
    icone: GraduationCap,
    cor: '#ef4444',
    links: [
      { nome: 'QConcursos – Farmácia', desc: 'Questões de concursos passados com gabarito e comentários.', url: 'https://www.qconcursos.com' },
      { nome: 'Passei Direto', desc: 'Materiais de estudo compartilhados por estudantes de Farmácia.', url: 'https://www.passeidireto.com' },
      { nome: 'Khan Academy (Bioquímica)', desc: 'Aulas gratuitas de bioquímica, biologia molecular e química orgânica.', url: 'https://pt.khanacademy.org' },
      { nome: 'Farmacologia Ilustrada (Resumos)', desc: 'Canal e comunidade com resumos visuais de farmacologia.', url: 'https://www.youtube.com' },
    ],
  },
  {
    titulo: 'Comunidades e Associações',
    icone: Globe,
    cor: '#06b6d4',
    links: [
      { nome: 'SBFTE – Sociedade Brasileira de Farmacologia', desc: 'Eventos, cursos e publicações da área de farmacologia.', url: 'https://www.sbfte.org.br' },
      { nome: 'ABRAFARMA', desc: 'Associação Brasileira de Redes de Farmácias e Drogarias.', url: 'https://www.abrafarma.com.br' },
      { nome: 'ICTQ', desc: 'Instituto de Ciência, Tecnologia e Qualidade – pós-graduações e cursos para farmacêuticos.', url: 'https://www.ictq.com.br' },
    ],
  },
];

export default function RecursosPage() {
  return (
    <div className="p-6 lg:p-8 max-w-5xl mx-auto space-y-8">
      {/* Header */}
      <div>
        <h1 className="text-2xl font-bold text-white flex items-center gap-3">
          <BookOpen className="w-7 h-7 text-indigo-400" /> Recursos Úteis
        </h1>
        <p className="text-slate-400 text-sm mt-1">Links essenciais para o dia a dia do estudante de Farmácia.</p>
      </div>

      {/* Stats */}
      <div className="flex gap-3 flex-wrap">
        <div className="bg-slate-800/50 border border-slate-700/50 rounded-xl px-4 py-2 text-sm">
          <span className="text-white font-semibold">{SECTIONS.length}</span>
          <span className="text-slate-400 ml-1">categorias</span>
        </div>
        <div className="bg-slate-800/50 border border-slate-700/50 rounded-xl px-4 py-2 text-sm">
          <span className="text-white font-semibold">{SECTIONS.reduce((a, s) => a + s.links.length, 0)}</span>
          <span className="text-slate-400 ml-1">recursos</span>
        </div>
      </div>

      {/* Sections */}
      {SECTIONS.map((sec, i) => {
        const Icon = sec.icone;
        return (
          <div key={i} className="space-y-3">
            <div className="flex items-center gap-3">
              <div className="p-2 rounded-xl" style={{ backgroundColor: sec.cor + '22' }}>
                <Icon className="w-5 h-5" style={{ color: sec.cor }} />
              </div>
              <h2 className="text-lg font-semibold text-white">{sec.titulo}</h2>
            </div>
            <div className="grid sm:grid-cols-2 lg:grid-cols-3 gap-3 pl-12">
              {sec.links.map((link, j) => (
                <a key={j} href={link.url} target="_blank" rel="noopener noreferrer"
                  className="group block rounded-xl bg-slate-800/50 border border-slate-700/50 p-4 hover:border-slate-600/50 transition">
                  <div className="flex items-start justify-between gap-2">
                    <h3 className="text-white text-sm font-medium group-hover:text-indigo-400 transition">{link.nome}</h3>
                    <ExternalLink className="w-3.5 h-3.5 text-slate-600 group-hover:text-indigo-400 transition flex-shrink-0 mt-0.5" />
                  </div>
                  <p className="text-slate-500 text-xs mt-1.5 leading-relaxed">{link.desc}</p>
                </a>
              ))}
            </div>
          </div>
        );
      })}
    </div>
  );
}
