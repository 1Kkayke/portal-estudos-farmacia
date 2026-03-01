using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using PortalEstudos.API.Models;

namespace PortalEstudos.API.Services;

/// <summary>
/// Busca artigos de fontes externas (PubMed E-utilities, RSS feeds) e combina com artigos curados.
/// Resultados são cacheados por 30 minutos para performance.
/// </summary>
public sealed class NewsFeedService
{
    private readonly HttpClient _http;
    private readonly ILogger<NewsFeedService> _logger;
    private readonly TranslationService _translator;

    private static readonly SemaphoreSlim _lock = new(1, 1);
    private static List<BlogArticle>? _cache;
    private static DateTime _lastFetch = DateTime.MinValue;
    private static readonly TimeSpan CacheTtl = TimeSpan.FromHours(2); // ⚡ 30min → 2h para menos requisições externas

    // ── PubMed E-utilities (free, no key required for ≤3 req/s) ──
    private const string PubMedSearch =
        "https://eutils.ncbi.nlm.nih.gov/entrez/eutils/esearch.fcgi?db=pubmed&retmode=json&retmax=8&sort=date&term=";
    private const string PubMedSummary =
        "https://eutils.ncbi.nlm.nih.gov/entrez/eutils/esummary.fcgi?db=pubmed&retmode=json&id=";

    private static readonly (string Query, string Categoria)[] PubMedQueries =
    {
        ("pharmaceutical+sciences+2025", "Ciências Farmacêuticas"),
        ("clinical+pharmacy+patient+care", "Farmácia Clínica"),
        ("pharmacology+drug+therapy+2025", "Farmacologia"),
        ("drug+delivery+systems", "Tecnologia Farmacêutica"),
        ("antibiotic+resistance+bacteria", "Microbiologia"),
        ("cancer+chemotherapy+treatment", "Oncologia"),
        ("cardiovascular+disease+prevention", "Cardiologia"),
        ("diabetes+management+insulin", "Endocrinologia"),
        ("mental+health+psychiatric+medication", "Psicofarmacologia"),
        ("vaccine+immunization+safety", "Imunologia"),
        // ⚡ Novas queries expandidas (10 → 20)
        ("pharmacokinetics+pharmacodynamics", "Ciências Farmacêuticas"),
        ("drug+interactions+metabolism", "Farmacologia"),
        ("adverse+effects+toxicity", "Farmacologia"),
        ("personalized+medicine+genomics", "Medicina Personalizada"),
        ("drug+efficacy+clinical+trials", "Farmácia Clínica"),
        ("antibiotic+stewardship", "Microbiologia"),
        ("immunotherapy+vaccines", "Imunologia"),
        ("epigenetics+gene+therapy", "Biotecnologia"),
        ("drug+repositioning+repurposing", "Inovação Farmacêutica"),
        ("precision+pharmacology+precision+medicine", "Ciências Farmacêuticas"),
    };

