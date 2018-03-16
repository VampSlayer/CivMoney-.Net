using CivMoney.DataBaseLayer;
using CivMoney.DataBaseLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivMoney.AccessAndBusinessLayer.Transactions
{
    public class GetTransactions
    {
        private CivMoneyContext _civMoneyContext;

        public GetTransactions(ICivMoneyContextFactory civMoneyContextFactory)
        {
            _civMoneyContext = civMoneyContextFactory.GetContext();
        }

        public List<Transaction> GetTransactionsForDateForUser(
            DateTime date, 
            int userId)
        {
            var transactionsForDate = _civMoneyContext.Transactions.Where(transactions => transactions.Date == date && transactions.UserId == userId).ToList();

            return transactionsForDate;
        }

        public List<Transaction> GetIncomesForDateForUser()
        {
            //TODO
            return new List<Transaction>();
        }

        public List<Transaction> GetExpenesForDateForUser()
        {
            //TODO
            return new List<Transaction>();
        }

        public List<Transaction> GetTransactionsForDateRangeForUser()
        {
            //TODO
            return new List<Transaction>();
        }

        public List<Transaction> GetIncomesForDateRangeForUser()
        {
            //TODO
            return new List<Transaction>();
        }

        public List<Transaction> GetExpensesForDateRangeForUser()
        {
            //TODO
            return new List<Transaction>();
        }
    }
}
