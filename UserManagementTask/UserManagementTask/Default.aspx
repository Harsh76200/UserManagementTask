<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserManagementTask.Default" %>


<!DOCTYPE html>
<html>
<head>
    <title>User Management</title>
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@100;200;300;400;500;600;700;800;900&display=swap');
        body {
            font-family: 'Montserrat', sans-serif;
            margin: 0;
            padding: 0;
            background-color:#ead7c3;
        }

        form {
            max-width:500px;
            margin: 20px auto;
            background-color: white;
            border-radius: 8px;
            padding: 20px;
            box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
        }

        h2, h3 {
            font-size:x-large;
            font-weight:bolder;
            color: black;
            text-align: center;
        }

        label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
            color: #333;
        }

        .form-control {
            width: 100%;
            padding: 10px;
          
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-sizing: border-box;
            font-size: 14px;
        }

        .btn {
            padding: 10px 15px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            margin: 5px;
             font-size: 14px;
  font-weight: bold;
font-family:'Montserrat', sans-serif;
        }

        .btn-success {
            background-color: #28a745;
            color: white;
        }

        .btn-success:hover {
            background-color: #218838;
        }

        .btn-danger {
            background-color: #dc3545;
            color: white;
        }

        .btn-danger:hover {
            background-color: #c82333;
        }

        .btn-secondary {
            background-color: #6c757d;
            color: white;
            position:absolute;
            font-size:12px;
        }

        .btn-secondary:hover {
            background-color: #5a6268;
        }

        #form1{
             background-color:#fbf6ef;
        }

        #addUserForm {
            margin-top: 20px;
            padding: 20px;
            background-color:whitesmoke;
            border: 1px solid #ddd;
            border-radius: 8px;
        }

        #ddlUserType, #ddlDistrict {
            height: 45px;
        }

       .captcha-container {
    background-color: #999999; /* Keep the background color */
    color: white; /* Font color */
    font-family: 'Lucida Handwriting', cursive; /* Font family */
    font-size: 14pt; /* Reduced font size */
    font-style: italic; /* Italic style */
    font-weight: bold; /* Bold font */
    text-decoration: none; /* Removed line-through for better readability */
    text-align: left; /* Align text to the left */
    padding: 5px; /* Reduced padding */
    margin-bottom: 15px; /* Spacing from elements below */
    border-radius: 3px; /* Smaller border radius */
    user-select: none; /* Prevent text selection */
    pointer-events: none; /* Prevent interactions */
    width: fit-content; /* Adjust width to fit the content */
}

        #btnGenerateCaptcha {
            display: block;
            margin: 0 auto;
            margin-bottom: 15px;
        }

        .btn-container {
            display: flex;
            justify-content: space-between;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>User Management</h2>
            <label>User Type: </label>
            <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control">
                <asp:ListItem Text="Select User Type" Value="" />
                <asp:ListItem Text="Candidate" Value="C" />
                <asp:ListItem Text="Staff" Value="S" />
            </asp:DropDownList>

            <asp:Button ID="btnAddUser" runat="server" Text="Add New User" CssClass="btn btn-success" OnClick="BtnAddUser_Click" />

            <!-- Add New User Form -->
            <asp:Panel ID="addUserForm" runat="server" Visible="false">
                <h3>Add New User</h3>
                
                <label>User Name:</label>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" ></asp:TextBox>
                
                <label>User Mobile Number:</label>
                <asp:TextBox ID="txtUserMobile" runat="server" CssClass="form-control" ></asp:TextBox>

                <label>User Email ID:</label>
                <asp:TextBox ID="txtUserEmail" runat="server" CssClass="form-control" ></asp:TextBox>
                
                <label>District:</label>
                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control"></asp:DropDownList>

                <label>Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" ></asp:TextBox>

                <label>Confirm Password:</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" ></asp:TextBox>

                <label>Captcha:</label>
                <asp:Label 
                    ID="lblCaptcha" 
                    runat="server" 
                    CssClass="form-control captcha-container" 
                    BackColor="#999999" 
                    Font-Bold="True" 
                    Font-Italic="True" 
                    Font-Names="Lucida Handwriting" 
                    Font-Size="15pt" 
                    Font-Strikeout="True" 
                    Font-Underline="False" 
                    ForeColor="White" BorderColor="#999999" BorderWidth="5px"></asp:Label>
                  <asp:Button ID="btnGenerateCaptcha" runat="server" Text="Refresh Captcha ↻ " CssClass="btn btn-secondary" OnClick="BtnGenerateCaptcha_Click" />


             <asp:TextBox ID="txtCaptcha" runat="server" CssClass="form-control" Style="margin-top:45px;"></asp:TextBox>

                <div class="btn-container">
                    <asp:Button ID="btnBack" runat="server" Text="<< Back" CssClass="btn btn-danger" OnClick="BtnBack_Click" />
               
                    <asp:Button ID="btnLogin" runat="server" Text="Login" Style="margin-left:200px;" CssClass="btn btn-secondary" OnClick="BtnLogin_Click"/>
                    <asp:Button ID="btnSave" runat="server" Text="Save ✓" CssClass="btn btn-success" OnClick="BtnSave_Click" />
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
