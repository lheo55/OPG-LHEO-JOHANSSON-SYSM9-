using System.Windows;
using WpfApp1.Managers;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public partial class RecipeListWindow : Window
    {
        private readonly UserManager _userManager;

        public RecipeListWindow(UserManager userManager)
        {
            InitializeComponent();
            _userManager = userManager;
            DataContext = new RecipeListViewModel(_userManager);
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new MainWindow(_userManager);
            loginWindow.Show();
            Close();

            
            
        }
        private void UserDetails_Click(object sender, RoutedEventArgs e)
        {
            var userDetailsWindow = new UserDetailsWindow(_userManager);
            
            userDetailsWindow.ShowDialog();

            
        }

    }
}
