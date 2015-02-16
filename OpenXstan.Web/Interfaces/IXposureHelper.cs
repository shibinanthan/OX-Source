using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenXstan.Common;

namespace OpenXstan.Web.Interfaces
{
    public interface IXposureHelper
    {
        List<Product> ConvertProducts(List<Common.Product> product);
        Common.Product ConvertProduct(Product item);
        List<Order> ConvertOrder(List<Common.Product> product);
        List<Category> ConvertCategory(List<Category> category);

        string GetConnectionString();
        XposureUser GetXposureUser();
        string GetUserName();
    }
}
