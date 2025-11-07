using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfApp1.Managers;

namespace WpfApp1.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly UserManager _userManager;

        public string Username { get; set; }
        public string Password { get; set; }
        public string SelectedCountry { get; set; }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public RelayCommand RegisterCommand { get; }

        public string[] Countries { get; } = new string[]
        {
            "Sweden", "Norway", "Denmark", "Finland", "Iceland"
        };

        public RegisterViewModel(UserManager userManager)
        {
            _userManager = userManager;
            RegisterCommand = new RelayCommand(_ => Register());
        }
        
        private void Register()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(SelectedCountry))
            {
                ErrorMessage = "Fyll i alla fält!";
                return;
            }

            if (_userManager.Register(Username, Password, SelectedCountry))
            {
                ErrorMessage = "Registrering lyckades!";
                MessageBox.Show(ErrorMessage);
                Application.Current.Windows.OfType<RegisterWindow>().FirstOrDefault().Close();
            }
            else
            {
                ErrorMessage = "Användarnamnet är redan taget!";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
