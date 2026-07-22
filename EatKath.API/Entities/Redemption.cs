namespace EatKath.API.Entities
{
    public class Redemption : BaseEntity
    {
        public int DealId { get; set; }

        public int UserId { get; set; }

        public decimal RedemptionAmount { get; set; }

        public DateTime RedeemedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Deal Deal { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}