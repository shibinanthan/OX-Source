using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Threading.Tasks;
using OpenXstan.Common;
using OpenXstan.Service;
using OpenXstan.Web.Helpers;
using Microsoft.AspNet.SignalR;

namespace OpenXstan.Web.Controllers
{
    public class SearchController : AsyncController
    {
        List<Product> _products = new List<Product>();
        private XposureHelper _helper = null;

        public SearchController()
        {
            this._helper = new XposureHelper();
            var product = XposureService.GetProduct(_helper.XposureUser, string.Empty);
            _products = _helper.ConvertProducts(product);
        }
       
        [AsyncTimeout(1500)]
        [HandleError(ExceptionType = typeof(TaskCanceledException), View = "AjaxTimedOut")]
        public async Task<ActionResult> SearchProduct(int id)
        {
            var product = await Search(id);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        private async Task<IEnumerable<Product>> Search(int id)
        {
            var product = _products.Where<Product>(i=> i.Id == id.ToString()).ToList();
            return product;
        }

        public ActionResult Index()
        {
            return Content("Welcome to Product Search page.");
        }

        public ActionResult Result(string query = "test", int page = 4)
        {
            return Content("The product search not yet available.");
        }
    }
}