using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivMoney.AccessAndBusinessLayer.Contracts
{
    public interface IUserAuthenticationService
    {
        bool DoesUserExist(string userName);
        bool VerifyUserLoginDetails(string userName, string password);
    }
}
