using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Managers;

namespace WpfApp1.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        public RelayCommand SignInCommand { get; set; }
        private readonly UserManager _userManager;

        public string Username { get; set; }
        public string Password { get; set; }

        public string Country { get; set; }

        private string _messageOnLogin;
        public string messageOnlogin
        {
            get => _messageOnLogin;
            set
            {
                _messageOnLogin = value;
                OnPropertyChanged();
            }
        }

        public void Register() 
        {
            if (_userManager.Register(Username, Password,Country))
            {
                messageOnlogin = "Ditt konto är nu registrerat!";
            }
            else
            {
                messageOnlogin = "Användarnamnet är redan taget!";
            }
         }

        
        public void Login()
        {
            var user = _userManager.Login(Username, Password);
            if (user != null) {
                messageOnlogin = "Inloggad!";
                var window = new RecipeListWindow(_userManager);
                window.Show();
                Application.Current.MainWindow.Close();
                
            }
            else
            {
                messageOnlogin = "Fel användarnamn eller lössenord!";
            }
        }
        public LoginViewModel(UserManager userManager)
        {
            _userManager = userManager;

            _userManager.Register("bobby", "bobby", "Sweden");
            
            SignInCommand = new RelayCommand(_ => Login());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? n = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
    }
}
