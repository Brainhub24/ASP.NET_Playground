using System;
using System.IO;

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
                // Log the redirection
                LogRedirection(Request.UserAgent);

                // Redirect bots to another site
                Response.Redirect("https://playground.rapidsource.eu/test/", true);
            }
            else
            {
                // Continue serving the regular landing page
            }
        }

        private void LogRedirection(string userAgent)
        {
            try
            {
                string logFilePath = Server.MapPath("~/Logs/redirection.log");
                string logMessage = $"Redirection for bot with user-agent: {userAgent} at {DateTime.Now}";

                // Append the log message to the log file
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during logging (e.g., write to event log)
                // For simplicity, i´m logging the exception message for testing #TEST_EXCEPTIONS
                string logFilePath = Server.MapPath("~/Logs/error.log");
                string logMessage = $"Error occurred while logging redirection: {ex.Message} at {DateTime.Now}";

                // Append the log message to the error log file
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
        }
    }
}
