using CivMoney.DataBaseLayer;
using CivMoney.DataBaseLayer.Contracts;
using System;
using System.Linq;

namespace CivMoney.AccessAndBusinessLayer
{
    public class UsersAccess
    {
        private CivMoneyContext _civMoneyContext;

        public UsersAccess(ICivMoneyContextFactory civMoneyContextFactory)
        {
            _civMoneyContext = civMoneyContextFactory.GetContext();
        }

        public int AddUser(string userName, string password, string currency)
        {
            var user = new User
            {
                UserName = userName,
                PasswordHash = HashPassword(password),
                Salt = GetPasswordSalt(password),
                Currency = currency,
                TimeModified = DateTime.UtcNow
            };

            _civMoneyContext.Users.Add(user);
            _civMoneyContext.SaveChanges();

            var userId = _civMoneyContext.Users.Where(users => users.UserName == userName).Single().Id;

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
            // TODO
            return true;
        }

        private string HashPassword(string password)
        {
            //TODO
            return password;
        }

        private string GetPasswordSalt(string password)
        {
            //TODO
            return "salt";
        }
    }
}
