using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenXstan.Common;
using OpenXstan.Common.WcfService.Contracts;

namespace OpenXstan.Web.Interfaces
{
    public interface IProductGateway
    {
        Status ValidateXposureUser(SecurityDetail security);
        bool ValidateUser(XposureUser user);
        List<Product> GetProduct(XposureUser user, string id);
        bool SaveProduct(XposureUser user, Product product);
    }
}