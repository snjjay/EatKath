namespace EatKath.API.Entities
{
    public class Area
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // Navigation Property
        public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    }
}