using CivMoney.AccessAndBusinessLayer.Contracts;
using CivMoney.DataBaseLayer;
using CivMoney.DataBaseLayer.Contracts;
using DevOne.Security.Cryptography.BCrypt;
using System.Linq;

namespace CivMoney.AccessAndBusinessLayer.Authentication
{
    public class UserAuthentication : IUserAuthenticationService
    {
        private CivMoneyContext _civMoneyContext;

        public UserAuthentication(ICivMoneyContextFactory civMoneyContextFactory)
        {
            _civMoneyContext = civMoneyContextFactory.GetContext();
        }

        public bool DoesUserExist(string userName)
        {
            return _civMoneyContext.Users.Where(users => users.UserName == userName).Count() == 1;
        }

        public bool VerifyUserLoginDetails(string userName, string password)
        {
            if (DoesUserExist(userName))
            {
                var user = _civMoneyContext.Users.Where(users => users.UserName == userName).SingleOrDefault();

                 return BCryptHelper.CheckPassword(password, user.PasswordHash);
            }

            return false;
        }
    }
}
