<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Financial.aspx.cs" Inherits="Financial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HospitalDB - Financial View</title>
    <style type="text/css">
        #form1
        {
            height: 526px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <br />
        <asp:Label ID="doctorSearchLabel" runat="server" 
            Text="Doctor Appointment Search"></asp:Label>
        <br />
        <br />
        <asp:Label ID="startYearLabel" runat="server" Text="Start Year"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="startMonthLabel" runat="server" Text="Start Month"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="startDayLabel" runat="server" Text="Start Day"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="startHourLabel" runat="server" Text="Start Hour (24h)"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="startMinuteLabel" runat="server" Text="Start Minute"></asp:Label>
        <br />
        <asp:TextBox ID="startYearBox" runat="server" Width="70px"></asp:TextBox>

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="startMonthBox" runat="server" Width="70px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="startDayBox" runat="server" Width="70px"></asp:TextBox>
&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:TextBox ID="startHourBox" runat="server" Width="70px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:TextBox 
            ID="startMinuteBox" runat="server" Width="70px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="endYearLabel" runat="server" Text="End Year"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="endMonthLabel" runat="server" Text="End Month"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="endDayLabel" runat="server" Text="End Day"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="endHourLabel" runat="server" Text="End Hour (24h)"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="endMinuteLabel" runat="server" Text="End Minute"></asp:Label>
        <br />
        <asp:TextBox ID="endYearBox" runat="server" Width="70px"></asp:TextBox>

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="endMonthBox" runat="server" Width="70px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="endDayBox" runat="server" Width="70px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="endHourBox" runat="server" 
            Width="70px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="endMinuteBox" runat="server" Width="70px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="doctorIDLabel" runat="server" Text="Doctor's ID"></asp:Label>
        <br />
        <asp:TextBox ID="doctorIDBox" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="docApptSearch" runat="server" onclick="docApptSearch_Click" 
            Text="Search" />
    
    </div>
    </form>
</body>
</html>
