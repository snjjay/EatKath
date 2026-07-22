namespace EatKath.API.Entities
{
    public class Deal : BaseEntity
    {
        public int RestaurantId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal OriginalPrice { get; set; }

        public decimal DiscountedPrice { get; set; }

        public string PromoImageUrl { get; set; } = string.Empty;

        public string TermsAndConditions { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public Restaurant Restaurant { get; set; } = null!;

        public ICollection<Redemption> Redemptions { get; set; } = new List<Redemption>();
    }
}