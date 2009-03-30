<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Legal.aspx.cs" Inherits="Legal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HospitalDB - Legal View</title>
    <style type="text/css">
        .style1
        {}
        .style2
        {
            text-decoration: underline;
        }
        .style3
        {
            color: #000099;
        }
    </style>
</head>
<body style="font-weight: 700">
    <form id="form1" runat="server">
    <div style="color: #000000">
    
        <span class="style3"><span class="style2">Hospital Database</span><br 
            class="style2" />
        </span>
        <br class="style2" />
        <span class="style1"><span class="style2">Legal and Auditing</span><br />
        <asp:GridView ID="auditGrid" runat="server" DataSourceID="auditConnect">
        </asp:GridView>
        <asp:SqlDataSource ID="auditConnect" runat="server" 
            ConnectionString="<%$ ConnectionStrings:hospital_G004ConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:hospital_G004ConnectionString.ProviderName %>" 
            SelectCommand="SELECT * FROM [audit]"></asp:SqlDataSource>
        <br />
        <asp:Button ID="getAuditButton" runat="server" onclick="getAuditButton_Click" 
            Text="View Audit Tail" />
        <br />
        <hr />
        <br />
        
        <input type="button" value="Back" name="ClickBack" onclick=(history.back())></span></div>
    </form>
</body>
</html>
