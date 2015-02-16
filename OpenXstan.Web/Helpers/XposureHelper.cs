using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenXstan.Common;
using OpenXstan.Web.Interfaces;
using Microsoft.Practices.Unity;

namespace OpenXstan.Web.Helpers
{
    public class XposureHelper : IXposureHelper
    {
        public string UserName 
        { 
            get 
            { 
                return GetUserName(); 
            }
            set
            {
                this.UserName = value;
            }
        }

        public string ConnectionString 
        {
            get
            {
                return GetConnectionString();
            }
            set
            {
                this.ConnectionString = value;
            }
        }

        public XposureUser XposureUser 
        {
            get
            {
                return GetXposureUser();
            }
            set
            {
                this.XposureUser = value;
            }
        }

        [InjectionConstructor]
        public XposureHelper() {}

        public XposureHelper(HttpContextBase httpContext)
        {
        }

        public string GetUserName()
        {
            XposureSession session = (XposureSession)HttpContext.Current.Session["UserDetail"];
            return session == null ? "Guest" : session.Name;
        }

        public List<Product> ConvertProducts(List<Product> product)
        {
            var productList = new List<Product>();
            foreach (var item in product)
            {
                productList.Add(new Product()
                {
                    Id = item.Id.Trim(),
                    Title = item.Title,
                    Description = item.Description,
                    StartPrice = item.StartPrice,
                    CurrentPrice = item.CurrentPrice,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                });
            }
            return productList;
        }

        public Common.Product ConvertProduct(Product item)
        {
            var product = new Common.Product()
                {
                    //Id = item.Id.Trim(),
                    Title = item.Title,
                    Description = item.Description,
                    StartPrice = item.StartPrice,
                    CurrentPrice = item.CurrentPrice,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                };
            return product;
        }

        public List<Order> ConvertOrder(List<Product> product)
        {
            var productList = new List<Order>();
            foreach (var item in product)
            {
                productList.Add(new Order()
                {
                    Id = item.Id.Trim(),
                    Title = item.Title,
                    Description = item.Description,
                    StartPrice = item.StartPrice,
                    CurrentPrice = item.CurrentPrice,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                });
            }
            return productList;
        }

        public List<Category> ConvertCategory(List<Category> category)
        {
            var categoryList = new List<Category>();
            foreach (var item in category)
            {
                categoryList.Add(new Category()
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }
            return categoryList;
        }

        //public string GetUserName()
        //{
        //    return Convert.ToString(HttpContext.Current.Application["UserName"]);
        //    return Convert.ToString(HttpContext.Current.Items["UserName"]);
        //    XposureSession session = (XposureSession)HttpContext.Current.Session["UserDetail"];
        //    return session == null ? string.Empty : session.UserId;
        //}
        
        public string GetConnectionString()
        {
            var dbFilePath = @"Server=(localdb)\v11.0;Integrated Security=true;AttachDbFileName={0}App_Data\Xposure.mdf;";
            dbFilePath = string.Format(dbFilePath, HttpContext.Current.Request.PhysicalApplicationPath);
            return dbFilePath;
        }

        public XposureUser GetXposureUser()
        {
            XposureSession session = (XposureSession)HttpContext.Current.Session["UserDetail"];
            return session == null ? new XposureUser() { UserName = "mkumar", Password = "shibi143", ConnectionString = GetConnectionString() } :
                                                        new XposureUser()
                                                        {
                                                            UserName = session.Name,
                                                            Password = session.Password,
                                                            ConnectionString = GetConnectionString()
                                                        };
        }
    }
}