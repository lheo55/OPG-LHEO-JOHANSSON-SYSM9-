using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class User
    {
        public string Username {  get; set; }
        public string Password { get; set; }
        public string Country { get; set; }

        public bool IsAdmin { get; set; }


        public ObservableCollection<Recipe> Recipes { get; set; } = new ObservableCollection<Recipe>();

        public User(string username, string password, string country, bool isAdmin = false)
        {
            Username = username;
            Password = password;
            Country = country;
            IsAdmin = isAdmin;
        }


    }

    
}
