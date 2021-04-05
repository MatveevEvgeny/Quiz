using System.Collections.Generic;

namespace DAL.Entityes
{
    /// <summary>Сущность теста</summary>
    public class Exam : Entity
    {
        public virtual User User { get; set; }

        public virtual ICollection<ExamAnswer> ExamAnswers { get; set; }
    }
}
