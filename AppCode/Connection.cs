using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace InternalLims.AppCode
{
    public class Connection
    {
        public string Con()
        {
            string ConString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            return ConString;
        }
        public string NovoAdmin()
        {
            string ConString = ConfigurationManager.ConnectionStrings["permission"].ConnectionString;
            return ConString;
        }
        //This returns the connection string
        public static string GetConnectionString(string connName)
        {
            string strReturn = string.Empty;
            if (!(string.IsNullOrEmpty(connName)))
            {
                strReturn = ConfigurationManager.ConnectionStrings[connName].ConnectionString;
            }
            else
            {
                //strReturn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
            return strReturn;
        }
    }
}