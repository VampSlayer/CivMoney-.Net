using CivMoney.AccessAndBusinessLayer.Contracts.Transactions;
using CivMoney.DataBaseLayer;
using CivMoney.DataBaseLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CivMoney.AccessAndBusinessLayer.Transactions
{
    public class TransactionTotals : ITransactionsTotalsService
    {
        private CivMoneyContext _civMoneyContext;

        public TransactionTotals(ICivMoneyContextFactory civMoneyContextFactory)
        {
            _civMoneyContext = civMoneyContextFactory.GetContext();
        }

        public decimal GetTotalForDateForUser(DateTime date, int userId)
        {
            var dayTotal = _civMoneyContext.Transactions.Where(transactions => transactions.Date == date && transactions.UserId == userId).Select(x => x.Amount).Sum();

            return dayTotal;
        }

        public decimal GetTotalForWeekForUser(DateTime date, int userId)
        {
            var startOfWeek = StartOfWeek(date);

            var endOfWeek = startOfWeek.AddDays(7);

            var weekTotal = _civMoneyContext.
                Transactions.
                Where(transactions => transactions.Date > startOfWeek && transactions.Date < endOfWeek && transactions.UserId == userId).
                Select(x => x.Amount).
                Sum();

            return weekTotal;
        }

        public decimal GetTotalForMonthForUser(DateTime date, int userId)
        {
            var startOfMonth = new DateTime(date.Year, date.Month, 1);

            var endOfMonth = new DateTime(date.Year, date.Month + 1, 1).AddDays(-1);

            var monthTotal = _civMoneyContext.
                Transactions.
                Where(transactions => transactions.Date > startOfMonth && transactions.Date < endOfMonth && transactions.UserId == userId).
                Select(x => x.Amount).
                Sum();

            return monthTotal;
        }

        public decimal GetTotalForYearForUser(DateTime date, int userId)
        {
            var startOfYear = new DateTime(date.Year, 1, 1);

            var endOfYear = new DateTime(date.Year, 12, 31);

            var yearTotal = _civMoneyContext.
                Transactions.
                Where(transactions => transactions.Date > startOfYear && transactions.Date < endOfYear && transactions.UserId == userId).
                Select(x => x.Amount).
                Sum();

            return yearTotal;
        }

        public List<Transaction> GetDailyTotalsForMonthForUser(DateTime date, int userId)
        {
            //TODO
            return new List<Transaction>();
        }

        public List<Transaction> GetWeeklyTotalsForMonthForUser(DateTime date, int userId)
        {
            //TODO
            return new List<Transaction>();
        }

        public List<Transaction> GetMonthlyTotalsForYearForUser(DateTime date, int userId)
        {
            //TODO
            return new List<Transaction>();
        }

        public List<Transaction> GetYearlyTotalsForUser(DateTime date, int userId)
        {
            //TODO
            return new List<Transaction>();
        }

        private DateTime StartOfWeek(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;

            return date.AddDays(-1 * diff).Date;
        }
    }
}
