using System.Data.Entity;
using CivMoney.AccessAndBusinessLayer.Contracts;
using CivMoney.AccessAndBusinessLayer.Tests.TestHelpers;
using CivMoney.AccessAndBusinessLayer.Users;
using CivMoney.DataBaseLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CivMoney.AccessAndBusinessLayer.Tests.Users
{
    [TestClass]
    public class CreateUsersTests
    {
        private Mock<DbSet<Transaction>> _mockDbSetTransaction;
        private Mock<DbSet<User>> _mockDbSetUser;
        private Mock<CivMoneyContext> _mockcivMoneyContext;
        private ICreateUsersService _usersAccessService;

        [TestInitialize]
        public void Setup()
        {
            // arrange
            _mockDbSetUser = DataBaseMockingHelpers.GetMockDbSetUser();
            _mockDbSetTransaction = DataBaseMockingHelpers.GetMockDbSetTransaction();
            _mockcivMoneyContext = DataBaseMockingHelpers.GetMockCivMoneyContext(_mockDbSetTransaction, _mockDbSetUser);
            _usersAccessService =
                new CreateUsers(DataBaseMockingHelpers.GetMockCivMoneyContextFactoryObject(_mockcivMoneyContext.Object));
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
        public void AddUser_IfUserNameAlreadyExistsInUsersTable_ReturnsMinus1AndAddOnMockDbSetIsNeverCalled()
        {
            // act
            var userId = _usersAccessService.AddUser("User1", "password", "CHF");

            _mockDbSetUser.Verify(x => x.Add(It.Is<User>(user => user.UserName == "User1")), Times.Never);
            Assert.AreEqual(-1, userId);
        }
    }
}
