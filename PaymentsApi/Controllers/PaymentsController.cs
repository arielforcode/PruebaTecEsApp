using Microsoft.AspNetCore.Mvc;
using PaymentsApi.DTOs;
using PaymentsApi.Gateways;

namespace PaymentsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentGateway _paymentGateway;

        public PaymentsController(IPaymentGateway paymentGateway)
        {
            _paymentGateway = paymentGateway;
        }

        /// <summary>
        /// Registra un nuevo pago de servicio
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequestDto request)
        {
            try
            {
                var payment = await _paymentGateway.CreatePaymentAsync(request);

                return CreatedAtAction(
                    nameof(CreatePayment),
                    new { paymentId = payment.PaymentId },
                    new
                    {
                        payment.PaymentId,
                        payment.ServiceProvider,
                        payment.Amount,
                        Status = payment.Status.ToString().ToLower(),
                        payment.CreatedAt
                    }
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtiene los pagos realizados por un cliente
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetPaymentsByCustomer([FromQuery] Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                return BadRequest(new { error = "customerId es requerido" });
            }

            var payments = await _paymentGateway.GetPaymentsByCustomerAsync(customerId);

            var response = payments.Select(p => new PaymentResponseDto
            {
                PaymentId = p.PaymentId,
                ServiceProvider = p.ServiceProvider,
                Amount = p.Amount,
                Status = p.Status.ToString().ToLower(),
                CreatedAt = p.CreatedAt
            });

            return Ok(response);
        }
    }
}
