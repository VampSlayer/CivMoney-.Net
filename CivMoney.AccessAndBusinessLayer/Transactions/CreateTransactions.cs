using CivMoney.DataBaseLayer;
using CivMoney.DataBaseLayer.Contracts;
using System;

namespace CivMoney.AccessAndBusinessLayer.Transactions
{
    public class CreateTransactions
    {
        private CivMoneyContext _civMoneyContext;

        public CreateTransactions(ICivMoneyContextFactory civMoneyContextFactory)
        {
            _civMoneyContext = civMoneyContextFactory.GetContext();
        }

        public bool AddSingleTransaction(
            decimal amount,
            string description, 
            DateTime date,
            int userId)
        {
            try
            {
                var transaction = new Transaction
                {
                    Amount = amount,
                    Description = description,
                    Date = date,
                    UserId = userId
                };
                _civMoneyContext.Transactions.Add(transaction);
                _civMoneyContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddMonthlyIncomesAndExpenesForUser(
            DateTime date, 
            decimal totalIncomes,
            decimal totalExpenes,
            int userId)
        {
            //TODO
            return true;
        }
    }
}
