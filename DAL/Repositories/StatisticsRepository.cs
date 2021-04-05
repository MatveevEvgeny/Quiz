using DAL.Context;
using DAL.Entityes;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repositories
{
    /// <summary>Репозиторий статистики</summary>
    public class StatisticsRepository
    {
        private readonly QuizDB _db;
        private readonly DbSet<ExamAnswer> _set;

        public StatisticsRepository(QuizDB db)
        {
            _db = db;
            _set = db.Set<ExamAnswer>();
        }

        public IQueryable<ExamAnswer> Stats => _set.Include(a => a.Answer)
                                                   .ThenInclude(q => q.Question);
    }
}
