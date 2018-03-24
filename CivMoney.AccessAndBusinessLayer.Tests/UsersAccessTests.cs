using System;
using System.Data.Entity;
using CivMoney.AccessAndBusinessLayer.Tests.TestHelpers;
using CivMoney.DataBaseLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CivMoney.AccessAndBusinessLayer.Tests
{
    [TestClass]
    public class UsersAccessTests
    {
        private Mock<DbSet<Transaction>> _mockDbSetTransaction;
        private Mock<DbSet<User>> _mockDbSetUser;
        private Mock<CivMoneyContext> _mockcivMoneyContext;
        private UsersAccess _usersAccessService;

        [TestInitialize]
        public void Setup()
        {
            // arrange
            _mockDbSetUser = DataBaseMockingHelpers.GetMockDbSetUser();
            _mockDbSetTransaction = DataBaseMockingHelpers.GetMockDbSetTransaction();
            _mockcivMoneyContext = DataBaseMockingHelpers.GetMockCivMoneyContext(_mockDbSetTransaction, _mockDbSetUser);
            _usersAccessService =
                new UsersAccess(DataBaseMockingHelpers.GetMockCivMoneyContextFactoryObject(_mockcivMoneyContext.Object));
        }

        [TestMethod]
        public void AddUserCallsSaveOnMockedCivMoneyContext_TimesOnce()
        {
            // act
            var Id = _usersAccessService.AddUser("User0", "password", "CHF");

            // assert
            _mockcivMoneyContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void AddUser_CallsAddOnMockedDbSetUserWithUserNameUser0_TimesOnce_AndReturnsId0()
        {
            // act
            var userId = _usersAccessService.AddUser("User0", "password", "CHF");

            // assert
            _mockDbSetUser.Verify(x => x.Add(It.Is<User>(user => user.UserName == "User0")), Times.Once);
            Assert.AreEqual(0, userId);
        }

        [TestMethod]
        public void GetUserIdFromUserName_ShouldGetUserIdFromSeededUserInMockedDbSet_ReturnsZero()
        {
            // act
            var Id = _usersAccessService.GetUserIdFromUserName("User1");

            // assert
            Assert.AreEqual(0, Id);
        }

        [TestMethod]
        public void GetUserCurrency_ShouldGetUserCurrencyFromSeededUserInMockedDbSet_ReturnsCHF()
        {
            // act
            var returnedUserCurrency = _usersAccessService.GetUserCurrency(0);

            // assert
            Assert.AreEqual(returnedUserCurrency, "CHF");
        }
    }
}
