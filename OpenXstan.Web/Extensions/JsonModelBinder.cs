using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace OpenXstan.Web.Extensions
{
    public class JsonModelBinder : DefaultModelBinder
    {
        //public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        //{
        //    string json = string.Empty;
        //    var provider = bindingContext.ValueProvider;
        //    var providerValue = provider.GetValue(bindingContext.ModelName);

        //    if (providerValue != null)
        //        json = providerValue.AttemptedValue;

        //    if (Regex.IsMatch(json, @"^(\[.*]|{.*})$"))
        //    {
        //        return new JavaScriptSerializer()
        //                    .Deserialize(json, bindingContext.ModelType);
        //    }
        //    return base.BindModel(controllerContext, bindingContext);
        //}
    }
}