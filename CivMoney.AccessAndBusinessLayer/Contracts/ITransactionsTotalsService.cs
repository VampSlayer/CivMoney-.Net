using System;

namespace CivMoney.AccessAndBusinessLayer.Contracts.Transactions
{
    public interface ITransactionsTotalsService
    {
        decimal GetTotalForDateForUser(DateTime date, int userId);
        decimal GetTotalForWeekForUser(DateTime date, int userId);
    }
}