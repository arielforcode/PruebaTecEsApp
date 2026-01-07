using PaymentsApi.Enums;

namespace PaymentsApi.Models
{
    public class Payment
    {
        public virtual Guid PaymentId { get; set; } = Guid.NewGuid();
        public virtual Guid CustomerId { get; set; }
        public virtual string ServiceProvider { get; set; } = string.Empty;
        public virtual decimal Amount { get; set; }
        public virtual PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public virtual DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
