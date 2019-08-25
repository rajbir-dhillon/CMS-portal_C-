using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Webnapp
{
    public partial class _Default : Page
    {
        SqlConnection scon = new SqlConnection("Data Source=sql-server;Initial Catalog=rd5038m;Persist Security Info=True;MultipleActiveResultSets=True;User ID=rd5038m;Password=!1SQLServer");
        StringBuilder htmlTable = new StringBuilder();

        public void Bind_ListPet()
        {
            scon.Open();
            SqlCommand com = new SqlCommand("SELECT * FROM PetType", scon);
            SqlDataReader dr = com.ExecuteReader();
            ListPet.DataSource = dr;
            ListPet.Items.Clear();
            ListPet.Items.Add("Please select pet type");
            ListPet.DataTextField = "PetType";
            ListPet.DataValueField = "PetType_ID";
            ListPet.DataBind();
            scon.Close();
        }

        public void Bind_Listbreed()
        {
            scon.Open();
            SqlCommand com = new SqlCommand("SELECT * FROM PetBreed WHERE PetType_ID='" + ListPet.SelectedValue + "'", scon);
            SqlDataReader dr = com.ExecuteReader();
            Listbreed.DataSource = dr;
            Listbreed.Items.Clear();
            Listbreed.Items.Add("Please select pet breed");
            Listbreed.DataTextField = "BreedName";
            Listbreed.DataValueField = "Breed_ID";
            Listbreed.DataBind();
            scon.Close();
        }

        protected void Page_Load(object sender, EventArgs e)
            {
            if (!IsPostBack)
                {
                Bind_ListPet();
                }

            }

        protected void ListPet_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_Listbreed();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT * FROM pets WHERE Breed_ID ='" + Listbreed.SelectedValue + "' AND Sanc='" + ListSanc.SelectedValue + "' AND adopted='False'";
                scon.Open();
                SqlDataReader articleReader = scmd.ExecuteReader();

                htmlTable.Append("<table border='1'>");
                htmlTable.Append("<tr><th>Pet_ID</th><th>Name</th><th>Type</th><th>Breed</th><th>Weight</th><th>Age</th><th>Gender</th><th>Housing cost</th><th>food cost</th><th>vet cost</th><th>rescuse date</th></tr>");

                if (articleReader.HasRows)
                {
                    while (articleReader.Read())
                    {
                        using (SqlCommand com = new SqlCommand("SELECT * FROM PetType join PetBreed on PetType.PetType_ID = PetBreed.PetType_ID where Breed_ID='" + articleReader["Breed_ID"] + "'",scon))
                        {
                            
                            SqlDataReader queryReader = com.ExecuteReader();
                            queryReader.Read();
                            htmlTable.Append("<tr>");
                            htmlTable.Append("<td>" + articleReader["Pet_ID"] + "</td>");
                            htmlTable.Append("<td>" + articleReader["Name"] + "</td>");
                            htmlTable.Append("<td>" + queryReader["PetType"] + "</td>");
                            htmlTable.Append("<td>" + queryReader["BreedName"] + "</td>");
                            htmlTable.Append("<td>" + articleReader["Weight"] + "</td>");
                            htmlTable.Append("<td>" + articleReader["Age"] + "</td>");
                            htmlTable.Append("<td>" + articleReader["Gender"] + "</td>");
                            htmlTable.Append("<td>" + queryReader["housingCost"] + "</td>");
                            htmlTable.Append("<td>" + queryReader["foodCost"] + "</td>");
                            htmlTable.Append("<td>" + queryReader["vetCost"] + "</td>");
                            htmlTable.Append("<td>" + articleReader["Rescue_date"] + "</td>");
                            htmlTable.Append("</tr>");

                        }
                    }
                    htmlTable.Append("</table>");

                    PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });

                    articleReader.Close();
                    articleReader.Dispose();
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            using (SqlCommand scmd = new SqlCommand())
            {
                scmd.Connection = scon;
                scmd.CommandType = CommandType.Text;
                scmd.CommandText = "SELECT * FROM pets WHERE adopted='False'";
                scon.Open();
                SqlDataReader articleReader = scmd.ExecuteReader();

                htmlTable.Append("<table border='1'>");
                htmlTable.Append("<tr><th>Pet_ID</th><th>Name</th><th>Type</th><th>Breed</th><th>Weight</th><th>Age</th><th>Gender</th><th>Housing cost</th><th>food cost</th><th>vet cost</th><th>rescuse date</th><th>Adopt</th></tr>");

                if (articleReader.HasRows)
                {
                    while (articleReader.Read())
                    {
                        using (SqlCommand com = new SqlCommand("SELECT * FROM PetType join PetBreed on PetType.PetType_ID = PetBreed.PetType_ID where Breed_ID='" + articleReader["Breed_ID"] + "'", scon))
                        {

                            SqlDataReader queryReader = com.ExecuteReader();
                            queryReader.Read();
                            htmlTable.Append("<tr>");
                            htmlTable.Append("<td>" + articleReader["Pet_ID"] + "</td>");
                            htmlTable.Append("<td>" + articleReader["Name"] + "</td>");
                            htmlTable.Append("<td>" + queryReader["PetType"] + "</td>");
                            htmlTable.Append("<td>" + queryReader["BreedName"] + "</td>");
                            htmlTable.Append("<td>" + articleReader["Weight"] + "</td>");
                            htmlTable.Append("<td>" + articleReader["Age"] + "</td>");
                            htmlTable.Append("<td>" + articleReader["Gender"] + "</td>");
                            htmlTable.Append("<td>" + queryReader["housingCost"] + "</td>");
                            htmlTable.Append("<td>" + queryReader["foodCost"] + "</td>");
                            htmlTable.Append("<td>" + queryReader["vetCost"] + "</td>");
                            htmlTable.Append("<td>" + articleReader["Rescue_date"] + "</td>");
                            htmlTable.Append("<td><a href='adopt.aspx?adoptID=" + articleReader["Pet_ID"] + "' class='btn btn-primary'>Adopt</a></th>");
                            htmlTable.Append("</tr>");
                        }
                    }
                    htmlTable.Append("</table>");

                    PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });

                    articleReader.Close();
                    articleReader.Dispose();
                }
            }
        }
    }
    }