using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WpfApp1.Models;

namespace WpfApp1.Managers
{
    public class UserManager : INotifyPropertyChanged
    {
        private User _currentUser;
        public readonly List<User> _users = new();

        public UserManager()
        {
            
            _users.Add(new User("test", "123", "Sweden"));
            _users.Add(new User("admin", "admin", "Sweden"));
            _users.First(u => u.Username == "admin").IsAdmin = true;

        }

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentUser)));
            }
        }

        public bool Register(string username, string password, string country)
        {
            if (_users.Any(u => u.Username == username))
                return false;

            var newUser = new User(username, password, country);
            _users.Add(newUser);
            return true;
        }

        public User Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                CurrentUser = user;
                return user;
            }

            return null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
