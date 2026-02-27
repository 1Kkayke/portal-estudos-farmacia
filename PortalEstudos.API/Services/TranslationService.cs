using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;

namespace PortalEstudos.API.Services;

/// <summary>
/// Serviço de tradução EN → PT-BR.
/// Estratégia: Google Translate (free) → MyMemory (backup) → Dicionário local.
/// Cache em memória permanente para evitar chamadas repetidas.
/// </summary>
public sealed class TranslationService
{
    private readonly HttpClient _http;
    private readonly ILogger<TranslationService> _logger;

    // Cache permanente em memória (texto EN → texto PT-BR)
    private static readonly ConcurrentDictionary<string, string> _cache = new();

    // Dicionário de termos farmacêuticos comuns EN→PT para pós-processamento / fallback
    private static readonly (string En, string Pt)[] PharmaTerms =
    [
        // Compostos primeiro (mais longos para evitar substituições parciais)
        ("World Health Organization", "Organização Mundial da Saúde"),
        ("United Nations", "Nações Unidas"),
        ("antimicrobial resistance", "resistência antimicrobiana"),
        ("clinical trial", "ensaio clínico"),
        ("clinical trials", "ensaios clínicos"),
        ("side effects", "efeitos colaterais"),
        ("adverse effects", "efeitos adversos"),
        ("public health", "saúde pública"),
        ("global health", "saúde global"),
        ("mental health", "saúde mental"),
        ("drug delivery", "administração de fármacos"),
        ("drug resistance", "resistência farmacológica"),
        ("gene therapy", "terapia gênica"),
        ("over-the-counter", "sem receita"),
        ("blood pressure", "pressão arterial"),
        // Termos simples
        ("pharmaceutical", "farmacêutico"), ("pharmaceuticals", "farmacêuticos"),
        ("pharmacokinetics", "farmacocinética"), ("pharmacodynamics", "farmacodinâmica"),
        ("bioavailability", "biodisponibilidade"),
        ("nanotechnology", "nanotecnologia"), ("biotechnology", "biotecnologia"),
        ("immunotherapy", "imunoterapia"), ("chemotherapy", "quimioterapia"),
        ("biosimilar", "biossimilar"), ("biosimilars", "biossimilares"),
        ("antibiotics", "antibióticos"), ("antibiotic", "antibiótico"),
        ("vaccines", "vacinas"), ("vaccine", "vacina"), ("vaccination", "vacinação"),
        ("pharmacy", "farmácia"),
        ("drugs", "medicamentos"), ("drug", "medicamento"),
        ("patients", "pacientes"), ("patient", "paciente"),
        ("diseases", "doenças"), ("disease", "doença"),
        ("infections", "infecções"), ("infection", "infecção"),
        ("treatment", "tratamento"), ("treatments", "tratamentos"),
        ("therapy", "terapia"), ("diagnosis", "diagnóstico"),
        ("symptoms", "sintomas"), ("symptom", "sintoma"),
        ("prescription", "prescrição"), ("dosage", "dosagem"),
        ("efficacy", "eficácia"), ("safety", "segurança"),
        ("approval", "aprovação"), ("approved", "aprovado"),
        ("mortality", "mortalidade"), ("morbidity", "morbidade"),
        ("outbreak", "surto"), ("pandemic", "pandemia"), ("epidemic", "epidemia"),
        ("research", "pesquisa"), ("studies", "estudos"), ("study", "estudo"),
        ("prevention", "prevenção"), ("guidelines", "diretrizes"),
        ("health", "saúde"), ("medicine", "medicina"),
        ("recalls", "recolhimento"), ("recall", "recolhimento"),
        ("warns", "alerta"), ("warning", "alerta"),
        ("report", "relatório"), ("reports", "relatórios"),
        ("children", "crianças"), ("elderly", "idosos"),
        ("pregnancy", "gravidez"), ("pregnant", "grávida"),
        ("hospital", "hospital"), ("surgery", "cirurgia"),
        ("cancer", "câncer"), ("tumor", "tumor"),
        ("liver", "fígado"), ("kidney", "rim"),
        ("WHO", "OMS"),
    ];

    public TranslationService(HttpClient http, ILogger<TranslationService> logger)
    {
        _http = http;
        _http.Timeout = TimeSpan.FromSeconds(10);
        _logger = logger;
    }

