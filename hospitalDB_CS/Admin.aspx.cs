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
        if (txtEmpName.Text.Length >= 149)
        {
            Label1.Text = "Patient Name is too long, please try again";
            return;
        }
        else if (txtAddress.Text.Length >= 199)
        {
            Label1.Text = "Address is too long, please try again";
            return;
        }
        else if (txtPhone.Text.Length >= 11)
        {
            Label1.Text = "Phone Number is too long, please try again";
            return;
        }
        else if (txtHealthCard.Text.Length != 10)
        {
            Label1.Text = "Health Card Number is not the correct length, please try again";
            return;
        }
        else if (txtSIN.Text.Length != 9)
        {
            Label1.Text = "SIN is not the correct length (9), please try again";
            return;
        }
        else if (txtEmpPass.Text.Length >= 50)
        {
            Label1.Text = "Password too long, please try again";
            return;
        }
        else if (txtStatus.Text.Length >= 5000)
        {
            Label1.Text = "Status too long, please try again";
            return;
        }

        Label1.Text = "Added Patient Successfully";
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
    protected void patientGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label1.Text = "";
        for (int i = 0; i < e.NewValues.Count; i++)
        {
            switch (i)
            {
                case 0:
                    if (e.NewValues[0].ToString().Length >= 149)
                    {
                        Label1.Text = "Patient Name is too long, please try again";
                        e.Cancel = true;
                    }
                    break;
                case 1:
                    if (e.NewValues[1].ToString().Length >= 199)
                    {
                        Label1.Text = "Address is too long, please try again";
                        e.Cancel = true;
                    }
                    break;
                case 2:
                    if (e.NewValues[2].ToString().Length >= 11)
                    {
                        Label1.Text = "Phone Number is too long, please try again";
                        e.Cancel = true;
                    }
                    break;
                case 3:
                    if (e.NewValues[3].ToString().Length != 10)
                    {
                        Label1.Text = "Health Card Number is not the correct length, please try again";
                        e.Cancel = true;
                    }
                    break;
                case 4:
                    if (e.NewValues[4].ToString().Length != 9)
                    {
                        Label1.Text = "SIN is not the correct length (9), please try again";
                        e.Cancel = true;
                    }
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    MySqlCommand cmd;
                    MySqlConnection con = new MySqlConnection("Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';");
                    con.Open();
                    cmd = new MySqlCommand("Select * from employees where EmployeeID = '" + e.NewValues[7].ToString() + "' AND Job='Doctor'", con);

                    try
                    {
                        MySqlDataReader data;
                        data = cmd.ExecuteReader();

                        data.Read();
                        if (!data.HasRows)
                        {
                            Label1.Text = "Doctor not valid, please try again";
                            e.Cancel = true;
                        }
                        con.Close();

                    }
                    catch (Exception ex)
                    {
                        Label1.Text = ex.Message;
                        con.Close();
                    }

                    break;
                case 8:
                    if (e.NewValues[8].ToString().Length >= 5000)
                    {
                        Label1.Text = "Comment too long, please try again";
                        e.Cancel = true;
                    }
                    break;
                default:
                    break;
            }
            //check each NewValue and see if it's not shit
            //we already know what the column numbers are
            //0=PatientName,1=Address,2=PhoneNum,3=HealthCardNum
            //4=SIN,5=NumVisits,6=Doctor,7=Status
        }

        //this is for audit
        if (e.Cancel != true)
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
            Label1.Text = "Update Successful";
        }
    }
}
