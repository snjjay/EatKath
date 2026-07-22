namespace EatKath.API.Entities
{
    public class RestaurantOpeningHour
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeOnly OpenTime { get; set; }

        public TimeOnly CloseTime { get; set; }

        public bool IsClosed { get; set; }

        // Navigation Property
        public Restaurant Restaurant { get; set; } = null!;
    }
}