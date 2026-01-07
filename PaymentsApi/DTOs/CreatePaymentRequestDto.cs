using System.ComponentModel.DataAnnotations;

namespace PaymentsApi.DTOs
{
    public class CreatePaymentRequestDto
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string ServiceProvider { get; set; } = string.Empty;

        [Range(0.01, 1500)]
        public decimal Amount { get; set; }
    }
}
