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

    protected void Page_Load(object sender, EventArgs e)
    {
        conServer = new MySqlConnection();
        conServer.ConnectionString = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='root';";
        conServer.Open();
        cmd = new MySqlCommand();
        cmd.Connection = conServer;
    }
    protected void docApptSearch_Click(object sender, EventArgs e)
    {
        
        int docID = 0;
        docID = Int32.Parse(doctorIDBox.Text);
        int[] startDate = new int[5];
        int[] endDate = new int[5];
        startDate[0] = Int32.Parse(startYearBox.Text);
        if (startDate[0] < 0) startDate[0] = 0;
        startDate[1] = Int32.Parse(startMonthBox.Text);
        if (startDate[1] < 1) startDate[1] = 1;
        else if(startDate[1] > 12) startDate[1] = 12;
       
        startDate[2] = Int32.Parse(startDayBox.Text);
        if (startDate[2] < 1) startDate[2] = 1;
        else if (startDate[2] > 31) startDate[2] = 31;

        startDate[3] = Int32.Parse(startHourBox.Text);
        if(startDate[3] < 0) startDate[3] = 0;
        else if(startDate[3] > 23) startDate[3] = 23;

        startDate[4] = Int32.Parse(startMinuteBox.Text);
        if(startDate[4] < 0) startDate[4] = 0;
        else if(startDate[4] > 59) startDate[4] = 59;


        endDate[0] = Int32.Parse(endYearBox.Text);
        if(endDate[0] < 0) endDate[0] = 0;

        endDate[1] = Int32.Parse(endMonthBox.Text);
        if(endDate[1] < 1) endDate[1] = 1;
        else if(endDate[1] > 12) endDate[1] = 12;

        endDate[2] = Int32.Parse(endDayBox.Text);
        if(endDate[2] < 1) endDate[4] = 1;
        else if(endDate[2] > 31) endDate[2] = 31;

        endDate[3] = Int32.Parse(endHourBox.Text);
        if(endDate[3] < 0) endDate[3] = 0;
        else if(endDate[3] > 23) endDate[3] = 23;

        endDate[4] = Int32.Parse(endMinuteBox.Text);
        if(endDate[4] < 0) endDate[4] = 0;
        else if(endDate[4] > 59) endDate[4] = 59;

        DateTime end = new DateTime(endDate[0], endDate[1], endDate[2], endDate[3], endDate[4], 0);
        DateTime start = new DateTime(startDate[0], startDate[1], startDate[2], startDate[3], startDate[4], 0);
    
        //doctorIDLabel.Text = start.ToString();
        //doctorSearchLabel.Text = end.ToString();
        

        cmd.CommandText = "SELECT * FROM visits WHERE DoctorID=" + docID;
        reader = cmd.ExecuteReader();
        apptResultGrid.DataSource = reader;
        apptResultGrid.DataBind();

        /*
         * Add New Appointment code:
          

        


                DateTime newDate = new DateTime(2004, 9, 14, 12, 30, 0);
                DateTime newDate2 = new DateTime(2005, 10, 15, 1, 59, 10);
                int newVisitID = 435;
                int newDoctorID = 11;
                int newPatientID = 85;
                string newComments = "kid is just too fat";


                cmd.CommandText = " INSERT INTO visits (VisitID, PatientID, DoctorID, StartDate, EndDate, Comments) VALUES ( " + newVisitID + ", " + newPatientID + ", " + newDoctorID + ", '" + newDate.ToString("s") + "', '" + newDate2.ToString("s") +"','" + newComments + "' )";

                cmd.ExecuteNonQuery();
         *  */

    }
}
