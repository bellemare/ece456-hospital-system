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

    protected void Page_Load(object sender, EventArgs e)
    {
        if( !Page.IsPostBack )
            Patients.Visible = false;
    }

    protected void Patients_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Patients.EditIndex = e.NewEditIndex;
        Patients.SelectedIndex = e.NewEditIndex;
    }

    protected void ViewYourPatients_Click(object sender, EventArgs e)
    {
        Patients.Visible = true;
    }
    
    protected void Patients_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Patients.EditIndex = -1;
    }
    
    protected void Patients_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //this is for error checking
        for (int i = 0; i < e.NewValues.Count; i++)
        {
            //check each NewValue and see if it's not shit
            //we already know what the column numbers are
            //0=PatientName,1=Address,2=PhoneNum,3=HealthCardNum
            //4=SIN,5=NumVisits,6=Doctor,7=Status
        }

        //this is for audit
        for (int i = 0; i < e.NewValues.Count; i++)
        {
            if (e.NewValues[i].ToString() != e.OldValues[i].ToString())
            {
                MySqlConnection con = new MySqlConnection("Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';");
                MySqlCommand cmd = new MySqlCommand();
                //string now = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + DateTime.Now.ToLongTimeString();
                string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.CommandText = "INSERT INTO audit (TableChanged, ColumnChanged, OldValue, NewValue, DateChanged) VALUES ('patients', '" + (i+1).ToString() + "', '" + e.OldValues[i].ToString() + "', '" + e.NewValues[i].ToString() + "', '" + now + "' )";
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string oldSel = PatientData.SelectCommand;
        PatientData.SelectCommand = "Select * from Patients where Doctor=" + Request.QueryString["empID"] + " AND ( PatientName LIKE '%" + txtPatient.Text + "%' OR PatientID LIKE '%" + txtPatient.Text + "%' OR SIN LIKE '%" + txtPatient.Text + "%')";
        Patients.DataSource = PatientData;
        Patients.DataBind();
        PatientData.SelectCommand = oldSel;
    }

    protected void  EditPatient_Clicked(object sender, EventArgs e)
    {
        if (Patients.SelectedIndex >= 0)
        {
            Response.Redirect("ViewPatient.aspx?patID=" + Patients.DataKeys[Patients.SelectedIndex].Values[0].ToString() + "&empID=" + Request.QueryString["empID"] );
        }
    }
    protected void Patients_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (Patients.EditIndex != -1)
        {
            e.Cancel = true; 
        }
    }
}
