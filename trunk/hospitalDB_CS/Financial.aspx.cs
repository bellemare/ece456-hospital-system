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

public partial class Financial : System.Web.UI.Page
{
    MySqlConnection conServer;
    MySqlDataReader reader;
    MySqlCommand cmd;
    protected int[] getDateTimeStart()
    {
        int[] startDate = new int[5];
        try
        {
            startDate[0] = Int32.Parse(startYearBox.Text);
            if (startDate[0] < 1900) startDate[0] = 1900;
            else if (startDate[0] > 2100) startDate[0] = 2100;
        }
        catch
        {
            startDate[0] = 1950;
        }

        try
        {
            startDate[1] = Int32.Parse(startMonthBox.Text);
            if (startDate[1] < 1) startDate[1] = 1;
            else if (startDate[1] > 12) startDate[1] = 12;
        }
        catch
        {
            startDate[1] = 1;
        }

        try
        {
            startDate[2] = Int32.Parse(startDayBox.Text);
            if (startDate[2] < 1) startDate[2] = 1;
            else if (startDate[2] > 31) startDate[2] = 31;
        }
        catch
        {
            startDate[2] = 1;

        }

        try
        {
            startDate[3] = Int32.Parse(startHourBox.Text);
            if (startDate[3] < 0) startDate[3] = 0;
            else if (startDate[3] > 23) startDate[3] = 23;
        }
        catch
        {
            startDate[3] = 1;
        }
        try
        {
            startDate[4] = Int32.Parse(startMinuteBox.Text);
            if (startDate[4] < 0) startDate[4] = 0;
            else if (startDate[4] > 59) startDate[4] = 59;
        }
        catch
        {
            startDate[4] = 1;
        }


        startYearBox.Text = startDate[0].ToString();
        startMonthBox.Text = startDate[1].ToString();
        startDayBox.Text = startDate[2].ToString();
        startHourBox.Text = startDate[3].ToString();
        startMinuteBox.Text = startDate[4].ToString();
        return startDate;
    }

    protected int[] getDateTimeEnd()
    {
        int[] endDate = new int[5];
        try
        {
            endDate[0] = Int32.Parse(endYearBox.Text);
            if (endDate[0] < 1900) endDate[0] = 1900;
            else if (endDate[0] > 2100) endDate[0] = 2100;
        }
        catch
        {
            endDate[0] = 2009;
        }

        try
        {
            endDate[1] = Int32.Parse(endMonthBox.Text);
            if (endDate[1] < 1) endDate[1] = 1;
            else if (endDate[1] > 12) endDate[1] = 12;
        }
        catch
        {
            endDate[1] = 1;
        }

        try
        {
            endDate[2] = Int32.Parse(endDayBox.Text);
            if (endDate[2] < 1) endDate[4] = 1;
            else if (endDate[2] > 31) endDate[2] = 31;
        }
        catch
        {
            endDate[2] = 1;
        }

        try
        {
            endDate[3] = Int32.Parse(endHourBox.Text);
            if (endDate[3] < 0) endDate[3] = 0;
            else if (endDate[3] > 23) endDate[3] = 23;
        }
        catch
        {
            endDate[3] = 1;
        }

        try
        {
            endDate[4] = Int32.Parse(endMinuteBox.Text);
            if (endDate[4] < 0) endDate[4] = 0;
            else if (endDate[4] > 59) endDate[4] = 59;
        }
        catch
        {
            endDate[4] = 1;
        }

        endYearBox.Text = endDate[0].ToString();
        endMonthBox.Text = endDate[1].ToString();
        endDayBox.Text = endDate[2].ToString();
        endHourBox.Text = endDate[3].ToString();
        endMinuteBox.Text = endDate[4].ToString();
        return endDate;
    }
    protected int getDocID()
    {
        int docID = -1;
        try
        {
            docID = Int32.Parse(doctorIDBox.Text);
        }
        catch
        {
            docID = 0;
        }
        return docID;
    }

