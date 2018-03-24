using CivMoney.AccessAndBusinessLayer.Contracts;
using CivMoney.DataBaseLayer;
using CivMoney.DataBaseLayer.Contracts;
using DevOne.Security.Cryptography.BCrypt;
using System;
using System.Linq;

namespace CivMoney.AccessAndBusinessLayer.Users
{
    public class CreateUsers : ICreateUsersService
    {
        private CivMoneyContext _civMoneyContext;

        public CreateUsers(ICivMoneyContextFactory civMoneyContextFactory)
        {
            _civMoneyContext = civMoneyContextFactory.GetContext();
        }

        public int AddUser(string userName, string password, string currency)
        {
            if(_civMoneyContext.Users.Where(users => users.UserName == userName).Count() != 0)
            {
                return -1;
            }

            var user = new User
            {
                UserName = userName,
                PasswordHash = BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt()),
                Currency = currency,
                TimeModified = DateTime.UtcNow
            };

            _civMoneyContext.Users.Add(user);
            _civMoneyContext.SaveChanges();

            var userId = _civMoneyContext.Users.Where(users => users.UserName == userName).SingleOrDefault().Id;

            return userId;
        }
    }
}
