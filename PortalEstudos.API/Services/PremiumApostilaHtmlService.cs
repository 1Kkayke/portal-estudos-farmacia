using PortalEstudos.API.Models;
using System.Text.RegularExpressions;

namespace PortalEstudos.API.Services;

public class PremiumApostilaHtmlService
{
    private readonly Dictionary<int, (string color, string icon)> _disciplineThemes = new()
    {
        { 1, ("#3B82F6", "💊") },      // Farmacologia Geral - Azul
        { 2, ("#10B981", "🏥") },      // Farmacologia Clínica - Verde
        { 3, ("#059669", "🌿") },      // Farmacognosia - Verde Escuro
        { 4, ("#8B5CF6", "⚗️") },      // Farmacotécnica - Roxo
        { 5, ("#EC4899", "🏭") },      // Tecnologia Farmacêutica - Rosa
        { 6, ("#F59E0B", "🎓") },      // Educação - Âmbar
        { 7, ("#EF4444", "📊") },      // Análise - Vermelho
        { 8, ("#06B6D4", "🧪") },      // Química - Cyan
        { 9, ("#A855F7", "🧬") },      // Biologia - Lilás
        { 10, ("#14B8A6", "💪") },     // Saúde - Verde Teal
    };

    public string GeneratePremiumApostilaHtml(Document doc, Topic topic)
    {
        var (themeColor, themeIcon) = _disciplineThemes.GetValueOrDefault(topic.Id, ("#3B82F6", "📚"));
        var conteudo = CleanHtmlContent(doc.Conteudo ?? "");
        var estimatedReadTime = Math.Max(5, conteudo.Length / 150);

        return $@"<!DOCTYPE html>
<html lang=""pt-BR"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>{Escape(doc.Titulo)} - {Escape(topic.Nome)}</title>
    <link rel=""preconnect"" href=""https://fonts.googleapis.com"">
    <link rel=""preconnect"" href=""https://fonts.gstatic.com"" crossorigin>
    <link href=""https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700;800&family=JetBrains+Mono:wght@400;600&display=swap"" rel=""stylesheet"">
    <style>
        {GenerateGlobalStyles()}
        {GenerateAnimations()}
        {GenerateThemeStyles(themeColor)}
        {GenerateComponentStyles()}
    </style>
</head>
<body>
    <div class=""progress-bar"" id=""progressBar""></div>
    
    {GenerateHeader(doc, topic, themeIcon, themeColor)}
    
    <div class=""layout"">
        {GenerateSidebar(doc, topic)}
        
        <main class=""main-content"">
            {GenerateContentBody(doc, conteudo, estimatedReadTime)}
        </main>
    </div>
    
    {GenerateFooter(topic)}
    
    <script>
        {GenerateProgressTracking()}
        {GenerateSmoothScroll()}
        {GenerateAnimationTriggers()}
    </script>
</body>
</html>";
    }

