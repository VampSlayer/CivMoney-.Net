using CivMoney.AccessAndBusinessLayer.Contracts;
using CivMoney.DataBaseLayer;
using CivMoney.DataBaseLayer.Contracts;
using System.Linq;

namespace CivMoney.AccessAndBusinessLayer.Transactions
{
    public class DeleteTransactions : IDeleteTransactionService
    {
        private CivMoneyContext _civMoneyContext;

        public DeleteTransactions(ICivMoneyContextFactory civMoneyContextFactory)
        {
            _civMoneyContext = civMoneyContextFactory.GetContext();
        }

        public bool DeleteTransactionForUser(int transactionId, int userId)
        {
            var transactionToBeRemoved =
                _civMoneyContext.Transactions.Where(transaction => transaction.Id == transactionId).SingleOrDefault();

            if(transactionToBeRemoved != null)
            {
                _civMoneyContext.Transactions.Remove(transactionToBeRemoved);
                _civMoneyContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
