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
    public class CreateTransactionsTests
    {
        private Mock<DbSet<Transaction>> _mockDbSetTransaction;
        private Mock<DbSet<User>> _mockDbSetUser;
        private Mock<CivMoneyContext> _mockcivMoneyContext;
        private CreateTransactions _createTransactionsService;

        [TestInitialize]
        public void Setup()
        {
            // arrange
            _mockDbSetUser = DataBaseMockingHelpers.GetMockDbSetUser();
            _mockDbSetTransaction = DataBaseMockingHelpers.GetMockDbSetTransaction();
            _mockcivMoneyContext = DataBaseMockingHelpers.GetMockCivMoneyContext(_mockDbSetTransaction, _mockDbSetUser);
            _createTransactionsService =
                new CreateTransactions(DataBaseMockingHelpers.GetMockCivMoneyContextFactoryObject(_mockcivMoneyContext.Object));
        }

        [TestMethod]
        public void AddSingleTransaction_ShouldCallAddOnMockedDbSetForTransactionsWithDescriptionTEST_TimesOnce()
        {
            // act
            _createTransactionsService.AddSingleTransaction(1.0m, "TEST", new DateTime(2000, 1, 1), 1);

            // assert
            _mockDbSetTransaction.Verify(
                x => x.Add(It.Is<Transaction>(
                    z => z.Amount == 1.0m && z.Description == "TEST" && z.Date == new DateTime(2000, 1, 1) && z.UserId == 1)), 
                Times.Once);
        }

        [TestMethod]
        public void AddSingleTransaction_ShouldCallSaveOnMockedCivMoneyContext_TimesOnce()
        {
            // act
            _createTransactionsService.AddSingleTransaction(1.0m, "TEST", new DateTime(2000, 1, 1), 1);

            // assert
            _mockcivMoneyContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
