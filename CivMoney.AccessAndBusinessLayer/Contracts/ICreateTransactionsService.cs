using System;
namespace CivMoney.AccessAndBusinessLayer.Contracts
{
    public interface ICreateTransactionsService
    {
        int AddSingleTransaction(decimal amount, string description, DateTime date, int userId);
        bool AddMonthlyIncomesAndExpenesForUser(DateTime date, decimal totalIncomes, decimal totalExpenes, int userId);
    }
}
