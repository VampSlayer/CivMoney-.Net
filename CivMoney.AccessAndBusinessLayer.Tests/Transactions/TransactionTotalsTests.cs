using CivMoney.AccessAndBusinessLayer.Contracts.Transactions;
using CivMoney.AccessAndBusinessLayer.Tests.TestHelpers;
using CivMoney.AccessAndBusinessLayer.Transactions;
using CivMoney.DataBaseLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CivMoney.AccessAndBusinessLayer.Tests.Transactions
{
    [TestClass]
    public class TransactionTotalsTests
    {
        private Mock<DbSet<Transaction>> _mockDbSetTransaction;
        private Mock<DbSet<User>> _mockDbSetUser;
        private Mock<CivMoneyContext> _mockcivMoneyContext;
        private ITransactionsTotalsService _transactionsTotalService;

        [TestInitialize]
        public void Setup()
        {
            // arrange
            _mockDbSetUser = DataBaseMockingHelpers.GetMockDbSetUser();
            _mockDbSetTransaction = DataBaseMockingHelpers.GetMockDbSetTransaction();
            _mockcivMoneyContext = DataBaseMockingHelpers.GetMockCivMoneyContext(_mockDbSetTransaction, _mockDbSetUser);
            _transactionsTotalService =
                new TransactionTotals(DataBaseMockingHelpers.GetMockCivMoneyContextFactoryObject(_mockcivMoneyContext.Object));
        }

        [TestMethod]
        public void GetTotalForDateForUser_ShoudlReturnSumForGivenDate20000101AndSeededTransactions_ReturnsTotalOf0()
        {
            // act
            var actualTotalForDay = _transactionsTotalService.GetTotalForDateForUser(new DateTime(2000, 1, 1), 0);

            Assert.AreEqual(0m, actualTotalForDay);
        }

        [TestMethod]
        public void GetTotalForWeekForUser_ShouldReturnSumForGivenWeekAroundDate20000101AndSeededTransactions_ReturnsTotalOf0()
        {
            // act
            var actualTotalForWeek = _transactionsTotalService.GetTotalForWeekForUser(new DateTime(2000, 1, 1), 0);

            Assert.AreEqual(0m, actualTotalForWeek);
        }

        [TestMethod]
        public void GetTotalForMonthForUser_ShouldReturnSumForJanuaryGivenDate20000101AndSeededTransactions_ReturnsTotalOf0()
        {
            // act
            var actualTotalForMonth = _transactionsTotalService.GetTotalForMonthForUser(new DateTime(2000, 1, 1), 0);

            Assert.AreEqual(0m, actualTotalForMonth);
        }

        [TestMethod]
        public void GetTotalForYearForUser_ShouldReturnSumForYear2000FromSeededTransactions_ReturnsTotalOf0()
        {
            // act
            var actutalTotalForYear = _transactionsTotalService.GetTotalForYearForUser(new DateTime(2000, 1, 1), 0);

            Assert.AreEqual(0m, actutalTotalForYear);
        }

        [TestMethod]
        public void GetDailyTotalsForMonthForUser_ShouldReturn31TransactionsOneValueForEachDayOfTheMonthForTheDate20000101_ReturnsCount31AndDayAsIdAndAmount100ForAll()
        {
            var seededTransactions = new List<Transaction>();

            for (int i = 1; i < 32; i++)
            {
                seededTransactions.Add(new Transaction { Id = i, Amount = 300.0m, Description = "Income", Date = new DateTime(2000, 1, i), UserId = 0 });
                seededTransactions.Add(new Transaction { Id = i * 100, Amount = -200.0m, Description = "Expense", Date = new DateTime(2000, 1, i), UserId = 0 });
            }

            _mockDbSetTransaction.SetupData(seededTransactions);

            //act
            var dailyTotalsForMonthForUser = _transactionsTotalService.GetDailyTotalsForMonthForUser(new DateTime(2000, 1, 1), 0);

            Assert.AreEqual(31, dailyTotalsForMonthForUser.Count());
            Assert.IsTrue(dailyTotalsForMonthForUser.All(x => x.Amount == 100m));

            for (int i = 1; i < 32; i++)
            {
                Assert.IsTrue(dailyTotalsForMonthForUser.Where(x => x.Id == i).SingleOrDefault().Date == new DateTime(2000, 1, i));
            }
        }

        [TestMethod]
        public void GetWeeklyTotalsForMonthForUser_ShouldReturnTheWeeklyTotalsForTheMonth1OfYear2018_ReturnsCountOf5AndExpectedTotals()
        {
            var seededTransactions = new List<Transaction>();

            for (int i = 1; i < 32; i++)
            {
                seededTransactions.Add(new Transaction { Id = i, Amount = 300.0m, Description = "Income", Date = new DateTime(2018, 1, i), UserId = 0 });
                seededTransactions.Add(new Transaction { Id = i * 100, Amount = -200.0m, Description = "Expense", Date = new DateTime(2018, 1, i), UserId = 0 });
            }

            _mockDbSetTransaction.SetupData(seededTransactions);

            //act
            var weeklyTotalsForMonth = _transactionsTotalService.GetWeeklyTotalsForMonthForUser(new DateTime(2018, 1, 1), 0);

            Assert.AreEqual(5, weeklyTotalsForMonth.Count);

            Assert.AreEqual(weeklyTotalsForMonth[0].Date, new DateTime(2018, 1, 1));
            Assert.AreEqual(weeklyTotalsForMonth[0].Amount, 700m);

            Assert.AreEqual(weeklyTotalsForMonth[4].Date, new DateTime(2018, 1, 29));
            Assert.AreEqual(weeklyTotalsForMonth[4].Amount, 300m);
        }

        [TestMethod]
        public void GetWeeklyTotalsForMonthForUser_ShouldReturnTheWeeklyTotalsForTheMonth2OfYear2018_ReturnsCountOf5AndExpectedTotals()
        {
            var seededTransactions = new List<Transaction>();

            for (int i = 1; i < 29; i++)
            {
                seededTransactions.Add(new Transaction { Id = i, Amount = 300.0m, Description = "Income", Date = new DateTime(2018, 2, i), UserId = 0 });
                seededTransactions.Add(new Transaction { Id = i * 100, Amount = -200.0m, Description = "Expense", Date = new DateTime(2018, 2, i), UserId = 0 });
            }

            _mockDbSetTransaction.SetupData(seededTransactions);

            //act
            var weeklyTotalsForMonth = _transactionsTotalService.GetWeeklyTotalsForMonthForUser(new DateTime(2018, 2, 1), 0);

            Assert.AreEqual(5, weeklyTotalsForMonth.Count);

            Assert.AreEqual(new DateTime(2018, 1, 29), weeklyTotalsForMonth[0].Date);
            Assert.AreEqual(weeklyTotalsForMonth[0].Amount, 400m);

            Assert.AreEqual(new DateTime(2018, 2, 5), weeklyTotalsForMonth[1].Date);
            Assert.AreEqual(weeklyTotalsForMonth[1].Amount, 700m);

            Assert.AreEqual(new DateTime(2018, 2, 26), weeklyTotalsForMonth[4].Date);
            Assert.AreEqual(weeklyTotalsForMonth[4].Amount, 300m);
        }

        [TestMethod]
        public void GetWeeklyTotalsForMonthForUser_ShouldReturnTheWeeklyTotalsForTheMonth2OfYear2010_ReturnsCountOf4AndExpectedTotals()
        {
            var seededTransactions = new List<Transaction>();

            for (int i = 1; i < 29; i++)
            {
                seededTransactions.Add(new Transaction { Id = i, Amount = 300.0m, Description = "Income", Date = new DateTime(2010, 2, i), UserId = 0 });
                seededTransactions.Add(new Transaction { Id = i * 100, Amount = -200.0m, Description = "Expense", Date = new DateTime(2010, 2, i), UserId = 0 });
            }

            _mockDbSetTransaction.SetupData(seededTransactions);

            //act
            var weeklyTotalsForMonth = _transactionsTotalService.GetWeeklyTotalsForMonthForUser(new DateTime(2010, 2, 1), 0);

            Assert.AreEqual(4, weeklyTotalsForMonth.Count);

            Assert.AreEqual(new DateTime(2010, 2, 1), weeklyTotalsForMonth[0].Date);
            Assert.AreEqual(weeklyTotalsForMonth[0].Amount, 700m);

            Assert.AreEqual(new DateTime(2010, 2, 22), weeklyTotalsForMonth[3].Date);
            Assert.AreEqual(weeklyTotalsForMonth[3].Amount, 700m);
        }

        [TestMethod]
        public void GetWeeklyTotalsForMonthForUser_ShouldReturnTheWeeklyTotalsForTheMonth2OfYear2016_ReturnsCountOf5AndExpectedTotals()
        {
            var seededTransactions = new List<Transaction>();

            for (int i = 1; i < 30; i++)
            {
                seededTransactions.Add(new Transaction { Id = i, Amount = 300.0m, Description = "Income", Date = new DateTime(2016, 2, i), UserId = 0 });
                seededTransactions.Add(new Transaction { Id = i * 100, Amount = -200.0m, Description = "Expense", Date = new DateTime(2016, 2, i), UserId = 0 });
            }

            _mockDbSetTransaction.SetupData(seededTransactions);

            //act
            var weeklyTotalsForMonth = _transactionsTotalService.GetWeeklyTotalsForMonthForUser(new DateTime(2016, 2, 1), 0);

            Assert.AreEqual(5, weeklyTotalsForMonth.Count);

            Assert.AreEqual(new DateTime(2016, 2, 1), weeklyTotalsForMonth[0].Date);
            Assert.AreEqual(weeklyTotalsForMonth[0].Amount, 700m);

            Assert.AreEqual(new DateTime(2016, 2, 29), weeklyTotalsForMonth[4].Date);
            Assert.AreEqual(weeklyTotalsForMonth[4].Amount, 100m);
        }

        [TestMethod]
        public void GetMonthlyTotalsForYearForUser_ShouldGetUsersTransactionsForYearAndGroupByMonthFor2000WithMonth1And12_ReturnsCountTwoAndMonthAsIdAndAmount3100()
        {
            var seededTransactions = new List<Transaction>();

            for (int i = 1; i < 32; i++)
            {
                seededTransactions.Add(new Transaction { Id = i, Amount = 300.0m, Description = "Income", Date = new DateTime(2000, 1, i), UserId = 0 });
                seededTransactions.Add(new Transaction { Id = i * 100, Amount = -200.0m, Description = "Expense", Date = new DateTime(2000, 1, i), UserId = 0 });
            }

            for (int i = 1; i < 32; i++)
            {
                seededTransactions.Add(new Transaction { Id = i, Amount = 300.0m, Description = "Income", Date = new DateTime(2000, 12, i), UserId = 0 });
                seededTransactions.Add(new Transaction { Id = i * 100, Amount = -200.0m, Description = "Expense", Date = new DateTime(2000, 12, i), UserId = 0 });
            }

            _mockDbSetTransaction.SetupData(seededTransactions);

            //act
            var dailyTotalsForMonthForUser = _transactionsTotalService.GetMonthlyTotalsForYearForUser(new DateTime(2000, 1, 1), 0);

            Assert.AreEqual(2, dailyTotalsForMonthForUser.Count());
            Assert.IsTrue(dailyTotalsForMonthForUser.Where(x => x.Id == 1).Single().Amount == 3100m);
            Assert.IsTrue(dailyTotalsForMonthForUser.Where(x => x.Id == 12).Single().Amount == 3100m);
        }

        [TestMethod]
        public void GetYearlyTotalsForUser_ShouldGetUsersTransactionsForYearAndGroupByYearFor2000And2001_ReturnsCountTwoAndYearAsIdAndAmount3100()
        {
            var seededTransactions = new List<Transaction>();

            for (int i = 1; i < 32; i++)
            {
                seededTransactions.Add(new Transaction { Id = i, Amount = 300.0m, Description = "Income", Date = new DateTime(2000, 1, i), UserId = 0 });
                seededTransactions.Add(new Transaction { Id = i * 100, Amount = -200.0m, Description = "Expense", Date = new DateTime(2000, 1, i), UserId = 0 });
            }

            for (int i = 1; i < 32; i++)
            {
                seededTransactions.Add(new Transaction { Id = i, Amount = 300.0m, Description = "Income", Date = new DateTime(2001, 1, i), UserId = 0 });
                seededTransactions.Add(new Transaction { Id = i * 100, Amount = -200.0m, Description = "Expense", Date = new DateTime(2001, 1, i), UserId = 0 });
            }

            _mockDbSetTransaction.SetupData(seededTransactions);

            //act
            var dailyTotalsForMonthForUser = _transactionsTotalService.GetYearlyTotalsForUser(new DateTime(2000, 1, 1), 0);

            Assert.AreEqual(2, dailyTotalsForMonthForUser.Count());
            Assert.IsTrue(dailyTotalsForMonthForUser.Where(x => x.Id == 2000).Single().Amount == 3100m);
            Assert.IsTrue(dailyTotalsForMonthForUser.Where(x => x.Id == 2001).Single().Amount == 3100m);
        }
    }
}
