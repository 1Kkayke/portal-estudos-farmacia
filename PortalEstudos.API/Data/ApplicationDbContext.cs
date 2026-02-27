using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortalEstudos.API.Models;

namespace PortalEstudos.API.Data
{
    /// <summary>
    /// Contexto do Entity Framework que integra ASP.NET Core Identity com nossas entidades.
    /// Herda de IdentityDbContext para gerenciar tabelas de autenticação automaticamente.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(w =>
            {
                // Suprimir aviso de pending changes - permite deploy sem erro
                w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning);
            });
        }

        /// <summary>Tabela de tópicos de estudo.</summary>
        public DbSet<Topic> Topics => Set<Topic>();

        /// <summary>Tabela de anotações dos alunos.</summary>
        public DbSet<Note> Notes => Set<Note>();

        public DbSet<Document> Documents => Set<Document>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<QuestionOption> QuestionOptions => Set<QuestionOption>();
        public DbSet<Exam> Exams => Set<Exam>();
        public DbSet<ExamAttempt> ExamAttempts => Set<ExamAttempt>();
        public DbSet<ExamAnswer> ExamAnswers => Set<ExamAnswer>();
        public DbSet<UserTopicInterest> UserTopicInterests => Set<UserTopicInterest>();
        public DbSet<UserTopicActivity> UserTopicActivities => Set<UserTopicActivity>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ---- Configuração do Topic ----
            builder.Entity<Topic>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Nome).IsRequired().HasMaxLength(150);
                entity.Property(t => t.Descricao).HasMaxLength(500);
            });

            // ---- Configuração do Note ----
            builder.Entity<Note>(entity =>
            {
                entity.HasKey(n => n.Id);
                entity.Property(n => n.Titulo).IsRequired().HasMaxLength(200);
                entity.Property(n => n.Conteudo).IsRequired();

                // Relacionamento: Note → Topic (muitos para um)
                entity.HasOne(n => n.Topic)
                      .WithMany(t => t.Notes)
                      .HasForeignKey(n => n.TopicId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relacionamento: Note → User (muitos para um)
                entity.HasOne(n => n.User)
                      .WithMany(u => u.Notes)
                      .HasForeignKey(n => n.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ---- Seed: 42 Tópicos de Farmácia organizados por categoria ----
            builder.Entity<Topic>().HasData(
                // ===== CIÊNCIAS FARMACÊUTICAS =====
                new Topic { Id = 1, Categoria = "Ciências Farmacêuticas", Nome = "Farmacologia Geral", Descricao = "Princípios de farmacocinética, farmacodinâmica, mecanismos de ação e interações medicamentosas.", Icone = "flask-conical", Cor = "#4F46E5" },
                new Topic { Id = 2, Categoria = "Ciências Farmacêuticas", Nome = "Farmacologia Clínica", Descricao = "Aplicação terapêutica dos fármacos, protocolos clínicos e medicina baseada em evidências.", Icone = "stethoscope", Cor = "#6366F1" },
                new Topic { Id = 3, Categoria = "Ciências Farmacêuticas", Nome = "Farmacognosia", Descricao = "Estudo de drogas naturais, plantas medicinais, fitoterápicos e seus princípios ativos.", Icone = "leaf", Cor = "#059669" },
                new Topic { Id = 4, Categoria = "Ciências Farmacêuticas", Nome = "Farmacotécnica", Descricao = "Técnicas de manipulação, formulação magistral e preparo de formas farmacêuticas.", Icone = "pill", Cor = "#0891B2" },
                new Topic { Id = 5, Categoria = "Ciências Farmacêuticas", Nome = "Tecnologia Farmacêutica", Descricao = "Produção industrial de medicamentos, controle de processos e scale-up.", Icone = "factory", Cor = "#0E7490" },
                new Topic { Id = 6, Categoria = "Ciências Farmacêuticas", Nome = "Farmácia Hospitalar", Descricao = "Gestão de medicamentos no ambiente hospitalar, farmácia clínica e uso racional.", Icone = "hospital", Cor = "#1D4ED8" },
                new Topic { Id = 7, Categoria = "Ciências Farmacêuticas", Nome = "Atenção Farmacêutica", Descricao = "Cuidado centrado no paciente, acompanhamento farmacoterapêutico e orientação.", Icone = "heart-pulse", Cor = "#E11D48" },
                new Topic { Id = 8, Categoria = "Ciências Farmacêuticas", Nome = "Farmacovigilância", Descricao = "Monitoramento de reações adversas, notificação e segurança de medicamentos.", Icone = "shield-alert", Cor = "#DC2626" },
                new Topic { Id = 9, Categoria = "Ciências Farmacêuticas", Nome = "Farmacoepidemiologia", Descricao = "Estudos populacionais sobre uso de medicamentos e impacto em saúde pública.", Icone = "bar-chart-3", Cor = "#9333EA" },
                new Topic { Id = 10, Categoria = "Ciências Farmacêuticas", Nome = "Farmacoterapia", Descricao = "Seleção racional de medicamentos para tratamento de doenças específicas.", Icone = "tablets", Cor = "#7C3AED" },

                // ===== QUÍMICA E ANÁLISES =====
                new Topic { Id = 11, Categoria = "Química e Análises", Nome = "Química Farmacêutica", Descricao = "Relação estrutura-atividade (REA), design e síntese de novos fármacos.", Icone = "atom", Cor = "#D97706" },
                new Topic { Id = 12, Categoria = "Química e Análises", Nome = "Química Orgânica", Descricao = "Fundamentos de reações orgânicas aplicadas à síntese de fármacos.", Icone = "hexagon", Cor = "#EA580C" },
                new Topic { Id = 13, Categoria = "Química e Análises", Nome = "Química Analítica", Descricao = "Métodos quantitativos e qualitativos para análise de substâncias.", Icone = "test-tubes", Cor = "#CA8A04" },
                new Topic { Id = 14, Categoria = "Química e Análises", Nome = "Controle de Qualidade", Descricao = "Ensaios físico-químicos, microbiológicos e validação de métodos analíticos.", Icone = "badge-check", Cor = "#16A34A" },
                new Topic { Id = 15, Categoria = "Química e Análises", Nome = "Análises Toxicológicas", Descricao = "Detecção e quantificação de substâncias tóxicas em amostras biológicas.", Icone = "skull", Cor = "#991B1B" },
                new Topic { Id = 16, Categoria = "Química e Análises", Nome = "Bioquímica", Descricao = "Metabolismo, enzimas, vias metabólicas e bioquímica clínica.", Icone = "dna", Cor = "#2563EB" },
                new Topic { Id = 17, Categoria = "Química e Análises", Nome = "Físico-Química", Descricao = "Termodinâmica, cinética química e equilíbrio aplicados à farmácia.", Icone = "thermometer", Cor = "#0D9488" },

                // ===== CIÊNCIAS BIOLÓGICAS E CLÍNICAS =====
                new Topic { Id = 18, Categoria = "Ciências Biológicas", Nome = "Microbiologia Clínica", Descricao = "Identificação de microrganismos patogênicos e testes de sensibilidade antimicrobiana.", Icone = "microscope", Cor = "#7C3AED" },
                new Topic { Id = 19, Categoria = "Ciências Biológicas", Nome = "Parasitologia", Descricao = "Estudo de parasitas humanos, ciclos biológicos e diagnóstico laboratorial.", Icone = "bug", Cor = "#65A30D" },
                new Topic { Id = 20, Categoria = "Ciências Biológicas", Nome = "Imunologia", Descricao = "Sistema imune, vacinas, imunodeficiências e reações de hipersensibilidade.", Icone = "shield", Cor = "#0284C7" },
                new Topic { Id = 21, Categoria = "Ciências Biológicas", Nome = "Hematologia", Descricao = "Estudo do sangue, hemograma, coagulopatias e doenças hematológicas.", Icone = "droplets", Cor = "#BE123C" },
                new Topic { Id = 22, Categoria = "Ciências Biológicas", Nome = "Fisiologia Humana", Descricao = "Funcionamento dos sistemas orgânicos e homeostase.", Icone = "activity", Cor = "#E11D48" },
                new Topic { Id = 23, Categoria = "Ciências Biológicas", Nome = "Anatomia e Histologia", Descricao = "Estrutura macro e microscópica do corpo humano.", Icone = "bone", Cor = "#A16207" },
                new Topic { Id = 24, Categoria = "Ciências Biológicas", Nome = "Patologia Geral", Descricao = "Processos patológicos básicos: inflamação, reparo, neoplasia e degeneração.", Icone = "scan-search", Cor = "#B91C1C" },
                new Topic { Id = 25, Categoria = "Ciências Biológicas", Nome = "Genética e Biologia Molecular", Descricao = "DNA, RNA, expressão gênica, farmacogenômica e terapia gênica.", Icone = "dna", Cor = "#4338CA" },
                new Topic { Id = 26, Categoria = "Ciências Biológicas", Nome = "Botânica Farmacêutica", Descricao = "Morfologia, taxonomia e identificação de plantas de interesse farmacêutico.", Icone = "flower-2", Cor = "#15803D" },

                // ===== SAÚDE PÚBLICA E GESTÃO =====
                new Topic { Id = 27, Categoria = "Saúde Pública", Nome = "Epidemiologia", Descricao = "Distribuição de doenças, indicadores de saúde e estudos epidemiológicos.", Icone = "globe", Cor = "#1E40AF" },
                new Topic { Id = 28, Categoria = "Saúde Pública", Nome = "Saúde Pública e SUS", Descricao = "Políticas de saúde, SUS, atenção primária e RENAME.", Icone = "landmark", Cor = "#047857" },
                new Topic { Id = 29, Categoria = "Saúde Pública", Nome = "Gestão Farmacêutica", Descricao = "Administração de farmácias, logística, estoque e gestão financeira.", Icone = "briefcase", Cor = "#92400E" },
                new Topic { Id = 30, Categoria = "Saúde Pública", Nome = "Legislação Farmacêutica", Descricao = "ANVISA, RDCs, Lei dos Genéricos, controle de medicamentos especiais.", Icone = "scale", Cor = "#4C1D95" },
                new Topic { Id = 31, Categoria = "Saúde Pública", Nome = "Ética e Deontologia", Descricao = "Código de ética farmacêutica, responsabilidade profissional e bioética.", Icone = "book-open", Cor = "#581C87" },
                new Topic { Id = 32, Categoria = "Saúde Pública", Nome = "Assistência Farmacêutica", Descricao = "Ciclo da assistência farmacêutica, seleção, programação e distribuição.", Icone = "package", Cor = "#0F766E" },

                // ===== ALIMENTOS E TOXICOLOGIA =====
                new Topic { Id = 33, Categoria = "Alimentos e Toxicologia", Nome = "Bromatologia", Descricao = "Composição, análise e controle de qualidade dos alimentos.", Icone = "apple", Cor = "#DC2626" },
                new Topic { Id = 34, Categoria = "Alimentos e Toxicologia", Nome = "Toxicologia Geral", Descricao = "Mecanismos de toxicidade, dose-resposta e antídotos.", Icone = "alert-triangle", Cor = "#B45309" },
                new Topic { Id = 35, Categoria = "Alimentos e Toxicologia", Nome = "Toxicologia de Alimentos", Descricao = "Contaminantes, aditivos, agrotóxicos e segurança alimentar.", Icone = "utensils", Cor = "#C2410C" },
                new Topic { Id = 36, Categoria = "Alimentos e Toxicologia", Nome = "Nutrição Clínica", Descricao = "Nutrientes, dietas terapêuticas, suplementação e interação fármaco-nutriente.", Icone = "salad", Cor = "#4D7C0F" },
                new Topic { Id = 37, Categoria = "Alimentos e Toxicologia", Nome = "Cosmetologia", Descricao = "Formulação de cosméticos, dermocosmética e regulamentação.", Icone = "sparkles", Cor = "#DB2777" },

                // ===== ANÁLISES CLÍNICAS =====
                new Topic { Id = 38, Categoria = "Análises Clínicas", Nome = "Bioquímica Clínica", Descricao = "Exames laboratoriais, marcadores bioquímicos e interpretação de resultados.", Icone = "test-tube", Cor = "#0369A1" },
                new Topic { Id = 39, Categoria = "Análises Clínicas", Nome = "Urinálise e Fluidos", Descricao = "Análise de urina, líquor, líquidos cavitários e outros fluidos biológicos.", Icone = "beaker", Cor = "#0E7490" },
                new Topic { Id = 40, Categoria = "Análises Clínicas", Nome = "Microbiologia de Alimentos", Descricao = "Microrganismos em alimentos, análise microbiológica e legislação.", Icone = "microscope", Cor = "#059669" },
                new Topic { Id = 41, Categoria = "Análises Clínicas", Nome = "Citologia Clínica", Descricao = "Análise celular, citopatologia e diagnóstico de neoplasias.", Icone = "scan", Cor = "#6D28D9" },
                new Topic { Id = 42, Categoria = "Análises Clínicas", Nome = "Micologia Clínica", Descricao = "Fungos patogênicos, diagnóstico laboratorial e antifúngicos.", Icone = "circle-dot", Cor = "#A21CAF" }
            );

            // ---- Configuração do Document ----
            builder.Entity<Document>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Titulo).IsRequired().HasMaxLength(300);
                entity.Property(d => d.Conteudo).IsRequired();
                entity.HasOne(d => d.Topic)
                      .WithMany(t => t.Documents)
                      .HasForeignKey(d => d.TopicId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ---- Configuração do Question ----
            builder.Entity<Question>(entity =>
            {
                entity.HasKey(q => q.Id);
                entity.Property(q => q.Enunciado).IsRequired().HasMaxLength(1000);
                entity.HasOne(q => q.Topic)
                      .WithMany(t => t.Questions)
                      .HasForeignKey(q => q.TopicId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<QuestionOption>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.HasOne(o => o.Question)
                      .WithMany(q => q.Opcoes)
                      .HasForeignKey(o => o.QuestionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ---- Configuração do Exam ----
            builder.Entity<Exam>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Topic)
                      .WithMany()
                      .HasForeignKey(e => e.TopicId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ExamAttempt>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.HasOne(a => a.Topic)
                      .WithMany()
                      .HasForeignKey(a => a.TopicId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.User)
                      .WithMany()
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ExamAnswer>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.HasOne(r => r.ExamAttempt)
                      .WithMany(a => a.Respostas)
                      .HasForeignKey(r => r.ExamAttemptId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(r => r.Question)
                      .WithMany()
                      .HasForeignKey(r => r.QuestionId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // ---- Seed: Documentos e Questões ----
            builder.Entity<Document>().HasData(StudyContentSeeder.GetDocuments());
            builder.Entity<Question>().HasData(
                StudyContentSeeder.GetQuestions().Select(q => new { q.Id, q.Enunciado, q.Explicacao, q.RespostaCorreta, q.Dificuldade, q.Ordem, q.TopicId }).ToArray()
            );
            builder.Entity<QuestionOption>().HasData(
                StudyContentSeeder.GetQuestionOptions().Select(o => new { o.Id, o.Texto, o.Indice, o.QuestionId }).ToArray()
            );
        }
    }
}
