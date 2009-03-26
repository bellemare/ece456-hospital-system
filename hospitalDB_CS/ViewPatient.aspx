<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPatient.aspx.cs" Inherits="ViewPatient" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        #form1
        {
            height: 437px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="PatientName" runat="server"></asp:Label>
        <br />
    
        <asp:Button ID="ViewVisits" runat="server" Text="View All Visits" 
            onclick="ViewVisits_Click" />
    
        <asp:Button ID="ViewSurgeries" runat="server" Text="View Surgeries" 
            onclick="ViewSurgeries_Click" />
        <asp:Button ID="ViewPrescriptions" runat="server" Text="View Prescriptions" 
            onclick="ViewPrescriptions_Click" />
    
    </div>
    <asp:GridView ID="PatientView" runat="server">
    </asp:GridView>
    <asp:SqlDataSource ID="PatientData" runat="server" 
        ConnectionString="<%$ ConnectionStrings:hospital_G004ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:hospital_G004ConnectionString.ProviderName %>" >
        
        </asp:SqlDataSource>
    </form>
</body>
</html>
