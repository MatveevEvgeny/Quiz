namespace DAL.Entityes
{
    /// <summary>Сущность варианта ответа на вопрос</summary>
    public class Answer : Entity
    {
        public string Text { get; set; }

        public bool IsRight { get; set; }

        public virtual Question Question { get; set; }
    }
}
