namespace Quiz.Models
{
    /// <summary>Модель результата тестирования</summary>
    class StatisticItem
    {
        public string Question { get; set; }
        public int RigthAnswers { get; set; }
        public int WrongAnswers { get; set; }
    }
}
