using DAL.Context;
using DAL.Entityes;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repositories
{
    /// <summary>Репозиторий вопросов с вариантами отвтов</summary>
    public class QuestionsRepository
    {
        private readonly QuizDB _db;
        private readonly DbSet<Question> _set;

        public QuestionsRepository(QuizDB db)
        {
            _db = db;
            _set = db.Set<Question>();
        }

        public IQueryable<Question> Questions => _set.OrderBy(q => q.Id)
                                                     .Include(q => q.Answers.OrderBy(a => a.Id));
    }
}
