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

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAddEmp_Click(object sender, EventArgs e)
    {
        MySqlConnection con = new MySqlConnection("Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';");
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandText = "INSERT INTO employees (EmployeeName, Job, Passwd) VALUES ('"+ txtEmpName.Text + "', '" + jobList.SelectedValue +"', '" + txtEmpPass.Text +"' )";
        cmd.Connection = con;
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }
    protected void employeeData_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        empGrid.DataSource = employeeData;
        empGrid.DataBind();
    }
}
