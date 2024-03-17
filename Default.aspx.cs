using System;

namespace RAPIDSOURCE
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user-agent indicates a bot
            if (Request.UserAgent != null && (Request.UserAgent.Contains("Googlebot") ||
                                               Request.UserAgent.Contains("Bingbot") ||
                                               Request.UserAgent.Contains("Slurp") ||
                                               Request.UserAgent.Contains("Baiduspider") ||
                                               Request.UserAgent.Contains("Yandex") ||
                                               Request.UserAgent.Contains("DuckDuckBot") ||
                                               Request.UserAgent.Contains("FacebookExternalHit") ||
                                               Request.UserAgent.Contains("Twitterbot") ||
                                               Request.UserAgent.Contains("LinkedInBot") ||
                                               Request.UserAgent.Contains("Pinterest")))
            {
                // Redirect bots to another site
                Response.Redirect("https://playground.rapidsource.eu/test/", true);
            }
            else
            {
                // Continue serving the regular landing page :)
            }
        }
    }
}
