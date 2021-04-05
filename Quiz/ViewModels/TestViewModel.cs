using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Quiz.Commands;
using DAL.Repositories;
using DAL.Entityes;
using Microsoft.Extensions.DependencyInjection;
using Quiz.Data;

namespace Quiz.ViewModels
{
    class TestViewModel : BaseViewModel
    {
        public TestViewModel() { }

        public TestViewModel(MainWindowViewModel parentViewModel)
        {
            SelectPrevQuestionCommand = new LambdaCommand(OnSelectPrevQuestionCommand, CanSelectPrevQuestionCommand);
            SelectNextQuestionCommand = new LambdaCommand(OnSelectNextQuestionCommand, CanSelectNextQuestionCommand);
            SaveResultCommand = new LambdaCommand(OnSaveResultCommand, CanSaveResultCommand);
            _parentViewModel = parentViewModel;

            _questionsRepository = App.Services.GetRequiredService<QuestionsRepository>();
            _examsRepository = App.Services.GetRequiredService<ExamsRepository>();

            GetQuestions();

            CurrentExam = new Models.Exam { User = _parentViewModel.SelectedUser, SelectedAnswers = new Dictionary<Question, Answer>() };
            SelectedQuestion = Questions[0];
        }

        /// <summary>Родительская ViewModel</summary>
        private MainWindowViewModel _parentViewModel;

        /// <summary>Репозиторий вопросов</summary>
        private readonly QuestionsRepository _questionsRepository;

        /// <summary>Репозиторий ответов на тесты</summary>
        private readonly ExamsRepository _examsRepository;

        /// <summary>Набор данных вопросов</summary>
        public ObservableCollection<Question> Questions { get; set; }

        /// <summary>Ответы текущего теста</summary>
        private Models.Exam _currentExam;

        /// <summary>Ответы текущего теста</summary>
        public Models.Exam CurrentExam
        {
            get { return _currentExam; }
            set { Set(ref _currentExam, value); }
        }

        /// <summary>Отображаемый ворос</summary>
        private Question _selectedQuestion;

        /// <summary>Отображаемый ворос</summary>
        public Question SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                Set(ref _selectedQuestion, value);
                if (value != null && CurrentExam.SelectedAnswers.ContainsKey(SelectedQuestion))
                    SelectedAnswer = CurrentExam.SelectedAnswers[SelectedQuestion];
            }
        }

        /// <summary>Выбранный ответ на отображаемый ворос</summary>
        private Answer _selectedAnswer;

        /// <summary>Выбранный ответ на отображаемый ворос</summary>
        public Answer SelectedAnswer
        {
            get { return _selectedAnswer; }
            set
            {
                Set(ref _selectedAnswer, value);
                if (value != null) CurrentExam.SelectedAnswers[SelectedQuestion] = value;
            }
        }


        /// <summary>Команда выбора предыдущего вопроса</summary>
        public ICommand SelectPrevQuestionCommand { get; }

        /// <summary>Логика выполнения - Команда выбора предыдущего вопроса</summary>
        private void OnSelectPrevQuestionCommand(object p)
        {
            SelectedQuestion = Questions[GetIndex() - 1];
        }

        /// <summary>Проверка возможности выполнения - Команда выбора предыдущего вопроса</summary>
        private bool CanSelectPrevQuestionCommand(object p)
        {
            return !(GetIndex() <= 0);
        }


        /// <summary>Команда выбора следующего вопроса</summary>
        public ICommand SelectNextQuestionCommand { get; }

        /// <summary>Логика выполнения - Команда выбора следующего вопроса</summary>
        private void OnSelectNextQuestionCommand(object p)
        {
            SelectedQuestion = Questions[GetIndex() + 1];
        }

        /// <summary>Проверка возможности выполнения - Команда выбора предыследующегодущего вопроса</summary>
        private bool CanSelectNextQuestionCommand(object p)
        {
            return !(GetIndex() == Questions.Count - 1);
        }


        /// <summary>Команда сохранения и отображения результата теста</summary>
        public ICommand SaveResultCommand { get; }

        /// <summary>Логика выполнения - Команда сохранения и отображения результата теста</summary>
        private void OnSaveResultCommand(object p)
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            foreach (var key in CurrentExam.SelectedAnswers.Keys)
            {
                result[key.Text] = CurrentExam.SelectedAnswers[key].IsRight;
            }

            _examsRepository.Add(ModelExamToEntityExamConverter.Convert(CurrentExam));

            _parentViewModel.CurrentViewModel = new ResultViewModel(result);
            _parentViewModel.IsTestIsBeing = false;
        }

        /// <summary>Проверка возможности выполнения - Команда сохранения и отображения результата теста</summary>
        private bool CanSaveResultCommand(object p)
        {
            return (CurrentExam.SelectedAnswers.Count == Questions.Count);
        }


        /// <summary>Получение индекса</summary>
        private int GetIndex()
        {
            return Questions.IndexOf(SelectedQuestion);
        }

        /// <summary>Получение списка вопросов и вариантов ответов</summary>
        private void GetQuestions()
        {
            Questions = new ObservableCollection<Question>(_questionsRepository.Questions);
        }
    }
}
