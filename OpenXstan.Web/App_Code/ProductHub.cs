using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using OpenXstan.Common;

namespace OpenXstan.Web
{
    [HubName("productHub")]
    public class ProductHub : Hub
    {
        public void SubmitNewProduct(string product)
        {
            var serializer = new JavaScriptSerializer();
            var productJson = serializer.Deserialize<Product>(product);

            Clients.All.newProductCreated(productJson);
        }
    }
}