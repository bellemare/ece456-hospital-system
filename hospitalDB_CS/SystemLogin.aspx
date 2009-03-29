<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemLogin.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HospitalDB Login - Welcome</title>
    <style type="text/css">
        .style1
        {
            color: #000099;
            font-weight: bold;
            text-decoration: underline;
        }
        .style2
        {
            font-weight: bold;
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <span class="style1">Welcome to the Hospital Database</span><br />
        <br />
        <span class="style2">User Login</span><br />
        Please enter your User ID and your password:<br />
    
        <asp:TextBox ID="userName" runat="server">User ID Number</asp:TextBox>
        <asp:TextBox ID="passBox" runat="server" TextMode="Password">Password</asp:TextBox>
        <asp:Button ID="loginBtn" runat="server" onclick="loginBtn_Click" 
            Text="Login" />
        <br />
        <asp:Label ID="statusLbl" runat="server"></asp:Label>
    
        <br />
        <br />
        Please select one of the following:<asp:RadioButtonList ID="radBtn" runat="server">
            <asp:ListItem>Patient</asp:ListItem>
            <asp:ListItem>Employee</asp:ListItem>
        </asp:RadioButtonList>
    
        <hr />
    
    </div>
    </form>
</body>
</html>
