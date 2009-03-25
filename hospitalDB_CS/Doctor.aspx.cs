using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using MySql.Data.MySqlClient;

public partial class Doctor : System.Web.UI.Page
{
    MySqlConnection conServer;
    MySqlDataReader reader;
    MySqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        //conServer = new MySqlConnection();
        //conServer.ConnectionString = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';";
        //conServer.Open();
        //cmd = new MySqlCommand();
        //cmd.CommandText = "SELECT * FROM patients WHERE Doctor=" + Request.QueryString["empID"];
        //cmd.Connection = conServer;
        //reader = cmd.ExecuteReader();
        //Session["PatientData"] = reader;
    }

    protected void Patients_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Patients.EditIndex = e.NewEditIndex;
        //Patients.DataSource = reader;        
    }
    
    protected void Patients_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Session["Deleting"] = Patients.Rows[e.RowIndex].Cells[0];
    }

    protected void ViewYourPatients_Click(object sender, EventArgs e)
    {
        Patients.DataSource = PatientData;
        Patients.DataBind();
    }
    
    protected void Patients_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    
    protected void Patients_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Patients.EditIndex = -1;
        //Patients.DataSource = reader;
    }
    
    protected void Patients_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
}
