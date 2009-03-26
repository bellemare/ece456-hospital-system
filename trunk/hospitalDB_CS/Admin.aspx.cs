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
        employeeData.Insert();
        
    }
    protected void employeeData_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        empGrid.DataBind();
    }
    protected void btnAddPatient_Click(object sender, EventArgs e)
    {
        patientData.Insert();
    }
    protected void patientData_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        patientGrid.DataBind();
    }
    protected void patientGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Session["PatientID"] = patientGrid.Rows[e.RowIndex].Cells[0];
    }
    protected void empGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Session["EmployeeID"] = empGrid.Rows[e.RowIndex].Cells[0];
    }
}
