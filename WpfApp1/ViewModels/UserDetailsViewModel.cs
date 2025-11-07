using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfApp1.Managers;

namespace WpfApp1.ViewModels
{
    public class UserDetailsViewModel : INotifyPropertyChanged
    {
        private readonly UserManager _userManager;

        private string _newUsername;
        public string NewUsername
        {
            get => _newUsername;
            set { _newUsername = value; OnPropertyChanged(); }
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set { _newPassword = value; OnPropertyChanged(); }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        private string _selectedCountry;
        public string SelectedCountry
        {
            get => _selectedCountry;
            set { _selectedCountry = value; OnPropertyChanged(); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public string[] Countries { get; } = { "Sweden", "Norway", "Denmark", "Finland", "Iceland" };

        public RelayCommand SaveCommand { get; }
        public RelayCommand CancelCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public UserDetailsViewModel(UserManager userManager)
        {
            _userManager = userManager;

            NewUsername = _userManager.CurrentUser.Username;
            SelectedCountry = _userManager.CurrentUser.Country;

            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Save(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NewUsername) || NewUsername.Length < 3)
            {
                ErrorMessage = "Användarnamnet måste vara minst 3 tecken.";
                return;
            }

            if (!string.IsNullOrEmpty(NewPassword))
            {
                if (NewPassword.Length < 5)
                {
                    ErrorMessage = "Lösenordet måste vara minst 5 tecken.";
                    return;
                }

                if (NewPassword != ConfirmPassword)
                {
                    ErrorMessage = "Lösenorden matchar inte.";
                    return;
                }

                _userManager.CurrentUser.Password = NewPassword;
            }

            _userManager.CurrentUser.Username = NewUsername;
            
            OnPropertyChanged(nameof(_userManager.CurrentUser.Username));
            _userManager.CurrentUser.Country = SelectedCountry;

            if (parameter is Window window)
                window.Close();
        }

        private void Cancel(object parameter)
        {
            if (parameter is Window window)
                window.Close();
        }
    }
}
