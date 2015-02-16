using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Net.Security;
using OpenXstan.Common;

namespace OpenXstan.Common.WcfService.Contracts
{
    [ServiceContract]
    public interface IDataService
    {
        [OperationContract]
        Status ValidateXposureUser(SecurityDetail security);

        [OperationContract(Name = "ValidateUser")]
        bool ValidateUser(XposureUser user);

        [OperationContract(Name = "ValidateAdminUser")]
        bool ValidateUser(XposureUser user, bool ignoreValidation);

        [OperationContract]
        List<Product> GetProduct(XposureUser user, string id);

        [OperationContract]
        bool SaveProduct(XposureUser user, Product product);
        bool NotOperationContract();
    }

    [MessageContract]
    public class SecurityDetail
    {
        [MessageHeader(ProtectionLevel = ProtectionLevel.EncryptAndSign)]
        public string SecurityToken;

        [MessageBodyMember(Order = 1, ProtectionLevel = ProtectionLevel.None)]
        public string UserId;

        [MessageBodyMember(Order = 2, ProtectionLevel = ProtectionLevel.None)]
        public string UserName { get; set; }

        [MessageBodyMember(Order = 3, ProtectionLevel = ProtectionLevel.None)]
        public string Password { get; set; }

        [MessageBodyMember(Order = 4, ProtectionLevel = ProtectionLevel.None)]
        public string ConnectionString { get; set; }
    }

    [MessageContract]
    public class Status
    {
         [MessageBodyMember(Order = 1, ProtectionLevel = ProtectionLevel.None)]
         public bool Valid { get; set; }
    }
}
