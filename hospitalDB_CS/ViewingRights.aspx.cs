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

public partial class ViewingRights : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void viewrightsgrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Session["empID"] = viewrightsgrid.Rows[e.RowIndex].Cells[1].Text;
        Session["patID"] = Request.QueryString["patID"];
    }
    protected void btnAddView_Click(object sender, EventArgs e)
    {
        MySqlConnection con = new MySqlConnection("Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';");
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "SELECT * FROM viewingrights WHERE EmployeeID = '" + drList.SelectedValue.ToString() + "' AND PatientID = '" + Request.QueryString["patID"].ToString() + "'";
        cmd.Connection = con;
        con.Open();
        MySqlDataReader reader = cmd.ExecuteReader();
        reader.Read();

        if (!reader.HasRows)
        {
            con.Close();
            con.Open();
            cmd.CommandText = "INSERT INTO viewingrights (EmployeeID, PatientID) VALUES ('" + drList.SelectedValue.ToString() + "', '" + Request.QueryString["patID"].ToString() + "' )";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            viewrightsgrid.DataBind();
            Label1.Text = "Added Successfully";
        }
        else
        {
            Label1.Text = "Entry Already Exists, cannot add";
        }
        reader.Close();
        con.Close();

    }
    protected void viewrightsgrid_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        drList.DataBind();
    }
    protected void viewrightsgrid_RowCreated(object sender, GridViewRowEventArgs e)
    {
    }
    protected void viewrightsgrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        for (int i = 0; i < e.NewValues.Count; i++)
        {
            if (e.NewValues[i].ToString() != e.OldValues[i].ToString())
            {
                MySqlConnection con = new MySqlConnection("Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';");
                MySqlCommand cmd = new MySqlCommand();
                //string now = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + DateTime.Now.ToLongTimeString();
                string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                cmd.CommandText = "INSERT INTO audit (TableChanged, ColumnChanged, OldValue, NewValue, DateChanged) VALUES ('patients', '" + (i + 1).ToString() + "', '" + e.OldValues[i].ToString() + "', '" + e.NewValues[i].ToString() + "', '" + now + "' )";
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
