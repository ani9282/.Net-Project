﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default3 : System.Web.UI.Page
{
    String data;
    SqlConnection con = new SqlConnection();
    SqlCommand com;
    int i = 0;
    String st = "product";
    int sd;
    protected void Page_Load(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

        con = new SqlConnection(conString);
        con.Open();
        cmd.Connection = con;
        cmd.CommandText = "select count(*) as Total from staffregistration ";
        String i = cmd.ExecuteScalar().ToString();
        int s = Convert.ToInt32(i);
        int[] arr = new int[s];
        String[] arr1 = new String[s];
        String[] arr2 = new String[s];
        String q2 = "select * from staffregistration ";
        SqlCommand com1 = new SqlCommand(q2, con);
        int count = 0;
        SqlDataReader reader1 = com1.ExecuteReader();
        while (reader1.Read())
        {
            arr[count] = Convert.ToInt32(reader1[0].ToString());     //imid
            arr1[count] = reader1[3].ToString();  //price
            arr2[count] = reader1[1].ToString();    //imname
            count++;


        }
        int mycount = 0;

        for (int d = 1; d <= s; d++)
        {
            ImageButton image = new ImageButton();
            image.ID = arr[mycount].ToString();
            // image = new ImageButton();
            // image.Attributes.Add("PostBackUrl", "ProdDetails.aspx");
            image.Click += new System.Web.UI.ImageClickEventHandler(this.image_Click);

            Label lbl = new Label();
            lbl.Text = "₹" + arr1[mycount];
            lbl.CssClass = ("lm");
            Label lbl1 = new Label();
            lbl1.Text = arr2[mycount];
            lbl1.CssClass = ("lm2");

            image.Width = 300;
            image.Height = 300;
            byte[] bytes = (byte[])GetData("SELECT photo FROM staffregistration WHERE teacherid ='" + arr[mycount] + "'").Rows[0]["photo"];

            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            image.ImageUrl = "data:image/png;base64," + base64String;

            ImagesDiv.Controls.Add(image);
            image.CssClass = ("lm1");
            ImagesDiv.Controls.Add(lbl);
            ImagesDiv.Controls.Add(lbl1);
            mycount++;

        }
    }

    void image_Click(object sender, ImageClickEventArgs e)
    {
        string message = "";
        //foreach (ImageButton image in ImagesDiv.Controls.OfType<ImageButton>())
        //{
        //    if (e.Equals(image.ID))
        //    {
        //        message += image.ID + ":\\n";
        //    }
        //}
        ImageButton sourceImageButton = sender as ImageButton;
        if (sourceImageButton.ID == sourceImageButton.ID)
        {

            //You know which image button fire that event }
            // ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + sourceImageButton.ID + "');", true);


            HttpCookie name1 = new HttpCookie("Name1");
            name1.Value = sourceImageButton.ID;
            Response.Cookies.Add(name1);
            Response.Redirect("~/Staffregistration.aspx");

        }

        //Session["img"] = image.ImageUrl;

    }


    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;


        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }


}