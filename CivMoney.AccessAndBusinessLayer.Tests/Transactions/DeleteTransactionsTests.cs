using CivMoney.AccessAndBusinessLayer.Contracts;
using CivMoney.AccessAndBusinessLayer.Tests.TestHelpers;
using CivMoney.AccessAndBusinessLayer.Transactions;
using CivMoney.DataBaseLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;

namespace CivMoney.AccessAndBusinessLayer.Tests.Transactions
{
    [TestClass]
    public class DeleteTransactionsTests
    {
        private Mock<DbSet<Transaction>> _mockDbSetTransaction;
        private Mock<DbSet<User>> _mockDbSetUser;
        private Mock<CivMoneyContext> _mockcivMoneyContext;
        private IDeleteTransactionService _deleteTransactionService;

        [TestInitialize]
        public void Setup()
        {
            // arrange
            _mockDbSetUser = DataBaseMockingHelpers.GetMockDbSetUser();
            _mockDbSetTransaction = DataBaseMockingHelpers.GetMockDbSetTransaction();
            _mockcivMoneyContext = DataBaseMockingHelpers.GetMockCivMoneyContext(_mockDbSetTransaction, _mockDbSetUser);
            _deleteTransactionService =
                new DeleteTransactions(DataBaseMockingHelpers.GetMockCivMoneyContextFactoryObject(_mockcivMoneyContext.Object));
        }

        [TestMethod]
        public void DeleteTransactionForUser_RemovesTransactionWithId0FromMockedDbSetTransactions_MockDbSetTransactionShouldHaveCount0_ReturnedTrue()
        {
            // act
            var isDeleted = _deleteTransactionService.DeleteTransactionForUser(0, 0);

            _mockcivMoneyContext.Verify(x => x.SaveChanges(), Times.Once);
            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public void DeleteTransactionForUser_DoesNotRemoveTransactionWithId999FromMockedDbSetTransactions_MockDbSetTransactionShouldHaveCount1_ReturnedFalse()
        {
            // act
            var isDeleted = _deleteTransactionService.DeleteTransactionForUser(999, 0);

            _mockcivMoneyContext.Verify(x => x.SaveChanges(), Times.Never);
            Assert.IsFalse(isDeleted);
        }
    }
}
