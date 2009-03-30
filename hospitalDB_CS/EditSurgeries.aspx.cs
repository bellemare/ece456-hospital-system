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

public partial class EditSurgeries : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["IsSurgUpdate"] = 0;
            Label1.Text = "Edit Surgery Details for visit #" + Request.QueryString["visID"];

            MySqlConnection con = new MySqlConnection("Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';");

            MySqlCommand cmd = new MySqlCommand("SELECT Type,Comments from surgeries WHERE VisitID=" + Request.QueryString["visID"], con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            if (reader.HasRows)
            {
                Session["IsSurgUpdate"] = 1;
                SurgType.Text = reader.GetString("Type");
                Commentstxt.Text = reader.GetString("Comments");
            }
        }
    }
    protected void AddDetailsBut_Click(object sender, EventArgs e)
    {
        if ( (int)Session["IsSurgUpdate"] == 0 )
        {
            MySqlConnection con = new MySqlConnection("Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';");
            MySqlCommand cmd = new MySqlCommand("INSERT INTO surgeries (VisitID,Type,Comments) VALUES ('" + Request.QueryString["visID"] + "','" + SurgType.Text + "','" + Commentstxt.Text + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("ViewPatient.aspx?empID=" + Request.QueryString["empID"] + "&patID=" + Request.QueryString["patID"]);
        }
        else if ( (int)Session["IsSurgUpdate"] == 1 )
        {
            MySqlConnection con = new MySqlConnection("Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';");
            MySqlCommand cmd = new MySqlCommand("UPDATE surgeries SET Type='" + SurgType.Text + "',Comments='" + Commentstxt.Text + "' WHERE VisitID= " + Request.QueryString["visID"], con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("ViewPatient.aspx?empID=" + Request.QueryString["empID"] + "&patID=" + Request.QueryString["patID"]);

        }
    }


}
