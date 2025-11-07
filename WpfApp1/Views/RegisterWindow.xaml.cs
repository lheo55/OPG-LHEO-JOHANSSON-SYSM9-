using System.Windows;
using WpfApp1.Managers;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public partial class RegisterWindow : Window
    {
        private readonly RegisterViewModel _vm;

        public RegisterWindow(UserManager userManager)
        {
            InitializeComponent();
            _vm = new RegisterViewModel(userManager);
            DataContext = _vm;
        }

        private void PwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _vm.Password = PwdBox.Password;
        }
    }
}
