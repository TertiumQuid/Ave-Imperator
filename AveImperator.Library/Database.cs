using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace AveImperator.Library.Security
{
    public static class Database
    {
        public static string ImperatorConnection
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["AveImperator"].ConnectionString;
            }
        }
    }
}