    // ── RSS / Atom feeds ──
    private static readonly (string Url, string Source, string Categoria)[] RssFeeds =
    {
        // Saúde Global
        ("https://www.who.int/rss-feeds/news-english.xml", "OMS (WHO)", "Saúde Global"),
        ("https://news.un.org/feed/subscribe/en/news/topic/health/feed/rss.xml", "UN News Health", "Saúde Global"),
        
        // Notícias Científicas
        ("https://rss.sciencedaily.com/health_medicine/pharmaceuticals.xml", "ScienceDaily - Farmácia", "Ciências Farmacêuticas"),
        ("https://rss.sciencedaily.com/health_medicine/drugs.xml", "ScienceDaily - Medicamentos", "Farmacologia"),
        ("https://rss.sciencedaily.com/health_medicine/diseases_conditions.xml", "ScienceDaily - Doenças", "Saúde Clínica"),
        
        // Regulação e Política
        ("https://www.fda.gov/about-fda/contact-fda/stay-informed/rss-feeds/press-releases/rss.xml", "FDA", "Notícias Médicas"),
        ("https://www.ema.europa.eu/en/rss.xml", "EMA - Europa", "Regulação"),
        
        // Saúde e Medicina
        ("https://www.nlm.nih.gov/databases/alerts/medlineplus_health_topics.xml", "NIH MedlinePlus", "Saúde Clínica"),
        ("https://feeds.aap.org/research/latest/feed.rss", "AAP - Pediatria", "Pediatria"),
        ("https://feeds.nature.com/nature/rss/current", "Nature Medicine", "Pesquisa Médica"),
        
        // ⚡ Novos feeds expandidos (10 → 20+)
        ("https://rss.sciencedaily.com/health_medicine/heart_disease.xml", "ScienceDaily - Cardiologia", "Cardiologia"),
        ("https://rss.sciencedaily.com/health_medicine/cancer.xml", "ScienceDaily - Câncer", "Oncologia"),
        ("https://feeds.bmj.com/bmj/news-and-analysis/rss/", "BMJ - Medical Journal", "Pesquisa Médica"),
        ("https://www.thelancet.com/rss", "The Lancet", "Pesquisa Médica"),
        ("https://feeds.nature.com/nature/rss/current", "Nature - All Articles", "Pesquisa Médica"),
        ("https://rss.sciencedaily.com/health_medicine/vaccines.xml", "ScienceDaily - Vacinas", "Imunologia"),
        ("https://rss.sciencedaily.com/health_medicine/infectious_diseases.xml", "ScienceDaily - Doenças Infecciosas", "Microbiologia"),
        ("https://rss.sciencedaily.com/health_medicine/microbiology.xml", "ScienceDaily - Microbiologia", "Microbiologia"),
        ("https://rss.sciencedaily.com/health_medicine/genetics.xml", "ScienceDaily - Genética", "Biotecnologia"),
        ("https://rss.sciencedaily.com/health_medicine/diagnostics.xml", "ScienceDaily - Diagnóstico", "Análises Clínicas"),
        ("https://rss.sciencedaily.com/health_medicine/pharmacology.xml", "ScienceDaily - Farmacologia", "Farmacologia"),
        ("https://feeds.jaha.ahajournals.org/", "AHA Journals - Cardiologia", "Cardiologia"),
    };

