﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenXstan.Web.Views.Product
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 1 "..\..\Views\Product\ViewProduct.cshtml"
    using OpenXstan.Web.Models;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Product/ViewProduct.cshtml")]
    public partial class ViewProduct : System.Web.Mvc.WebViewPage<dynamic>
    {
        public ViewProduct()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n\r\n");

            
            #line 4 "..\..\Views\Product\ViewProduct.cshtml"
  
    Layout = "~/Views/Shared/_LayoutMaster.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" id=\"Main\"");

WriteLiteral(">\r\n       \r\n    <div");

WriteLiteral(" id=\"showProduct\"");

WriteLiteral(">\r\n        <span>\r\n            <h4>Product Id: ");

            
            #line 11 "..\..\Views\Product\ViewProduct.cshtml"
                       Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral("<br />\r\n                Description: ");

            
            #line 12 "..\..\Views\Product\ViewProduct.cshtml"
                        Write(Model.Description);

            
            #line default
            #line hidden
WriteLiteral("<br />\r\n                Product Name: ");

            
            #line 13 "..\..\Views\Product\ViewProduct.cshtml"
                         Write(Model.Title);

            
            #line default
            #line hidden
WriteLiteral(" <br />\r\n                Price: $");

            
            #line 14 "..\..\Views\Product\ViewProduct.cshtml"
                   Write(Model.CurrentPrice);

            
            #line default
            #line hidden
WriteLiteral("<br />\r\n                Available Date: ");

            
            #line 15 "..\..\Views\Product\ViewProduct.cshtml"
                           Write(Model.EndTime.ToShortDateString());

            
            #line default
            #line hidden
WriteLiteral("</h4>\r\n        </span>\r\n    </div>\r\n       \r\n    <div>\r\n");

WriteLiteral("        ");

            
            #line 20 "..\..\Views\Product\ViewProduct.cshtml"
   Write(Html.ActionLink("Back", "Index"));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n");

DefineSection("PageFeature", () => {

WriteLiteral("\r\n    <span>This section describes the features of this product</span>\r\n    <span" +
">");

            
            #line 26 "..\..\Views\Product\ViewProduct.cshtml"
     Write(System.DateTime.Now);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n");

});

        }
    }
}
#pragma warning restore 1591
