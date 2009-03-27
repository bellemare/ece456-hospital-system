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

public partial class ViewPatient : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PatientName.Text = "Patient #" + Request.QueryString["patID"];
    }

    protected void ViewSurgeries_Click(object sender, EventArgs e)
    {
        string surgEmp = "SELECT visits.VisitID,visits.StartDate,visits.EndDate,visits.Comments,surgeries.Type,surgeries.Comments FROM visits,surgeries WHERE PatientID = " + Request.QueryString["patID"] + " AND DoctorID = " + Request.QueryString["empID"] + " AND visits.VisitID = surgeries.VisitID";
        string surgPat = "SELECT visits.VisitID,visits.StartDate,visits.EndDate,visits.Comments,surgeries.Type,surgeries.Comments FROM visits,surgeries WHERE PatientID = " + Request.QueryString["patID"] + " AND visits.VisitID = surgeries.VisitID";

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
        Session["visits"] = 1;
        Session["surg"] = 0;
        Session["pres"] = 0;

        if (Request.QueryString["empID"] != null)
        {
            PatientData.SelectCommand = "SELECT visits.VisitID, visits.StartDate, visits.EndDate, visits.Comments, diagnosis.Outcome, diagnosis.Comments FROM visits,diagnosis WHERE PatientID = " + Request.QueryString["patID"] + " AND DoctorID = " + Request.QueryString["empID"] + " AND visits.VisitID = diagnosis.VisitID";
            PatientView.AutoGenerateEditButton = true;
        }
        else
        {
            PatientData.SelectCommand = "SELECT visits.VisitID, visits.StartDate, visits.EndDate, visits.Comments, diagnosis.Outcome, diagnosis.Comments FROM visits,diagnosis WHERE PatientID = " + Request.QueryString["patID"] + " AND visits.VisitID = diagnosis.VisitID";
            PatientView.AutoGenerateEditButton = false;
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

    }

    protected void PatientView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string oldDate;
        DateTime newDateTime;

        oldDate = ((TextBox)PatientView.SelectedRow.Cells[2].Controls[0]).Text;
        newDateTime = Convert.ToDateTime(oldDate);
        Session["newStartDate"] = newDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        oldDate = ((TextBox)PatientView.SelectedRow.Cells[3].Controls[0]).Text;
        newDateTime = Convert.ToDateTime(oldDate);
        Session["newEndDate"] = newDateTime.ToString("yyyy-MM-dd HH:mm:ss");       
    }

    protected void PatientView_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        if ((int)Session["visits"] == 1)
            ViewVisits_Click(null, EventArgs.Empty);
        else if ((int)Session["surg"] == 1)
            ViewSurgeries_Click(null, EventArgs.Empty);
        else if ((int)Session["pres"] == 1)
            ViewPrescriptions_Click(null, EventArgs.Empty);
    }
}
