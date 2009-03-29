<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Staff.aspx.cs" Inherits="Staff" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HospitalDB - Staff View</title>
    <style type="text/css">
        .style1
        {
            font-weight: bold;
            text-decoration: underline;
            color: #000099;
        }
    </style>
</head>
<body>
    <p class="style1">
        Hospital Database</p>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <br />
        <asp:TextBox ID="PatNum" runat="server">Patient Number</asp:TextBox>
        <asp:TextBox ID="DocNum" runat="server">Doctor Number</asp:TextBox>
    
        <hr />
    
    </div>
    <asp:Calendar ID="AppointmentCal" runat="server" Caption="Start Date" 
        onselectionchanged="AppointmentCal_SelectionChanged"></asp:Calendar>
    <asp:DropDownList ID="StartHour" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="StartMin" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="StartAmPm" runat="server">
    </asp:DropDownList>
    <br />
    <asp:Calendar ID="AppointmentCalEnd" runat="server" Caption="End Date">
    </asp:Calendar>
    <asp:DropDownList ID="EndHour" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="EndMin" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="EndAmPm" runat="server">
    </asp:DropDownList>
    <br />
    \<asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Book Appointment" />
    </form>
<p class="style1">
        
        <input type="button" value="Back" name="ClickBack" onclick=(history.back())></p>
</body>
</html>
