 using System;

namespace CivMoney.DataBaseLayer
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime TimeModified { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}