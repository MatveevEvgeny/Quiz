using DAL.Entityes;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Quiz.Commands;

namespace Quiz.ViewModels
{
    internal class UsersViewModel : BaseViewModel
    {
        public UsersViewModel() { }

        public UsersViewModel(MainWindowViewModel parentViewModel)
        {
            AddNewUserCommand = new LambdaCommand(OnAddNewUserCommandExecuted, CanAddNewUserCommandExecute);
            SelectUserCommand = new LambdaCommand(OnSelectUserCommandExecuted, CanSelectUserCommandExecute);

            _parentViewModel = parentViewModel;

            _newUser = new User();

            _usersRepository = App.Services.GetRequiredService<UsersRepository>();

            GetUsers();
        }

        /// <summary>Родительская ViewModel</summary>
        private MainWindowViewModel _parentViewModel;

        /// <summary>Репозиторий пользователей</summary>
        private readonly UsersRepository _usersRepository;

        /// <summary>Новый пользователь</summary>
        private User _newUser;

        /// <summary>Новый пользователь</summary>
        public User NewUser
        {
            get { return _newUser; }
            set { Set(ref _newUser, value); }
        }

        /// <summary>Набор данных пользователей</summary>
        private ObservableCollection<User> _users;

        /// <summary>Набор данных пользователей</summary>
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { Set(ref _users, value); }
        }

        /// <summary>Выбранный пользователь</summary>
        private User _selectedUser = null;

        /// <summary>Выбранный пользователь</summary>
        public User SelectedUser
        {
            get { return _selectedUser; }
            set { Set(ref _selectedUser, value); }
        }


        /// <summary>Команда добавления нового пользователя</summary>
        public ICommand AddNewUserCommand { get; }

        /// <summary>Логика выполнения - Команда добавления нового пользователя</summary>
        private void OnAddNewUserCommandExecuted(object p)
        {
            Users.Add(NewUser);
            _usersRepository.Add(NewUser);
            SelectedUser = NewUser;
            NewUser = new User();
        }

        /// <summary>Проверка возможности выполнения - Команда добавления нового пользователя</summary>
        private bool CanAddNewUserCommandExecute(object p)
        {
            if (!(p is User user) || (user == null))
                return false;
            if (user.Name == null || user.Surname == null || user.Patronymic == null)
                return false;
            return true;
        }


        /// <summary>Команда выбора пользователя</summary>
        public ICommand SelectUserCommand { get; }

        /// <summary>Логика выполнения - Команда выбора пользователя</summary>
        private void OnSelectUserCommandExecuted(object p)
        {
            _parentViewModel.SelectedUser = SelectedUser;
        }

        /// <summary>Проверка возможности выполнения - Команда выбора пользователя</summary>
        private bool CanSelectUserCommandExecute(object p) => p is User user && user != null;


        /// <summary>Получение списка пользователей</summary>
        private void GetUsers()
        {
            Users = new ObservableCollection<User>(_usersRepository.Users);
        }

    }
}
