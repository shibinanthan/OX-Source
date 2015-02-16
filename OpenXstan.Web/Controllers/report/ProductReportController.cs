using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenXstan.Web.Models;

namespace OpenXstan.Web.Controllers
{
    public class ProductReportController : Controller
    {
        public ActionResult List(string category1)
        {
            return Content("ProductReportController::List:: The product listing not yet implemented.");
        }
    }
}
