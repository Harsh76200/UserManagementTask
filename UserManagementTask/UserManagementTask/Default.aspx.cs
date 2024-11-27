using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserManagementTask
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // Redirect to Dashboard if already logged in
            if (Session["UserID"] != null)
            {
                //Response.Write("<script>alert('Already Logged In');</script>");
                //Response.Redirect("Dashboard.aspx");
                // Show alert and redirect
                ClientScript.RegisterStartupScript(this.GetType(), "AlreadyLoggedInAlert",
                    "alert('Already Logged In.. Logout First!'); window.location='Dashboard.aspx';", true);
                return;
            }

            if (!IsPostBack)
            {
                PopulateDistrictDropdown();
                GenerateCaptcha();
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {

            Response.Redirect("Login.aspx");

        }

        // Show the Add User form
        protected void BtnAddUser_Click(object sender, EventArgs e)
        {
            // Logic for button click
            string selectedUserType = ddlUserType.SelectedValue;

            if (string.IsNullOrEmpty(selectedUserType))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select a user type!');", true);
                return;
            }
            // Make the form visible when "Add New User" is clicked
            addUserForm.Visible = true;
        }

        // Populate district dropdown with Maharashtra districts
        private void PopulateDistrictDropdown()
        {
            List<string> districts = new List<string>
            {
                "Ahilyanagar", "Akola", "Amravati", "Beed", "Bhandara", "Buldhana",
                "Chandrapur", "Chhatrapati Sambhajinagar", "Dharashiv", "Dhule", "Gadchiroli", "Gondia",
                "Hingoli", "Jalgaon", "Jalna", "Kolhapur", "Latur", "Mumbai",
                "Mumbai Suburban", "Nagpur", "Nanded", "Nandurbar", "Nashik", "Palghar",
                "Parbhani", "Pune", "Raigad", "Ratnagiri", "Sangli", "Satara",
                "Sindhudurg", "Solapur", "Thane", "Wardha", "Washim", "Yavatmal"
            };

            ddlDistrict.DataSource = districts;
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("Select District", ""));
        }

        // Generate a new captcha
        private void GenerateCaptcha()
        {
            Random random = new Random();
            int captchaValue = random.Next(1000, 9999);
            lblCaptcha.Text = captchaValue.ToString();
        }

        protected void BtnGenerateCaptcha_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }

        // Save user details
        protected void BtnSave_Click(object sender, EventArgs e)
        { // Retrieving values from form controls
            string userName = txtUserName.Text.Trim();  // User Name
            string userMobile = txtUserMobile.Text.Trim();  // User Mobile
            string userEmail = txtUserEmail.Text.Trim();  // User Email
            string district = ddlDistrict.SelectedValue;  // District from DropDownList
            string userType = ddlUserType.SelectedValue;  // Assuming you have a DropDownList for user types
            string password = txtPassword.Text.Trim();  // Password (consider hashing before saving)
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string enteredCaptcha = txtCaptcha.Text.Trim();
            string generatedCaptcha = lblCaptcha.Text;

        
            // Mobile number regex: Indian mobile numbers (10 digits starting with 6, 7, 8, or 9)
            string mobilePattern = @"^[6-9]\d{9}$";
            if (!Regex.IsMatch(userMobile, mobilePattern))
            {
                Response.Write("<script>alert('Invalid mobile number! Please enter a valid 10-digit mobile number starting with 6, 7, 8, or 9.');</script>");
                return;
            }

            // Email regex: Standard email validation
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(userEmail, emailPattern))
            {
                Response.Write("<script>alert('Invalid email address! Please enter a valid email.');</script>");
                return;
            }

            // Save to database (mock implementation)

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UserManagement"].ConnectionString;


           // Check if the mobile number or email already exists in the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE UserMobile = @UserMobile OR UserEmail = @UserEmail";
                using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserMobile", userMobile);
                    cmd.Parameters.AddWithValue("@UserEmail", userEmail);

                    try
                    {
                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            Response.Write("<script>alert('The mobile number or email is already registered. Please use a different one.');</script>");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error checking duplicate mobile/email: " + ex.Message + "');</script>");
                        return;
                    }
                }
            }


            // Validate captcha
            if (enteredCaptcha != generatedCaptcha)
            {
                Response.Write("<script>alert('Invalid Captcha!');</script>");
                return;
            }

            // Validate password
            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            if (!Regex.IsMatch(password, passwordPattern))
            {
                Response.Write("<script>alert('Password must be at least 8 characters, include one letter, one digit, and one special character.');</script>");
                return;
            }

            // Confirm passwords match
            if (password != confirmPassword)
            {
                Response.Write("<script>alert('Passwords do not match!');</script>");
                return;
            }
            if (string.IsNullOrEmpty(userType))
            {
                Response.Write("<script>alert('Please select a user type.');</script>");
                return;
            }
            string userLoginID = $"{DateTime.Now:yyyyMM}{userType}{new Random().Next(100000, 999999)}";


            // SQL query to insert user data into the Users table
            string query = "INSERT INTO Users (UserLoginID, UserPassword, UserName, UserMobile, UserEmail, District, UserType, CreatedAt, UpdatedAt) " +
                           "VALUES (@UserLoginID, @UserPassword, @UserName, @UserMobile, @UserEmail, @District, @UserType, GETDATE(), GETDATE())";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Adding parameters to avoid SQL injection
                    cmd.Parameters.AddWithValue("@UserLoginID", userLoginID);
                    cmd.Parameters.AddWithValue("@UserPassword", password);  // Consider hashing the password before saving
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@UserMobile", userMobile);  // New field
                    cmd.Parameters.AddWithValue("@UserEmail", userEmail);  // New field
                    cmd.Parameters.AddWithValue("@District", district);  // New field
                    cmd.Parameters.AddWithValue("@UserType", userType);  // UserType field

                    try
                    {
                        conn.Open();
                        int i =  cmd.ExecuteNonQuery(); // Execute the insert query
                        if (i > 0)
                        {
                            //Response.Write("<script>alert('User added successfully!');</script>"); // Optional: Display success message
                            // Send email to the user with Login ID
                            SendLoginIDEmail(userEmail, userName, userLoginID);
                           
                        }

                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error: " + ex.Message + "');</script>"); // Optional: Display error message
                    }
                }
            }
            //Response.Write($"<script>alert('Remember Login ID: {userLoginID}');</script>");
            
        }


        private void SendLoginIDEmail(string emailAddress, string userName, string userLoginID)
        {
            string subject = "Welcome to User Management - Your Login ID";
            string body = $@"
        <p>Hi {userName},</p>
        <p>Thank you for registering with us. Here are your login details:</p>
        <p><strong>Login ID:</strong> {userLoginID}</p>
        <p>Please keep this information secure.</p>
        <p>Best regards,<br/>Harsh Jain</p>";

            MailMessage mailMessage = new MailMessage("harshjain76200@gmail.com", emailAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("harshjain76200@gmail.com", "hqhe lboo plnr fvwb");
                smtpClient.Send(mailMessage);
            }
            ResetForm();
            ClientScript.RegisterStartupScript(this.GetType(), "UserAddedAlert",
                "alert('User added successfully! Login ID has been sent to the user\\'s email address.'); window.location='Login.aspx';", true);

        }

        // Reset the form
        private void ResetForm()
        {
            txtUserName.Text = "";
            txtUserMobile.Text = "";
            txtUserEmail.Text = "";
            ddlDistrict.SelectedIndex = 0;
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCaptcha.Text = "";
            GenerateCaptcha();
            addUserForm.Visible = false;
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Back button clicked!');</script>");

            addUserForm.Visible = false;
        }
    }
}
