using CivMoney.AccessAndBusinessLayer;
using CivMoney.AccessAndBusinessLayer.Contracts;
using CivMoney.DataBaseLayer;
using System.Web.Http;

namespace CivMoney.Web.Api.Controllers
{
    [RoutePrefix("user")]
    public class UsersController : ApiController
    {
        private IUserAccessService userAccessService;

        public UsersController()
        {
            var civMoneyContextFactory = new CivMoneyContextFactory();

            userAccessService = new UsersAccess(civMoneyContextFactory);
        }

        // GET user/RegisterUser?userName=sayam1&password=password&currency=CHF
        [HttpGet]
        [Route("RegisterUser")]
        public string RegisterUser([FromUri]string userName, [FromUri]string password, [FromUri]string currency)
        {
            var newUserId = userAccessService.AddUser(userName, password, currency);

            return "New user id is : " + newUserId;
        }
    }
}
