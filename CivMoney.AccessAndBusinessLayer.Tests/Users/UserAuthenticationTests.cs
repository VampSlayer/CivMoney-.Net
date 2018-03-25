using CivMoney.AccessAndBusinessLayer.Authentication;
using CivMoney.AccessAndBusinessLayer.Contracts;
using CivMoney.AccessAndBusinessLayer.Tests.TestHelpers;
using CivMoney.DataBaseLayer;
using DevOne.Security.Cryptography.BCrypt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;

namespace CivMoney.AccessAndBusinessLayer.Tests.Users
{
    [TestClass]
    public class UserAuthenticationTests
    {
        private Mock<DbSet<Transaction>> _mockDbSetTransaction;
        private Mock<DbSet<User>> _mockDbSetUser;
        private Mock<CivMoneyContext> _mockcivMoneyContext;
        private IUserAuthenticationService _userAuthenticationService;

        [TestInitialize]
        public void Setup()
        {
            // arrange
            _mockDbSetUser = DataBaseMockingHelpers.GetMockDbSetUser();
            var seededUsers = new List<User>
            {
                new User { Id = 0, Currency = "CHF", PasswordHash = BCryptHelper.HashPassword("password", BCryptHelper.GenerateSalt()), UserName = "User1" }
            };
            _mockDbSetUser.SetupData(seededUsers);
            _mockDbSetTransaction = DataBaseMockingHelpers.GetMockDbSetTransaction();
            _mockcivMoneyContext = DataBaseMockingHelpers.GetMockCivMoneyContext(_mockDbSetTransaction, _mockDbSetUser);
            _userAuthenticationService = new UserAuthentication(DataBaseMockingHelpers.GetMockCivMoneyContextFactoryObject(_mockcivMoneyContext.Object));
        }

        [TestMethod]
        public void DoesUserExist_FindsIfUserNameIsStoredWithinMockUsersDbSet_ReturnsTrueForUser1()
        {
            var doesUserExist = _userAuthenticationService.DoesUserExist("User1");

            Assert.IsTrue(doesUserExist);
        }

        [TestMethod]
        public void DoesUserExist_FindsIfUserNameIsStoredWithinMockUsersDbSet_ReturnsFlaseForUser0()
        {
            var doesUserExist = _userAuthenticationService.DoesUserExist("User0");

            Assert.IsFalse(doesUserExist);
        }

        [TestMethod]
        public void VerifyUserLoginDetails_VerifiesUserIsValidUserByCheckingUserNameAndPasswordHashStoredInDataBase_ReturnsTrueWhenUserNameAndPasswordIsCorrect()
        {
            var isUserLoginDetailsValid = _userAuthenticationService.VerifyUserLoginDetails("User1", "password");

            Assert.IsTrue(isUserLoginDetailsValid);
        }

        [TestMethod]
        public void VerifyUserLoginDetails_VerifiesUserIsNotValidUserByCheckingUserNameAndPasswordHashStoredInDataBase_ReturnsFlaseWhenUserNameAndPasswordIsNotCorrect()
        {
            var isUserLoginDetailsValid = _userAuthenticationService.VerifyUserLoginDetails("User0", "notCorrectPassword");

            Assert.IsFalse(isUserLoginDetailsValid);
        }

        [TestMethod]
        public void VerifyUserLoginDetails_VerifiesUserIsNotValidUserByCheckingUserNameAndPasswordHashStoredInDataBase_ReturnsFlaseWhenUserNameIsCorrectButPasswordIsNot()
        {
            var isUserLoginDetailsValid = _userAuthenticationService.VerifyUserLoginDetails("User1", "notCorrectPassword");

            Assert.IsFalse(isUserLoginDetailsValid);
        }
    }
}
