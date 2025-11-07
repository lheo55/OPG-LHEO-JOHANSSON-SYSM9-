using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfApp1.Managers;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class RecipeDetailsViewModel : INotifyPropertyChanged
    {
        private readonly Recipe _recipe;
        private readonly UserManager _userManager;

        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public int Time { get; set; }

        private bool _isReadOnly = true;
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set { _isReadOnly = value; OnPropertyChanged(); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public RelayCommand EditCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand CancelCommand { get; }

        public RecipeDetailsViewModel(Recipe recipe, UserManager userManager)
        {
            _recipe = recipe;
            _userManager = userManager;

            Name = recipe.Name;
            Description = recipe.Description;
            Ingredients = recipe.Ingredients;
            Time = recipe.Time;

            EditCommand = new RelayCommand(_ => IsReadOnly = false);
            SaveCommand = new RelayCommand(param => Save(param as Window));
            CancelCommand = new RelayCommand(param => Cancel(param as Window));
        }

        private void Save(Window window)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description) ||
                string.IsNullOrWhiteSpace(Ingredients) || Time <= 0)
            {
                ErrorMessage = "Fyll i alla fält korrekt!";
                return;
            }

            _recipe.Name = Name;
            _recipe.Description = Description;
            _recipe.Ingredients = Ingredients;
            _recipe.Time = Time;

            window?.Close();
        }

        private void Cancel(Window window)
        {
            window?.Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
