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
        public void AddSingleTransaction_ShouldCallAddOnMockedDbSetForTransactionsWithDescriptionTEST_TimesOnce_AndShouldReturnTransactionId0()
        {
            // act
           var transactionId = _createTransactionsService.AddSingleTransaction(1.0m, "TEST", new DateTime(2000, 1, 1), 1);

            // assert
            _mockDbSetTransaction.Verify(
                x => x.Add(It.Is<Transaction>(
                    z => z.Amount == 1.0m && z.Description == "TEST" && z.Date == new DateTime(2000, 1, 1) && z.UserId == 1)), 
                Times.Once);
            Assert.AreEqual(0, transactionId);
        }

        [TestMethod]
        public void AddSingleTransaction_ShouldCallSaveOnMockedCivMoneyContext_TimesOnce()
        {
            // act
            _createTransactionsService.AddSingleTransaction(1.0m, "TEST", new DateTime(2000, 1, 1), 1);

            // assert
            _mockcivMoneyContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        [DataRow(4)]
        [DataRow(6)]
        [DataRow(9)]
        [DataRow(11)]
        public void AddMonthlyIncomesAndExpenesForUser_ShouldCallAddOnMockedDbSetForMonthsWith30Days_60Times(int month)
        {
            // act
            var isSuccessful = _createTransactionsService.AddMonthlyIncomesAndExpenesForUser(new DateTime(2000, month, 1), 3000, 3000, 0);

            // assert
            _mockDbSetTransaction.Verify(x => x.Add(It.IsAny<Transaction>()), Times.Exactly(60));
            Assert.IsTrue(isSuccessful);
        }

        [TestMethod]
        [DataRow(4)]
        [DataRow(6)]
        [DataRow(9)]
        [DataRow(11)]
        public void AddMonthlyIncomesAndExpenesForUser_ShouldCallAddOnMockedDbSetForMonthsWith30DaysForIncomesAndExpensesEachFor100AndMinus100_30Times(int month)
        {
            // act
            var isSuccessful = _createTransactionsService.AddMonthlyIncomesAndExpenesForUser(new DateTime(2000, month, 1), 3000, 3000, 0);

            // assert
            _mockDbSetTransaction.Verify(x => x.Add(It.Is<Transaction>(y => y.Amount == 100)), Times.Exactly(30));
            _mockDbSetTransaction.Verify(x => x.Add(It.Is<Transaction>(y => y.Amount == -100)), Times.Exactly(30));
            Assert.IsTrue(isSuccessful);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        [DataRow(12)]
        public void AddMonthlyIncomesAndExpenesForUser_ShouldCallAddOnMockedDbSetForMonthsWith31Days_62Times(int month)
        {
            // act
            var isSuccessful = _createTransactionsService.AddMonthlyIncomesAndExpenesForUser(new DateTime(2000, month, 1), 3000, 300, 0);

            // assert
            _mockDbSetTransaction.Verify(x => x.Add(It.IsAny<Transaction>()), Times.Exactly(62));
            Assert.IsTrue(isSuccessful);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        [DataRow(12)]
        public void AddMonthlyIncomesAndExpenesForUser_ShouldCallAddOnMockedDbSetForMonthsWith31DaysForIncomesAndExpensesEachFor100AndMinus100_31Times(int month)
        {
            // act
            var isSuccessful = _createTransactionsService.AddMonthlyIncomesAndExpenesForUser(new DateTime(2000, month, 1), 3100, 3100, 0);

            // assert
            _mockDbSetTransaction.Verify(x => x.Add(It.Is<Transaction>(y => y.Amount == 100 && y.Description == "Monthly Incomes")), Times.Exactly(31));
            _mockDbSetTransaction.Verify(x => x.Add(It.Is<Transaction>(y => y.Amount == -100 && y.Description == "Monthly Expenses")), Times.Exactly(31));
            Assert.IsTrue(isSuccessful);
        }

        [TestMethod]
        public void AddMonthlyIncomesAndExpenesForUser_ShouldCallAddOnMockedDbSetForMonthsWith28DaysFor2018_56Times()
        {
            // act
            var isSuccessful = _createTransactionsService.AddMonthlyIncomesAndExpenesForUser(new DateTime(2018, 2, 1), 3000, 300, 0);

            // assert
            _mockDbSetTransaction.Verify(x => x.Add(It.IsAny<Transaction>()), Times.Exactly(56));
            Assert.IsTrue(isSuccessful);
        }

        [TestMethod]
        public void AddMonthlyIncomesAndExpenesForUser_ShouldCallAddOnMockedDbSetForMonthsWith28DaysFor2018ForIncomesAndExpensesEachFor100AndMinus100_28Times()
        {
            // act
            var isSuccessful = _createTransactionsService.AddMonthlyIncomesAndExpenesForUser(new DateTime(2018, 2, 1), 2800, 2800, 0);

            // assert
            _mockDbSetTransaction.Verify(x => x.Add(It.Is<Transaction>(y => y.Amount == 100 && y.Description == "Monthly Incomes")), Times.Exactly(28));
            _mockDbSetTransaction.Verify(x => x.Add(It.Is<Transaction>(y => y.Amount == -100 && y.Description == "Monthly Expenses")), Times.Exactly(28));
            Assert.IsTrue(isSuccessful);
        }

        [TestMethod]
        public void AddMonthlyIncomesAndExpenesForUser_ShouldCallAddOnMockedDbSetForMonthsWith29DaysFor2016_58Times()
        {
            // act
            var isSuccessful = _createTransactionsService.AddMonthlyIncomesAndExpenesForUser(new DateTime(2016, 2, 1), 3000, 300, 0);

            // assert
            _mockDbSetTransaction.Verify(x => x.Add(It.IsAny<Transaction>()), Times.Exactly(58));
            Assert.IsTrue(isSuccessful);
        }

        [TestMethod]
        public void AddMonthlyIncomesAndExpenesForUser_ShouldCallAddOnMockedDbSetForMonthsWith29DaysFor2016ForIncomesAndExpensesEachFor100AndMinus100_28Times()
        {
            // act
            var isSuccessful = _createTransactionsService.AddMonthlyIncomesAndExpenesForUser(new DateTime(2016, 2, 1), 2900, 2900, 0);

            // assert
            _mockDbSetTransaction.Verify(x => x.Add(It.Is<Transaction>(y => y.Amount == 100 && y.Description == "Monthly Incomes")), Times.Exactly(29));
            _mockDbSetTransaction.Verify(x => x.Add(It.Is<Transaction>(y => y.Amount == -100 && y.Description == "Monthly Expenses")), Times.Exactly(29));
            Assert.IsTrue(isSuccessful);
        }
    }
}
