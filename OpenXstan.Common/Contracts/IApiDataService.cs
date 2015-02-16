using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace OpenXstan.Common.WcfService.Contracts
{
    [ServiceContract]
    public interface IApiDataService
    {
        [OperationContract(Name = "ValidateUser")]
        [WebGet(UriTemplate = "validateuser/{userName}", 
                ResponseFormat = WebMessageFormat.Json, 
                BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool ValidateUser(string userName);

        [OperationContract]
        [WebInvoke(Method="GET", UriTemplate = "/getproduct/{id}",
               ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
               BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Product> GetProduct(string id);
    }
}
