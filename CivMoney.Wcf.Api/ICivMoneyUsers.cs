using System.ServiceModel;
using System.ServiceModel.Web;

namespace CivMoney.Wcf.Api
{
    [ServiceContract]
    public interface ICivMoneyUsers
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/userId={id}")]
        string GetUserName(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped,
           UriTemplate = "json/AddUser?username={userName}&password={password}&currency={currency}")]
        string AddUser(string userName, string password, string currency);
    }
}
