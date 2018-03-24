using CivMoney.AccessAndBusinessLayer.Contracts;
using CivMoney.DataBaseLayer;
using CivMoney.DataBaseLayer.Contracts;
using System;
using System.Linq;

namespace CivMoney.AccessAndBusinessLayer.Users
{
    public class UpdateUsers : IUpdateUsersService
    {
        private CivMoneyContext _civMoneyContext;

        public UpdateUsers(ICivMoneyContextFactory civMoneyContextFactory)
        {
            _civMoneyContext = civMoneyContextFactory.GetContext();
        }

        public bool UpdateUserCurrency(int userId, string currency)
        {
            var user = _civMoneyContext.Users.Where(users => users.Id == userId).SingleOrDefault();

            if (user != null)
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
