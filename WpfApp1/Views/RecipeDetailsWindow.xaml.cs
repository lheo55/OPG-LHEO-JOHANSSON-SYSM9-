using System.Windows;
using WpfApp1.Managers;
using WpfApp1.Models;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public partial class RecipeDetailsWindow : Window
    {
        public RecipeDetailsWindow(Recipe recipe, UserManager userManager)
        {
            InitializeComponent();
            DataContext = new RecipeDetailsViewModel(recipe, userManager);
        }
    }
}
