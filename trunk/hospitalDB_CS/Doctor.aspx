<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor.aspx.cs" Inherits="Doctor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HospitalDB - Doctor View</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:Label ID="Label1" runat="server" Text="::"></asp:Label>
    <asp:Label ID="Label2" runat="server" Text="::"></asp:Label>
    <p>
        <asp:Button ID="ViewYourPatients" runat="server" onclick="ViewYourPatients_Click" 
            Text="View Your Patients" />
    </p>
    <asp:TextBox ID="txtPatient" runat="server"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="Search Patients" 
        onclick="btnSearch_Click" />
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
    <asp:Button ID="EditPatient" runat="server" Text="View Patient Details" 
        onclick="EditPatient_Clicked" />
    <asp:SqlDataSource ID="PatientData" runat="server" 
        ConnectionString="<%$ ConnectionStrings:hospital_G004ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:hospital_G004ConnectionString.ProviderName %>" 
        SelectCommand="SELECT PatientID,PatientName,Address,PhoneNum,HealthCardNum,SIN,(SELECT COUNT(*) FROM visits as v WHERE v.PatientID=p.PatientID) as NumVisits,Doctor,Status FROM patients as p WHERE (Doctor = @empID)" 
        UpdateCommand="UPDATE patients SET PatientName=@PatientName, Address=@Address,PhoneNum=@PhoneNum,HealthCardNum=@HealthCardNum,SIN=@SIN,NumVisits=@NumVisits,Doctor=@Doctor,Status=@Status WHERE PatientID=@PatientID"
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
    </form>
</body>
</html>
