using System;
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

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<SurveyAnswer>(entity =>
            {
                entity.Property(e => e.Answervalue).IsUnicode(false);

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

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
                entity.Property(e => e.QuestionType).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
