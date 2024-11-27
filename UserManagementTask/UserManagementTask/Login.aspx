<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UserManagementTask.WebForm2" %>

<!DOCTYPE html>
<html>
<head>
    <title>User Login</title>
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@100;200;300;400;500;600;700;800;900&display=swap');
        body {
            font-family: 'Montserrat', sans-serif;
            margin: 0;
            padding: 0;
            background-color: #ead7c3;
        }

        form {
            max-width: 500px;
            margin: 20px auto;
            background-color: white;
            border-radius: 8px;
            padding: 20px;
            box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
        }

        h2 {
            font-size: x-large;
            font-weight: bolder;
            color: black;
            text-align: center;
            margin-bottom: 20px;
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
            width: 100%;
            padding: 10px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            background-color: #28a745;
            color: white;
            margin-top: 10px;
             font-size: 16px;
              font-weight: bold;
            font-family:'Montserrat', sans-serif;
        }

        .btn:hover {
            background-color: #218838;
        }

        .error-message {
            color: red;
            font-size: 12px;
            text-align: center;
            margin-top: 10px;
        }

        #form1 {
            background-color: #fbf6ef;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>User Login</h2>
        <label for="txtUserID">User ID:</label>
        <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control"></asp:TextBox>

        <label for="txtPassword">Password:</label>
        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>

        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn" OnClick="BtnLogin_Click" />

        <asp:Label ID="lblMessage" runat="server" CssClass="error-message"></asp:Label>
    </form>
</body>
</html>
