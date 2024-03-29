﻿using System;
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
    MySqlConnection conServer;

    protected void Page_Load(object sender, EventArgs e)
    {
        string strConnection = "";
        conServer = new MySqlConnection();

        strConnection = "Server='localhost';Database='hospital_G004';Uid=root;Pwd='sasha';";
        conServer.ConnectionString = strConnection;

        try
        {
            conServer.Open();
            if (conServer.State == ConnectionState.Open)
            {
                statusLbl.Text = "Connected";
            }
        }
        catch (Exception ex)
        {
            statusLbl.Text = ex.Message;
        }

    }
    protected void loginBtn_Click(object sender, EventArgs e)
    {
        MySqlCommand cmd;
        if (radBtn.Items[0].Selected)
        {
            cmd = new MySqlCommand("Select * from patients where PatientID='" + userName.Text + "' AND Password='" + passBox.Text + "'", conServer);

            try
            {
                MySqlDataReader data;
                data = cmd.ExecuteReader();

                data.Read();
                if (data.HasRows)
                    Response.Redirect("ViewPatient.aspx?patID=" + userName.Text, true);

            }
            catch (Exception ex)
            {
                statusLbl.Text = ex.Message;
            }


        }
        else
        {
            cmd = new MySqlCommand("Select * from employees where EmployeeID='" + userName.Text + "' AND Passwd='" + passBox.Text + "'", conServer);
            try
            {
                MySqlDataReader data;
                data = cmd.ExecuteReader();
                
                data.Read();

                if (data.HasRows && data.GetValue(2).ToString() == "Admin")
                {
                    Response.Redirect("Admin.aspx", true);
                }
                else if (data.HasRows && data.GetValue(2).ToString() == "Doctor")
                {
                    Response.Redirect("Doctor.aspx?empId=" + userName.Text, true);
                }
                else if (data.HasRows && data.GetValue(2).ToString() == "Finance")
                {
                    Response.Redirect("Financial.aspx?empID=" + userName.Text, true);
                }
                else if (data.HasRows && data.GetValue(2).ToString() == "Legal")
                {
                    Response.Redirect("Legal.aspx?empId=" + userName.Text, true);
                }
                else if (data.HasRows && data.GetValue(2).ToString() == "Staff")
                {
                    Response.Redirect("Staff.aspx?empId=" + userName.Text, true);
                }
                else 
                {
                    statusLbl.Text = "Username and/or Password is incorrect";
                }
            }

            catch (Exception ex)
            {
                statusLbl.Text = ex.Message;
            }
        }
    }
}
