using CivMoney.DataBaseLayer;
using System;
using System.Collections.Generic;

namespace CivMoney.AccessAndBusinessLayer.Contracts
{
    public interface IReadTransactionService
    {
        List<Transaction> GetTransactionsForDateForUser(
            DateTime date,
            int userId);

        List<Transaction> GetIncomesForDateForUser(
            DateTime date,
            int userId);

        List<Transaction> GetExpenesForDateForUser(
            DateTime date,
            int userId);

        List<Transaction> GetTransactionsForDateRangeForUser(
            DateTime firstDate,
            DateTime secondDate,
            int userId);

        List<Transaction> GetIncomesForDateRangeForUser(
            DateTime firstDate,
            DateTime secondDate,
            int userId);

        List<Transaction> GetExpensesForDateRangeForUser(
            DateTime firstDate,
            DateTime secondDate,
            int userId);
    }
}
