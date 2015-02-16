using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenXstan.Web.Models
{
    public class ModelBase
    {
        public XposureSession UserSession { get; set; }
    }
    public class XposureSession
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}