    protected int getPatientID()
    {
        int patID = -1;
        try
        {
            patID = Int32.Parse(patientIDBox.Text);
        }
        catch
        {
            patID = 0;
        }
        return patID;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void docApptSearch_Click(object sender, EventArgs e)
    {
        int docID = getDocID();


        int[] endDate = new int[5];
        int[] startDate = new int[5];

        endDate = getDateTimeEnd();
        startDate = getDateTimeStart();

        DateTime end = new DateTime(endDate[0], endDate[1], endDate[2], endDate[3], endDate[4], 0);
        DateTime start = new DateTime(startDate[0], startDate[1], startDate[2], startDate[3], startDate[4], 0);
    
        conServer = new MySqlConnection();
        conServer.ConnectionString = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';";
        conServer.Open();
        cmd = new MySqlCommand();
        cmd.Connection = conServer;
        //cmd.CommandText = "SELECT * FROM visits WHERE DoctorID=" + docID + " AND StartDate >='" + start.ToString("s") + "' AND StartDate <='" + end.ToString("s") + "'"; 

        cmd.CommandText = "select visits.VisitID, visits.PatientID, visits.DoctorID, visits.StartDate, visits.EndDate, visits.Comments from visits,patients WHERE DoctorID=" + docID + " AND StartDate >='" + start.ToString("s") + "' AND StartDate <='" + end.ToString("s") + "' AND visits.PatientID = patients.PatientID";
        reader = cmd.ExecuteReader();
        apptResultGrid.DataSource = reader;
        apptResultGrid.DataBind();

        conServer.Close();
        /* Add New Appointment code:
        *
                DateTime newDate = new DateTime(2008, 1, 14, 16, 30, 0);
                DateTime newDate2 = new DateTime(2008, 1, 18, 17, 00, 10);
                int newVisitID = 443;
                int newDoctorID = 3;
                int newPatientID = 88;
                string newComments = "ate a wrench";
                cmd.CommandText = " INSERT INTO visits (VisitID, PatientID, DoctorID, StartDate, EndDate, Comments) VALUES ( " + newVisitID + ", " + newPatientID + ", " + newDoctorID + ", '" + newDate.ToString("s") + "', '" + newDate2.ToString("s") +"','" + newComments + "' )";
                cmd.ExecuteNonQuery();
        */

    }
    protected void doctorPres_Click(object sender, EventArgs e)
    {
        int docID = getDocID();
        int[] endDate = new int[5];
        int[] startDate = new int[5];

        endDate = getDateTimeEnd();
        startDate = getDateTimeStart();

        DateTime end = new DateTime(endDate[0], endDate[1], endDate[2], endDate[3], endDate[4], 0);
        DateTime start = new DateTime(startDate[0], startDate[1], startDate[2], startDate[3], startDate[4], 0);

        conServer = new MySqlConnection();
        conServer.ConnectionString = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';";
        conServer.Open();
        cmd = new MySqlCommand();
        cmd.Connection = conServer;
        cmd.CommandText = "select * from visits,prescriptions WHERE DoctorID=" + docID + " AND StartDate >='" + start.ToString("s") + "' AND StartDate <='" + end.ToString("s") + "' AND visits.VisitID = prescriptions.VisitID";

        reader = cmd.ExecuteReader();
        apptResultGrid.DataSource = reader;
        apptResultGrid.DataBind();
        conServer.Close();
    }
    protected void doctorSurgeriesButton_Click(object sender, EventArgs e)
    {
        int docID = getDocID();
        int[] endDate = new int[5];
        int[] startDate = new int[5];

        endDate = getDateTimeEnd();
        startDate = getDateTimeStart();

        DateTime end = new DateTime(endDate[0], endDate[1], endDate[2], endDate[3], endDate[4], 0);
        DateTime start = new DateTime(startDate[0], startDate[1], startDate[2], startDate[3], startDate[4], 0);

        conServer = new MySqlConnection();
        conServer.ConnectionString = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';";
        conServer.Open();
        cmd = new MySqlCommand();
        cmd.Connection = conServer;
        cmd.CommandText = "select * from visits,surgeries WHERE DoctorID=" + docID + " AND StartDate >='" + start.ToString("s") + "' AND StartDate <='" + end.ToString("s") + "' AND visits.VisitID = surgeries.VisitID";

        reader = cmd.ExecuteReader();
        apptResultGrid.DataSource = reader;
        apptResultGrid.DataBind();
        conServer.Close();
    }
    protected void doctorDiagnosisButton_Click(object sender, EventArgs e)
    {
        int docID = getDocID();
        int[] endDate = new int[5];
        int[] startDate = new int[5];

        endDate = getDateTimeEnd();
        startDate = getDateTimeStart();

        DateTime end = new DateTime(endDate[0], endDate[1], endDate[2], endDate[3], endDate[4], 0);
        DateTime start = new DateTime(startDate[0], startDate[1], startDate[2], startDate[3], startDate[4], 0);

        conServer = new MySqlConnection();
        conServer.ConnectionString = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';";
        conServer.Open();
        cmd = new MySqlCommand();
        cmd.Connection = conServer;
        cmd.CommandText = "select * from visits,diagnosis WHERE DoctorID=" + docID + " AND StartDate >='" + start.ToString("s") + "' AND StartDate <='" + end.ToString("s") + "' AND visits.VisitID = diagnosis.VisitID";

        reader = cmd.ExecuteReader();
        apptResultGrid.DataSource = reader;
        apptResultGrid.DataBind();
        conServer.Close();
    }
    protected void patientVisitsButton_Click(object sender, EventArgs e)
    {
        int patientID = getPatientID();
        int[] endDate = new int[5];
        int[] startDate = new int[5];

        endDate = getDateTimeEnd();
        startDate = getDateTimeStart();

        DateTime end = new DateTime(endDate[0], endDate[1], endDate[2], endDate[3], endDate[4], 0);
        DateTime start = new DateTime(startDate[0], startDate[1], startDate[2], startDate[3], startDate[4], 0);

        conServer = new MySqlConnection();
        conServer.ConnectionString = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';";
        conServer.Open();
        cmd = new MySqlCommand();
        cmd.Connection = conServer;
        cmd.CommandText = "select * from visits WHERE PatientID=" + patientID + " AND StartDate >='" + start.ToString("s") + "' AND StartDate <='" + end.ToString("s") + "'";

        reader = cmd.ExecuteReader();
        apptResultGrid.DataSource = reader;
        apptResultGrid.DataBind();
        conServer.Close();
    }
    protected void patientPresButton_Click(object sender, EventArgs e)
    {
        int patientID = getPatientID();
        int[] endDate = new int[5];
        int[] startDate = new int[5];

        endDate = getDateTimeEnd();
        startDate = getDateTimeStart();

        DateTime end = new DateTime(endDate[0], endDate[1], endDate[2], endDate[3], endDate[4], 0);
        DateTime start = new DateTime(startDate[0], startDate[1], startDate[2], startDate[3], startDate[4], 0);

        conServer = new MySqlConnection();
        conServer.ConnectionString = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';";
        conServer.Open();
        cmd = new MySqlCommand();
        cmd.Connection = conServer;
        cmd.CommandText = "select * from visits,prescriptions WHERE PatientID=" + patientID + " AND StartDate >='" + start.ToString("s") + "' AND StartDate <='" + end.ToString("s") + "' AND visits.VisitID = prescriptions.VisitID";

        reader = cmd.ExecuteReader();
        apptResultGrid.DataSource = reader;
        apptResultGrid.DataBind();
        conServer.Close();
    }
    protected void patientSurgeriesButton_Click(object sender, EventArgs e)
    {
        int patientID = getPatientID();
        int[] endDate = new int[5];
        int[] startDate = new int[5];

        endDate = getDateTimeEnd();
        startDate = getDateTimeStart();

        DateTime end = new DateTime(endDate[0], endDate[1], endDate[2], endDate[3], endDate[4], 0);
        DateTime start = new DateTime(startDate[0], startDate[1], startDate[2], startDate[3], startDate[4], 0);

        conServer = new MySqlConnection();
        conServer.ConnectionString = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';";
        conServer.Open();
        cmd = new MySqlCommand();
        cmd.Connection = conServer;
        cmd.CommandText = "select * from visits,surgeries WHERE PatientID=" + patientID + " AND StartDate >='" + start.ToString("s") + "' AND StartDate <='" + end.ToString("s") + "' AND visits.VisitID = surgeries.VisitID";

        reader = cmd.ExecuteReader();
        apptResultGrid.DataSource = reader;
        apptResultGrid.DataBind();
        conServer.Close();
    }
    protected void patientDiagnosisButton_Click(object sender, EventArgs e)
    {
        int patientID = getPatientID();
        int[] endDate = new int[5];
        int[] startDate = new int[5];

        endDate = getDateTimeEnd();
        startDate = getDateTimeStart();

        DateTime end = new DateTime(endDate[0], endDate[1], endDate[2], endDate[3], endDate[4], 0);
        DateTime start = new DateTime(startDate[0], startDate[1], startDate[2], startDate[3], startDate[4], 0);

        conServer = new MySqlConnection();
        conServer.ConnectionString = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';";
        conServer.Open();
        cmd = new MySqlCommand();
        cmd.Connection = conServer;
        cmd.CommandText = "select * from visits,diagnosis WHERE PatientID=" + patientID + " AND StartDate >='" + start.ToString("s") + "' AND StartDate <='" + end.ToString("s") + "' AND visits.VisitID = diagnosis.VisitID";

        reader = cmd.ExecuteReader();
        apptResultGrid.DataSource = reader;
        apptResultGrid.DataBind();
        conServer.Close();
    }
    protected void patToDocButton_Click(object sender, EventArgs e)
    {
        int patientID = getPatientID();
        int docID = getDocID();
        int[] endDate = new int[5];
        int[] startDate = new int[5];

        endDate = getDateTimeEnd();
        startDate = getDateTimeStart();

        DateTime end = new DateTime(endDate[0], endDate[1], endDate[2], endDate[3], endDate[4], 0);
        DateTime start = new DateTime(startDate[0], startDate[1], startDate[2], startDate[3], startDate[4], 0);

        conServer = new MySqlConnection();
        conServer.ConnectionString = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';";
        conServer.Open();
        cmd = new MySqlCommand();
        cmd.Connection = conServer;
        cmd.CommandText = "select * from visits WHERE PatientID=" + patientID + " AND DoctorID=" + docID + " AND StartDate >='" + start.ToString("s") + "' AND StartDate <='" + end.ToString("s")+"'";

        reader = cmd.ExecuteReader();
        apptResultGrid.DataSource = reader;
        apptResultGrid.DataBind();
        conServer.Close();
    }
    protected void patPresDocButton_Click(object sender, EventArgs e)
    {
        int patientID = getPatientID();
        int docID = getDocID();
        int[] endDate = new int[5];
        int[] startDate = new int[5];

        endDate = getDateTimeEnd();
        startDate = getDateTimeStart();

        DateTime end = new DateTime(endDate[0], endDate[1], endDate[2], endDate[3], endDate[4], 0);
        DateTime start = new DateTime(startDate[0], startDate[1], startDate[2], startDate[3], startDate[4], 0);

        conServer = new MySqlConnection();
        conServer.ConnectionString = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';";
        conServer.Open();
        cmd = new MySqlCommand();
        cmd.Connection = conServer;
        cmd.CommandText = "select * from visits,prescriptions WHERE PatientID=" + patientID + " AND DoctorID=" + docID + " AND StartDate >='" + start.ToString("s") + "' AND StartDate <='" + end.ToString("s") + "' AND prescriptions.VisitID = visits.VisitID";

        reader = cmd.ExecuteReader();
        apptResultGrid.DataSource = reader;
        apptResultGrid.DataBind();
        conServer.Close();
    }
    protected void patSurgDocButton_Click(object sender, EventArgs e)
    {
        int patientID = getPatientID();
        int docID = getDocID();
        int[] endDate = new int[5];
        int[] startDate = new int[5];

        endDate = getDateTimeEnd();
        startDate = getDateTimeStart();

        DateTime end = new DateTime(endDate[0], endDate[1], endDate[2], endDate[3], endDate[4], 0);
        DateTime start = new DateTime(startDate[0], startDate[1], startDate[2], startDate[3], startDate[4], 0);

        conServer = new MySqlConnection();
        conServer.ConnectionString = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';";
        conServer.Open();
        cmd = new MySqlCommand();
        cmd.Connection = conServer;
        cmd.CommandText = "select * from visits,surgeries WHERE PatientID=" + patientID + " AND DoctorID=" + docID + " AND StartDate >='" + start.ToString("s") + "' AND StartDate <='" + end.ToString("s") + "' AND surgeries.VisitID = visits.VisitID";

        reader = cmd.ExecuteReader();
        apptResultGrid.DataSource = reader;
        apptResultGrid.DataBind();
        conServer.Close();
    }
    protected void patDiagDocButton_Click(object sender, EventArgs e)
    {
        int patientID = getPatientID();
        int docID = getDocID();
        int[] endDate = new int[5];
        int[] startDate = new int[5];

        endDate = getDateTimeEnd();
        startDate = getDateTimeStart();

        DateTime end = new DateTime(endDate[0], endDate[1], endDate[2], endDate[3], endDate[4], 0);
        DateTime start = new DateTime(startDate[0], startDate[1], startDate[2], startDate[3], startDate[4], 0);

        conServer = new MySqlConnection();
        conServer.ConnectionString = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';";
        conServer.Open();
        cmd = new MySqlCommand();
        cmd.Connection = conServer;
        cmd.CommandText = "select * from visits,diagnosis WHERE PatientID=" + patientID + " AND DoctorID=" + docID + " AND StartDate >='" + start.ToString("s") + "' AND StartDate <='" + end.ToString("s") + "' AND diagnosis.VisitID = visits.VisitID";

        reader = cmd.ExecuteReader();
        apptResultGrid.DataSource = reader;
        apptResultGrid.DataBind();
        conServer.Close();
    }
}
