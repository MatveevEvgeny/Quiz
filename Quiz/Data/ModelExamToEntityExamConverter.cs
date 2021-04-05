using DAL.Entityes;
using System.Collections.Generic;

namespace Quiz.Data
{
    /// <summary>Конвертер ответов пользователя (из модели в сущность)</summary>
    static class ModelExamToEntityExamConverter
    {
        /// <summary>Конвертация из модели в сущность</summary>
        public static Exam Convert(Models.Exam mExam)
        {
            var newExam = new Exam { User = mExam.User };
            newExam.ExamAnswers = GetAnswersList(mExam.SelectedAnswers, newExam);
            return newExam;
        }

        private static List<ExamAnswer> GetAnswersList(Dictionary<Question, Answer> values, Exam exam)
        {
            List<ExamAnswer> answers = new List<ExamAnswer>();
            foreach (var key in values.Keys)
            {
                answers.Add(new ExamAnswer { Exam = exam, Answer = values[key] });
            }
            return answers;
        }
    }
}
