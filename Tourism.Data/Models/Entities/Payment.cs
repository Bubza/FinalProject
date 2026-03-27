namespace Tourism.Data.Models.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public int BookingId { get; set; }

        public decimal Amount { get; set; }

        // "Card", "Borica", "Cash"
        public string Method { get; set; } = "Card";

        // "Paid", "Pending", "Refunded"
        public string Status { get; set; } = "Paid";

        public string TransactionId { get; set; } = string.Empty;

        public DateTime PaidAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Booking Booking { get; set; } = null!;
    }
}