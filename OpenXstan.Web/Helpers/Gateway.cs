using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenXstan.Common;
using OpenXstan.Common.WcfService.Contracts;
using OpenXstan.Web.Interfaces;

namespace OpenXstan.Web.Helpers
{
    public class ProductGateway : GatewayBase, IProductGateway
    {
        private static ProductGateway instance = null;
        private static string lockObject = "lock";

        //Singleton pattern implementation
        public static ProductGateway Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new ProductGateway();
                    }
                    return instance;
                }
            }
        }

        protected override string EndpointName
        {
            get { return "OpenXstan.WcfService.Http"; }
        }

        public bool ValidateUser(XposureUser user)
        {
            return WcfWrapper((IDataService dataService) =>
                {
                    return dataService.ValidateUser(user);
                });
        }

        public Status ValidateXposureUser(SecurityDetail security)
        {
            return WcfWrapper((IDataService dataService) =>
            {
                return dataService.ValidateXposureUser(security);
            });
        }

        public List<Product> GetProduct(XposureUser user, string id)
        {
            return WcfWrapper((IDataService dataService) =>
                {
                    return dataService.GetProduct(user, id);
                });
        }

        public bool SaveProduct(XposureUser user, Product product)
        {
            return WcfWrapper((IDataService dataService) =>
            {
                return dataService.SaveProduct(user, product);
            });
        }
    }
}