using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using System.Web.Routing;
using OpenXstan.Web.Models;
using OpenXstan.Web.Extensions;
using OpenXstan.Common;
using System.Web.Helpers;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Net;
using OpenXstan.Web.Helpers;

namespace OpenXstan.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CustomAuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            base.AuthorizeCore(httpContext);
            if (httpContext.Session["UserDetail"] != null)
            {
                return true;
            }
            return false;
        }

        public override void OnAuthorization(AuthorizationContext context)
        {
            base.OnAuthorization(context);

            if (context.HttpContext.Session["UserDetail"] == null)
            {
                context.Result = new RedirectToRouteResult(
                                    new RouteValueDictionary { { "controller", "Account" }, { "action", "Login" } });
            }

            if (context.HttpContext.Request.HttpMethod == "POST")
            {
                if (context.HttpContext.Request.IsAjaxRequest() || context.HttpContext.Request.IsJsonRequest())
                    ValidateAntiForgeryToken(context);
                else new ValidateAntiForgeryTokenAttribute().OnAuthorization(context);
            }
        }

        private bool ValidateAntiForgeryToken(AuthorizationContext context)
        {
            try
            {
                var httpContext = context.HttpContext;
                var cookie = httpContext.Request.Cookies[AntiForgeryConfig.CookieName];
                var token = httpContext.Request.Headers["__RequestVerificationToken"] == null ?
                                    httpContext.Request.Params["__RequestVerificationToken"] :
                                    httpContext.Request.Headers["__RequestVerificationToken"];

                AntiForgery.Validate(cookie != null ? cookie.Value : null, token);
            }
            catch (System.Web.Mvc.HttpAntiForgeryException ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

        private string CookieValue(HttpRequestBase request)
        {
            var cookie = request.Cookies[AntiForgeryConfig.CookieName];
            return cookie != null ? cookie.Value : null;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomActionAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //var session = (XposureSession)context.HttpContext.Session["UserDetail"];
            //HttpContext.Current.Items["UserName"] = session.UserId;
            
            foreach (var actionParameter in context.ActionParameters.Values)
            {
                if (actionParameter != null && actionParameter.GetType() == typeof(Product))
                {
                    var product = (Product)actionParameter;
                    //product.UserSession = session;
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.Controller.ViewBag.OnActionExecuted = "IActionFilter.OnActionExecuted filter called";

            var request = context.HttpContext.Request;
            var viewResult = context.Result as ViewResult;

            if (viewResult == null) return;

            //when the request through AJAX call
            if (request.IsJsonRequest())
            {
                context.Result = new JsonResult
                {
                    Data = viewResult.Model, JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            //when the request through partial load
            else if (request.IsAjaxRequest())
            {
                context.Result = new PartialViewResult
                {
                    TempData = viewResult.TempData,
                    ViewData = viewResult.ViewData,
                    ViewName = viewResult.ViewName
                };
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null) base.OnException(filterContext);

            LogHelper.LogException(filterContext.Exception);

            if (filterContext.HttpContext.IsCustomErrorEnabled)
            {
                filterContext.ExceptionHandled = true;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class HttpHeaderAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public HttpHeaderAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.AppendHeader(Name, Value);
            base.OnResultExecuted(filterContext);
        }
    }
}