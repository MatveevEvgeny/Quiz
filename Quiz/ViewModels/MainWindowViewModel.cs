using DAL.Entityes;
using System.Windows.Input;
using Quiz.Commands;

namespace Quiz.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            MainWindowToUsersNavigationCommand = new LambdaCommand(OnMainWindowToUsersNavigationCommandExecuted, CanMainWindowToUsersNavigationCommandExecute);
            MainWindowToTestNavigationCommand = new LambdaCommand(OnMainWindowToTestNavigationCommandExecuted, CanMainWindowToTestNavigationCommandExecute);
            MainWindowToStatisticNavigationCommand = new LambdaCommand(OnMainWindowToStatisticNavigationCommand, CanMainWindowToStatisticNavigationCommand);
        }

        /// <summary>Текущая отображаемая ViewModel</summary>
        private BaseViewModel _currentViewModel;

        /// <summary>Текущая отображаемая ViewModel</summary>
        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set { Set(ref _currentViewModel, value); }
        }

        /// <summary>Выбранный пользователь</summary>
        private User _selectedUser;

        /// <summary>Выбранный пользователь</summary>
        public User SelectedUser
        {
            get { return _selectedUser; }
            set { Set(ref _selectedUser, value); }
        }

        /// <summary>Отметка что тест сейчас проводиться</summary>
        private bool _IsTestIsBeing;

        /// <summary>Отметка что тест сейчас проводиться</summary>
        public bool IsTestIsBeing
        {
            get { return _IsTestIsBeing; }
            set { Set(ref _IsTestIsBeing, value); }
        }

        /// <summary>Команда навигации к View Пользователей</summary>
        public ICommand MainWindowToUsersNavigationCommand { get; }

        /// <summary>Логика выполнения - Команда навигации к View Пользователей</summary>
        private void OnMainWindowToUsersNavigationCommandExecuted(object p)
        {
            CurrentViewModel = new UsersViewModel(this);
        }

        /// <summary>Проверка возможности выполнения - Команда навигации к View Пользователей</summary>
        private bool CanMainWindowToUsersNavigationCommandExecute(object p) => !IsTestIsBeing;


        /// <summary>Команда навигации к View Тестирования</summary>
        public ICommand MainWindowToTestNavigationCommand { get; }

        /// <summary>Логика выполнения - Команда навигации к View Тестирования</summary>
        private void OnMainWindowToTestNavigationCommandExecuted(object p)
        {
            CurrentViewModel = new TestViewModel(this);
            IsTestIsBeing = true;
        }

        /// <summary>Проверка возможности выполнения - Команда навигации к View Тестирования</summary>
        private bool CanMainWindowToTestNavigationCommandExecute(object p) => p is User user && user != null;


        /// <summary>Команда навигации к View Статистики</summary>
        public ICommand MainWindowToStatisticNavigationCommand { get; }

        /// <summary>Логика выполнения - Команда навигации к View Статистики</summary>
        private void OnMainWindowToStatisticNavigationCommand(object p)
        {
            CurrentViewModel = new StatisticViewModel();
        }

        /// <summary>Проверка возможности выполнения - Команда навигации к View Статистики</summary>
        private bool CanMainWindowToStatisticNavigationCommand(object p) => !IsTestIsBeing;


    }
}