    /// <summary>
    /// Traduz texto de EN para PT-BR. Retorna o texto original se a tradução falhar.
    /// </summary>
    public async Task<string> TranslateAsync(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        var input = text.Trim();

        // Verificar cache
        if (_cache.TryGetValue(input, out var cached))
            return cached;

        // Tentar Google Translate → MyMemory → Dicionário
        var result = await TryGoogleTranslateAsync(input)
                  ?? await TryMyMemoryAsync(input)
                  ?? LocalTranslate(input);

        _cache.TryAdd(input, result);
        return result;
    }

    /// <summary>
    /// Traduz título e resumo em paralelo.
    /// </summary>
    public async Task<(string titulo, string resumo)> TranslateTitleAndSummaryAsync(string title, string summary)
    {
        var results = await Task.WhenAll(
            TranslateAsync(title),
            TranslateAsync(summary)
        );
        return (results[0], results[1]);
    }

    // ═══════════════════════════════════════════════════════════════
    //  GOOGLE TRANSLATE (free endpoint, sem chave)
    // ═══════════════════════════════════════════════════════════════

    private async Task<string?> TryGoogleTranslateAsync(string text)
    {
        try
        {
            // Dividir textos longos (>5000 chars) em partes
            if (text.Length > 4500)
            {
                var parts = SplitText(text, 4500);
                var translated = new List<string>();
                foreach (var part in parts)
                {
                    var t = await TryGoogleTranslateAsync(part);
                    translated.Add(t ?? part);
                }
                return string.Join(" ", translated);
            }

            var encoded = HttpUtility.UrlEncode(text);
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=en&tl=pt&dt=t&q={encoded}";

            var response = await _http.GetStringAsync(url);
            using var doc = JsonDocument.Parse(response);

            // A resposta é um array aninhado: [[["tradução","original",null,null,10],...],null,"en"]
            var root = doc.RootElement;
            if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
            {
                var sentences = root[0];
                if (sentences.ValueKind == JsonValueKind.Array)
                {
                    var sb = new System.Text.StringBuilder();
                    foreach (var sentence in sentences.EnumerateArray())
                    {
                        if (sentence.ValueKind == JsonValueKind.Array && sentence.GetArrayLength() > 0)
                        {
                            var part = sentence[0].GetString();
                            if (!string.IsNullOrEmpty(part))
                                sb.Append(part);
                        }
                    }
                    var result = sb.ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(result) && result != text)
                        return result;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex, "Google Translate failed, trying MyMemory...");
        }
        return null;
    }

    // ═══════════════════════════════════════════════════════════════
    //  MYMEMORY (backup)
    // ═══════════════════════════════════════════════════════════════

    private async Task<string?> TryMyMemoryAsync(string text)
    {
        try
        {
            var input = text.Length > 480 ? text[..480] + "..." : text;
            var encoded = HttpUtility.UrlEncode(input);
            var url = $"https://api.mymemory.translated.net/get?q={encoded}&langpair=en|pt-br";

            var response = await _http.GetStringAsync(url);
            using var doc = JsonDocument.Parse(response);

            var root = doc.RootElement;
            if (root.TryGetProperty("responseData", out var data)
                && data.TryGetProperty("translatedText", out var translated))
            {
                var result = translated.GetString() ?? "";

                if (!string.IsNullOrWhiteSpace(result)
                    && !result.Contains("MYMEMORY WARNING", StringComparison.OrdinalIgnoreCase)
                    && !result.Equals(text, StringComparison.OrdinalIgnoreCase))
                {
                    return result;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex, "MyMemory failed, using dictionary fallback...");
        }
        return null;
    }

    // ═══════════════════════════════════════════════════════════════
    //  DICTIONARY FALLBACK
    // ═══════════════════════════════════════════════════════════════

    private static string LocalTranslate(string text)
    {
        var result = text;
        foreach (var (en, pt) in PharmaTerms)
        {
            var pattern = @"\b" + Regex.Escape(en) + @"\b";
            result = Regex.Replace(result, pattern, pt, RegexOptions.IgnoreCase);
        }
        return result;
    }

    // ═══════════════════════════════════════════════════════════════
    //  HELPERS
    // ═══════════════════════════════════════════════════════════════

    private static List<string> SplitText(string text, int maxLen)
    {
        var parts = new List<string>();
        var sentences = Regex.Split(text, @"(?<=[.!?])\s+");
        var current = "";

        foreach (var s in sentences)
        {
            if ((current + " " + s).Trim().Length > maxLen)
            {
                if (current.Length > 0) parts.Add(current.Trim());
                current = s;
            }
            else
            {
                current = (current + " " + s).Trim();
            }
        }
        if (current.Length > 0) parts.Add(current.Trim());
        return parts;
    }
}
