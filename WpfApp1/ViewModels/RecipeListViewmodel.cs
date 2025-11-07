using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp1.Managers;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class RecipeListViewModel : INotifyPropertyChanged
    {
        private readonly UserManager _userManager;

        public ObservableCollection<Recipe> Recipes { get; } = new ObservableCollection<Recipe>();

        private Recipe _selectedRecipe;
        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged();
                RemoveRecipeCommand.RaiseCanExecuteChanged();
                OpenRecipeDetailsCommand.RaiseCanExecuteChanged();
            }
        }

        public string CurrentUsername => _userManager.CurrentUser?.Username;

        public RelayCommand AddRecipeCommand { get; }
        public RelayCommand RemoveRecipeCommand { get; }
        public RelayCommand OpenRecipeDetailsCommand { get; }
        public RelayCommand OpenUserDetailsCommand { get; }


        public RecipeListViewModel(UserManager userManager)
        {
            _userManager = userManager;

            AddRecipeCommand = new RelayCommand(_ => AddRecipe());
            RemoveRecipeCommand = new RelayCommand(_ => RemoveSelectedRecipe(), _ => SelectedRecipe != null);
            OpenRecipeDetailsCommand = new RelayCommand(_ => OpenRecipeDetails(), _ => SelectedRecipe != null);
            OpenUserDetailsCommand = new RelayCommand(_ => OpenUserDetails());

            LoadRecipes();
        }

        public void LoadRecipes()
        {
            Recipes.Clear();

            if (_userManager.CurrentUser == null) return;

            if (_userManager.CurrentUser.IsAdmin)
            {
                
                foreach (var u in _userManager._users)
                {
                    foreach (var r in u.Recipes)
                    {
                        Recipes.Add(r);
                    }
                }
            }
            else
            {
                
                if (_userManager.CurrentUser.Recipes != null)
                {
                    foreach (var r in _userManager.CurrentUser.Recipes)
                        Recipes.Add(r);
                }
            }
        }

        private void AddRecipe()
        {
            var window = new AddRecipeWindow(_userManager);
            window.ShowDialog();
            LoadRecipes();
        }

        private void RemoveSelectedRecipe()
        {
            if (SelectedRecipe == null) return;

            if (_userManager.CurrentUser.IsAdmin)
            {
                
                var owner = _userManager._users.FirstOrDefault(u => u.Recipes.Contains(SelectedRecipe));
                owner?.Recipes.Remove(SelectedRecipe);
            }
            else
            {
                _userManager.CurrentUser.Recipes.Remove(SelectedRecipe);
            }

            LoadRecipes();
        }


        private void OpenRecipeDetails()
        {
            if (SelectedRecipe == null) return;
            var window = new RecipeDetailsWindow(SelectedRecipe, _userManager);
            window.ShowDialog();
            LoadRecipes();
        }

        private void OpenUserDetails()
        {
            var window = new UserDetailsWindow(_userManager);
            window.ShowDialog();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
