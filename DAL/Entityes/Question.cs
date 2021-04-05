using System.Collections.Generic;

namespace DAL.Entityes
{
    /// <summary>Сущность вопроса</summary>
    public class Question : Entity
    {
        public string Text { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