    private string GenerateGlobalStyles()
    {
        return @"
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        :root {
            --primary-color: #3B82F6;
            --dark-bg: #0F172A;
            --card-bg: #1E293B;
            --border-color: #334155;
            --text-primary: #F1F5F9;
            --text-secondary: #CBD5E1;
            --text-muted: #94A3B8;
        }

        html {
            scroll-behavior: smooth;
        }

        body {
            font-family: 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;
            background: linear-gradient(135deg, #0F172A 0%, #1A1F3A 100%);
            color: var(--text-primary);
            line-height: 1.8;
            font-size: 16px;
            overflow-x: hidden;
        }

        .progress-bar {
            position: fixed;
            top: 0;
            left: 0;
            height: 4px;
            background: linear-gradient(to right, var(--primary-color), #A855F7);
            width: 0%;
            z-index: 1000;
            transition: width 0.3s cubic-bezier(0.33, 0.66, 0.66, 1);
        }

        /* Scrollbar */
        ::-webkit-scrollbar {
            width: 10px;
        }

        ::-webkit-scrollbar-track {
            background: rgba(30, 41, 59, 0.5);
        }

        ::-webkit-scrollbar-thumb {
            background: var(--primary-color);
            border-radius: 5px;
        }

        ::-webkit-scrollbar-thumb:hover {
            background: #A855F7;
        }

        strong {
            color: var(--primary-color);
            font-weight: 600;
        }

        em {
            color: var(--text-secondary);
            font-style: italic;
        }

        code {
            background: rgba(59, 130, 246, 0.1);
            padding: 0.2em 0.5em;
            border-radius: 4px;
            font-family: 'JetBrains Mono', monospace;
            font-size: 0.9em;
            color: #7DD3FC;
        }

        pre {
            background: #0F172A;
            padding: 1.5rem;
            border-radius: 8px;
            overflow-x: auto;
            border: 1px solid var(--border-color);
            margin: 1.5rem 0;
        }

        pre code {
            background: none;
            padding: 0;
            color: inherit;
        }

        @media (max-width: 768px) {
            body {
                font-size: 15px;
            }
        }
        ";
    }

    private string GenerateAnimations()
    {
        return @"
        @keyframes fadeInUp {
            from {
                opacity: 0;
                transform: translateY(20px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        @keyframes slideInLeft {
            from {
                opacity: 0;
                transform: translateX(-30px);
            }
            to {
                opacity: 1;
                transform: translateX(0);
            }
        }

        @keyframes slideInRight {
            from {
                opacity: 0;
                transform: translateX(30px);
            }
            to {
                opacity: 1;
                transform: translateX(0);
            }
        }

        @keyframes pulse {
            0%, 100% { opacity: 1; }
            50% { opacity: 0.5; }
        }

        @keyframes shimmer {
            0% { background-position: -1000px 0; }
            100% { background-position: 1000px 0; }
        }

        @keyframes glow {
            0%, 100% { box-shadow: 0 0 5px rgba(59, 130, 246, 0.3); }
            50% { box-shadow: 0 0 20px rgba(59, 130, 246, 0.6); }
        }

        .animate-fade-in {
            animation: fadeInUp 0.6s ease-out forwards;
        }

        .animate-slide-left {
            animation: slideInLeft 0.6s ease-out forwards;
        }

        .animate-slide-right {
            animation: slideInRight 0.6s ease-out forwards;
        }

        .animate-on-scroll {
            opacity: 0;
            animation: fadeInUp 0.8s ease-out forwards;
        }
        ";
    }

    private string GenerateThemeStyles(string themeColor)
    {
        return $@"
        :root {{
            --primary-color: {themeColor};
        }}

        .theme-accent {{
            color: {themeColor};
        }}

        .theme-bg {{
            background: {themeColor};
        }}

        .theme-border {{
            border-color: {themeColor};
        }}

        .badge {{
            background: {themeColor}20;
            color: {themeColor};
            border: 1px solid {themeColor}40;
        }}
        ";
    }

    private string GenerateComponentStyles()
    {
        return @"
        /* Header */
        .header {
            background: linear-gradient(135deg, rgba(30, 41, 59, 0.8), rgba(15, 23, 42, 0.8));
            backdrop-filter: blur(10px);
            border-bottom: 1px solid var(--border-color);
            padding: 1.5rem 0;
            position: sticky;
            top: 0;
            z-index: 100;
            animation: slideInLeft 0.5s ease-out;
        }

        .header-container {
            max-width: 1400px;
            margin: 0 auto;
            padding: 0 2rem;
            display: flex;
            justify-content: space-between;
            align-items: center;
            gap: 2rem;
        }

        .header-left {
            flex: 1;
            display: flex;
            align-items: center;
            gap: 1rem;
        }

        .header-icon {
            font-size: 2rem;
            animation: pulse 2s infinite;
        }

        .header-title {
            flex: 1;
        }

        .header-title h1 {
            font-size: 1.5rem;
            font-weight: 700;
            margin-bottom: 0.25rem;
        }

        .header-breadcrumb {
            font-size: 0.875rem;
            color: var(--text-muted);
        }

        .header-meta {
            display: flex;
            gap: 1rem;
            font-size: 0.875rem;
            color: var(--text-secondary);
        }

        /* Layout */
        .layout {
            display: grid;
            grid-template-columns: 280px 1fr;
            max-width: 1400px;
            margin: 0 auto;
            gap: 2rem;
            padding: 2rem;
            min-height: calc(100vh - 150px);
        }

        .main-content {
            animation: fadeInUp 0.8s ease-out;
        }

        /* Sidebar */
        .sidebar {
            position: sticky;
            top: 100px;
            height: fit-content;
            animation: slideInLeft 0.6s ease-out;
        }

        .sidebar-section {
            background: var(--card-bg);
            border: 1px solid var(--border-color);
            border-radius: 10px;
            padding: 1.5rem;
            margin-bottom: 1.5rem;
            transition: all 0.3s ease;
        }

        .sidebar-section:hover {
            border-color: var(--primary-color);
            box-shadow: 0 10px 30px rgba(59, 130, 246, 0.1);
        }

        .sidebar-title {
            font-size: 0.875rem;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 0.1em;
            color: var(--text-muted);
            margin-bottom: 1rem;
        }

        .sidebar-item {
            font-size: 0.9rem;
            padding: 0.75rem 0;
            border-bottom: 1px solid rgba(51, 65, 85, 0.3);
            display: flex;
            justify-content: space-between;
            align-items: center;
            transition: all 0.2s ease;
        }

        .sidebar-item:last-child {
            border-bottom: none;
        }

        .sidebar-item:hover {
            color: var(--primary-color);
            padding-left: 0.5rem;
        }

        /* Content Sections */
        .content-section {
            background: var(--card-bg);
            border: 1px solid var(--border-color);
            border-radius: 12px;
            padding: 2rem;
            margin-bottom: 2rem;
            transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
            animation: fadeInUp 0.8s ease-out both;
        }

        .content-section:hover {
            border-color: var(--primary-color);
            box-shadow: 0 20px 50px rgba(59, 130, 246, 0.1);
            transform: translateY(-2px);
        }

        .content-section h2 {
            font-size: 2rem;
            font-weight: 700;
            margin-bottom: 1rem;
            color: var(--primary-color);
            display: flex;
            align-items: center;
            gap: 0.75rem;
        }

        .content-section h2::before {
            content: '';
            display: inline-block;
            width: 4px;
            height: 32px;
            background: linear-gradient(to bottom, var(--primary-color), #A855F7);
            border-radius: 2px;
        }

        .content-section h3 {
            font-size: 1.5rem;
            font-weight: 600;
            margin: 2rem 0 1rem;
            color: var(--text-primary);
        }

        .content-section p {
            margin-bottom: 1.5rem;
            line-height: 1.9;
            color: var(--text-secondary);
        }

        .content-section ul, .content-section ol {
            margin-left: 2rem;
            margin-bottom: 1.5rem;
        }

        .content-section li {
            margin-bottom: 0.75rem;
            line-height: 1.8;
        }

        /* Callout/Alert Boxes */
        .callout {
            background: rgba(59, 130, 246, 0.05);
            border-left: 4px solid var(--primary-color);
            padding: 1.5rem;
            border-radius: 8px;
            margin: 1.5rem 0;
            animation: slideInRight 0.5s ease-out;
        }

        .callout.info {
            border-left-color: #3B82F6;
            background: rgba(59, 130, 246, 0.05);
        }

        .callout.warning {
            border-left-color: #F59E0B;
            background: rgba(245, 158, 11, 0.05);
        }

        .callout.success {
            border-left-color: #10B981;
            background: rgba(16, 185, 129, 0.05);
        }

        .callout-title {
            font-weight: 600;
            margin-bottom: 0.5rem;
            color: var(--text-primary);
        }

        .callout p {
            margin-bottom: 0;
            font-size: 0.95rem;
        }

        /* Tables */
        table {
            width: 100%;
            border-collapse: collapse;
            margin: 1.5rem 0;
            font-size: 0.95rem;
            overflow: hidden;
            border-radius: 8px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2);
        }

        thead {
            background: linear-gradient(to right, var(--primary-color), #A855F7);
            color: white;
        }

        th {
            padding: 1rem;
            text-align: left;
            font-weight: 600;
            text-transform: uppercase;
            font-size: 0.85rem;
            letter-spacing: 0.05em;
        }

        td {
            padding: 1rem;
            border-bottom: 1px solid var(--border-color);
            background: rgba(0, 0, 0, 0.2);
        }

        tbody tr:last-child td {
            border-bottom: none;
        }

        tbody tr:hover td {
            background: rgba(59, 130, 246, 0.05);
        }

        /* Buttons */
        .btn {
            display: inline-flex;
            align-items: center;
            gap: 0.5rem;
            padding: 0.75rem 1.5rem;
            border-radius: 8px;
            border: none;
            cursor: pointer;
            font-weight: 500;
            transition: all 0.3s ease;
            font-size: 0.95rem;
        }

        .btn-primary {
            background: var(--primary-color);
            color: white;
        }

        .btn-primary:hover {
            transform: translateY(-2px);
            box-shadow: 0 10px 25px rgba(59, 130, 246, 0.3);
        }

        .btn-secondary {
            background: transparent;
            color: var(--primary-color);
            border: 1px solid var(--primary-color);
        }

        .btn-secondary:hover {
            background: var(--primary-color);
            color: white;
        }

        /* Footer */
        .footer {
            background: linear-gradient(135deg, rgba(30, 41, 59, 0.8), rgba(15, 23, 42, 0.8));
            border-top: 1px solid var(--border-color);
            padding: 3rem 2rem;
            margin-top: 4rem;
            text-align: center;
            animation: slideInLeft 0.5s ease-out;
        }

        .footer-content {
            max-width: 1400px;
            margin: 0 auto;
        }

        .footer-links {
            display: flex;
            justify-content: center;
            gap: 2rem;
            margin-bottom: 1.5rem;
            flex-wrap: wrap;
            font-size: 0.9rem;
        }

        .footer-links a {
            color: var(--primary-color);
            text-decoration: none;
            transition: all 0.3s ease;
        }

        .footer-links a:hover {
            color: #A855F7;
            text-decoration: underline;
        }

        .footer-divider {
            height: 1px;
            background: var(--border-color);
            margin: 1.5rem 0;
        }

        .footer-text {
            font-size: 0.85rem;
            color: var(--text-muted);
        }

        /* Badges */
        .badge {
            display: inline-block;
            padding: 0.4rem 0.8rem;
            border-radius: 6px;
            font-size: 0.8rem;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 0.05em;
        }

        /* Read Time */
        .read-time {
            display: flex;
            align-items: center;
            gap: 0.5rem;
            font-size: 0.875rem;
            color: var(--text-secondary);
            margin: 1.5rem 0;
            padding: 1rem;
            background: rgba(59, 130, 246, 0.05);
            border-radius: 8px;
            border-left: 3px solid var(--primary-color);
        }

        /* Responsive */
        @media (max-width: 1024px) {
            .layout {
                grid-template-columns: 1fr;
                gap: 1.5rem;
                padding: 1.5rem;
            }

            .sidebar {
                position: relative;
                top: 0;
            }
        }

        @media (max-width: 768px) {
            .header-container {
                flex-direction: column;
                align-items: flex-start;
                gap: 1rem;
            }

            .header-meta {
                flex-direction: column;
                gap: 0.5rem;
            }

            .content-section {
                padding: 1.5rem 1rem;
            }

            .content-section h2 {
                font-size: 1.5rem;
            }

            table {
                font-size: 0.85rem;
            }

            th, td {
                padding: 0.75rem;
            }

            .footer-links {
                gap: 1rem;
                font-size: 0.85rem;
            }
        }
        ";
    }

    private string GenerateHeader(Document doc, Topic topic, string icon, string color)
    {
        return $@"
    <header class=""header"">
        <div class=""header-container"">
            <div class=""header-left"">
                <div class=""header-icon theme-accent"">{icon}</div>
                <div class=""header-title"">
                    <h1>{Escape(doc.Titulo)}</h1>
                    <div class=""header-breadcrumb"">{Escape(topic.Nome)} / {Escape(topic.Categoria)}</div>
                </div>
            </div>
            <div class=""header-meta"">
                <span>📖 Leitura</span>
                <span>🕐 ~{doc.LeituraMinutos} min</span>
                <span>📅 {DateTime.Now:dd/MM/yyyy}</span>
            </div>
        </div>
    </header>";
    }

    private string GenerateSidebar(Document doc, Topic topic)
    {
        return $@"
    <aside class=""sidebar"">
        <div class=""sidebar-section"">
            <div class=""sidebar-title"">📚 Disciplina</div>
            <div style=""color: var(--text-secondary);"">{Escape(topic.Nome)}</div>
        </div>

        <div class=""sidebar-section"">
            <div class=""sidebar-title"">📊 Nível</div>
            <div class=""badge theme-bg"" style=""color: white;"">{Escape(doc.Dificuldade)}</div>
        </div>

        <div class=""sidebar-section"">
            <div class=""sidebar-title"">📑 Seções</div>
            <div class=""sidebar-item"">
                <span>Introdução</span>
                <span style=""color: var(--primary-color);"">✓</span>
            </div>
            <div class=""sidebar-item"">
                <span>Conceitos</span>
                <span style=""color: var(--primary-color);"">✓</span>
            </div>
            <div class=""sidebar-item"">
                <span>Exemplos</span>
                <span style=""color: var(--primary-color);"">✓</span>
            </div>
            <div class=""sidebar-item"">
                <span>Conclusão</span>
                <span style=""color: var(--primary-color);"">✓</span>
            </div>
        </div>

        <div class=""sidebar-section"">
            <div class=""sidebar-title"">⚙️ Ações</div>
            <button class=""btn btn-primary"" style=""width: 100%; margin-bottom: 0.5rem;"" onclick=""window.print()"">
                🖨️ Imprimir
            </button>
            <button class=""btn btn-secondary"" style=""width: 100%;"" onclick=""shareContent()"">
                📤 Compartilhar
            </button>
        </div>
    </aside>";
    }

    private string GenerateContentBody(Document doc, string conteudo, int readTime)
    {
        return $@"
        <div class=""read-time"">
            <span>⏱️ Tempo estimado de leitura: {readTime} minutos</span>
        </div>

        <article>
            {conteudo}
        </article>";
    }

    private string GenerateFooter(Topic topic)
    {
        return $@"
    <footer class=""footer"">
        <div class=""footer-content"">
            <div class=""footer-links"">
                <a href=""#"">📚 Voltar aos Estudos</a>
                <a href=""#"">❓ Questões</a>
                <a href=""#"">💬 Fórum</a>
                <a href=""#"">⭐ Favoritar</a>
            </div>
            <div class=""footer-divider""></div>
            <div class=""footer-text"">
                <p><strong>Portal Estudos</strong> • Conteúdo acadêmico de qualidade para sua formação profissional</p>
                <p>Disciplina: {Escape(topic.Nome)} | Categoria: {Escape(topic.Categoria)}</p>
                <p style=""margin-top: 1rem; opacity: 0.7;"">© 2026 Portal Estudos - Todos os direitos reservados</p>
            </div>
        </div>
    </footer>";
    }

    private string GenerateProgressTracking()
    {
        return @"
        function updateProgressBar() {
            const visibleHeight = document.documentElement.clientHeight;
            const totalHeight = document.documentElement.scrollHeight - visibleHeight;
            const scrollProgress = (window.scrollY / totalHeight) * 100;
            document.getElementById('progressBar').style.width = scrollProgress + '%';
        }

        window.addEventListener('scroll', updateProgressBar);
        updateProgressBar();";
    }

    private string GenerateSmoothScroll()
    {
        return @"
        document.querySelectorAll('a[href^=""#""]').forEach(anchor => {
            anchor.addEventListener('click', function (e) {
                e.preventDefault();
                const target = document.querySelector(this.getAttribute('href'));
                if (target) {
                    target.scrollIntoView({ behavior: 'smooth' });
                }
            });
        });";
    }

    private string GenerateAnimationTriggers()
    {
        return @"
        function triggerAnimationsOnScroll() {
            const elements = document.querySelectorAll('.content-section');
            const observer = new IntersectionObserver((entries) => {
                entries.forEach(entry => {
                    if (entry.isIntersecting) {
                        entry.target.classList.add('animate-on-scroll');
                        observer.unobserve(entry.target);
                    }
                });
            }, { threshold: 0.1 });

            elements.forEach(el => observer.observe(el));
        }

        function shareContent() {
            if (navigator.share) {
                navigator.share({
                    title: document.title,
                    text: 'Confira esta apostila no Portal Estudos',
                    url: window.location.href
                });
            } else {
                alert('Copiar link: ' + window.location.href);
            }
        }

        document.addEventListener('DOMContentLoaded', triggerAnimationsOnScroll);";
    }

    private string CleanHtmlContent(string html)
    {
        // Remove script tags and keep only safe HTML
        html = Regex.Replace(html, @"<script.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        html = Regex.Replace(html, @"on\w+\s*=", "", RegexOptions.IgnoreCase);
        return html;
    }

    private static string Escape(string text) => System.Web.HttpUtility.HtmlEncode(text ?? "");
}
