using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenXstan.Web.Extensions
{
    public static class JsonRequestExtensions
    {
        public static bool IsJsonRequest(this HttpRequestBase request)
        {
            return request.ContentType.Contains("application/json");
        }
    }
}