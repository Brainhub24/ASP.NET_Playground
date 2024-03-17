using System;

namespace RAPIDSOURCE
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user-agent indicates a bot
            if (Request.UserAgent != null && (Request.UserAgent.Contains("Googlebot") || Request.UserAgent.Contains("Bingbot")))
            {
                // Redirect bots to another site
                Response.Redirect("https://playground.rapidsource.eu/test/", true);
            }
            else
            {
                // Continue serving the regular landing page
            }
        }
    }
}
