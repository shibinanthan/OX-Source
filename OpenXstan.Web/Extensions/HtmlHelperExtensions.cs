using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace OpenXstan.Web.HtmlHelpers
{
    public static class Extensions
    {
        public static MvcHtmlString NumericTextBox(this HtmlHelper html, string name)
        {
            var htmlString = InputExtensions.TextBox(html, name);
            TagBuilder tag = new TagBuilder("Text");
            //tag.Attributes.Add()
            return new MvcHtmlString(tag.ToString());
        }
    }
}