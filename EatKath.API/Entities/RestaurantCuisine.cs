namespace EatKath.API.Entities
{
    public class RestaurantCuisine
    {
        public int RestaurantId { get; set; }

        public int CuisineId { get; set; }

        // Navigation Properties
        public Restaurant Restaurant { get; set; } = null!;

        public Cuisine Cuisine { get; set; } = null!;
    }
}