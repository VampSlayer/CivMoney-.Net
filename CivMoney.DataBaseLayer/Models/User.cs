using System;
using System.Collections.Generic;

namespace CivMoney.DataBaseLayer
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Currency { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public DateTime TimeModified { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
    }
}
