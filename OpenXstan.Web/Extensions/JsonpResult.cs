using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenXstan.Web.Extensions
{
    public class JsonpResult : JsonResult
    {
        public string Callback { get; set; }

        public JsonpResult()
        {
            JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var httpContext = context.HttpContext;
            var callback = Callback;

            if (string.IsNullOrWhiteSpace(callback))
                callback = httpContext.Request["callback"];

            httpContext.Response.Write(callback + "(");
            base.ExecuteResult(context);
            httpContext.Response.Write(");");
        }
    }
}