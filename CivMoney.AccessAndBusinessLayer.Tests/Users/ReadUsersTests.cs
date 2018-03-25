using CivMoney.AccessAndBusinessLayer.Contracts;
using CivMoney.AccessAndBusinessLayer.Tests.TestHelpers;
using CivMoney.AccessAndBusinessLayer.Users;
using CivMoney.DataBaseLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;

namespace CivMoney.AccessAndBusinessLayer.Tests.Users
{
    [TestClass]
    public class ReadUsersTests
    {
        private Mock<DbSet<Transaction>> _mockDbSetTransaction;
        private Mock<DbSet<User>> _mockDbSetUser;
        private Mock<CivMoneyContext> _mockcivMoneyContext;
        private IReadUsersService _readUsersService;

        [TestInitialize]
        public void Setup()
        {
            // arrange
            _mockDbSetUser = DataBaseMockingHelpers.GetMockDbSetUser();
            _mockDbSetTransaction = DataBaseMockingHelpers.GetMockDbSetTransaction();
            _mockcivMoneyContext = DataBaseMockingHelpers.GetMockCivMoneyContext(_mockDbSetTransaction, _mockDbSetUser);
            _readUsersService =
                new ReadUsers(DataBaseMockingHelpers.GetMockCivMoneyContextFactoryObject(_mockcivMoneyContext.Object));
        }

        [TestMethod]
        public void GetUserName_ShouldReturnUserNameFromSeededUserInMockedDbSetWhenGiveId0_ReturnsUser1()
        {
            // act
            var userName = _readUsersService.GetUserName(0);

            // assert
            Assert.AreEqual("User1", userName);
        }

        [TestMethod]
        public void GetUserIdFromUserName_ShouldGetUserIdFromSeededUserInMockedDbSet_ReturnsZero()
        {
            // act
            var id = _readUsersService.GetUserIdFromUserName("User1");

            // assert
            Assert.AreEqual(0, id);
        }

        [TestMethod]
        public void GetUserCurrency_ShouldGetUserCurrencyFromSeededUserInMockedDbSet_ReturnsCHF()
        {
            // act
            var returnedUserCurrency = _readUsersService.GetUserCurrency(0);

            // assert
            Assert.AreEqual(returnedUserCurrency, "CHF");
        }
    }
}
