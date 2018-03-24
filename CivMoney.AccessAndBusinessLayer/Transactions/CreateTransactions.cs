using CivMoney.AccessAndBusinessLayer.Contracts;
using CivMoney.DataBaseLayer;
using CivMoney.DataBaseLayer.Contracts;
using System;

namespace CivMoney.AccessAndBusinessLayer.Transactions
{
    public class CreateTransactions : ICreateTransactionsService
    {
        private CivMoneyContext _civMoneyContext;

        public CreateTransactions(ICivMoneyContextFactory civMoneyContextFactory)
        {
            _civMoneyContext = civMoneyContextFactory.GetContext();
        }
        
        public int AddSingleTransaction(
            decimal amount,
            string description, 
            DateTime date,
            int userId)
        {
            var transaction = new Transaction
            {
                Amount = amount,
                Description = description,
                Date = date,
                UserId = userId
            };
            var transactionAdded = _civMoneyContext.Transactions.Add(transaction);
            _civMoneyContext.SaveChanges();

            return transactionAdded.Id;
        }

        public bool AddMonthlyIncomesAndExpenesForUser(
            DateTime date, 
            decimal totalIncomes,
            decimal totalExpenes,
            int userId)
        {
            try
            {
                var numberOfDaysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

                for (int i = 1; i < numberOfDaysInMonth + 1; i++)
                {
                    AddSingleTransaction(totalIncomes / numberOfDaysInMonth, "Monthly Incomes", new DateTime(date.Year, date.Month, i), userId);
                    AddSingleTransaction(-totalExpenes / numberOfDaysInMonth, "Monthly Expenses", new DateTime(date.Year, date.Month, i), userId);
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
