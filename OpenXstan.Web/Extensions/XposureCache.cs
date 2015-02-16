using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using OpenXstan.Common;
using OpenXstan.Service;
using OpenXstan.Web.Helpers;

namespace OpenXstan.Web.Extensions
{
    public class XposureCache : IXposureCache
    {
        public List<Category> GetCategory(XposureUser user)
        {
            List<Category> category = new List<Category>();

            if (HttpContext.Current.Cache.Get("Category") != null)
            {
                category = (List<Category>)HttpContext.Current.Cache["Category"];
            }
            else
            {
                category = XposureService.GetCategory(user);
                HttpContext.Current.Cache["Category"] = category;
                HttpContext.Current.Cache.Add("Category", category, null, DateTime.Now.AddHours(4), new TimeSpan(), CacheItemPriority.High, null);
            }
            return category;
        }
    }

    public interface IXposureCache
    {
        List<Category> GetCategory(XposureUser user);
    }
}