using CivMoney.AccessAndBusinessLayer.Contracts.Transactions;
using CivMoney.AccessAndBusinessLayer.Tests.TestHelpers;
using CivMoney.AccessAndBusinessLayer.Transactions;
using CivMoney.DataBaseLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

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
            //act
            var actualTotalForDay = _transactionsTotalService.GetTotalForDateForUser(new DateTime(2000, 1, 1), 0);

            Assert.AreEqual(0m, actualTotalForDay);
        }
    }
}
