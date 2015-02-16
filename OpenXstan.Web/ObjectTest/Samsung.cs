using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenXstan.Web.ObjectTest
{
    public class Samsung : AbstractProduct
    {
        public override string ProductName { get; set; }
        public override double CalculatePrice()
        {
            return 540.00;
        }
        //public new bool IsSimSupported()
        //{
        //    return true;
        //}
    }
}