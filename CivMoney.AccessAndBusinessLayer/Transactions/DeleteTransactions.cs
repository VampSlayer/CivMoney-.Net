using CivMoney.AccessAndBusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivMoney.AccessAndBusinessLayer.Transactions
{
    public class DeleteTransactions : IDeleteTransactionService
    {
        public bool DeleteTransactionForUser(int transactionId, int userId)
        {
            return false;
        }
    }
}
