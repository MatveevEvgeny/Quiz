using System.Collections.Generic;

namespace DAL.Entityes
{
    /// <summary>Сущность пользователя</summary>
    public class User : Entity
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }

        public override string ToString()
        {
            return $"{Surname} {Name} {Patronymic}";
        }
    }
}
