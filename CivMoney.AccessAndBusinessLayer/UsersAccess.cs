using CivMoney.AccessAndBusinessLayer.Contracts;
using CivMoney.DataBaseLayer;
using CivMoney.DataBaseLayer.Contracts;
using DevOne.Security.Cryptography.BCrypt;
using System;
using System.Linq;

namespace CivMoney.AccessAndBusinessLayer
{
    public class UsersAccess : IUserAccessService
    {
        private CivMoneyContext _civMoneyContext;

        public UsersAccess(ICivMoneyContextFactory civMoneyContextFactory)
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

        public string GetUserName(int userId)
        {
            var userName = _civMoneyContext.Users.Where(users => users.Id == userId).SingleOrDefault().UserName;

            return userName;
        }

        public int GetUserIdFromUserName(string userName)
        {
            var userId = _civMoneyContext.Users.Where(users => users.UserName == userName).SingleOrDefault().Id;

            return userId;
        }

        public string GetUserCurrency(int userId)
        {
            var userCurrency = _civMoneyContext.Users.Where(users => users.Id == userId).SingleOrDefault().Currency;

            return userCurrency;
        }

        public bool UpdateUserCurrency(int userId, string currency)
        {
            var user = _civMoneyContext.Users.Where(users => users.Id == userId).SingleOrDefault();

            if(user != null)
            {
                user.Currency = currency;
                user.TimeModified = DateTime.UtcNow;
                _civMoneyContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
