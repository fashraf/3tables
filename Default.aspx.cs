using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims
{
    public partial class _Default : Page
    {
        AppCode.Audit Audit= new AppCode.Audit();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //    var authority = "https://login.microsoftonline.com";
                //    var tenant = "816f04cb-c722-49e4-995a-9b81548eec8d";
                //    var authorizeSuffix = "oauth2";
                //    var EndPointUrl = String.Format("{0}/{1}/{2}", authority, tenant, authorizeSuffix);

                //    var code = Request.QueryString["code"].ToString();

                //    var clientId = "22526908-21f4-4d74-bc08-a79904ca4eac";
                //    var resource = "https://graph.microsoft.com";
                //    var secrect = "0~68Q~23~vCmv7Srdq4xWchek33lAxbNRTNF5aPO";
                //    var redirectURL = "https://admintal.novogenomics.sa/Default";

                //    //Request access token
                //    var parameters = new Dictionary<string, string>
                //{
                //    { "resource", resource},
                //    { "client_id", clientId },
                //    { "code",  code},
                //    { "grant_type", "authorization_code" },
                //    { "redirect_uri", redirectURL},
                //    { "client_secret",secrect}
                //};


                //    var list = new List<string>();

                //    foreach (var parameter in parameters)
                //    {
                //        if (!string.IsNullOrEmpty(parameter.Value))
                //            list.Add(string.Format("{0}={1}", parameter.Key, HttpUtility.UrlEncode(parameter.Value)));
                //    }
                //    var strParameters = string.Join("&", list);


                //    var content = new StringContent(strParameters, Encoding.GetEncoding("utf-8"), "application/x-www-form-urlencoded");

                //    var client = new HttpClient();

                //    var url = string.Format("{0}/token", EndPointUrl);


                //    var response = client.PostAsync(url, content).Result;


                //    var text = response.Content.ReadAsStringAsync().Result;


                //    var result = JsonConvert.DeserializeObject(text) as JObject;

                //    var AccessToken = result.GetValue("access_token").Value<string>();
                //    var RefreshToken = result.GetValue("refresh_token").Value<string>();

                //    Session["accessToken"] = AccessToken;
                //    Session["refreshToken"] = RefreshToken;


                ////add code read the user info from access token for login in
                //string accessToken = AccessToken;

                //byte[] data = Convert.FromBase64String(accessToken);
                //string decodedString = Encoding.UTF8.GetString(data);

                //JToken token = JObject.Parse(decodedString);
                //Response.Write(token["name"].Value<string>());

                //var stream = AccessToken;
                //var handler = new JwtSecurityTokenHandler();
                //var jsonToken = handler.ReadToken(stream);
                //var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

                //foreach (System.Security.Claims.Claim cl in tokenS.Claims)
                //{
                //    if (cl.Type == "unique_name")
                //    {
                //        lblEmailId.Text = cl.Value;

                //    }

                //    if (cl.Type == "family_name")
                //    {
                //        lblUserName.Text = cl.Value;
                //    }

                //    //
                //}
                //String[] parts = lblEmailId.Text.Split(new[] { '@' });
                //String username = parts[0]; // "hello"
                //String FullName = lblUserName.Text;
                //CheclForLadap(username, FullName);
                CheclForLadap("fashraf", "");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        private void CheclForLadap(string username, string Fullname)
        {
            AppCode.Connection Con = new AppCode.Connection();
            String Connection = Con.NovoAdmin();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand chk = new SqlCommand("SELECT UserMaster.UserId UserID,UserMaster.First_Name + ' ' + UserMaster.Last_Name AS FullName, UserMaster.LoginID AS UserName,UserMaster.RoleId,RoleMaster.RedirectPage FROM UserMaster INNER JOIN RoleMaster ON UserMaster.RoleId = RoleMaster.RoleID WHERE  (UserMaster.LoginID = '" + username + "') AND (UserMaster.User_Status = 1)", con);
            chk.CommandType = CommandType.Text;
            SqlDataReader reader = chk.ExecuteReader();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    HttpCookie userInfo = new HttpCookie("adminportal");
                    string Name = reader["FullName"].ToString();

                    userInfo["UserID"] = reader["UserID"].ToString();
                    userInfo["FullName"] = reader["FullName"].ToString();
                    userInfo["UserName"] = reader["UserName"].ToString();
                    userInfo["RoleId"] = reader["RoleId"].ToString();

                    Session["UserID"] = reader["UserID"].ToString();
                    Session["FullName"] = Name;
                    Session["UserName"] = reader["UserName"].ToString();
                    Session["RoleId"] = reader["RoleId"].ToString();
                    string RedirectPg = reader["RedirectPage"].ToString();

                    userInfo.Expires = System.DateTime.Now.AddMonths(2);
                    Response.Cookies.Add(userInfo);
                    FormsAuthentication.RedirectFromLoginPage(Name, false);
                 
                  
                    string metadata = username + " Logged In";
                    int UID = Convert.ToInt32(Session["UserID"].ToString());
                    string UserName = Session["UserName"].ToString();
                    int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                    string meta = Name + " Logged In";
                    AppCode.Audit.auditlog(UID, UserName, "Log In", meta, RoleId, false);
                    Response.Redirect(RedirectPg);
                    //Response.Redirect("main/admin/Dashboard.aspx");
                }
            }
            else
            {
                Session.Abandon();
                Session.Clear();
                Response.Redirect("Login.aspx");
            }
            reader.Dispose();
            con.Dispose();
            chk.Dispose();
            con.Close();
        }
    }
}