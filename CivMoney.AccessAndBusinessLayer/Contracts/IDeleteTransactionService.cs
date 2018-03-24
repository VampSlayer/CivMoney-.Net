using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivMoney.AccessAndBusinessLayer.Contracts
{
    public interface IDeleteTransactionService
    {
        bool DeleteTransactionForUser(int transactionId, int userId);
    }
}
