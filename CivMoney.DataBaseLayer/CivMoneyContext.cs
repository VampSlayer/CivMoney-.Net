using System.Data.Entity;

namespace CivMoney.DataBaseLayer
{
    public class CivMoneyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
