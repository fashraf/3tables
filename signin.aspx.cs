using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims
{
    public partial class signin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var authority = "https://login.microsoftonline.com";
            var tenant = "816f04cb-c722-49e4-995a-9b81548eec8d";
            var authorizeSuffix = "oauth2";

            var EndPointUrl = String.Format("{0}/{1}/{2}/authorize?", authority, tenant, authorizeSuffix);

            var clientId = "22526908-21f4-4d74-bc08-a79904ca4eac";
            var redirectURL = "https://admintal.novogenomics.sa/Default";
            var parameters = new Dictionary<string, string>
            {
                { "response_type", "code" },
                { "client_id", clientId },
                { "redirect_uri", redirectURL },
                { "prompt", "login"}
            };

            var list = new List<string>();

            foreach (var parameter in parameters)
            {
                if (!string.IsNullOrEmpty(parameter.Value))
                    list.Add(string.Format("{0}={1}", parameter.Key, HttpUtility.UrlEncode(parameter.Value)));
            }
            var strParameters = string.Join("&", list);
            var requestURL = String.Concat(EndPointUrl, strParameters);

            Response.Redirect(requestURL);
        }
    }
}