<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="UserManagementTask.Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
    <style>

         @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@100;200;300;400;500;600;700;800;900&display=swap');
        body {
            font-family: 'Montserrat', sans-serif;
            margin: 0;
            padding: 0;
            background-color: #ead7c3;
        }

        .dashboard {
            max-width: 800px;
            margin: 50px auto;
            padding: 20px;
            background-color: #fbf6ef;
            border-radius: 8px;
            box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
        }

        h2 {
            text-align: center;
            font-size: x-large;
            font-weight: bold;
            color: black;
        }

        .info-grid {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px;
            margin-top: 20px;
        }

        .info-box {
            display: flex;
            flex-direction: column;
            padding: 15px;
            background-color: white;
            border-radius: 8px;
            box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1);
        }

        .info-box label {
            font-weight: bold;
            color: #333;
            margin-bottom: 5px;
        }

        .info-value {
            color: #555;
            font-size: 14px;
            margin-top: 5px;
        }

        .logout-btn {
            display: block;
            margin: 30px auto 0;
            padding: 10px 15px;
            font-size: 16px;
            font-weight: bold;
            color: white;
            background-color: #dc3545;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            width: 150px;
            text-align: center;
        }

        .logout-btn:hover {
            background-color: #c82333;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
       <div class="dashboard">
    <h2>Welcome to the Dashboard</h2>
    <div class="info-grid">
        <div class="info-box">
            <label>User Login ID:</label>
            <asp:Label ID="lblUserLoginID" runat="server" CssClass="info-value"></asp:Label>
        </div>
        <div class="info-box">
            <label>User Type:</label>
            <asp:Label ID="lblUserType" runat="server" CssClass="info-value"></asp:Label>
        </div>
        <div class="info-box">
            <label>User Name:</label>
            <asp:Label ID="lblUserName" runat="server" CssClass="info-value"></asp:Label>
        </div>
        <div class="info-box">
            <label>IP Address:</label>
            <asp:Label ID="lblIPAddress" runat="server" CssClass="info-value"></asp:Label>
        </div>
        <div class="info-box">
            <label>Previous Login Time:</label>
            <asp:Label ID="lblPreviousLoginTime" runat="server" CssClass="info-value"></asp:Label>
        </div>
        <div class="info-box">
            <label>Current Login Time:</label>
            <asp:Label ID="lblCurrentLoginTime" runat="server" CssClass="info-value"></asp:Label>
        </div>
    </div>
    <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="logout-btn" OnClick="BtnLogout_Click" />
</div>

    </form>
</body>
</html>
