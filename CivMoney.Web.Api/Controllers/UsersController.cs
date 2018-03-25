using CivMoney.AccessAndBusinessLayer.Contracts;
using CivMoney.AccessAndBusinessLayer.Users;
using CivMoney.DataBaseLayer;
using System.Web.Http;

namespace CivMoney.Web.Api.Controllers
{
    [RoutePrefix("user")]
    public class UsersController : ApiController
    {
        private ICreateUsersService createUserService;

        public UsersController()
        {
            var civMoneyContextFactory = new CivMoneyContextFactory();

            createUserService = new CreateUsers(civMoneyContextFactory);
        }

        // POST user/RegisterUser?userName={username}1&password={password}&currency={currency}
        [HttpPost]
        [Route("RegisterUser")]
        public int RegisterUser([FromUri]string userName, [FromUri]string password, [FromUri]string currency)
        {
            var newUserId = createUserService.AddUser(userName, password, currency);

            return newUserId;
        }
    }
}
