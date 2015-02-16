using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenXstan.Web.Models;

namespace OpenXstan.Web
{
    public partial class MyProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString.Get("id");
            //var productList = new List<Product>()
            //{
            //    new Product() { Id=101, Title ="Dell Inspiron", CurrentPrice= 545.3M, EndTime = System.DateTime.Now.AddMonths(1) },
            //    new Product() { Id=102, Title ="Sony Vaio", CurrentPrice= 556M, EndTime = System.DateTime.Now.AddMonths(2) },
            //    new Product() { Id=103, Title ="iPhone 5", CurrentPrice= 245.3M, EndTime = System.DateTime.Now.AddMonths(3) }
            //};
            //listProduct.DataSource = productList;
            //listProduct.DataBind();
        }
    }
}