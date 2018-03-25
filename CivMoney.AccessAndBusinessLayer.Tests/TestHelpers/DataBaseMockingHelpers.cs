using CivMoney.DataBaseLayer;
using CivMoney.DataBaseLayer.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace CivMoney.AccessAndBusinessLayer.Tests.TestHelpers
{
    static public class DataBaseMockingHelpers
    {
        public static Mock<DbSet<User>> GetMockDbSetUser()
        {
            var seededUsers = new List<User>
            {
                new User { Id = 0, Currency = "CHF", PasswordHash = "password", UserName = "User1" }
            };

            var usersMockSet = new Mock<DbSet<User>>().SetupData(seededUsers);

            return usersMockSet;
        }

        public static Mock<DbSet<Transaction>> GetMockDbSetTransaction()
        {
            var seededTransactions = new List<Transaction>()
            {
                new Transaction { Id = 0, Amount = 1.0m, Description = "First Income", Date = new DateTime(2000, 1, 1), UserId = 0 },
                new Transaction { Id = 1, Amount = -1.0m, Description = "First Expense", Date = new DateTime(2000, 1, 1), UserId = 0 },
                new Transaction { Id = 2, Amount = 1.0m, Description = "Second Income", Date = new DateTime(2000, 12, 1), UserId = 0 },
                new Transaction { Id = 3, Amount = -1.0m, Description = "Second Expense", Date = new DateTime(2000, 12, 1), UserId = 0 }
            };

            var transactionsMockSet = new Mock<DbSet<Transaction>>().SetupData(seededTransactions);

            return transactionsMockSet;
        }

        public static Mock<CivMoneyContext> GetMockCivMoneyContext(
            Mock<DbSet<Transaction>> mockDbSetTransaction,
            Mock<DbSet<User>> mockDbSetUser)
        {
            var mockContext = new Mock<CivMoneyContext>();
            mockContext.Setup(m => m.Transactions).Returns(mockDbSetTransaction.Object);
            mockContext.Setup(m => m.Users).Returns(mockDbSetUser.Object);

            return mockContext;
        }

        public static ICivMoneyContextFactory GetMockCivMoneyContextFactoryObject(CivMoneyContext civMoneyContext)
        {
            var mockContextFactory = new Mock<ICivMoneyContextFactory>();
            mockContextFactory.Setup(m => m.GetContext()).Returns(civMoneyContext);

            return mockContextFactory.Object;
        }
    }
}
