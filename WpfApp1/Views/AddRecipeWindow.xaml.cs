using System.Windows;
using WpfApp1.Managers;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public partial class AddRecipeWindow : Window
    {
        public AddRecipeWindow(UserManager userManager)
        {
            InitializeComponent();
            DataContext = new AddRecipeViewModel(userManager);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
