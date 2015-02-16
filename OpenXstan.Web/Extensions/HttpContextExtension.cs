using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenXstan.Common;

namespace OpenXstan.Web.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetUserName(this HttpRequestBase context)
        {
            XposureSession session = (XposureSession)HttpContext.Current.Session["UserDetail"];
            return session == null ? "Guest" : session.Name;
        }

        public static XposureUser GetXposureUser(this HttpRequestBase context)
        {
            XposureSession session = (XposureSession)HttpContext.Current.Session["UserDetail"];
            return session == null ? new XposureUser() { UserName = "mkumar", Password = "shibi143", ConnectionString = context.GetConnectionString() } :
                                                        new XposureUser()
                                                        {
                                                            UserName = session.Name,
                                                            Password = session.Password,
                                                            ConnectionString = context.GetConnectionString()
                                                        };
        }

        public static string GetConnectionString(this HttpRequestBase context)
        {
            var dbFilePath = @"Server=(localdb)\v11.0;Integrated Security=true;AttachDbFileName={0}App_Data\Xposure.mdf;";
            dbFilePath = string.Format(dbFilePath, HttpContext.Current.Request.PhysicalApplicationPath);
            return dbFilePath;
        }
    }
}