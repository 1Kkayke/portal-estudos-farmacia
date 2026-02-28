using System.Net;
using System.Text.RegularExpressions;
using DocModel = PortalEstudos.API.Models.Document;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PortalEstudos.API.Services;

public sealed class DocumentPdfService
{
    public byte[] BuildPdf(DocModel doc, string topicName, string topicCategoria = "")
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var resumo = CleanText(doc.Resumo);
        var sections = ExtractSections(doc.Conteudo);
        var bullets = ExtractListItems(doc.Conteudo).Take(6).ToList();

        if (sections.Count == 0)
        {
            sections.Add(("Introdução", CleanText(doc.Conteudo)));
        }

        if (bullets.Count == 0)
        {
            bullets = new List<string>
            {
                "Compreender conceitos fundamentais do tema",
                "Relacionar teoria com aplicações na prática farmacêutica",
                "Resolver questões com raciocínio técnico",
                "Revisar pontos críticos antes da prova"
            };
        }

        var allText = string.Join("\n\n", sections.Select(s => $"{s.Title}\n{s.Body}"));
        var chunks = SplitText(allText, 3200);
        var primaryColor = GetCategoryColor(topicCategoria);

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(36);
                page.DefaultTextStyle(x => x.FontSize(11).FontColor(Colors.Black));

                page.Header().Column(col =>
                {
                    col.Spacing(4);
                    col.Item().Text("Portal Estudos").FontSize(11).SemiBold().FontColor(primaryColor);
                    col.Item().Text(topicName).FontSize(13).SemiBold();
                    col.Item().Text(doc.Titulo).FontSize(22).Bold();
                    col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                });

                page.Content().PaddingTop(10).Column(col =>
                {
                    col.Spacing(10);
                    col.Item().Text("Resumo").FontSize(14).SemiBold().FontColor(primaryColor);
                    col.Item().Text(string.IsNullOrWhiteSpace(resumo) ? "Material de estudo dirigido para apoio acadêmico." : resumo).LineHeight(1.5f);

                    col.Item().Text("Objetivos de aprendizagem").FontSize(14).SemiBold().FontColor(primaryColor);
                    col.Item().Column(list =>
                    {
                        list.Spacing(3);
                        foreach (var item in bullets)
                            list.Item().Text($"• {item}").LineHeight(1.4f);
                    });

                    col.Item().Text("Seções incluídas neste PDF").FontSize(14).SemiBold().FontColor(primaryColor);
                    col.Item().Column(list =>
                    {
                        list.Spacing(2);
                        foreach (var section in sections)
                            list.Item().Text($"• {section.Title}");
                    });
                });

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Portal Estudos | ");
                    text.CurrentPageNumber();
                    text.Span("/");
                    text.TotalPages();
                });
            });

            for (var i = 0; i < chunks.Count; i++)
            {
                var chunk = chunks[i];

                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(36);
                    page.DefaultTextStyle(x => x.FontSize(11).FontColor(Colors.Black));

                    page.Header().Column(col =>
                    {
                        col.Spacing(4);
                        col.Item().Text(topicName).FontSize(12).SemiBold().FontColor(primaryColor);
                        col.Item().Text(doc.Titulo).FontSize(16).SemiBold();
                        col.Item().Text($"Conteúdo acadêmico — Parte {i + 1}").FontSize(10).FontColor(Colors.Grey.Darken1);
                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                    });

                    page.Content().PaddingTop(10).Text(chunk).LineHeight(1.5f);

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Portal Estudos | ");
                        text.CurrentPageNumber();
                        text.Span("/");
                        text.TotalPages();
                    });
                });
            }
        }).GeneratePdf();
    }

    private static string GetCategoryColor(string categoria)
    {
        var normalized = (categoria ?? string.Empty).ToLowerInvariant();

        if (normalized.Contains("clinica") || normalized.Contains("clínica")) return Colors.Red.Darken1;
        if (normalized.Contains("farmacologia")) return Colors.Purple.Darken2;
        if (normalized.Contains("farmacotecnica") || normalized.Contains("farmacotécnica")) return Colors.Orange.Darken2;
        if (normalized.Contains("gestao") || normalized.Contains("gestão")) return Colors.Green.Darken2;
        if (normalized.Contains("saude") || normalized.Contains("saúde")) return Colors.Teal.Darken2;

        return Colors.Blue.Darken2;
    }

    private static string CleanText(string? html)
    {
        if (string.IsNullOrWhiteSpace(html)) return string.Empty;

        var withLineBreaks = Regex.Replace(html, "</(p|h2|h3|li|tr|section|article)>", "\n", RegexOptions.IgnoreCase);
        var noTags = Regex.Replace(withLineBreaks, "<.*?>", " ");
        var decoded = WebUtility.HtmlDecode(noTags);
        var normalizedLines = decoded
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => Regex.Replace(line, "\\s+", " ").Trim())
            .Where(line => !string.IsNullOrWhiteSpace(line));

        return string.Join("\n", normalizedLines);
    }

    private static List<(string Title, string Body)> ExtractSections(string? html)
    {
        var sections = new List<(string Title, string Body)>();

        if (string.IsNullOrWhiteSpace(html))
            return sections;

        var matches = Regex.Matches(
            html,
            "<h3>(.*?)</h3>(.*?)(?=<h3>|$)",
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        foreach (Match match in matches)
        {
            var title = CleanText(match.Groups[1].Value);
            var body = CleanText(match.Groups[2].Value);

            if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(body))
                sections.Add((title, body));
        }

        return sections;
    }

    private static List<string> ExtractListItems(string? html)
    {
        var items = new List<string>();

        if (string.IsNullOrWhiteSpace(html))
            return items;

        foreach (Match m in Regex.Matches(html, "<li>(.*?)</li>", RegexOptions.IgnoreCase | RegexOptions.Singleline))
        {
            var text = CleanText(m.Groups[1].Value);
            if (!string.IsNullOrWhiteSpace(text))
                items.Add(text);
        }

        return items;
    }

    private static List<string> SplitText(string text, int maxLen)
    {
        var chunks = new List<string>();
        if (string.IsNullOrWhiteSpace(text))
            return chunks;

        var paragraphs = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var current = new List<string>();
        var currentLen = 0;

        foreach (var paragraph in paragraphs)
        {
            var p = paragraph.Trim();
            if (p.Length == 0)
                continue;

            if (currentLen + p.Length + 2 > maxLen && current.Count > 0)
            {
                chunks.Add(string.Join("\n\n", current));
                current.Clear();
                currentLen = 0;
            }

            current.Add(p);
            currentLen += p.Length + 2;
        }

        if (current.Count > 0)
            chunks.Add(string.Join("\n\n", current));

        return chunks;
    }
}