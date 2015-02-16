using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenXstan.Web.ObjectTest
{
    public class Apple : AbstractProduct, IDisposable
    {
        public override string ProductName { get; set; }
        public override double CalculatePrice()
        {
            return 540.00;
        }
        public override bool IsSimSupported()
        {
            return true;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}