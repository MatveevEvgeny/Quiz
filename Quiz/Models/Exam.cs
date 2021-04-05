using DAL.Entityes;
using System.Collections.Generic;

namespace Quiz.Models
{
    /// <summary>Модель ответов на тест</summary>
    class Exam
    {
        public User User { get; set; }
        public Dictionary<Question, Answer> SelectedAnswers { get; set; }
    }
}
