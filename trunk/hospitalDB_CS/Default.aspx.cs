using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using MySql.Data.MySqlClient;

public partial class _Default : System.Web.UI.Page 
{
    MySqlConnection conServer;
    protected void Page_Load(object sender, EventArgs e)
    {
        string strConnection = "";
        conServer = new MySqlConnection();

        strConnection = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';";

        conServer.ConnectionString = strConnection;

        //MySqlCommand cmd = new MySqlCommand("CREATE TABLE Employees(EmployeeID INT NOT NULL, EmployeeName varchar(150) NOT NULL, Job varchar(150) NOT NULL, Passwd varchar(50) NOT NULL, PRIMARY KEY(EmployeeID))",conServer);

        //MySqlCommand cmd = new MySqlCommand("CREATE TABLE Visits(VisitID int NOT NULL,PatientID int NOT NULL,DoctorID int NOT NULL,StartDate DateTime NOT NULL,EndDate DateTime NOT NULL,Comments text,PRIMARY KEY(VisitID))", conServer);
         
        //MySqlCommand cmd = new MySqlCommand("CREATE TABLE Patients(PatientID int NOT NULL, PatientName varchar(150) NOT NULL, Address varchar(200) NOT NULL, PhoneNum char(10) NOT NULL, HealthCardNum char(20) NOT NULL, SIN char(10) NOT NULL, Password varchar(50) NOT NULL, NumVisits int NOT NULL, Doctor int, Status int NOT NULL,PRIMARY KEY(PatientID))", conServer);
        
        //MySqlCommand cmd = new MySqlCommand("CREATE TABLE Surgeries(SurgeryID int NOT NULL, VisitID int NOT NULL, Type varchar(250) NOT NULL, Comments text, PRIMARY KEY(SurgeryID,VisitID))", conServer);
        
        //MySqlCommand cmd = new MySqlCommand("CREATE TABLE Prescriptions(PrescriptionID int NOT NULL, VisitID int NOT NULL, Type varchar(250) NOT NULL, Dosage varchar(250) NOT NULL, Comments text, PRIMARY KEY(PrescriptionID,VisitID))", conServer);
        
        //MySqlCommand cmd = new MySqlCommand("CREATE TABLE Diagnosis(DiagnosisID int NOT NULL, VisitID int NOT NULL, Outcome text NOT NULL, Comments text, PRIMARY KEY(DiagnosisID,VisitID))", conServer);

        //MySqlCommand cmd = new MySqlCommand("CREATE TABLE Diagnosis(DiagnosisID int NOT NULL, VisitID int NOT NULL, Outcome text NOT NULL, Comments text, PRIMARY KEY(DiagnosisID,VisitID))", conServer);

        MySqlCommand cmd = new MySqlCommand("CREATE TABLE ViewingRights(EmployeeID int NOT NULL, PatientID int NOT NULL, PRIMARY KEY(EmployeeID,PatientID))", conServer);
            
        try
        {
            Label1.Text = "Connection to MySQL opened through OLE DB Provider";
            conServer.Open();
            if (conServer.State == ConnectionState.Open)
            {
                Label1.Text = "!!!!!!!";

                if (cmd.ExecuteNonQuery() == 0)
                {
                    Label1.Text = "Yah";
                }
                else
                {
                    Label1.Text = "Nay";
                }
            }
            conServer.Close();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
}
