namespace PortalEstudos.API.Models;

public class BlogArticle
{
    public string Id { get; set; } = "";
    public string Titulo { get; set; } = "";
    public string Subtitulo { get; set; } = "";
    public string Resumo { get; set; } = "";
    public string Conteudo { get; set; } = "";
    public string Autor { get; set; } = "";
    public string Fonte { get; set; } = "";
    public string FonteUrl { get; set; } = "";
    public string ImagemUrl { get; set; } = "";
    public string ImagemCredito { get; set; } = "";
    public string Categoria { get; set; } = "";
    public List<string> Tags { get; set; } = new();
    public DateTime DataPublicacao { get; set; }
    public int LeituraMinutos { get; set; }
    public string LinkExterno { get; set; } = "";
    public bool IsExterno { get; set; }
}
