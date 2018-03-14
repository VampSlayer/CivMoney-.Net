using CivMoney.DataBaseLayer;
using System;
using System.Linq;

namespace CivMoney.AccessAndBusinessLayer
{
    public class Users
    {
        public int AddUser(string userName, string password, string currency)
        {
            using (var db = new CivMoneyContext())
            {
                var user = new User
                {
                    UserName = userName,
                    PasswordHash = HashPassword(password),
                    Salt = GetPasswordSalt(password),
                    Currency = currency,
                    TimeModified = DateTime.UtcNow
                };

                db.Users.Add(user);
                db.SaveChanges();

                var query = db.Users.Where(users => users.UserName == userName).Single();

                return query.Id;
            }
        }

        public string GetUserName(int id)
        {
            using (var db = new CivMoneyContext())
            {
                var query = db.Users.Where(users => users.Id == id).Single();

                return query.UserName;
            }
        }

        private string HashPassword(string password)
        {
            return password;
        }

        private string GetPasswordSalt(string password)
        {
            return "salt";
        }
    }
}
