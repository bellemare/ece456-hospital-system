<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPatient.aspx.cs" Inherits="ViewPatient" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Patient Information - HospitalDB</title>
    <style type="text/css">
        #form1
        {
            height: 437px;
        }
        .style1
        {
            font-weight: bold;
            text-decoration: underline;
            color: #000099;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <span class="style1">Hospital Database<br />
        </span>
        <br />
        Welcome,     
        <asp:Label ID="PatientName" runat="server"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <asp:GridView ID="selfgrid" runat="server" AutoGenerateEditButton="True" 
            DataSourceID="selfinfo" Visible="False" DataKeyNames="PatientID" 
            onrowupdating="selfgrid_RowUpdating">
        </asp:GridView>
        <asp:SqlDataSource ID="selfinfo" runat="server" 
            ConnectionString="<%$ ConnectionStrings:hospital_G004ConnectionString %>" 
            InsertCommand="s" 
            ProviderName="<%$ ConnectionStrings:hospital_G004ConnectionString.ProviderName %>" 
            SelectCommand="SELECT PatientID, PatientName, Address, PhoneNum, HealthCardNum, SIN, Password FROM patients WHERE PatientID = @patID" 
            UpdateCommand="UPDATE patients SET PatientName=@PatientName, Address=@Address,PhoneNum=@PhoneNum,HealthCardNum=@HealthCardNum,SIN=@SIN,Password=@Password WHERE PatientID=@patID">
                    <SelectParameters>
            <asp:QueryStringParameter Name="patID" QueryStringField="patID" />
        </SelectParameters>
        <UpdateParameters>
        <asp:QueryStringParameter Name="patID" QueryStringField="patID" />
            <asp:ControlParameter ControlID="selfgrid" PropertyName="SelectedRow.Cells[0].Text" Name="PatientID" Type="UInt32" />
            <asp:ControlParameter ControlID="selfgrid" PropertyName="SelectedRow.Cells[1].Text" Name="PatientName" Type="String" />
            <asp:ControlParameter ControlID="selfgrid" PropertyName="SelectedRow.Cells[2].Text" Name="Address" Type="String" />
            <asp:ControlParameter ControlID="selfgrid" PropertyName="SelectedRow.Cells[3].Text" Name="PhoneNum" Type="String" />
            <asp:ControlParameter ControlID="selfgrid" PropertyName="SelectedRow.Cells[4].Text" Name="HealthCardNum" Type="String" />
            <asp:ControlParameter ControlID="selfgrid" PropertyName="SelectedRow.Cells[5].Text" Name="SIN" Type="String" />
            <asp:ControlParameter ControlID="selfgrid" PropertyName="SelectedRow.Cells[6].Text" Name="Password" Type="String" />
        </UpdateParameters>
        </asp:SqlDataSource>
        <br />
        <hr />
        <br />
        View all your previous and future appointments to the Hospital<br />
    
        <asp:Button ID="ViewVisits" runat="server" Text="View Appointments" 
            onclick="ViewVisits_Click" Width="160px" />
    
        <br />
        <hr />
        <br />
        View all completed and scheduled surgeries<br />
    
        <asp:Button ID="ViewSurgeries" runat="server" Text="View Surgeries" 
            onclick="ViewSurgeries_Click" Width="160px" />
        <br />
        <hr />
        <br />
        View all your past and present prescriptions<br />
        <asp:Button ID="ViewPrescriptions" runat="server" Text="View Prescriptions" 
            onclick="ViewPrescriptions_Click" Width="160px" />
    
        <br />
    
    </div>
    <asp:GridView ID="PatientView" runat="server" 
        onrowcancelingedit="PatientView_RowCancelingEdit" 
        onrowediting="PatientView_RowEditing" DataKeyNames="VisitID" 
        DataSourceID="PatientData" onrowupdating="PatientView_RowUpdating" 
        onrowupdated="PatientView_RowUpdated">
    </asp:GridView>
    <asp:SqlDataSource ID="PatientData" runat="server" 
        ConnectionString="<%$ ConnectionStrings:hospital_G004ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:hospital_G004ConnectionString.ProviderName %>"
        UpdateCommand="UPDATE visits SET visits.StartDate=@StartDate,visits.EndDate=@EndDate,visits.Comments=@Comments WHERE VisitID=@VisitID" >
        <UpdateParameters>
            <asp:SessionParameter Name="StartDate" SessionField="newStartDate" Type="DateTime"/>
            <asp:SessionParameter Name="EndDate" SessionField="newEndDate" Type="DateTime"/>
            <asp:ControlParameter Name="Comments"  ControlID="PatientView" PropertyName="SelectedRow.Cells[4].Text" />
        </UpdateParameters>
        </asp:SqlDataSource>
        <br />
        
        <input type="button" value="Back" name="ClickBack" onclick=(history.back())>
    </form>
</body>
</html>
