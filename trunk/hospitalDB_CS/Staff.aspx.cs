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
        if (!Page.IsPostBack)
        {
            ListItem l;
            for (int i = 1; i <= 12; i++)
            {
                if (i < 10)
                    l = new ListItem("0" + i.ToString(), "0" + i.ToString());
                else
                    l = new ListItem(i.ToString(), i.ToString());

                StartHour.Items.Add(l);
                EndHour.Items.Add(l);
            }

            for (int i = 0; i <= 59; i++)
            {
                if (i < 10)
                    l = new ListItem("0" + i.ToString(), "0" + i.ToString());
                else
                    l = new ListItem(i.ToString(), i.ToString());

                StartMin.Items.Add(l);
                EndMin.Items.Add(l);
            }
            l = new ListItem("AM", "AM");
            StartAmPm.Items.Add(l);
            EndAmPm.Items.Add(l);
            l = new ListItem("PM", "PM");
            StartAmPm.Items.Add(l);
            EndAmPm.Items.Add(l);
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
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM visits WHERE ('" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + "' between visits.StartDate and visits.EndDate OR '" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + "' between visits.StartDate and visits.EndDate)", con);
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
        cmd.CommandText = "INSERT INTO visits (PatientID,DoctorID,StartDate,EndDate,Comments) VALUES ('" + PatNum.Text + "','" + DocNum.Text + "','" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + "','')";
        cmd.ExecuteNonQuery();
        con.Close();
    }
}
