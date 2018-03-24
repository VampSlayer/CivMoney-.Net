using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivMoney.AccessAndBusinessLayer.Contracts
{
    public interface IUserAccessService
    {
        int AddUser(string userName, string password, string currency);
        string GetUserName(int userId);
    }
}