    // ── Category → image mapping (Unsplash, fallback) ──
    private static readonly Dictionary<string, string> CategoryImages = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Ciências Farmacêuticas"] = "https://images.unsplash.com/photo-1471864190281-a93a3070b6de?w=800&h=400&fit=crop&auto=format&q=80",
        ["Farmácia Clínica"] = "https://images.unsplash.com/photo-1576091160550-2173dba999ef?w=800&h=400&fit=crop&auto=format&q=80",
        ["Farmacologia"] = "https://images.unsplash.com/photo-1587854692152-cbe660dbde88?w=800&h=400&fit=crop&auto=format&q=80",
        ["Notícias Médicas"] = "https://images.unsplash.com/photo-1584308666744-24d5c474f2ae?w=800&h=400&fit=crop&auto=format&q=80",
        ["Saúde Global"] = "https://images.unsplash.com/photo-1559757175-0eb30cd8c063?w=800&h=400&fit=crop&auto=format&q=80",
        ["Tecnologia Farmacêutica"] = "https://images.unsplash.com/photo-1530725807519-2d1f1849e56a?w=800&h=400&fit=crop&auto=format&q=80",
        ["Microbiologia"] = "https://images.unsplash.com/photo-1530727191805-c1a2f1c9f9b5?w=800&h=400&fit=crop&auto=format&q=80",
        ["Oncologia"] = "https://images.unsplash.com/photo-1576091160679-112b8d7d5cad?w=800&h=400&fit=crop&auto=format&q=80",
        ["Cardiologia"] = "https://images.unsplash.com/photo-1576091160399-0ff0b764b04a?w=800&h=400&fit=crop&auto=format&q=80",
        ["Endocrinologia"] = "https://images.unsplash.com/photo-1576091160568-4286e3a5c15e?w=800&h=400&fit=crop&auto=format&q=80",
        ["Psicofarmacologia"] = "https://images.unsplash.com/photo-1489749798305-4fea3ba63d60?w=800&h=400&fit=crop&auto=format&q=80",
        ["Imunologia"] = "https://images.unsplash.com/photo-1573782645897-e8207e773f10?w=800&h=400&fit=crop&auto=format&q=80",
        ["Saúde Clínica"] = "https://images.unsplash.com/photo-1576091160550-2173dba999ef?w=800&h=400&fit=crop&auto=format&q=80",
        ["Regulação"] = "https://images.unsplash.com/photo-1589939705882-02d0e1c0f998?w=800&h=400&fit=crop&auto=format&q=80",
        ["Pesquisa Médica"] = "https://images.unsplash.com/photo-1579154204601-01d6f179fbe9?w=800&h=400&fit=crop&auto=format&q=80",
        ["Pediatria"] = "https://images.unsplash.com/photo-1576091160485-112184aa202e?w=800&h=400&fit=crop&auto=format&q=80",
    };
    private const string FallbackImage = "https://images.unsplash.com/photo-1532187863486-abf9dbad1b69?w=800&h=400&fit=crop&auto=format&q=80";

    public NewsFeedService(HttpClient http, ILogger<NewsFeedService> logger, TranslationService translator)
    {
        _http = http;
        _http.Timeout = TimeSpan.FromSeconds(12);
        _http.DefaultRequestHeaders.UserAgent.ParseAdd("PortalEstudos/1.0");
        _logger = logger;
        _translator = translator;
    }

    // ═══════════════════════════════════════════════════════════════
    //  PUBLIC API
    // ═══════════════════════════════════════════════════════════════

    public async Task<List<BlogArticle>> GetAllArticlesAsync()
    {
        if (_cache is not null && DateTime.UtcNow - _lastFetch < CacheTtl)
            return _cache;

        await _lock.WaitAsync();
        try
        {
            // double-check
            if (_cache is not null && DateTime.UtcNow - _lastFetch < CacheTtl)
                return _cache;
            try
            {
                var buildTask = BuildArticlesAsync();
                var completedTask = await Task.WhenAny(buildTask, Task.Delay(TimeSpan.FromSeconds(8)));

                if (completedTask != buildTask)
                {
                    _logger.LogWarning("Blog feed timeout on cold fetch. Returning fallback payload.");
                    return GetFallbackArticles();
                }

                _cache = await buildTask;
                _lastFetch = DateTime.UtcNow;

                _logger.LogInformation("Blog: {Total} articles loaded ({External} external, {Curated} curated)",
                    _cache.Count, _cache.Count(a => a.IsExterno), _cache.Count(a => !a.IsExterno));

                return _cache;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Blog feed failed. Returning fallback payload.");
                return GetFallbackArticles();
            }
        }
        finally
        {
            _lock.Release();
        }
    }

    private async Task<List<BlogArticle>> BuildArticlesAsync()
    {
        var articles = new List<BlogArticle>();

        // 1) Fetch from PubMed + RSS in parallel
        var tasks = new List<Task<List<BlogArticle>>>();
        foreach (var (query, cat) in PubMedQueries)
            tasks.Add(FetchPubMedAsync(query, cat));
        foreach (var (url, src, cat) in RssFeeds)
            tasks.Add(FetchRssAsync(url, src, cat));

        var results = await Task.WhenAll(tasks);
        foreach (var r in results) articles.AddRange(r);

        // 2) Translate external articles to PT-BR
        await TranslateArticlesAsync(articles);

        // 3) Always include curated articles (already in PT-BR)
        articles.AddRange(CuratedArticlesProvider.GetArticles());

        // 4) Sort by date descending, limit to 100
        return articles
            .OrderByDescending(a => a.DataPublicacao)
            .Take(100)
            .ToList();
    }

    private List<BlogArticle> GetFallbackArticles()
    {
        if (_cache is not null && _cache.Count > 0)
            return _cache;

        return CuratedArticlesProvider.GetArticles()
            .OrderByDescending(a => a.DataPublicacao)
            .Take(100)
            .ToList();
    }

    public async Task<BlogArticle?> GetArticleByIdAsync(string id)
    {
        var all = await GetAllArticlesAsync();
        return all.FirstOrDefault(a => a.Id == id);
    }

    public async Task<List<string>> GetCategoriesAsync()
    {
        var all = await GetAllArticlesAsync();
        return all.Select(a => a.Categoria).Distinct().OrderBy(c => c).ToList();
    }

    // ═══════════════════════════════════════════════════════════════
    //  TRANSLATION (EN → PT-BR)
    // ═══════════════════════════════════════════════════════════════

    private async Task TranslateArticlesAsync(List<BlogArticle> articles)
    {
        // Traduzir em batches de 3 para não sobrecarregar a API gratuita
        const int batchSize = 3;
        for (int i = 0; i < articles.Count; i += batchSize)
        {
            var batch = articles.Skip(i).Take(batchSize).ToList();
            // Traduzir sequencialmente dentro do batch para ser gentil com a API
            foreach (var article in batch)
            {
                try
                {
                    var (titulo, resumo) = await _translator.TranslateTitleAndSummaryAsync(
                        article.Titulo, article.Resumo);
                    article.Titulo = titulo;
                    article.Resumo = resumo;

                    // Traduzir conteúdo curto (resumo expandido) para artigos RSS
                    if (article.Conteudo.Length < 1000)
                    {
                        var plainContent = StripHtml(article.Conteudo);
                        var translatedContent = await _translator.TranslateAsync(plainContent);
                        // Re-wrapar em HTML
                        article.Conteudo = $"<p class='lead'>{translatedContent}</p>" +
                            (article.LinkExterno != null
                                ? $"<p><a href=\"{article.LinkExterno}\" target=\"_blank\" rel=\"noopener\">🔗 Leia a matéria completa em {article.Fonte} →</a></p>"
                                : "");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to translate article {Id}", article.Id);
                }
            }

            // Delay entre batches para respeitar rate limit da API
            if (i + batchSize < articles.Count)
                await Task.Delay(500);
        }

        _logger.LogInformation("Translated {Count} external articles to PT-BR", articles.Count);
    }

    // ═══════════════════════════════════════════════════════════════
    //  PUBMED E-UTILITIES
    // ═══════════════════════════════════════════════════════════════

    private async Task<List<BlogArticle>> FetchPubMedAsync(string query, string category)
    {
        var articles = new List<BlogArticle>();
        try
        {
            // Step 1: search for IDs
            var searchJson = await _http.GetStringAsync(PubMedSearch + Uri.EscapeDataString(query));
            using var searchDoc = JsonDocument.Parse(searchJson);
            var idList = searchDoc.RootElement
                .GetProperty("esearchresult")
                .GetProperty("idlist")
                .EnumerateArray()
                .Select(e => e.GetString()!)
                .ToList();

            if (idList.Count == 0) return articles;

            // Step 2: get summaries
            var ids = string.Join(",", idList);
            var summaryJson = await _http.GetStringAsync(PubMedSummary + ids);
            using var summaryDoc = JsonDocument.Parse(summaryJson);
            var result = summaryDoc.RootElement.GetProperty("result");

            foreach (var id in idList)
            {
                if (!result.TryGetProperty(id, out var item)) continue;

                var title = item.TryGetProperty("title", out var t) ? t.GetString() ?? "" : "";
                var source = item.TryGetProperty("source", out var s) ? s.GetString() ?? "" : "";
                var pubDate = item.TryGetProperty("pubdate", out var pd) ? pd.GetString() ?? "" : "";
                var sortTitle = item.TryGetProperty("sorttitle", out var st) ? st.GetString() ?? "" : "";

                // Get authors
                var authors = new List<string>();
                if (item.TryGetProperty("authors", out var auths) && auths.ValueKind == JsonValueKind.Array)
                {
                    foreach (var auth in auths.EnumerateArray())
                    {
                        if (auth.TryGetProperty("name", out var n))
                            authors.Add(n.GetString() ?? "");
                    }
                }

                // Build article
                var doi = item.TryGetProperty("elocationid", out var eid) ? eid.GetString() ?? "" : "";
                var link = $"https://pubmed.ncbi.nlm.nih.gov/{id}/";

                var resumo = $"Publicado em {source}. {(authors.Count > 0 ? $"Autores: {string.Join(", ", authors.Take(3))}{(authors.Count > 3 ? " et al." : "")}." : "")}";

                var conteudo = $@"
<p class='lead'>{StripHtml(title)}</p>
<p><strong>Periódico:</strong> {source}</p>
<p><strong>Autores:</strong> {string.Join(", ", authors)}</p>
<p><strong>Data de publicação:</strong> {pubDate}</p>
<p>Este artigo está disponível na íntegra no PubMed. Clique no link abaixo para acessar o texto completo, abstract e referências.</p>
<p><a href=""{link}"" target=""_blank"" rel=""noopener"">📄 Acessar artigo completo no PubMed →</a></p>";

                articles.Add(new BlogArticle
                {
                    Id = $"pubmed-{id}",
                    Titulo = StripHtml(title),
                    Resumo = resumo.Length > 200 ? resumo[..200] + "..." : resumo,
                    Conteudo = conteudo,
                    Autor = authors.Count > 0 ? authors[0] : "PubMed",
                    Fonte = $"PubMed / {source}",
                    FonteUrl = link,
                    ImagemUrl = CategoryImages.GetValueOrDefault(category, FallbackImage),
                    Categoria = category,
                    Tags = new List<string> { "pesquisa", "PubMed", category.ToLower() },
                    DataPublicacao = ParseFlexDate(pubDate),
                    LeituraMinutos = 5,
                    LinkExterno = link,
                    IsExterno = true,
                });
            }

            _logger.LogInformation("PubMed [{Category}]: fetched {Count} articles", category, articles.Count);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "PubMed fetch failed for query '{Query}'", query);
        }
        return articles;
    }

    // ═══════════════════════════════════════════════════════════════
    //  RSS / ATOM FEED PARSER
    // ═══════════════════════════════════════════════════════════════

    private async Task<List<BlogArticle>> FetchRssAsync(string url, string sourceName, string category)
    {
        var articles = new List<BlogArticle>();
        try
        {
            var xml = await _http.GetStringAsync(url);
            var doc = XDocument.Parse(xml);

            // RSS 2.0 format
            var items = doc.Descendants("item").Take(10);
            int idx = 0;
            foreach (var item in items)
            {
                var title = item.Element("title")?.Value ?? "";
                var desc = item.Element("description")?.Value ?? "";
                var link = item.Element("link")?.Value ?? "";
                var pubDateStr = item.Element("pubDate")?.Value;

                // Try media:content for image
                var imageUrl = ExtractImageFromXml(item, desc);

                var plainDesc = StripHtml(desc);

                articles.Add(new BlogArticle
                {
                    Id = $"rss-{sourceName.ToLower().Replace(" ", "")}-{idx++}",
                    Titulo = StripHtml(title),
                    Resumo = plainDesc.Length > 250 ? plainDesc[..250] + "..." : plainDesc,
                    Conteudo = $"<p class='lead'>{plainDesc}</p><p><a href=\"{link}\" target=\"_blank\" rel=\"noopener\">🔗 Leia a matéria completa em {sourceName} →</a></p>",
                    Autor = sourceName,
                    Fonte = sourceName,
                    FonteUrl = link,
                    ImagemUrl = imageUrl.Length > 0 ? imageUrl : CategoryImages.GetValueOrDefault(category, FallbackImage),
                    Categoria = category,
                    Tags = new List<string> { sourceName.ToLower(), category.ToLower() },
                    DataPublicacao = pubDateStr != null ? ParseFlexDate(pubDateStr) : DateTime.UtcNow,
                    LeituraMinutos = Math.Max(3, plainDesc.Split(' ').Length / 200),
                    LinkExterno = link,
                    IsExterno = true,
                });
            }

            _logger.LogInformation("RSS [{Source}]: fetched {Count} articles", sourceName, articles.Count);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "RSS fetch failed for {Url}", url);
        }
        return articles;
    }

    // ═══════════════════════════════════════════════════════════════
    //  HELPERS
    // ═══════════════════════════════════════════════════════════════

    private static string StripHtml(string html)
    {
        if (string.IsNullOrEmpty(html)) return "";
        return Regex.Replace(html, "<.*?>", " ").Trim();
    }

    private static DateTime ParseFlexDate(string s)
    {
        if (DateTime.TryParse(s, out var d)) return d.ToUniversalTime();
        // Try "2025 Jan 15" format from PubMed
        var parts = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= 2 && int.TryParse(parts[0], out var year))
        {
            var monthStr = parts.Length > 1 ? parts[1] : "Jan";
            var dayStr = parts.Length > 2 ? parts[2] : "1";
            if (DateTime.TryParse($"{monthStr} {dayStr}, {year}", out var d2))
                return d2.ToUniversalTime();
            return new DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }
        return DateTime.UtcNow;
    }

    private static string ExtractImageFromXml(XElement item, string desc)
    {
        // Try media:content or media:thumbnail
        XNamespace media = "http://search.yahoo.com/mrss/";
        var mediaContent = item.Element(media + "content");
        if (mediaContent != null)
        {
            var url = mediaContent.Attribute("url")?.Value;
            if (!string.IsNullOrEmpty(url)) return url;
        }
        var mediaThumbnail = item.Element(media + "thumbnail");
        if (mediaThumbnail != null)
        {
            var url = mediaThumbnail.Attribute("url")?.Value;
            if (!string.IsNullOrEmpty(url)) return url;
        }

        // Try <enclosure>
        var enclosure = item.Element("enclosure");
        if (enclosure?.Attribute("type")?.Value?.StartsWith("image") == true)
        {
            var url = enclosure.Attribute("url")?.Value;
            if (!string.IsNullOrEmpty(url)) return url;
        }

        // Try img tag in description
        var imgMatch = Regex.Match(desc, @"<img[^>]+src=[""']([^""']+)[""']", RegexOptions.IgnoreCase);
        if (imgMatch.Success) return imgMatch.Groups[1].Value;

        return "";
    }
}
