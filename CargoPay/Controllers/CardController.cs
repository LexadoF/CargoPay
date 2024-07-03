using CargoPay.Models;
using CargoPay.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CargoPay.Services;

namespace CargoPay.Controllers
{
    [ApiController]
    [Route("card")]
    public class CardController : ControllerBase
    {

        private readonly DbSource _context;
        public CardController(DbSource context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddCard([FromBody] CardRequest request)
        {
            if (request.CardNumber.Length != 15)
            {
                return BadRequest("Card number must be 15 digits and unique.");
            }

            if (_context.Cards.Any(c => c.CardNumber == request.CardNumber))
            {
                return BadRequest("This card already exists, try another number");
            }

            Card card = new()
            {
                CardNumber = request.CardNumber,
                Balance = 0.00
            };

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return Ok(new { card.CardNumber, card.Balance });
        }

        [Authorize]
        [HttpPost("addBalance")]
        public async Task<IActionResult> AddBalance([FromBody] BalanceRequest request)
        {
            Card card = _context.Cards.Single(c => c.CardNumber == request.CardNumber);
            if (card == null)
            {
                return NotFound("Card not found.");
            }

            if (request.Amount <= 0)
            {
                return BadRequest($"Amount must be positive.{request.Amount}");
            }

            card.Balance += request.Amount;
            await _context.SaveChangesAsync();

            return Ok(new { card.CardNumber, card.Balance });
        }

        [Authorize]
        [HttpGet("getBalance/{cardNumber}")]
        public IActionResult GetBalance(string cardNumber)
        {
            Card card = _context.Cards.Single(c => c.CardNumber == cardNumber);
            if (card == null)
            {
                return NotFound("Card not found.");
            }

            return Ok(new { card.CardNumber, card.Balance });
        }

        [Authorize]
        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromBody] PaymentRequest request)
        {
            Card? card = _context.Cards.SingleOrDefault(c => c.CardNumber == request.CardNumber);
            if (card == null)
            {
                return NotFound("Card not found.");
            }

            double fee = FeeService.Instance.GetCurrentFee();
            double totalAmount = request.Amount + fee;

            if (card.Balance < totalAmount)
            {
                return BadRequest("Insufficient balance.");
            }

            card.Balance -= totalAmount;
            await _context.SaveChangesAsync();

            return Ok(new { card.CardNumber, card.Balance, Fee = fee });
        }

    }
    public class CardRequest
    {
        public string? CardNumber { get; set; }
    }

    public class BalanceRequest
    {
        public string? CardNumber { get; set; }
        public double Amount { get; set; }
    }

    public class PaymentRequest
    {
        public string? CardNumber { get; set; }
        public double Amount { get; set; }
    }
}