namespace DAL.Entityes
{
    /// <summary>Сущность ответа на тест</summary>
    public class ExamAnswer : Entity
    {
        public virtual Exam Exam { get; set; }
        public virtual Answer Answer { get; set; }

    }
}
