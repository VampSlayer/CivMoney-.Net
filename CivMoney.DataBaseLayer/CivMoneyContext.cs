using CivMoney.DataBaseLayer.Contracts;
using System.Data.Entity;

namespace CivMoney.DataBaseLayer
{
    public class CivMoneyContext : DbContext, ICivMoneyContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
