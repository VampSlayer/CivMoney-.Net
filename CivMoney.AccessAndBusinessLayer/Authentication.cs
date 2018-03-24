using DevOne.Security.Cryptography.BCrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivMoney.AccessAndBusinessLayer
{
    public class Authentication
    {
        public bool LoginUser(string userName, string password)
        {
            //TODO
            var passwordHash = "hash";

            BCryptHelper.CheckPassword(password, passwordHash);
            return true;
        }

        public bool DoesUserExist()
        {
            //TODO
            return true;
        }

        public bool CreateSessionId()
        {
            //TODO
            return true;
        }

        public bool DoesUserHaveValidSessionId()
        {
            //TODO
            return true;
        }
    }
}
