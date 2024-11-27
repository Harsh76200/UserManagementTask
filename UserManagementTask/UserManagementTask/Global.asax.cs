using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace UserManagementTask
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Get the Session ID or User ID
            string userId = Session["UserID"]?.ToString(); // Assuming UserID is stored in Session

            if (!string.IsNullOrEmpty(userId))
            {
                // Update the Session End Time in the database
                UpdateSessionEndTime(Convert.ToInt32(userId));
            }
        }

        // Method to update the session end time in the database
        private void UpdateSessionEndTime(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UserManagement"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            UPDATE UserSessions
            SET SessionEndTime = GETDATE()
            WHERE UserID = @UserID AND SessionEndTime IS NULL"; // Ensures the most recent session is updated

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}