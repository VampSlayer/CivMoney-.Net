using CivMoney.DataBaseLayer;
using CivMoney.DataBaseLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<Transaction> GetIncomesForDateForUser(
            DateTime date,
            int userId)
        {
            var incomesForDate = _civMoneyContext.Transactions.Where(transactions => transactions.Date == date && transactions.UserId == userId && transactions.Amount > 0m).ToList();

            return incomesForDate;
        }

        public List<Transaction> GetExpenesForDateForUser(
            DateTime date,
            int userId)
        {
            var expensesForDate = _civMoneyContext.Transactions.Where(transactions => transactions.Date == date && transactions.UserId == userId && transactions.Amount < 0m).ToList();

            return expensesForDate;
        }

        public List<Transaction> GetTransactionsForDateRangeForUser(
            DateTime firstDate,
            DateTime secondDate,
            int userId)
        {
            var transactionsForDate = _civMoneyContext.Transactions.Where(transactions => transactions.Date > firstDate && transactions.Date < secondDate && transactions.UserId == userId).ToList();

            return transactionsForDate;
        }

        public List<Transaction> GetIncomesForDateRangeForUser(
            DateTime firstDate,
            DateTime secondDate,
            int userId)
        {
            var incomesForDate = _civMoneyContext.Transactions.Where(transactions => transactions.Date > firstDate && transactions.Date < secondDate && transactions.UserId == userId && transactions.Amount > 0m).ToList();

            return incomesForDate;
        }

        public List<Transaction> GetExpensesForDateRangeForUser(
            DateTime firstDate,
            DateTime secondDate,
            int userId)
        {
            var expensesForDate = _civMoneyContext.Transactions.Where(transactions => transactions.Date > firstDate && transactions.Date < secondDate && transactions.UserId == userId && transactions.Amount < 0m).ToList();

            return expensesForDate;
        }
    }
}
