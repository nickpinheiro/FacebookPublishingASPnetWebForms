using System;
using System.Configuration;

namespace FacebookPublishingASPnetWebForms
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.NavigateUrl = "https://www.facebook.com/dialog/oauth/?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&redirect_uri=http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + "/status/update.aspx&response_type=code&state=1&scope=email,publish_actions";
            HyperLink1.Text = "Login with Facebook";
        }
    }
}