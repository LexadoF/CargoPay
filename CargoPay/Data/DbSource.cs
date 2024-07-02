using Microsoft.EntityFrameworkCore;
using CargoPay.Models;

namespace CargoPay.Data
{
    public class DbSource : DbContext
    {
        public DbSource(DbContextOptions<DbSource> options) : base(options) { }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Users> Users { get; set; }

    }
}
