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
        conServer = new MySqlConnection();
        conServer.ConnectionString = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';";

        conServer.Open();
        Label1.Text = Request.QueryString["empID"];
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        cmd = new MySqlCommand();
        cmd.CommandText = "SELECT * FROM patients WHERE Doctor=1";
        cmd.Connection = conServer;

        reader = cmd.ExecuteReader();
        Patients.DataSource = reader;
        Patients.DataBind();
    }

    protected void Patients_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Patients.EditIndex = e.NewEditIndex;
        Button1_Click( null, EventArgs.Empty);

    }
    protected void Patients_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Patients.DeleteRow(e.RowIndex);
        Patients.DataBind();
    }
}
