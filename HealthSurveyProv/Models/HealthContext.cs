using System;
using HealthSurveyProv.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace HealthSurveyProv.Models
{
    public partial class HealthContext : DbContext
    {
        public HealthContext()
        {
        }

        public HealthContext(DbContextOptions<HealthContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public virtual DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual DbSet<SurveyAnswerViewModel> SurveyQuestionsAndAnswers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot Configuration = builder.Build();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Finnish_Swedish_CI_AS");

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.SurveyHeaderText).HasMaxLength(4000);

                entity.Property(e => e.SurveyfooterText).HasMaxLength(4000);
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.ToTable("Survey");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SubmitDate).HasColumnType("datetime");

                entity.Property(e => e.SurveyRnd).HasColumnName("SurveyRND");
            });

            modelBuilder.Entity<SurveyAnswer>(entity =>
            {

                entity.Property(e => e.Answervalue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                entity.HasKey(x => x.Id);
                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.SurveyQuestionId).HasColumnName("SurveyQuestionID");

                entity.HasOne(d => d.Survey)
                    .WithMany()
                    .HasForeignKey(d => d.SurveyId)
                    .HasConstraintName("FK__SurveyAns__Surve__3B75D760");

                entity.HasOne(d => d.SurveyQuestion)
                    .WithMany()
                    .HasForeignKey(d => d.SurveyQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SurveyAns__Surve__3C69FB99");
            });

            modelBuilder.Entity<SurveyQuestion>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.QuestionPhrase).HasMaxLength(4000);

                entity.Property(e => e.QuestionType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<SurveyAnswerViewModel>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.SurveyID);
                entity.Property(e => e.QuestionPhrase);
                entity.Property(e => e.AnswerValue);


            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
