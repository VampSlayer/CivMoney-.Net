using CivMoney.DataBaseLayer;
using System;
using System.Collections.Generic;

namespace CivMoney.AccessAndBusinessLayer.Contracts.Transactions
{
    public interface ITransactionsTotalsService
    {
        decimal GetTotalForDateForUser(DateTime date, int userId);
        decimal GetTotalForWeekForUser(DateTime date, int userId);
        decimal GetTotalForMonthForUser(DateTime date, int userId);
        decimal GetTotalForYearForUser(DateTime date, int userId);
        List<Transaction> GetDailyTotalsForMonthForUser(DateTime date, int userId);
        List<Transaction> GetWeeklyTotalsForMonthForUser(DateTime date, int userId);
        List<Transaction> GetYearlyTotalsForUser(DateTime date, int userId);
        List<Transaction> GetMonthlyTotalsForYearForUser(DateTime date, int userId);
    }
}