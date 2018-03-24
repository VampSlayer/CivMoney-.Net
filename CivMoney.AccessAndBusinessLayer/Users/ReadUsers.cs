using CivMoney.AccessAndBusinessLayer.Contracts;
using CivMoney.DataBaseLayer;
using CivMoney.DataBaseLayer.Contracts;
using System.Linq;

namespace CivMoney.AccessAndBusinessLayer.Users
{
    public class ReadUsers : IReadUsersService
    {
        private CivMoneyContext _civMoneyContext;

        public ReadUsers(ICivMoneyContextFactory civMoneyContextFactory)
        {
            _civMoneyContext = civMoneyContextFactory.GetContext();
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
    }
}
