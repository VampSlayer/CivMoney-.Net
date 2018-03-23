using System.ServiceModel;
using System.ServiceModel.Web;

namespace CivMoney.Wcf.Api
{
    [ServiceContract]
    public interface ICivMoneyTransactions
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/{name}")]
        void AddTransaction(string name);
    }
}
