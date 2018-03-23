using CivMoney.DataBaseLayer.Contracts;
using System.Data.Entity;

namespace CivMoney.DataBaseLayer
{
    public class CivMoneyContext : DbContext, ICivMoneyContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
    }
}
