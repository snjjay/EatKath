namespace EatKath.API.Entities
{
    public class Menu : BaseEntity
    {
        

        public int RestaurantId { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;


        // Navigation Properties
        public Restaurant Restaurant { get; set; } = null!;

        public ICollection<MenuCategory> Categories { get; set; } = new List<MenuCategory>();
    }
}