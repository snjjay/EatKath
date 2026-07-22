namespace EatKath.API.Entities
{
    public class Cuisine
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // Navigation Property
        public ICollection<RestaurantCuisine> RestaurantCuisines { get; set; } = new List<RestaurantCuisine>();
    }
}