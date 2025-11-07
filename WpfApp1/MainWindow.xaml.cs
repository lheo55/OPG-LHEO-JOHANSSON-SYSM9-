using System.Linq;
using System.Windows;
using WpfApp1.Managers;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private readonly LoginViewModel loginViewModel;
        private readonly UserManager _userManager;

        public MainWindow() : this(null) { }

        public MainWindow(UserManager userManager = null)
        {
            InitializeComponent();

            _userManager = userManager ?? new UserManager();

            if (_userManager._users.Count == 0)
                _userManager.Register("bobby", "bobby", "Sweden");

            loginViewModel = new LoginViewModel(_userManager);
            DataContext = loginViewModel;

            Application.Current.MainWindow = this;
        }

        private void PwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            loginViewModel.Password = PwdBox.Password;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow(_userManager);
            registerWindow.ShowDialog();
        }
    }
}
