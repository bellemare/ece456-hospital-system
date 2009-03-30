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

public partial class Staff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["NowDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        if (!Page.IsPostBack)
        {
            ListItem l;
            for (int i = 1; i <= 12; i++)
            {
                if (i < 10)
                    l = new ListItem("0" + i.ToString(), "0" + i.ToString());
                else
                    l = new ListItem(i.ToString(), i.ToString());
            }
        }

    }
    protected void AppointmentCal_SelectionChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime startDate = AppointmentCal.SelectedDate;
        DateTime endDate = AppointmentCalEnd.SelectedDate;
        string startSearch =  startDate.Year.ToString() + "-" +  startDate.Month.ToString() + "-" + startDate.Day.ToString() + " ";
        string endSearch = endDate.Year.ToString() + "-" + endDate.Month.ToString() + "-" + endDate.Day.ToString() + " ";
        
        if (StartAmPm.SelectedValue == "AM")
        {
            startSearch = startSearch + StartHour.SelectedValue + ":" + StartMin.SelectedValue + ":00";
        }
        else
        {
            startSearch = startSearch + ((int)Convert.ToInt32(StartHour.SelectedValue) + 12).ToString() + ":" + StartMin.SelectedValue + ":00";
        }

        if (EndAmPm.SelectedValue == "AM")
        {
            endSearch = endSearch + EndHour.SelectedValue + ":" + EndMin.SelectedValue + ":00";
        }
        else
        {
            endSearch = endSearch + ((int)Convert.ToInt32(EndHour.SelectedValue) + 12).ToString() + ":" + EndMin.SelectedValue + ":00";
        }

        startDate = Convert.ToDateTime(startSearch);
        endDate = Convert.ToDateTime(endSearch);

        MySqlConnection con = new MySqlConnection("Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';");
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM visits WHERE (('" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + "' between visits.StartDate and visits.EndDate OR '" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + "' between visits.StartDate and visits.EndDate) AND DoctorID = '" + DocNum.SelectedValue + "')", con);
        con.Open();
        MySqlDataReader reader = cmd.ExecuteReader();
        reader.Read();
        if (reader.HasRows)
        {
            Label1.Text = "I'm Sawrry, 'at 'pointment tam's alr'y tayken";
            return;
        }
        if (endDate < startDate)
        {
            Label1.Text = "EndDate must be after StartDate";
            return;
        }
        con.Close();
        con.Open();
        cmd.CommandText = "INSERT INTO visits (PatientID,DoctorID,StartDate,EndDate,Comments) VALUES ('" + PatNum.SelectedValue + "','" + DocNum.SelectedValue + "','" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + CommentsBox.Text + "')";
        cmd.ExecuteNonQuery();
        con.Close();
    }
    protected void AppointmentsBut_Click(object sender, EventArgs e)
    {
        AppointmentView.Visible = true;
    }
    protected void AppointmentView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        AppointmentView.SelectedIndex = e.NewEditIndex;
        AppointmentView.EditIndex = e.NewEditIndex;
    }
    protected void AppointmentView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Session["DeletingVisit"] = AppointmentView.Rows[e.RowIndex].Cells[1].Text;
    }
    protected void btnAddPatient_Click(object sender, EventArgs e)
    {
        if (patientName.Text.Length >= 149)
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
        else if (txtPatientPass.Text.Length >= 50)
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
    }
}
