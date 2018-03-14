using CivMoney.AccessAndBusinessLayer;

namespace CivMoney.Wcf.Api
{
    public class CivMoneyUsers : ICivMoneyUsers
    {
        private Users _usersDataBaseAccess;

        public CivMoneyUsers()
        {
            _usersDataBaseAccess = new Users();
        }

        public string AddUser(string userName, string password, string currency)
        {
            var userId = _usersDataBaseAccess.AddUser(userName, password, currency);

            return "New user Id is " + userId;
        }

        public string GetUserName(string id)
        {
            var userName = _usersDataBaseAccess.GetUserName(int.Parse(id));

            return userName;
        }
    }
}
