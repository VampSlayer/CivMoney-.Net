using CivMoney.AccessAndBusinessLayer.Tests.TestHelpers;
using CivMoney.AccessAndBusinessLayer.Transactions;
using CivMoney.DataBaseLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;
using System.Linq;

namespace CivMoney.AccessAndBusinessLayer.Tests.Transactions
{
    [TestClass]
    public class GetTransactionsTests
    {
        private Mock<DbSet<Transaction>> _mockDbSetTransaction;
        private Mock<DbSet<User>> _mockDbSetUser;
        private Mock<CivMoneyContext> _mockcivMoneyContext;
        private GetTransactions _getTransactionsService;

        [TestInitialize]
        public void Setup()
        {
            // arrange
            _mockDbSetUser = DataBaseMockingHelpers.GetMockDbSetUser();
            _mockDbSetTransaction = DataBaseMockingHelpers.GetMockDbSetTransaction();
            _mockcivMoneyContext = DataBaseMockingHelpers.GetMockCivMoneyContext(_mockDbSetTransaction, _mockDbSetUser);
            _getTransactionsService =
                new GetTransactions(DataBaseMockingHelpers.GetMockCivMoneyContextFactoryObject(_mockcivMoneyContext.Object));
        }

        [TestMethod]
        public void GetTransactionsForDateForUser_ShouldReturnTwoTransactionsFromSeededTransactionsForGivenDate_WithAmount1()
        {
            // act
            var returnedTransaction = _getTransactionsService.GetTransactionsForDateForUser(new DateTime(2000, 1, 1), 0);

            Assert.AreEqual(returnedTransaction.Count(), 2);
            Assert.AreEqual(returnedTransaction.First().Amount, 1.0m);
            Assert.AreEqual(returnedTransaction[1].Amount, -1.0m);
        }

        [TestMethod]
        public void GetIncomeForDateForUser_ShouldReturnOneTransactionFromSeededTransactionsForGivenDate_WithAmount1()
        {
            // act
            var returnedTransaction = _getTransactionsService.GetIncomesForDateForUser(new DateTime(2000, 1, 1), 0);

            Assert.AreEqual(returnedTransaction.Count(), 1);
            Assert.AreEqual(returnedTransaction.First().Amount, 1.0m);
        }

        [TestMethod]
        public void GetExpenesForDateForUser_ShouldReturnOneTransactionFromSeededTransactionsForGivenDate_WithAmountMinus1()
        {
            // act
            var returnedTransaction = _getTransactionsService.GetExpenesForDateForUser(new DateTime(2000, 1, 1), 0);

            Assert.AreEqual(returnedTransaction.Count(), 1);
            Assert.AreEqual(returnedTransaction.First().Amount, -1.0m);
        }

        [TestMethod]
        public void GetTransactionsForDateRangeForUser_ShouldReturnFourTransactionsFromSeededTransactionsForGivenDateRange()
        {
            // act
            var returnedTransactions = _getTransactionsService.GetTransactionsForDateRangeForUser(new DateTime(2000, 1, 1), new DateTime(2000, 12, 1), 0);

            Assert.AreEqual(returnedTransactions.Count(), 4);
        }

        [TestMethod]
        public void GetIncomesForDateRangeForUser_ShouldReturnTwoTransactionsFromSeededTransactionsForGivenDateRange_BothOfAmount1()
        {
            // act
            var returnedTransactions = _getTransactionsService.GetIncomesForDateRangeForUser(new DateTime(2000, 1, 1), new DateTime(2000, 12, 1), 0);

            Assert.AreEqual(returnedTransactions.Count(), 2);
            Assert.IsTrue(returnedTransactions.All(x => x.Amount == 1.0m));
        }

        [TestMethod]
        public void GetExpensesForDateRangeForUser_ShouldReturnTwoTransactionsFromSeededTransactionsForGivenDateRange_BothOfAmountMinus1()
        {
            // act
            var returnedTransactions = _getTransactionsService.GetExpensesForDateRangeForUser(new DateTime(2000, 1, 1), new DateTime(2000, 12, 1), 0);

            Assert.AreEqual(returnedTransactions.Count(), 2);
            Assert.IsTrue(returnedTransactions.All(x => x.Amount == -1.0m));
        }
    }
}
