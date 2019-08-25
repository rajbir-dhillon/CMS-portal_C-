using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;

namespace Webnapp
{
    public partial class About : Page
    {
        SqlConnection scon = new SqlConnection("Data Source=sql-server;Initial Catalog=rd5038m;Persist Security Info=True;User ID=rd5038m;Password=!1SQLServer");
        StringBuilder htmlTable = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {

            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT * FROM Customers";
                scon.Open();
                SqlDataReader articleReader = scmd.ExecuteReader();

                htmlTable.Append("<table border='1'>");
                htmlTable.Append("<tr><th>Cust_ID</th><th>Image</th><th>Name</th><th>Email</th><th>Address</th><th>Country</th><th>Contact</th><th>Delete</th></tr>");

                if (articleReader.HasRows)
                {
                    while (articleReader.Read())
                    {
                        byte[] img = (byte[])(articleReader["picture"]);
                        string imgbyte = "data:image/jpg;base64, " + Convert.ToBase64String(img);

                        htmlTable.Append("<tr>");
                        htmlTable.Append("<td>" + articleReader["Cust_ID"] + "</td>");
                        htmlTable.Append("<td><asp:Image ID='Image1' runat='server' src =" + imgbyte + "/></td>");
                        htmlTable.Append("<td>" + articleReader["Name"] + "</td>");
                        htmlTable.Append("<td>" + articleReader["Email"] + "</td>");
                        htmlTable.Append("<td>" + articleReader["Address"] + "</td>");
                        htmlTable.Append("<td>" + articleReader["Country"] + "</td>");
                        htmlTable.Append("<td>" + articleReader["tel_no"] + "</td>");
                        htmlTable.Append("<td><a href='viewcust.aspx?deleteID=" + articleReader["Cust_ID"] + "' class='btn btn-primary'>Delete</a></th>");
                        htmlTable.Append("</tr>");

                    }
                    htmlTable.Append("</table>");

                    PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });

                    articleReader.Close();
                    articleReader.Dispose();
                }
            }
            if (Request.QueryString["deleteID"] != null)
            {
                String delete = Request.QueryString["deleteID"];

                using (SqlCommand com = new SqlCommand("DELETE FROM Customers  where Cust_ID='" + delete + "'", scon))
                {

                    SqlDataReader queryReader = com.ExecuteReader();
                    queryReader.Read();
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('id = " + delete + " has been deleted');", true);
                    Response.Redirect("viewcust.aspx");
                }



            }
        }
    }
}