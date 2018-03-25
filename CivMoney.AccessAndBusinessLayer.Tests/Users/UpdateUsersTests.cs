using CivMoney.AccessAndBusinessLayer.Contracts;
using CivMoney.AccessAndBusinessLayer.Tests.TestHelpers;
using CivMoney.AccessAndBusinessLayer.Users;
using CivMoney.DataBaseLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivMoney.AccessAndBusinessLayer.Tests.Users
{
    [TestClass]
    public class UpdateUsersTests
    {
        private Mock<DbSet<Transaction>> _mockDbSetTransaction;
        private Mock<DbSet<User>> _mockDbSetUser;
        private Mock<CivMoneyContext> _mockcivMoneyContext;
        private IUpdateUsersService _updateUsersService;

        [TestInitialize]
        public void Setup()
        {
            // arrange
            _mockDbSetUser = DataBaseMockingHelpers.GetMockDbSetUser();
            _mockDbSetTransaction = DataBaseMockingHelpers.GetMockDbSetTransaction();
            _mockcivMoneyContext = DataBaseMockingHelpers.GetMockCivMoneyContext(_mockDbSetTransaction, _mockDbSetUser);
            _updateUsersService =
                new UpdateUsers(DataBaseMockingHelpers.GetMockCivMoneyContextFactoryObject(_mockcivMoneyContext.Object));
        }

        [TestMethod]
        public void UpdateUserCurrency_ShouldCallSaveChangesOnMockCivMoneyContext_TimesOnceAndReturnTrue()
        {
            // act
            var isSuccesful = _updateUsersService.UpdateUserCurrency(0, "GBP");

            // assert
            _mockcivMoneyContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.IsTrue(isSuccesful);
        }

        [TestMethod]
        public void UpdateUserCurrency_ShouldUpdateSeededUserCurrencyFromCHF_ToGBPAndReturnTrue()
        {
            // act
            var isSuccesful = _updateUsersService.UpdateUserCurrency(0, "GBP");

            // assert
            Assert.IsTrue(_mockDbSetUser.Object.FirstAsync().Result.Currency == "GBP");
            Assert.IsTrue(isSuccesful);
        }

        [TestMethod]
        public void UpdateUserCurrency_ShouldNotUpdateSeededUserCurrencyFromCHFAsUserIdIsNotInTable_ReturnFalse()
        {
            // act
            var isSuccesful = _updateUsersService.UpdateUserCurrency(999, "GBP");

            // assert
            Assert.IsTrue(_mockDbSetUser.Object.FirstAsync().Result.Currency == "CHF");
            Assert.IsFalse(isSuccesful);
        }
    }
}
