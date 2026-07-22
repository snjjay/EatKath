namespace EatKath.API.Entities
{
    public class RestaurantImage : BaseEntity
    {
       

        public int RestaurantId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsPrimary { get; set; }


        // Navigation Property
        public Restaurant Restaurant { get; set; } = null!;
    }
}