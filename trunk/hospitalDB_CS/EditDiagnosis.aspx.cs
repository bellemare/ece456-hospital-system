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

public partial class EditDiagnosis : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["IsDiagUpdated"] = 0;
            Label1.Text = "Edit Prescription Details for visit #" + Request.QueryString["visID"];

            MySqlConnection con = new MySqlConnection("Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';");

            MySqlCommand cmd = new MySqlCommand("SELECT Outcome,Comments from diagnosis WHERE VisitID=" + Request.QueryString["visID"], con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            if (reader.HasRows)
            {
                Session["IsDiagUpdated"] = 1;
                OutcomeTxt.Text = reader.GetString("Outcome");
                Commentstxt.Text = reader.GetString("Comments");
            }
        }
    }

    protected void AddDetailsBut_Click(object sender, EventArgs e)
    {
        if ((int)Session["IsDiagUpdated"] == 0)
        {
            MySqlConnection con = new MySqlConnection("Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';");
            MySqlCommand cmd = new MySqlCommand("INSERT INTO diagnosis (VisitID,Outcome,Comments) VALUES ('" + Request.QueryString["visID"] + "','" + OutcomeTxt.Text + "','" + Commentstxt.Text + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("ViewPatient.aspx?empID=" + Request.QueryString["empID"] + "&patID=" + Request.QueryString["patID"]);
        }
        else if ((int)Session["IsDiagUpdated"] == 1)
        {
            MySqlConnection con = new MySqlConnection("Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';");
            MySqlCommand cmd = new MySqlCommand("UPDATE diagnosis SET Outcome='" + OutcomeTxt.Text + "',Comments='" + Commentstxt.Text + "' WHERE VisitID= " + Request.QueryString["visID"], con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("ViewPatient.aspx?empID=" + Request.QueryString["empID"] + "&patID=" + Request.QueryString["patID"]);
        }

    }

}
