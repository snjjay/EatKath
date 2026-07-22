namespace EatKath.API.Entities
{
    public class DiningType
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // Navigation Property
        public ICollection<RestaurantDiningType> RestaurantDiningTypes { get; set; } = new List<RestaurantDiningType>();
    }
}