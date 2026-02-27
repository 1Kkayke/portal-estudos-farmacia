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
        var conteudo = CleanText(doc.Conteudo);
        var bullets = ExtractListItems(doc.Conteudo).Take(8).ToList();
        if (bullets.Count == 0)
        {
            bullets = new List<string>
            {
                "Compreender conceitos basicos e definicoes",
                "Relacionar teoria com pratica academica",
                "Aplicar em situacoes clinicas e laboratoriais",
                "Revisar pontos cobrados em prova"
            };
        }

        var contentChunks = SplitText(conteudo, 1400);
        var (primaryColor, accentColor) = GetCategoryColors(topicCategoria);

        return Document.Create(container =>
        {
            // ===== CAPA =====
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(0);
                page.PageColor(primaryColor);
                page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial").FontColor(Colors.White));

                page.Content().Column(col =>
                {
                    col.Spacing(0);
                    
                    // Topo
                    col.Item().Height(150).Background(accentColor).AlignMiddle().AlignCenter().Column(top =>
                    {
                        top.Item().Text("PORTAL ESTUDOS").FontSize(28).Bold();
                        top.Item().Text("Apostila Academica").FontSize(16);
                    });

                    col.Item().PaddingVertical(60).PaddingHorizontal(40).Column(middle =>
                    {
                        middle.Spacing(15);
                        middle.Item().Text(topicCategoria.ToUpper()).FontSize(14).Bold().FontColor(Colors.Yellow.Lighten3);
                        middle.Item().Text(topicName).FontSize(32).Bold();
                        middle.Item().LineHorizontal(3).LineColor(Colors.White);
                        middle.Item().PaddingTop(20).Text(doc.Titulo).FontSize(22).SemiBold();
                        middle.Item().PaddingTop(10).Text(resumo.Length > 200 ? resumo.Substring(0, 200) + "..." : resumo).FontSize(12).LineHeight(1.5f);
                    });

                    col.Item().AlignBottom().Height(100).Background(accentColor).PaddingHorizontal(40).AlignMiddle().Column(bottom =>
                    {
                        bottom.Item().Text($"Documento {doc.Id} | {DateTime.Now.Year}").FontSize(10);
                        bottom.Item().Text("Material de apoio academico para estudo e revisao").FontSize(9).Italic();
                    });
                });
            });

            // ===== SUMARIO =====
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

                page.Header().Column(col =>
                {
                    col.Item().Text("SUMARIO").FontSize(24).Bold().FontColor(primaryColor);
                    col.Item().PaddingTop(5).LineHorizontal(2).LineColor(primaryColor);
                });

                page.Content().PaddingTop(20).Column(col =>
                {
                    col.Spacing(12);
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Text("1. Introducao e Resumo").FontSize(14);
                        row.ConstantItem(40).Text("3").AlignRight();
                    });
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Text("2. Objetivos de Aprendizagem").FontSize(14);
                        row.ConstantItem(40).Text("3").AlignRight();
                    });
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Text("3. Quadro de Referencia").FontSize(14);
                        row.ConstantItem(40).Text("3").AlignRight();
                    });
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Text("4. Diagrama Conceitual").FontSize(14);
                        row.ConstantItem(40).Text("4").AlignRight();
                    });
                    
                    for (int i = 0; i < contentChunks.Count; i++)
                    {
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text($"5.{i+1}. Conteudo - Parte {i+1}").FontSize(14);
                            row.ConstantItem(40).Text($"{5+i}").AlignRight();
                        });
                    }
                    
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Text("6. Pontos-chave para Revisao").FontSize(14);
                        row.ConstantItem(40).Text($"{5+contentChunks.Count}").AlignRight();
                    });
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Text("7. Leituras Recomendadas").FontSize(14);
                        row.ConstantItem(40).Text($"{5+contentChunks.Count}").AlignRight();
                    });
                });

                page.Footer()
                    .AlignCenter()
                    .DefaultTextStyle(x => x.FontSize(9).FontColor(Colors.Grey.Medium))
                    .Text(text =>
                    {
                        text.Span("Portal Estudos | ");
                        text.CurrentPageNumber();
                    });
            });

            // ===== PAGINA DE INTRODUCAO =====
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

                page.Header().Column(col =>
                {
                    col.Item().Text("Portal Estudos").FontSize(10).FontColor(Colors.Grey.Medium);
                    col.Item().Text("Apostila Academica").FontSize(12).FontColor(Colors.Grey.Darken1);
                    col.Item().Text(topicName).FontSize(16).FontColor(primaryColor);
                    col.Item().Text(doc.Titulo).FontSize(26).Bold().FontColor(Colors.Black);
                    col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                });

                page.Content().Column(col =>
                {
                    col.Spacing(8);
                    col.Item().Text("Resumo").FontSize(14).Bold().FontColor(primaryColor);
                    col.Item().Text(resumo);

                    col.Item().PaddingTop(10).Text("Objetivos de aprendizagem").FontSize(14).Bold().FontColor(primaryColor);
                    col.Item().Column(list =>
                    {
                        foreach (var b in bullets.Take(6))
                            list.Item().Text($"• {b}").LineHeight(1.5f);
                    });

                    col.Item().PaddingTop(10).Text("Quadro de referencia").FontSize(14).Bold().FontColor(primaryColor);
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background(primaryColor).Padding(5).Text("Topico").SemiBold().FontColor(Colors.White);
                            header.Cell().Background(primaryColor).Padding(5).Text("Resumo").SemiBold().FontColor(Colors.White);
                        });

                        table.Cell().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text("Definicoes");
                        table.Cell().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text("Termos essenciais e conceito operacional");
                        table.Cell().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text("Classificacoes");
                        table.Cell().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text("Principais grupos e criterios");
                        table.Cell().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text("Aplicacoes");
                        table.Cell().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text("Uso clinico e correlacoes praticas");
                    });
                });

                page.Footer()
                    .AlignCenter()
                    .DefaultTextStyle(x => x.FontSize(9).FontColor(Colors.Grey.Medium))
                    .Text(text =>
                    {
                        text.Span("Documento academico - Portal Estudos | ");
                        text.CurrentPageNumber();
                        text.Span("/");
                        text.TotalPages();
                    });
            });

            // ===== DIAGRAMA CONCEITUAL =====
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

                page.Header().Column(col =>
                {
                    col.Item().Text(topicName).FontSize(14).FontColor(primaryColor);
                    col.Item().Text("Diagrama Conceitual").FontSize(20).Bold();
                    col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                });

                page.Content().PaddingTop(20).Column(col =>
                {
                    col.Spacing(15);
                    
                    // Estrutura visual hierarquica
                    col.Item().AlignCenter().Column(diagram =>
                    {
                        diagram.Spacing(10);
                        
                        // Nivel 1 - Conceito principal
                        diagram.Item().AlignCenter().Width(300).Height(60).Background(primaryColor).Padding(10).AlignMiddle().Text(doc.Titulo.ToUpper()).FontSize(14).Bold().FontColor(Colors.White).AlignCenter();
                        
                        diagram.Item().AlignCenter().Text("↓").FontSize(24).FontColor(primaryColor);
                        
                        // Nivel 2 - Conceitos secundarios
                        diagram.Item().Row(row =>
                        {
                            row.Spacing(20);
                            row.RelativeItem().Height(50).Background(accentColor).Padding(8).AlignMiddle().Text("DEFINICOES\nE CONCEITOS").FontSize(11).Bold().FontColor(Colors.White).AlignCenter();
                            row.RelativeItem().Height(50).Background(accentColor).Padding(8).AlignMiddle().Text("CLASSIFICACOES\nE TIPOS").FontSize(11).Bold().FontColor(Colors.White).AlignCenter();
                            row.RelativeItem().Height(50).Background(accentColor).Padding(8).AlignMiddle().Text("MECANISMOS\nE PROCESSOS").FontSize(11).Bold().FontColor(Colors.White).AlignCenter();
                        });
                        
                        diagram.Item().AlignCenter().Text("↓").FontSize(24).FontColor(primaryColor);
                        
                        // Nivel 3 - Aplicacoes
                        diagram.Item().Row(row =>
                        {
                            row.Spacing(15);
                            row.RelativeItem().Height(45).Background(Colors.Grey.Lighten1).Padding(8).AlignMiddle().Text("Aplicacao\nClinica").FontSize(10).SemiBold().AlignCenter();
                            row.RelativeItem().Height(45).Background(Colors.Grey.Lighten1).Padding(8).AlignMiddle().Text("Aplicacao\nLaboratorial").FontSize(10).SemiBold().AlignCenter();
                            row.RelativeItem().Height(45).Background(Colors.Grey.Lighten1).Padding(8).AlignMiddle().Text("Aplicacao\nFarmaceutica").FontSize(10).SemiBold().AlignCenter();
                            row.RelativeItem().Height(45).Background(Colors.Grey.Lighten1).Padding(8).AlignMiddle().Text("Aplicacao\nPratica").FontSize(10).SemiBold().AlignCenter();
                        });
                    });
                    
                    col.Item().PaddingTop(30).Column(legend =>
                    {
                        legend.Spacing(8);
                        legend.Item().Text("Legenda do Diagrama").FontSize(13).Bold().FontColor(primaryColor);
                        legend.Item().Row(row =>
                        {
                            row.ConstantItem(15).Height(15).Background(primaryColor);
                            row.ConstantItem(10);
                            row.RelativeItem().Text("Conceito principal e tema central do documento").FontSize(11);
                        });
                        legend.Item().Row(row =>
                        {
                            row.ConstantItem(15).Height(15).Background(accentColor);
                            row.ConstantItem(10);
                            row.RelativeItem().Text("Conceitos secundarios e areas de conhecimento relacionadas").FontSize(11);
                        });
                        legend.Item().Row(row =>
                        {
                            row.ConstantItem(15).Height(15).Background(Colors.Grey.Lighten1);
                            row.ConstantItem(10);
                            row.RelativeItem().Text("Aplicacoes praticas em diferentes contextos farmaceuticos").FontSize(11);
                        });
                    });
                    
                    col.Item().PaddingTop(20).Text("Este diagrama representa a estrutura hierarquica do conhecimento, partindo do conceito principal, passando por areas tematicas relacionadas, ate chegar nas aplicacoes praticas no contexto farmaceutico e da saude.").FontSize(10).Italic().LineHeight(1.5f);
                });

                page.Footer()
                    .AlignCenter()
                    .DefaultTextStyle(x => x.FontSize(9).FontColor(Colors.Grey.Medium))
                    .Text(text =>
                    {
                        text.Span("Portal Estudos | ");
                        text.CurrentPageNumber();
                        text.Span("/");
                        text.TotalPages();
                    });
            });

            // ===== PAGINAS DE CONTEUDO =====
            for (var i = 0; i < contentChunks.Count; i++)
            {
                var chunk = contentChunks[i];
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("Portal Estudos").FontSize(10).FontColor(Colors.Grey.Medium);
                        col.Item().Text(topicName).FontSize(14).FontColor(primaryColor);
                        col.Item().Text(doc.Titulo).FontSize(18).Bold().FontColor(Colors.Black);
                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                    });

                    page.Content().Column(col =>
                    {
                        col.Spacing(8);
                        col.Item().Text($"Conteudo - Parte {i + 1}").FontSize(14).Bold().FontColor(primaryColor);
                        col.Item().Text(chunk).LineHeight(1.5f);
                    });

                    page.Footer()
                        .AlignCenter()
                        .DefaultTextStyle(x => x.FontSize(9).FontColor(Colors.Grey.Medium))
                        .Text(text =>
                        {
                            text.Span("Portal Estudos | ");
                            text.CurrentPageNumber();
                            text.Span("/");
                            text.TotalPages();
                        });
                });
            }

            // ===== PAGINA FINAL - REVISAO =====
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

                page.Header().Column(col =>
                {
                    col.Item().Text("Portal Estudos").FontSize(10).FontColor(Colors.Grey.Medium);
                    col.Item().Text(topicName).FontSize(14).FontColor(primaryColor);
                    col.Item().Text("Revisao e Referencias").FontSize(20).Bold().FontColor(primaryColor);
                    col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                });

                page.Content().PaddingTop(20).Column(col =>
                {
                    col.Spacing(15);
                    
                    col.Item().Text("Pontos-chave para revisao").FontSize(16).Bold().FontColor(primaryColor);
                    col.Item().Column(list =>
                    {
                        list.Spacing(6);
                        foreach (var b in bullets.Take(6))
                            list.Item().Text($"• {b}").LineHeight(1.5f);
                    });

                    col.Item().PaddingTop(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                    
                    col.Item().PaddingTop(10).Text("Leituras recomendadas").FontSize(16).Bold().FontColor(primaryColor);
                    col.Item().Column(readings =>
                    {
                        readings.Spacing(6);
                        readings.Item().Text("• Diretrizes e protocolos oficiais da area farmaceutica").LineHeight(1.5f);
                        readings.Item().Text("• Livros-texto base da disciplina e referencias bibliograficas").LineHeight(1.5f);
                        readings.Item().Text("• Artigos de revisao e material complementar atualizado").LineHeight(1.5f);
                        readings.Item().Text("• Bases de dados cientificas: PubMed, Scielo, Cochrane").LineHeight(1.5f);
                    });

                    col.Item().PaddingTop(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                    
                    col.Item().PaddingTop(10).Text("Observacoes finais").FontSize(16).Bold().FontColor(primaryColor);
                    col.Item().Text("Este material foi elaborado com fins didaticos e de apoio ao estudo academico. Recomenda-se complementar a leitura com as referencias bibliograficas oficiais da disciplina e consultar sempre fontes atualizadas.").LineHeight(1.6f).Italic();
                });

                page.Footer()
                    .AlignCenter()
                    .DefaultTextStyle(x => x.FontSize(9).FontColor(Colors.Grey.Medium))
                    .Text(text =>
                    {
                        text.Span("Fim do documento - Portal Estudos | ");
                        text.CurrentPageNumber();
                        text.Span("/");
                        text.TotalPages();
                    });
            });
        }).GeneratePdf();
    }

    private static (string primaryColor, string accentColor) GetCategoryColors(string categoria)
    {
        return categoria.ToLower() switch
        {
            var c when c.Contains("basica") || c.Contains("básica") => (Colors.Blue.Darken2, Colors.Blue.Darken1),
            var c when c.Contains("clinica") || c.Contains("clínica") => (Colors.Red.Darken1, Colors.Red.Medium),
            var c when c.Contains("farmacologia") => (Colors.Purple.Darken2, Colors.Purple.Darken1),
            var c when c.Contains("farmacotecnica") || c.Contains("farmacotécnica") => (Colors.Orange.Darken2, Colors.Orange.Darken1),
            var c when c.Contains("gestao") || c.Contains("gestão") => (Colors.Green.Darken2, Colors.Green.Darken1),
            var c when c.Contains("saude") || c.Contains("saúde") => (Colors.Teal.Darken2, Colors.Teal.Darken1),
            _ => (Colors.Blue.Darken2, Colors.Blue.Darken1)
        };
    }

    private static string CleanText(string html)
    {
        if (string.IsNullOrWhiteSpace(html)) return string.Empty;
        var noTags = Regex.Replace(html, "<.*?>", " ");
        var decoded = WebUtility.HtmlDecode(noTags);
        return Regex.Replace(decoded, "\\s+", " ").Trim();
    }

    private static List<string> ExtractListItems(string html)
    {
        var items = new List<string>();
        if (string.IsNullOrWhiteSpace(html)) return items;
        foreach (Match m in Regex.Matches(html, "<li>(.*?)</li>", RegexOptions.IgnoreCase))
        {
            var text = CleanText(m.Groups[1].Value);
            if (!string.IsNullOrWhiteSpace(text)) items.Add(text);
        }
        return items;
    }

    private static List<string> SplitText(string text, int maxLen)
    {
        var chunks = new List<string>();
        if (string.IsNullOrWhiteSpace(text))
            return chunks;

        var words = text.Split(' ');
        var current = new List<string>();
        var length = 0;

        foreach (var w in words)
        {
            if (length + w.Length + 1 > maxLen && current.Count > 0)
            {
                chunks.Add(string.Join(' ', current));
                current.Clear();
                length = 0;
            }

            current.Add(w);
            length += w.Length + 1;
        }

        if (current.Count > 0)
            chunks.Add(string.Join(' ', current));

        return chunks;
    }
}
