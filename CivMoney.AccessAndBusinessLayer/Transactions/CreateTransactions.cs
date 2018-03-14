using CivMoney.DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivMoney.AccessAndBusinessLayer.Transactions
{
    public class CreateTransactions
    {
        public bool AddSingleTransaction(
            decimal amount,
            string description, 
            DateTime date,
            int userId)
        {
            try
            {
                using (var db = new CivMoneyContext())
                {
                    var transaction = new Transaction
                    {
                        Amount = amount,
                        Description = description,
                        Date = date,
                        UserId = userId
                    };
                    db.Transactions.Add(transaction);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
