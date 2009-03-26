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
        PatientData.SelectCommand = "SELECT * FROM visits INNER JOIN surgeries ON surgeries.VisitID=visits.VisitID WHERE PatientID = " + Request.QueryString["patID"] + " AND DoctorID = " + Request.QueryString["empID"];    
        PatientView.DataSource = PatientData;
        PatientView.DataBind();
    }
    protected void ViewPrescriptions_Click(object sender, EventArgs e)
    {
        PatientData.SelectCommand = "SELECT * FROM visits INNER JOIN prescriptions ON prescriptions.VisitID=visits.VisitID WHERE PatientID = " + Request.QueryString["patID"] + " AND DoctorID = " + Request.QueryString["empID"];
        PatientView.DataSource = PatientData;
        PatientView.DataBind();
    }
    protected void ViewVisits_Click(object sender, EventArgs e)
    {
        PatientData.SelectCommand = "SELECT visits.VisitID, visits.StartDate, visits.EndDate, visits.Comments, diagnosis.Outcome, diagnosis.Comments FROM visits,diagnosis WHERE PatientID = " + Request.QueryString["patID"] + " AND DoctorID = " + Request.QueryString["empID"] + " AND visits.VisitID = diagnosis.VisitID";
        PatientView.DataSource = PatientData;
        PatientView.DataBind();
    }
}
