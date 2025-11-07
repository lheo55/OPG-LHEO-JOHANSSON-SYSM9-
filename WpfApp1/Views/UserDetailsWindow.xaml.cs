using System.Windows;
using WpfApp1.Managers;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public partial class UserDetailsWindow : Window
    {
        public UserDetailsWindow(UserManager userManager)
        {
            InitializeComponent();
            DataContext = new UserDetailsViewModel(userManager);
        }

        private void PwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserDetailsViewModel vm)
                vm.NewPassword = PwdBox.Password;
        }

        private void ConfirmPwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserDetailsViewModel vm)
                vm.ConfirmPassword = ConfirmPwdBox.Password;
        }
    }
}
