<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditPrescriptions.aspx.cs" Inherits="EditPrescriptions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <div>
    
        <asp:Label ID="Label1" runat="server"></asp:Label>
    
    </div>
    <asp:TextBox ID="PresType" runat="server">Prescription Type</asp:TextBox>
    <asp:TextBox ID="DosageTxt" runat="server" Height="101px" TextMode="MultiLine" 
        Width="178px">Dosage</asp:TextBox>
    <asp:TextBox ID="Commentstxt" runat="server" Height="100px" 
        TextMode="MultiLine" Width="195px">Comments</asp:TextBox>
    <asp:Button ID="AddDetailsBut" runat="server" onclick="AddDetailsBut_Click" 
        Text="Add Prescription Details" />
    </form>
</body>
</html>
