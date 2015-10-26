using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace FacebookPublishingASPnetWebForms.Status
{
    public partial class update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the Facebook code from the querystring
            if (Request.QueryString["code"] != null)
            {
                Session["accessToken"] = GetFacebookAccessToken(Request.QueryString["code"]);
                Response.Redirect("update.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Post to facebook  //
            // Construct the Graph API Post URI
            string dialogUrl = "https://graph.facebook.com/me/feed?"

                + AppendQueryString("access_token", Session["accessToken"].ToString())
                + AppendQueryString("message", TextBox1.Text);

            // Mark this request as a POST, and write the parameters to the method body (as opposed to the query string for a GET)
            var webRequest = WebRequest.Create(dialogUrl);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(dialogUrl);
            webRequest.ContentLength = bytes.Length;
            Stream os = webRequest.GetRequestStream();
            os.Write(bytes, 0, bytes.Length);
            os.Close();

            TextBox1.Text = "";
            Label1.Text = "Your post has been published to facebook";
        }

        protected static string AppendQueryString(string key, string value)
        {
            return string.Format("{0}={1}&", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value));
        }

        public string GetFacebookAccessToken(string code)
        {
            // Exchange the access code for an access token
            Uri targetUri = new Uri("https://graph.facebook.com/oauth/access_token?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&redirect_uri=http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + "/status/update.aspx&code=" + code);
            HttpWebRequest at = (HttpWebRequest)HttpWebRequest.Create(targetUri);

            StreamReader str = new StreamReader(at.GetResponse().GetResponseStream());
            string token = str.ReadToEnd().ToString().Replace("access_token=", "");

            // Split the access token and expiration from the single string
            string[] combined = token.Split('&');
            string accessToken = combined[0];

            // Exchange the code for an extended access token
            Uri eatTargetUri = new Uri("https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&fb_exchange_token=" + accessToken);
            HttpWebRequest eat = (HttpWebRequest)HttpWebRequest.Create(eatTargetUri);

            StreamReader eatStr = new StreamReader(eat.GetResponse().GetResponseStream());
            string eatToken = eatStr.ReadToEnd().ToString().Replace("access_token=", "");

            // Split the access token and expiration from the single string
            string[] eatWords = eatToken.Split('&');
            string extendedAccessToken = eatWords[0];

            return extendedAccessToken;
        }
    }
}