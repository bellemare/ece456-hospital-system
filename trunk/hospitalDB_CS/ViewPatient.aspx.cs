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

public partial class ViewPatient : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PatientName.Text = "Patient #" + Request.QueryString["patID"];
        if (Request.QueryString["empID"] == null)
        {
            selfgrid.Visible = true;
        }
    }

    protected void ViewSurgeries_Click(object sender, EventArgs e)
    {
        string surgEmp = "SELECT visits.VisitID,visits.StartDate,visits.EndDate,visits.Comments,surgeries.Type,surgeries.Comments FROM visits,surgeries WHERE PatientID = " + Request.QueryString["patID"] + " AND DoctorID = " + Request.QueryString["empID"] + " AND visits.VisitID = surgeries.VisitID";
        string surgPat = "SELECT visits.VisitID,visits.StartDate,visits.EndDate,visits.Comments,surgeries.Type,surgeries.Comments FROM visits,surgeries WHERE PatientID = " + Request.QueryString["patID"] + " AND visits.VisitID = surgeries.VisitID";
        
        Session["appointments"] = 0;
        Session["visits"] = 0;
        Session["surg"] = 1;
        Session["pres"] = 0;
        if (Request.QueryString["empID"] != null)
        {
            PatientData.SelectCommand = surgEmp;
        }
        else
        {
            PatientData.SelectCommand = surgPat;
        }
    }

    protected void ViewPrescriptions_Click(object sender, EventArgs e)
    {
        string presEmp = "SELECT visits.VisitID,visits.StartDate,visits.EndDate,visits.Comments,prescriptions.Type,prescriptions.Dosage,prescriptions.Comments FROM visits,prescriptions WHERE PatientID = " + Request.QueryString["patID"] + " AND DoctorID = " + Request.QueryString["empID"] + " AND visits.VisitID = prescriptions.VisitID";
        string presPat = "SELECT visits.VisitID,visits.StartDate,visits.EndDate,visits.Comments,prescriptions.Type,prescriptions.Dosage,prescriptions.Comments FROM visits,prescriptions WHERE PatientID = " + Request.QueryString["patID"] + " AND visits.VisitID = prescriptions.VisitID";

        Session["appointments"] = 0;
        Session["visits"] = 0;
        Session["surg"] = 0;
        Session["pres"] = 1;
        if (Request.QueryString["empID"] != null)
        {
            PatientData.SelectCommand = presEmp;
        }
        else
        {
            PatientData.SelectCommand = presPat;
        }
    }
    protected void ViewVisits_Click(object sender, EventArgs e)
    {
        Session["appointments"] = 0;
        Session["visits"] = 1;
        Session["surg"] = 0;
        Session["pres"] = 0;

        if (Request.QueryString["empID"] != null)
        {
            PatientData.SelectCommand = "SELECT visits.VisitID, visits.StartDate, visits.EndDate, visits.Comments, diagnosis.Outcome, diagnosis.Comments FROM visits,diagnosis WHERE PatientID = " + Request.QueryString["patID"] + " AND DoctorID = " + Request.QueryString["empID"] + " AND visits.VisitID = diagnosis.VisitID";
            PatientView.AutoGenerateEditButton = true;
            SurgBut.Visible = true;
            PresBut.Visible = true;
            DiagBut.Visible = true;
            SurgBut.Text = "Edit Surgery Details";
            PresBut.Text = "Edit Prescription Details";
            DiagBut.Text = "Edit Diagnosis Details";
        }
        else
        {
            PatientData.SelectCommand = "SELECT visits.VisitID, visits.StartDate, visits.EndDate, visits.Comments, diagnosis.Outcome, diagnosis.Comments FROM visits,diagnosis WHERE PatientID = " + Request.QueryString["patID"] + " AND visits.VisitID = diagnosis.VisitID";
            PatientView.AutoGenerateEditButton = false;
            SurgBut.Visible = false;
            PresBut.Visible = false;
            DiagBut.Visible = false;
        }
    }
    protected void PatientView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        PatientView.EditIndex = -1;
    }
    protected void PatientView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        PatientView.EditIndex = e.NewEditIndex;
        PatientView.SelectedIndex = e.NewEditIndex;
        if ((int)Session["visits"] == 1)
            ViewVisits_Click(null, EventArgs.Empty);
        else if ((int)Session["surg"] == 1)
            ViewSurgeries_Click(null, EventArgs.Empty);
        else if ((int)Session["pres"] == 1)
            ViewPrescriptions_Click(null, EventArgs.Empty);
        else if ((int)Session["appointments"] == 1)
            Button1_Click(null, EventArgs.Empty);

    }

    protected void PatientView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string oldDate;
        DateTime newDateTime;
        DateTime newDateTime2;


        oldDate = ((TextBox)PatientView.SelectedRow.Cells[2].Controls[0]).Text;
        newDateTime = Convert.ToDateTime(oldDate);
        Session["newStartDate"] = newDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        oldDate = ((TextBox)PatientView.SelectedRow.Cells[3].Controls[0]).Text;
        newDateTime2 = Convert.ToDateTime(oldDate);
        Session["newEndDate"] = newDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        if (newDateTime2 < newDateTime)
        {
            e.Cancel = true;
            Label1.Text = "End Date must be after Start Date";
        }

    }

    protected void PatientView_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if ((int)Session["visits"] == 1)
            ViewVisits_Click(null, EventArgs.Empty);
        else if ((int)Session["surg"] == 1)
            ViewSurgeries_Click(null, EventArgs.Empty);
        else if ((int)Session["pres"] == 1)
            ViewPrescriptions_Click(null, EventArgs.Empty);
        else if ((int)Session["appointments"] == 1)
            Button1_Click(null, EventArgs.Empty);

        Label1.Text = "";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["appointments"] = 1;
        Session["visits"] = 0;
        Session["surg"] = 0;
        Session["pres"] = 0;
        
        if (Request.QueryString["empID"] != null)
        {
            PatientData.SelectCommand = "SELECT DISTINCT visits.VisitID, visits.StartDate, visits.EndDate, visits.Comments FROM visits,diagnosis WHERE PatientID = " + Request.QueryString["patID"] + " AND DoctorID = " + Request.QueryString["empID"] + " AND visits.VisitID not in (Select VisitID from diagnosis)";
            PatientView.AutoGenerateEditButton = true;
            PatientView.AutoGenerateSelectButton = true;
            SurgBut.Visible = true;
            PresBut.Visible = true;
            DiagBut.Visible = true;
            SurgBut.Text = "Edit Surgery Details";
            PresBut.Text = "Edit Prescription Details";
            DiagBut.Text = "Edit Diagnosis Details";
        }
        else
        {
            PatientData.SelectCommand = "SELECT visits.VisitID, visits.StartDate, visits.EndDate, visits.Comments FROM visits WHERE PatientID = " + Request.QueryString["patID"];
            PatientView.AutoGenerateEditButton = false;
            SurgBut.Visible = false;
            PresBut.Visible = false;
            DiagBut.Visible = false;
        }

    }

    protected void selfgrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
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
