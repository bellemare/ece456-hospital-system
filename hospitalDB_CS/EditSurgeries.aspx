<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditSurgeries.aspx.cs" Inherits="EditSurgeries" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server"></asp:Label>
    
    </div>
    <asp:TextBox ID="SurgType" runat="server">Surgery Type</asp:TextBox>
    <asp:TextBox ID="Commentstxt" runat="server" Height="100px" 
        TextMode="MultiLine" Width="195px">Comments</asp:TextBox>
    <asp:Button ID="AddDetailsBut" runat="server" onclick="AddDetailsBut_Click" 
        Text="Add Surgery Details" />
    </form>
</body>
</html>
