namespace EatKath.API.Entities
{
    public class RestaurantDiningType
    {
        public int RestaurantId { get; set; }

        public int DiningTypeId { get; set; }

        // Navigation Properties
        public Restaurant Restaurant { get; set; } = null!;

        public DiningType DiningType { get; set; } = null!;
    }
}