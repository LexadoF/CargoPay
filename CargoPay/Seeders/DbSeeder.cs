using CargoPay.Data;
using CargoPay.Helpers;
using CargoPay.Models;

namespace CargoPay.Seeders
{
    public static class DbSeeder
    {
        public static void Seed(IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();
            DbSource? context = serviceScope.ServiceProvider.GetService<DbSource>();
            bool v = context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                Users adminUser = new()
                {
                    Username = "admin",
                    Password = PasswordHelper.HashPassword("123456")
                };

                context.Users.Add(adminUser);
                context.SaveChanges();
            }
        }
    }

}
