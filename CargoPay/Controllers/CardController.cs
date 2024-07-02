using CargoPay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargoPay.Controllers
{
    [ApiController]
    [Route("card")]
    public class CardController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("list")]
        public dynamic ListCard()
        {
            List<Card> cards = new List<Card>
            {
                new Card
                {
                    CardNumber = "123456789012345",
                    Balance = 10.000,
                },
            };

            return cards;
        }
    }
}
