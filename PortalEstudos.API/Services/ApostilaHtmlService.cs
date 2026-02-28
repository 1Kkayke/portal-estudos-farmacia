using PortalEstudos.API.Models;

namespace PortalEstudos.API.Services;

public class ApostilaHtmlService
{
    public string GenerateApostilaHtml(Document doc, Topic topic)
    {
        var (titulo, resumo, conteudo) = (doc.Titulo, doc.Resumo, doc.Conteudo);
        
        return $@"<!DOCTYPE html>
<html lang=""pt-BR"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>{System.Web.HttpUtility.HtmlEncode(titulo)} - Portal Estudos</title>
    <style>
        * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }}

        :root {{
            --primary-color: #3B82F6;
            --secondary-color: #6366F1;
            --accent-color: #8B5CF6;
            --dark-bg: #0F172A;
            --card-bg: #1E293B;
            --border-color: #334155;
            --text-primary: #F1F5F9;
            --text-secondary: #CBD5E1;
            --success-color: #10B981;
            --warning-color: #F59E0B;
            --danger-color: #EF4444;
        }}

        body {{
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif;
            background: linear-gradient(135deg, #0F172A 0%, #1A1F3A 100%);
            color: var(--text-primary);
            line-height: 1.75;
            font-size: 16px;
        }}

        .container {{
            max-width: 900px;
            margin: 0 auto;
            padding: 2rem;
        }}

        /* Header */
        .header {{
            background: linear-gradient(135deg, var(--secondary-color), var(--accent-color));
            padding: 3rem 2rem;
            border-radius: 12px;
            margin-bottom: 3rem;
            border: 1px solid rgba(99, 102, 241, 0.3);
            box-shadow: 0 20px 50px rgba(0, 0, 0, 0.3);
        }}

        .header__badge {{
            display: inline-block;
            background: rgba(255, 255, 255, 0.2);
            color: #fff;
            padding: 0.5rem 1rem;
            border-radius: 6px;
            font-size: 0.875rem;
            margin-bottom: 1rem;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 0.05em;
        }}

        .header__title {{
            font-size: 2.5rem;
            font-weight: 700;
            margin-bottom: 1rem;
            color: #fff;
            line-height: 1.2;
        }}

        .header__subtitle {{
            font-size: 1.125rem;
            color: rgba(255, 255, 255, 0.9);
            margin-bottom: 1.5rem;
            font-style: italic;
        }}

        .header__meta {{
            display: flex;
            gap: 2rem;
            flex-wrap: wrap;
            font-size: 0.95rem;
            color: rgba(255, 255, 255, 0.85);
        }}

        .header__meta-item {{
            display: flex;
            align-items: center;
            gap: 0.5rem;
        }}

        /* Sections */
        .section {{
            background: var(--card-bg);
            border: 1px solid var(--border-color);
            border-radius: 10px;
            padding: 2rem;
            margin-bottom: 2rem;
            transition: all 0.3s ease;
        }}

        .section:hover {{
            border-color: var(--primary-color);
            box-shadow: 0 10px 30px rgba(59, 130, 246, 0.1);
        }}

        .section h3 {{
            color: var(--primary-color);
            font-size: 1.5rem;
            margin-bottom: 1.5rem;
            padding-bottom: 1rem;
            border-bottom: 3px solid var(--primary-color);
            display: flex;
            align-items: center;
            gap: 0.75rem;
        }}

        .section h3::before {{
            content: '';
            display: inline-block;
            width: 4px;
            height: 24px;
            background: linear-gradient(to bottom, var(--primary-color), var(--accent-color));
            border-radius: 2px;
        }}

        .section p {{
            margin-bottom: 1.5rem;
            line-height: 1.8;
        }}

        .section ul, .section ol {{
            margin-left: 2rem;
            margin-bottom: 1.5rem;
        }}

        .section li {{
            margin-bottom: 1rem;
            line-height: 1.8;
        }}

        .section li strong {{
            color: var(--primary-color);
        }}

        /* Table Styling */
        table {{
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 1.5rem;
            background: rgba(0, 0, 0, 0.3);
            border: 1px solid var(--border-color);
            border-radius: 8px;
            overflow: hidden;
        }}

        thead {{
            background: linear-gradient(to right, var(--primary-color), var(--secondary-color));
            color: white;
        }}

        th {{
            padding: 1rem;
            text-align: left;
            font-weight: 600;
            font-size: 0.95rem;
            text-transform: uppercase;
            letter-spacing: 0.05em;
        }}

        td {{
            padding: 1rem;
            border-bottom: 1px solid var(--border-color);
            color: var(--text-secondary);
        }}

        tbody tr:hover {{
            background: rgba(59, 130, 246, 0.1);
        }}

        tbody tr:last-child td {{
            border-bottom: none;
        }}

        /* Code and highlights */
        strong {{
            color: var(--primary-color);
            font-weight: 600;
        }}

        em {{
            color: var(--text-secondary);
            font-style: italic;
        }}

        /* Footer */
        .footer {{
            text-align: center;
            padding: 2rem;
            border-top: 1px solid var(--border-color);
            margin-top: 3rem;
            color: var(--text-secondary);
            font-size: 0.9rem;
        }}

        .footer a {{
            color: var(--primary-color);
            text-decoration: none;
            transition: all 0.3s ease;
        }}

        .footer a:hover {{
            text-decoration: underline;
            color: var(--accent-color);
        }}

        /* Print styles */
        @media print {{
            body {{
                background: white;
                color: #000;
            }}

            .header {{
                page-break-after: always;
                background: #f3f4f6;
                color: #000;
            }}

            .section {{
                page-break-inside: avoid;
                background: white;
                color: #000;
                border: 1px solid #ddd;
            }}

            .section h3 {{
                color: #1F2937;
                border-bottom-color: #1F2937;
            }}

            strong {{
                color: #1F2937;
            }}
        }}

        /* Responsive */
        @media (max-width: 768px) {{
            .container {{
                padding: 1rem;
            }}

            .header {{
                padding: 2rem 1.5rem;
            }}

            .header__title {{
                font-size: 1.75rem;
            }}

            .header__meta {{
                flex-direction: column;
                gap: 1rem;
            }}

            .section {{
                padding: 1.5rem 1rem;
            }}

            th, td {{
                padding: 0.75rem;
                font-size: 0.9rem;
            }}
        }}
    </style>
</head>
<body>
    <main class=""container"">
        <header class=""header"">
            <div class=""header__badge"">Portal Estudos</div>
            <h1 class=""header__title"">{System.Web.HttpUtility.HtmlEncode(titulo)}</h1>
            <p class=""header__subtitle"">{System.Web.HttpUtility.HtmlEncode(resumo)}</p>
            <div class=""header__meta"">
                <div class=""header__meta-item"">
                    <span>📚</span>
                    <strong>{System.Web.HttpUtility.HtmlEncode(topic.Nome)}</strong>
                </div>
                <div class=""header__meta-item"">
                    <span>🏷️</span>
                    <span>{System.Web.HttpUtility.HtmlEncode(topic.Categoria)}</span>
                </div>
                <div class=""header__meta-item"">
                    <span>📅</span>
                    <span>{DateTime.Now:dd/MM/yyyy}</span>
                </div>
            </div>
        </header>

        {conteudo}

        <footer class=""footer"">
            <p>Apostila gerada pelo <strong>Portal Estudos</strong> • Conteúdo acadêmico para formação profissional em Farmácia</p>
            <p style=""margin-top: 1rem; color: var(--text-secondary);"">© 2026 • Todos os direitos reservados</p>
        </footer>
    </main>
</body>
</html>";
    }
}
