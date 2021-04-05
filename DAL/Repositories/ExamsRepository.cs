using DAL.Context;
using DAL.Entityes;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    /// <summary>Репозиторий пройденых тестов</summary>
    public class ExamsRepository
    {
        private readonly QuizDB _db;
        private readonly DbSet<Exam> _set;

        public ExamsRepository(QuizDB db)
        {
            _db = db;
            _set = db.Set<Exam>();
        }

        public Exam Add(Exam exam)
        {
            _db.Exams.Add(exam);
            _db.SaveChanges();
            return exam;
        }
    }
}
