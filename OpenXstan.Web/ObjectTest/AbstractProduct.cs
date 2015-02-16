using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenXstan.Web.ObjectTest
{
    public abstract class AbstractProduct : IAbstractProduct
    {
        public abstract string ProductName {get; set;}
        public abstract double CalculatePrice();

        public virtual bool IsSimSupported()
        {
            return false;
        }
    }

    public interface IAbstractProduct
    {
        string ProductName { get; set; }
        double CalculatePrice();
        bool IsSimSupported();
    }
}