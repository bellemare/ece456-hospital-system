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
public partial class EditPrescriptions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["IsPresUpdated"] = 0;
            Label1.Text = "Edit Prescription Details for visit #" + Request.QueryString["visID"];

            MySqlConnection con = new MySqlConnection("Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';");

            MySqlCommand cmd = new MySqlCommand("SELECT Type,Dosage,Comments from prescriptions WHERE VisitID=" + Request.QueryString["visID"], con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            if (reader.HasRows)
            {
                Session["IsPresUpdated"] = 1;
                PresType.Text = reader.GetString("Type");
                DosageTxt.Text = reader.GetString("Dosage");
                Commentstxt.Text = reader.GetString("Comments");
            }
        }
    }
    protected void AddDetailsBut_Click(object sender, EventArgs e)
    {
        if ( (int)Session["IsPresUpdated"] == 0 )
        {
            MySqlConnection con = new MySqlConnection("Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';");
            MySqlCommand cmd = new MySqlCommand("INSERT INTO prescriptions (VisitID,Type,Dosage,Comments) VALUES ('" + Request.QueryString["visID"] + "','" + PresType.Text + "','" + DosageTxt.Text + "','" + Commentstxt.Text + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("ViewPatient.aspx?empID=" + Request.QueryString["empID"] + "&patID=" + Request.QueryString["patID"]);
        }
        else if ((int)Session["IsPresUpdated"] == 1)
        {
            MySqlConnection con = new MySqlConnection("Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';");
            MySqlCommand cmd = new MySqlCommand("UPDATE Prescriptions SET Type='" + PresType.Text + "',Comments='" + Commentstxt.Text + "',Dosage='" + DosageTxt.Text + "' WHERE VisitID= " + Request.QueryString["visID"], con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("ViewPatient.aspx?empID=" + Request.QueryString["empID"] + "&patID=" + Request.QueryString["patID"]);
        }

    }
}
