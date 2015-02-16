using System.Web;
using System.Web.Mvc;
using OpenXstan.Web.Filters;
using System.ServiceModel;
namespace OpenXstan.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new CustomExceptionAttribute(){
            //        ExceptionType = typeof(System.Data.DataException),
            //        View = "Error"
            //    }, 1);
            filters.Add(new CustomExceptionAttribute()
            {
                View = "Error"
            }, 1);
            filters.Add(new CustomExceptionAttribute()
            {
                ExceptionType = typeof(FaultException),
                View = "Error"
            }, 2);
            filters.Add(new HandleErrorAttribute(), 3);
        }
    }
}