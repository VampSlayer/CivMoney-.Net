using System;
using CivMoney.DataBaseLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CivMoney.AccessAndBusinessLayer.Tests
{
    [TestClass]
    public class UsersAccessTests
    {
        [TestMethod]
        public void AddUser()
        {
            var usersAccess = new UsersAccess(new CivMoneyContextFactory());

            var random = new Random();

            var actualUserName = "User" + random.Next();

            var userId = usersAccess.AddUser(actualUserName, "password", "CHF");

            var userNameForReturnedUserId = usersAccess.GetUserName(userId);

            Assert.AreEqual(actualUserName, userNameForReturnedUserId);
        }
    }
}
