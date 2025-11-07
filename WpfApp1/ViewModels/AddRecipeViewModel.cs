using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfApp1.Managers;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class AddRecipeViewModel : INotifyPropertyChanged
    {
        private readonly UserManager _userManager;
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public int Time { get; set; }

        public RelayCommand SaveCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public AddRecipeViewModel(UserManager userManager)
        {
            _userManager = userManager;

            if (_userManager.CurrentUser.Recipes == null)
                _userManager.CurrentUser.Recipes = new System.Collections.ObjectModel.ObservableCollection<Recipe>();

            SaveCommand = new RelayCommand(Save);
        }

        private void Save(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description) ||
                string.IsNullOrWhiteSpace(Ingredients) || Time <= 0) return;

            _userManager.CurrentUser.Recipes.Add(new Recipe(Name, Description, Ingredients, Time));


            if (parameter is Window window)
                window.Close();
        }
    }
}
