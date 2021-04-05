using DAL.Entityes;
using Microsoft.EntityFrameworkCore;

// Add-Migration Initial -v
// Update-Database -v

namespace DAL.Context
{
    /// <summary>Контекст БД</summary>
    public class QuizDB : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<ExamAnswer> ExamAnswers { get; set; }

        public QuizDB(DbContextOptions<QuizDB> options) : base(options)
        {

        }
    }
}
