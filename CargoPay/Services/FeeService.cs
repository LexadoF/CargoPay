using CargoPay.Data;
using CargoPay.Models;

namespace CargoPay.Services
{
    public class FeeService
    {
        private static readonly Lazy<FeeService> instance = new Lazy<FeeService>(() => new FeeService());
        private double currentFee;
        private readonly Random random;

        private FeeService()
        {
            random = new Random();
            currentFee = 1.0;
        }

        public static FeeService Instance => instance.Value;

        private void LoadInitialFee(DbSource context)
        {
            var lastFee = context.Fees.OrderByDescending(f => f.Id).FirstOrDefault();
            currentFee = lastFee?.U_fee ?? 1.0;
        }

        public double GetCurrentFee()
        {
            return currentFee;
        }

        public void UpdateFee(DbSource context)
        {
            LoadInitialFee(context);

            double randomDecimal = random.NextDouble() * 2;
            currentFee *= randomDecimal;

            var existingFee = context.Fees.FirstOrDefault();
            if (existingFee != null)
            {
                existingFee.U_fee = currentFee;
                context.Fees.Update(existingFee);
            }
            else
            {
                var fee = new Fee { U_fee = currentFee };
                context.Fees.Add(fee);
            }

            context.SaveChanges();

            Console.WriteLine($"Fee updated to: {currentFee}");
        }
    }
}
