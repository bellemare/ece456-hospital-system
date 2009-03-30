<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor.aspx.cs" Inherits="Doctor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HospitalDB - Doctor View</title>
    <style type="text/css">
        .style1
        {
            font-weight: bold;
            text-decoration: underline;
            color: #000099;
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
    <div class="style1">
    
        Hospital Database</div>
    <asp:Label ID="Label1" runat="server" Text="::"></asp:Label>
    <asp:Label ID="Label2" runat="server" Text="::"></asp:Label>
    <br />
    <span class="style2">Doctor</span><br />
    <p>
        View all patients assigned to you:&nbsp;<br />
        <asp:Button ID="ViewYourPatients" runat="server" onclick="ViewYourPatients_Click" 
            Text="View Your Patients" Width="160px" />
    </p>
    Search for a specific patient (User ID, Name, Status, Comments)<br />
    <asp:Button ID="btnSearch" runat="server" Text="Search Patients" 
        onclick="btnSearch_Click" />
    &nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtPatient" runat="server"></asp:TextBox>
    <br />
    <asp:GridView ID="Patients" runat="server" 
        AutoGenerateEditButton="True" 
        onrowediting="Patients_RowEditing" 
        onrowcancelingedit="Patients_RowCancelingEdit" 
        DataSourceID="PatientData" DataKeyNames="PatientID" 
        onrowupdating="Patients_RowUpdating" AutoGenerateSelectButton="True" 
        onselectedindexchanging="Patients_SelectedIndexChanging">
        <SelectedRowStyle BorderColor="Red" BorderStyle="Solid" />
    </asp:GridView>
    <asp:SqlDataSource ID="PatientData" runat="server" 
        ConnectionString="<%$ ConnectionStrings:hospital_G004ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:hospital_G004ConnectionString.ProviderName %>" 
        SelectCommand="SELECT PatientID,PatientName,Address,PhoneNum,(SELECT COUNT(*) FROM visits as v WHERE v.PatientID=p.PatientID) as NumVisits,Doctor,Status FROM patients as p WHERE (Doctor = @empID)" 
        UpdateCommand="UPDATE patients SET PatientName=@PatientName, Address=@Address,PhoneNum=@PhoneNum,NumVisits=@NumVisits,Doctor=@Doctor,Status=@Status WHERE PatientID=@PatientID"
        DeleteCommand="DELETE FROM patients WHERE PatientID=@PatientID">
        <DeleteParameters>
            <asp:SessionParameter SessionField="Deleting" Name="PatientID" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="empID" QueryStringField="empID" />
        </SelectParameters>
        <UpdateParameters>
            <asp:ControlParameter ControlID="Patients" PropertyName="SelectedRow.Cells[0].Text" Name="PatientID" Type="UInt32" />
            <asp:ControlParameter ControlID="Patients" PropertyName="SelectedRow.Cells[1].Text" Name="PatientName" Type="String" />
            <asp:ControlParameter ControlID="Patients" PropertyName="SelectedRow.Cells[2].Text" Name="Address" Type="String" />
            <asp:ControlParameter ControlID="Patients" PropertyName="SelectedRow.Cells[3].Text" Name="PhoneNum" Type="String" />
            <asp:ControlParameter ControlID="Patients" PropertyName="SelectedRow.Cells[4].Text" Name="HealthCardNum" Type="String" />
            <asp:ControlParameter ControlID="Patients" PropertyName="SelectedRow.Cells[5].Text" Name="SIN" Type="String" />
            <asp:ControlParameter ControlID="Patients" PropertyName="SelectedRow.Cells[6].Text" Name="NumVisits" Type="UInt32" />
            <asp:ControlParameter ControlID="Patients" PropertyName="SelectedRow.Cells[7].Text" Name="Doctor" Type="UInt32" />
            <asp:ControlParameter ControlID="Patients" PropertyName="SelectedRow.Cells[8].Text" Name="Status" Type="UInt32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    View the details of the selected patient:<br />
    <asp:Button ID="EditPatient" runat="server" Text="View Patient Details" 
        onclick="EditPatient_Clicked" Width="160px" />
    <asp:Button ID="btnViewRight" runat="server" Text="Viewing Rights" 
        onclick="btnViewRight_Click" />
    <br />
    <br />
    <asp:Button ID="btnViewAll" runat="server" Text="View All Patients" 
        onclick="btnViewAll_Click" />
    <asp:GridView ID="allpatientGrid" runat="server" DataSourceID="viewall" 
        Visible="False">
    </asp:GridView>
    <asp:SqlDataSource ID="viewall" runat="server" 
        ConnectionString="<%$ ConnectionStrings:hospital_G004ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:hospital_G004ConnectionString.ProviderName %>" 
        SelectCommand="SELECT PatientID, PatientName, Doctor FROM patients"></asp:SqlDataSource>
    </form>
<hr />
<p>
        
        <input type="button" value="Back" name="ClickBack" onclick=(history.back())></p>
</body>
</html>
