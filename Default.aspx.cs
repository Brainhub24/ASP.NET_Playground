using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RAPIDSOURCE
{
    public partial class Default : System.Web.UI.Page
    {
        // Define a class to represent the bot details
        public class BotDetails
        {
            public string Name { get; set; }
            public List<string> UserAgents { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Load bot details from the JSON file
            List<BotDetails> bots = LoadBotDetailsFromJson("MOSTWANTED.json");

            // Set server signature
            Response.Headers.Add("Server", "DragonX by Webservice Digital (https://dragon.webservice.digital)");


            try
            {
                // Check if the user-agent indicates a bot
                if (Request.UserAgent != null && IsBot(Request.UserAgent, bots))
                {
                    // Log the redirection
                    LogRedirection(Request.UserAgent);

                    // Redirect bots to another site
                    Response.Redirect("https://playground.rapidsource.eu/test/?testview.py", true);
                }
                else
                {
                    // Continue serving the regular landing page
                    // 404 and 403 issue simulation for testing
                    /* + + + + + + + + + + + + + + + + + + + + + + + + + + + */
                    // Simulate the errors 4 test:
                    //if (Request.Url.AbsoluteUri.Contains("404test"))
                    //{
                    //    Response.StatusCode = 404; // Not Found
                    //    Response.End();
                    //}
                    //else if (Request.Url.AbsoluteUri.Contains("403test"))
                    //{
                    //    Response.StatusCode = 403; // Forbidden
                    //    Response.End();
                    //}
                    /* + + + + + + + + + + + + + + + + + + + + + + + + + + + */
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during page load
                // SO: https://stackoverflow.com/questions/3316762/what-is-deserialize-and-serialize-in-json
                LogError($"Error occurred during page load: {ex.Message}");
                // Handle the error gracefully or display an error page
                Response.StatusCode = 500; // Internal Server Error
                Response.End();
            }
        }

        private List<BotDetails> LoadBotDetailsFromJson(string jsonFilePath)
        {
            try
            {
                // Read the JSON file
                string json = File.ReadAllText(Server.MapPath(jsonFilePath));

                // Deserialize JSON into a list of BotDetails objects
                List<BotDetails> bots = JsonConvert.DeserializeObject<List<BotDetails>>(json);

                return bots;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during JSON deserialization
                LogError($"Error occurred while loading bot details from JSON file: {ex.Message}");
                return new List<BotDetails>();
            }
        }

        private bool IsBot(string userAgent, List<BotDetails> bots)
        {
            // Check if the user-agent matches any of the specified bots
            foreach (var bot in bots)
            {
                foreach (var botUserAgent in bot.UserAgents)
                {
                    if (userAgent.Contains(botUserAgent))
                    {
                        return true;
                    }
                }
            }
            return false;
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
                // Handle any exceptions that occur during logging
                LogError($"Error occurred while logging redirection: {ex.Message}");
            }
        }

        private void LogError(string errorMessage)
        {
            try
            {
                string logFilePath = Server.MapPath("~/Logs/error.log");
                string logMessage = $"Error: {errorMessage} at {DateTime.Now}";

                // Append the log message to the error log file
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // Log the exception message if an error occurs while logging
                Console.WriteLine($"Error occurred while logging error: {ex.Message}");
            }
        }
    }
}
