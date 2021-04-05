using DAL.Context;
using DAL.Entityes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quiz.Data
{
    class DbInitializer
    {
        private readonly QuizDB _db;

        public DbInitializer(QuizDB db)
        {
            _db = db;
        }

        /// <summary>Инициализация БД</summary>
        public async Task InitializeAsync()
        {
            await _db.Database.MigrateAsync().ConfigureAwait(false);

            if (await _db.Questions.AnyAsync()) return;

            await InitializeQuestions().ConfigureAwait(false);
        }

        /// <summary>Добавление вопросов и вариантов ответов</summary>
        private async Task InitializeQuestions()
        {
            List<Answer> answers1 = new List<Answer>
            {
                new Answer { Text = "Венера" , IsRight = true},
                new Answer { Text = "Меркурий" , IsRight = false},
                new Answer { Text = "Земля" , IsRight = false}
            };

            Question question1 = new Question { Text = "Вторая планета Солнечной системы", Answers = answers1 };

            List<Answer> answers2 = new List<Answer>
            {
                new Answer { Text = "111000" , IsRight = false},
                new Answer { Text = "101010" , IsRight = false},
                new Answer { Text = "11011" , IsRight = true}
            };

            Question question2 = new Question { Text = "Число 27 в двоичной системе исчисления", Answers = answers2 };

            List<Answer> answers3 = new List<Answer>
            {
                new Answer { Text = "7 млрд." , IsRight = true},
                new Answer { Text = "10 млрд." , IsRight = false},
                new Answer { Text = "5 млрд." , IsRight = false}
            };

            Question question3 = new Question { Text = "Примерное количество людей на Земле", Answers = answers3 };

            List<Answer> answers4 = new List<Answer>
            {
                new Answer { Text = "Лермонтов" , IsRight = false},
                new Answer { Text = "Пушкин" , IsRight = true},
                new Answer { Text = "Некрасов" , IsRight = false}
            };

            Question question4 = new Question { Text = "Кто написал «Сказка о царе Салтане»", Answers = answers4 };

            List<Answer> answers5 = new List<Answer>
            {
                new Answer { Text = "6" , IsRight = true},
                new Answer { Text = "8" , IsRight = false},
                new Answer { Text = "12" , IsRight = false}
            };

            Question question5 = new Question { Text = "Сколько граней у куба", Answers = answers5 };

            List<Question> questions = new List<Question> { question1, question2, question3, question4, question5 };

            await _db.Questions.AddRangeAsync(questions);
            await _db.SaveChangesAsync();
        }
    }
}
