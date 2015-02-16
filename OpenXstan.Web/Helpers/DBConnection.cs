using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenXstan.Web.Helpers
{
    public class DBConnection
    {
        private static DBConnection instance = null;
        private static string lockObject = "lock";

        private  DBConnection()
        { }

        public static DBConnection GetInstance
        {
            get 
            { 
                lock(lockObject)
                {
                    if(instance == null)
                    {
                        instance = new DBConnection();
                    }
                    return instance;
                }
            }
        }
    }
}