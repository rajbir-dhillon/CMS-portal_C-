using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Webnapp
{
    public partial class Contact : Page
    {
        SqlConnection scon = new SqlConnection("Data Source=sql-server;Initial Catalog=rd5038m;Persist Security Info=True;MultipleActiveResultSets=True;User ID=rd5038m;Password=!1SQLServer");
        StringBuilder htmlTable = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            using (SqlCommand scmd = new SqlCommand())
            {
                int total = 0;
                scmd.Connection = scon;
                scmd.CommandText = "SELECT * FROM AdoptionData join pets on AdoptionData.Pet_ID = pets.Pet_ID";
                scon.Open();
                SqlDataReader articleReader = scmd.ExecuteReader();

                htmlTable.Append("<table border='1'>");
                htmlTable.Append("<tr><th>Customer Name</th><th>Pet Name</th><th>Donation</th><th>Date</th></tr>");

                if (articleReader.HasRows)
                {
                    
                    while (articleReader.Read())
                    {
                            htmlTable.Append("<tr>");
                            htmlTable.Append("<td>" + articleReader["CustName"] + "</td>");
                            htmlTable.Append("<td>" + articleReader["Name"] + "</td>");
                            htmlTable.Append("<td> £:" + articleReader["donation"] + "</td>");
                            htmlTable.Append("<td>" + articleReader["dateofadoption"] + "</td>");
                            htmlTable.Append("</tr>");
                            string donation = articleReader["donation"].ToString();
                            int donationint = Int32.Parse(donation);
                            total = total + donationint;

                    }
                }
                htmlTable.Append("<tr>");
                htmlTable.Append("<td> Total =£" + total + "</td>");
                htmlTable.Append("</tr>");
                htmlTable.Append("</table>");

                    PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });

                    articleReader.Close();
                    articleReader.Dispose();
            }
        }
    }
}
