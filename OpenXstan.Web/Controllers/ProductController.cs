using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using OpenXstan.Common;
using OpenXstan.Web.Filters;
using OpenXstan.Web.Extensions;
using OpenXstan.Web.Helpers;
using OpenXstan.Web.Interfaces;
using OpenXstan.Service;
using Microsoft.Practices.Unity;
using OpenXstan.Web.ObjectTest;

namespace OpenXstan.Web.Controllers
{
    [CustomAuthorization]
    [CustomAction]
    //[HttpHeaderAttribute("Access-Control-Allow-Origin", "*")]
    public class ProductController : Controller
    {
        private IXposureHelper _helper = null;
        private IXposureCache _cache = null;
        private IProductGateway _gateWay;

        [InjectionConstructor]
        public ProductController(IProductGateway gateway, IXposureHelper helper, IXposureCache cache)
        {
            this._helper = helper;
            this._cache = cache;
            this._gateWay = gateway;
        }

        public ProductController(IXposureHelper hepler, IXposureCache cache, IProductGateway gateway)
        {
            this._helper = hepler;
            this._cache = cache;
            this._gateWay = gateway;
        }

        public ActionResult Index()
        {
            var user = _helper.GetXposureUser();
            var product = _gateWay.GetProduct(user, string.Empty);
            var productList = new ProductViewModel() { Product = product };
            return View(productList);
        }

        //[OutputCache(Duration=60, VaryByParam="id")]
        public ActionResult Detail(string id)
        {
            var product = _gateWay.GetProduct(_helper.GetXposureUser(), id);
            AbstractProduct productObject;
            if (product != null && product.Count() > 0)
            {
                if (product.First().CategoryID == 3)
                    productObject = new Apple();
                else productObject = new Samsung();

                if (!Request.IsAjaxRequest())
                {
                    return View("ViewProduct", product.First());
                }
                else if (Request.ContentType.Contains("application/json"))
                {
                    //return Json(product.First(), JsonRequestBehavior.AllowGet);
                    return new JsonpResult { Data = product.First() };
                }
                else return PartialView("ViewProduct", product.First());
            }
            else return Content("The product details are not found.");
        }

        public ActionResult Save()
        {
            var category = _cache.GetCategory(_helper.GetXposureUser());
            var product = new Product() { Category = category };
            return View("SaveProduct", product);
        }

        //[HandleError(ExceptionType = typeof(System.Data.DataException), View = "Error")]
        [HttpPost]
        public ActionResult Save(Product product)
        {
            var status = _gateWay.SaveProduct(_helper.GetXposureUser(), product);
            return View("Index");
        }

        public ActionResult Error()
        {
            return View("Error");
        }

        public ActionResult Search(string query)
        {
            return View("Error");
        }

        public  ActionResult Country(string region)
        {
            return Content("You are in USA. Welcome to USA!!!");
        }

        public ActionResult HTML5()
        {
            ViewBag.Message = "Features of HTML5";
            return View();
        }
    }
}