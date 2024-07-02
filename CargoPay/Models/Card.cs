namespace CargoPay.Models
{
    public class Card
    {
        public int Id { get; set; }
        public required string CardNumber { get; set; }
        public double Balance { get; set; }
    }
}
