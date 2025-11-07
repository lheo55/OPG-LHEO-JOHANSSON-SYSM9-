namespace WpfApp1.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public int Time { get; set; }

        public Recipe(string name, string description, string ingredients, int time)
        {
            Name = name;
            Description = description;
            Ingredients = ingredients;
            Time = time;
        }
    }
}
