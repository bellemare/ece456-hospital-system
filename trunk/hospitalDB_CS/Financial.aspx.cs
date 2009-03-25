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

public partial class Financial : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
        if (startDate[1] < 0) startDate[1] = 0;
        else if(startDate[1] > 12) startDate[1] = 12;
       
        startDate[2] = Int32.Parse(startDayBox.Text);
        if (startDate[2] < 0) startDate[2] = 0;
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
        if(endDate[1] < 0) endDate[1] = 0;
        else if(endDate[1] > 12) endDate[1] = 12;

        endDate[2] = Int32.Parse(endDayBox.Text);
        if(endDate[2] < 0) endDate[4] = 0;
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

    }
}
