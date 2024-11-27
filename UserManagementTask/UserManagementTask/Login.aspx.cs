using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserManagementTask
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect to Dashboard if already logged in
            if (Session["UserID"] != null)
            {
                //Response.Write("<script>alert('Already Logged In');</script>");
                //Response.Redirect("Dashboard.aspx");
                ClientScript.RegisterStartupScript(this.GetType(), "AlreadyLoggedInAlert",
                    "alert('Already Logged In.. Logout First!'); window.location='Dashboard.aspx';", true);
                return;
            }

        }

        // Helper method to determine operating system
        string GetOperatingSystem(string userAgent)
        {
            if (userAgent.Contains("Windows NT 10.0"))
                return "Windows 10";
            if (userAgent.Contains("Windows NT 6.3"))
                return "Windows 8.1";
            if (userAgent.Contains("Windows NT 6.2"))
                return "Windows 8";
            if (userAgent.Contains("Windows NT 6.1"))
                return "Windows 7";
            if (userAgent.Contains("Mac OS X"))
                return "MacOS";
            if (userAgent.Contains("Linux"))
                return "Linux";
            if (userAgent.Contains("Android"))
                return "Android";
            if (userAgent.Contains("iPhone"))
                return "iOS";
            return "Unknown OS";
        }


        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string userID = txtUserID.Text.Trim();
            string password = txtPassword.Text.Trim();

            string ip = Request.UserHostAddress;
            string browser = Request.Browser.Browser;
            string browserVersion = Request.Browser.Version;
            string os = GetOperatingSystem(Request.UserAgent);
            string ipAddress = $"{ip}/{browser} {browserVersion}/{os}/N";

            DateTime currentLoginTime = DateTime.Now;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UserManagement"].ConnectionString;

            string userQuery = "SELECT UserID, UserName, UserType FROM Users WHERE UserLoginID = @UserID AND UserPassword = @Password";
            string sessionQuery = @"
        INSERT INTO UserSessions (UserID, UserLoginID, UserType, UserName, IPAddress, CurrentLoginTime, PreviousLoginTime, SessionStartTime)
        VALUES (@UserID, @UserLoginID, @UserType, @UserName, @IPAddress, @CurrentLoginTime, 
                (SELECT TOP 1 CurrentLoginTime FROM UserSessions WHERE UserID = @UserID ORDER BY SessionID DESC), 
                @SessionStartTime)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(userQuery, conn))
                {
                    // Adding parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                // Retrieve user details
                                int dbUserID = Convert.ToInt32(reader["UserID"]);
                                string dbUserName = reader["UserName"].ToString();
                                string dbUserType = reader["UserType"].ToString();

                                reader.Close(); // Close the reader before executing another query

                                // Insert session details into UserSessions table
                                using (SqlCommand sessionCmd = new SqlCommand(sessionQuery, conn))
                                {
                                    sessionCmd.Parameters.AddWithValue("@UserID", dbUserID);
                                    sessionCmd.Parameters.AddWithValue("@UserLoginID", userID);
                                    sessionCmd.Parameters.AddWithValue("@UserType", dbUserType);
                                    sessionCmd.Parameters.AddWithValue("@UserName", dbUserName);
                                    sessionCmd.Parameters.AddWithValue("@IPAddress", ipAddress);
                                    sessionCmd.Parameters.AddWithValue("@CurrentLoginTime", currentLoginTime);
                                    sessionCmd.Parameters.AddWithValue("@SessionStartTime", currentLoginTime);

                                    sessionCmd.ExecuteNonQuery();
                                }

                                // Set session variables for the logged-in user
                                Session["UserID"] = dbUserID;
                                Session["UserName"] = dbUserName;
                                Session["UserType"] = dbUserType;

                                // Redirect to Dashboard
                                Response.Redirect("Dashboard.aspx");
                            }
                        }
                        else
                        {
                            // Invalid login
                            lblMessage.Text = "Invalid User ID or Password.";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error: " + ex.Message;
                    }
                }
            }


        }

    }
}