using System;
using System.Configuration;
using System.Data.SqlClient;

namespace UserManagementTask
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Ensure this logic only runs once per request
            {
                if (Session["UserID"] == null)
                {
                    // Redirect to login if session does not exist
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    try
                    {
                        // Fetch session details and populate the dashboard
                        int userId = Convert.ToInt32(Session["UserID"]);
                        FetchSessionDetails(userId);

                        // Display the logged-in user's name
                        lblUserName.Text = Session["UserName"]?.ToString();
                    }
                    catch (Exception ex)
                    {
                        // Log exception (consider using a logging library)
                        lblUserName.Text = "Error loading user details.";
                        // Optionally redirect or show a friendly error message
                    }
                }
            }
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            // Clear session and redirect to login page
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

      

        private void FetchSessionDetails(int userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UserManagement"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT TOP 1
                            UserLoginID, UserType, UserName, IPAddress,
                            CurrentLoginTime, PreviousLoginTime
                        FROM UserSessions
                        WHERE UserID = @UserID
                        ORDER BY SessionID DESC"; // Fetch the latest session

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblUserLoginID.Text = reader["UserLoginID"]?.ToString() ?? "N/A";
                                String userType = reader["UserType"]?.ToString() ?? "N/A";
                                lblUserType.Text = userType.Equals('S') ? "Staff" : "Candidate";
                                lblIPAddress.Text = reader["IPAddress"]?.ToString() ?? "N/A";

                                lblCurrentLoginTime.Text = reader["CurrentLoginTime"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["CurrentLoginTime"]).ToString("dd/MM/yyyy hh:mm:ss tt")
                                    : "N/A";

                                lblPreviousLoginTime.Text = reader["PreviousLoginTime"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["PreviousLoginTime"]).ToString("dd/MM/yyyy hh:mm:ss tt")
                                    : "N/A";
                            }
                            else
                            {
                                // Handle the case where no session is found for the user
                                lblUserLoginID.Text = "N/A";
                                lblUserType.Text = "N/A";
                                lblIPAddress.Text = "N/A";
                                lblCurrentLoginTime.Text = "N/A";
                                lblPreviousLoginTime.Text = "N/A";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception (consider using a logging library)
                lblUserLoginID.Text = "Error loading session details.";
                lblUserType.Text = "Error loading session details.";
                lblIPAddress.Text = "Error loading session details.";
                lblCurrentLoginTime.Text = "Error loading session details.";
                lblPreviousLoginTime.Text = "Error loading session details.";

                // Optionally, rethrow or handle the exception
                throw;
            }
        }
    }
}
