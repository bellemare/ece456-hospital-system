<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemLogin.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HospitalDB Login - Welcome</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="userName" runat="server">Username</asp:TextBox>
        <asp:TextBox ID="passBox" runat="server" TextMode="Password">Password</asp:TextBox>
        <asp:Button ID="loginBtn" runat="server" onclick="loginBtn_Click" 
            Text="Login" />
        <br />
        <asp:Label ID="statusLbl" runat="server"></asp:Label>
    
        <asp:RadioButtonList ID="radBtn" runat="server">
            <asp:ListItem>Patient</asp:ListItem>
            <asp:ListItem>Employee</asp:ListItem>
        </asp:RadioButtonList>
    
    </div>
    </form>
</body>
</html>
