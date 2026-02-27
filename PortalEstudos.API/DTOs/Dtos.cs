namespace PortalEstudos.API.DTOs
{
    // ---- DTOs de Autenticação ----

    /// <summary>DTO para registro de novo usuário.</summary>
    public class RegisterDto
    {
        public string NomeCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    /// <summary>DTO para login.</summary>
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    /// <summary>Resposta de autenticação contendo o token JWT.</summary>
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }

    // ---- DTOs de Topic ----

    /// <summary>DTO de leitura para tópicos.</summary>
    public class TopicDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Icone { get; set; } = string.Empty;
        public string Cor { get; set; } = string.Empty;
        public int TotalNotas { get; set; }
    }

    // ---- DTOs de Note ----

    /// <summary>DTO de leitura para anotações.</summary>
    public class NoteDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Conteudo { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int TopicId { get; set; }
        public string TopicNome { get; set; } = string.Empty;
    }

    /// <summary>DTO para criação/edição de anotação.</summary>
    public class CreateNoteDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Conteudo { get; set; } = string.Empty;
        public int TopicId { get; set; }
    }

    /// <summary>DTO para atualização de anotação.</summary>
    public class UpdateNoteDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Conteudo { get; set; } = string.Empty;
    }

    // ---- DTOs de Document ----

    public class DocumentDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Resumo { get; set; } = string.Empty;
        public string Conteudo { get; set; } = string.Empty;
        public int Ordem { get; set; }
        public string Dificuldade { get; set; } = string.Empty;
        public int LeituraMinutos { get; set; }
        public int TopicId { get; set; }
    }

    public class DocumentListDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Resumo { get; set; } = string.Empty;
        public int Ordem { get; set; }
        public string Dificuldade { get; set; } = string.Empty;
        public int LeituraMinutos { get; set; }
    }

    // ---- DTOs de Question ----

    public class QuestionDto
    {
        public int Id { get; set; }
        public string Enunciado { get; set; } = string.Empty;
        public string Dificuldade { get; set; } = string.Empty;
        public int Ordem { get; set; }
        public int TopicId { get; set; }
        public List<QuestionOptionDto> Opcoes { get; set; } = new();
    }

    public class QuestionWithAnswerDto : QuestionDto
    {
        public int RespostaCorreta { get; set; }
        public string Explicacao { get; set; } = string.Empty;
    }

    public class QuestionOptionDto
    {
        public int Id { get; set; }
        public string Texto { get; set; } = string.Empty;
        public int Indice { get; set; }
    }

    // ---- DTOs de Exam ----

    public class StartExamDto
    {
        public int TopicId { get; set; }
        public int TotalQuestoes { get; set; } = 20;
    }

    public class SubmitExamDto
    {
        public int AttemptId { get; set; }
        public List<ExamAnswerDto> Respostas { get; set; } = new();
    }

    public class ExamAnswerDto
    {
        public int QuestionId { get; set; }
        public int RespostaEscolhida { get; set; }
    }

    public class ExamAttemptResultDto
    {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public double Nota { get; set; }
        public int Acertos { get; set; }
        public int Erros { get; set; }
        public int TotalQuestoes { get; set; }
        public bool Finalizada { get; set; }
        public int TopicId { get; set; }
        public string TopicNome { get; set; } = string.Empty;
        public List<ExamAnswerResultDto> Respostas { get; set; } = new();
    }

    public class ExamAnswerResultDto
    {
        public int QuestionId { get; set; }
        public string Enunciado { get; set; } = string.Empty;
        public int RespostaEscolhida { get; set; }
        public int RespostaCorreta { get; set; }
        public bool Correta { get; set; }
        public string Explicacao { get; set; } = string.Empty;
        public List<QuestionOptionDto> Opcoes { get; set; } = new();
    }

    public class ExamHistoryDto
    {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public double Nota { get; set; }
        public int Acertos { get; set; }
        public int TotalQuestoes { get; set; }
        public bool Finalizada { get; set; }
        public string TopicNome { get; set; } = string.Empty;
    }

    public class ExamQuestionsDto
    {
        public int AttemptId { get; set; }
        public int TopicId { get; set; }
        public string TopicNome { get; set; } = string.Empty;
        public int TotalQuestoes { get; set; }
        public int TempoMinutos { get; set; }
        public List<QuestionDto> Questoes { get; set; } = new();
    }
}
