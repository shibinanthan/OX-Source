using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace OpenXstan.Web.Helpers
{
    public class LogHelper
    {
        public static void LogException(Exception ex)
        {
            EventLog log = new EventLog();
            log.Source = "Xposure";
            log.WriteEntry(ex.Message);
        }
    }
}