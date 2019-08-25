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
    public partial class adopt : System.Web.UI.Page
    {
        SqlConnection scon = new SqlConnection("Data Source=sql-server;Initial Catalog=rd5038m;MultipleActiveResultSets=true;Persist Security Info=True;User ID=rd5038m;Password=!1SQLServer");
        StringBuilder htmlTable = new StringBuilder();//start string to store html table
        protected void Page_Load(object sender, EventArgs e)
        {
            Label2.Visible = false;
                if (Request.QueryString["adoptID"] != null)//if adopt id is not null
                {
                    String adopt = Request.QueryString["adoptID"];//save adopt id in string

                    using (SqlCommand com = new SqlCommand("SELECT * FROM pets where Pet_ID='" + adopt + "'", scon))//sql query
                    {
                        scon.Open();// open sql connection
                        SqlDataReader articleReader = com.ExecuteReader();//excute query
                        

                        htmlTable.Append("<table border='1'>");// start stroing table 
                        htmlTable.Append("<tr><th>Pet_ID</th><th>Name</th><th>Type</th><th>Breed</th><th>Weight</th><th>Age</th><th>Gender</th><th>Housing cost</th><th>food cost</th><th>vet cost</th><th>rescuse date</th><th>Sanct</th></tr>");

                        if (articleReader.HasRows)//if query returns result
                        {
                            while (articleReader.Read())//while reader has data
                            {
                                using (SqlCommand comm = new SqlCommand("SELECT * FROM PetType join PetBreed on PetType.PetType_ID = PetBreed.PetType_ID where Breed_ID='" + articleReader["Breed_ID"] + "'", scon))
                                {

                                    SqlDataReader queryReader = comm.ExecuteReader();
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
                                    htmlTable.Append("<th>" + articleReader["Sanc"] + "</td>");
                                    htmlTable.Append("</tr>");//above rows reads data from both queries
                                }
                            
                            }
                            htmlTable.Append("</table>");

                            PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });//add table to asp.net placeholder

                            articleReader.Close();
                            articleReader.Dispose();
                        }
                    
                    


                }
                }
            


        }

        protected void Button1_Click(object sender, EventArgs e)//if button is pressed
        {
            int custid;
            string donation;
            string famestr;
            
            if (!String.IsNullOrEmpty(TextBox1.Text))// cust id is not epmty 
            {
                custid = Int32.Parse(TextBox1.Text); //convert string to int 
                //check if id exists
                using (SqlCommand comm = new SqlCommand("SELECT * FROM Customers where Cust_ID='" + custid + "'", scon))
                {
                    
                    SqlDataReader queryReader = comm.ExecuteReader();
                    queryReader.Read();
                    
                    //if id exists
                    if (queryReader.HasRows)
                    {
                        String adopt = Request.QueryString["adoptID"];
                        using (SqlCommand petCom = new SqlCommand("SELECT * FROM pets join PetBreed on pets.Breed_ID = PetBreed.Breed_ID join PetType on PetType.PetType_ID = PetBreed.PetType_ID where pets.Pet_ID ='" + adopt + "'", scon))

                        {
                            SqlDataReader petReader = petCom.ExecuteReader();
                            petReader.Read();
                            
                            Label2.Text = "id = " + queryReader["Cust_ID"];
                            Label2.Visible = true;
                            //if not donation empty
                            if (!String.IsNullOrEmpty(TextBox2.Text))
                            {
                                //if pet and user from same sanc
                                string petsanc = petReader["Sanc"].ToString();
                                string custsanc = queryReader["Country"].ToString();
                                
                                if (petsanc == custsanc)
                                {
                                    double finalint;
                                    donation = TextBox2.Text;
                                    double donationdouble = Convert.ToDouble(donation);
                                    string currencyvalue = DropDownList1.SelectedValue;

                                    //if pound
                                    if (currencyvalue == "1")
                                    {
                                        finalint = donationdouble;

                                        Label2.Text = "amount = " + finalint;
                                        Label2.Visible = true;

                                        //check donation amount
                                        string pettype = petReader["PetType"].ToString();
                                        petReader.Close();

                                        DateTime today = DateTime.Today;//get todays date
                                        if (pettype == "Cat" && finalint > 50)// if pet type = cat and donation = more than suggested
                                        {
                                            famestr = "yes";//set fame to yes
                                            string custnamestr = queryReader["Name"].ToString();//get name from query and save to string
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame, dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();//run query
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);//update pet to show adopted
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();//clsoe connection
                                            drupdate.Close();
                                            drupdate.Dispose();
                                            Response.Redirect("default.aspx");//redirect to home page
                                        }
                                        else if (pettype == "Dog" && finalint > 100)
                                        {
                                            famestr = "yes";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            drupdate.Close();
                                            drupdate.Dispose();
                                            Response.Redirect("default.aspx");
                                        }
                                        else if (pettype == "Ferret" && finalint > 20)
                                        {
                                            famestr = "yes";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            drupdate.Close();
                                            drupdate.Dispose();
                                            Response.Redirect("default.aspx");
                                        }
                                        else if (pettype == "Chinchilla" && finalint > 20)
                                        {
                                            famestr = "yes";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            drupdate.Close();
                                            drupdate.Dispose();
                                            Response.Redirect("default.aspx");
                                        }
                                        else if (pettype == "Guinea pig" && finalint > 15)
                                        {
                                            famestr = "yes";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            drupdate.Close();
                                            drupdate.Dispose();
                                            Response.Redirect("default.aspx");
                                        }
                                        else if (pettype == "Rabbit" && finalint > 30)
                                        {
                                            famestr = "yes";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            drupdate.Close();
                                            drupdate.Dispose();
                                            Response.Redirect("default.aspx");
                                        }
                                        else
                                        {
                                            famestr = "no";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            drupdate.Close();
                                            drupdate.Dispose();
                                            Response.Redirect("default.aspx");
                                        }
                                        //if euro
                                    }
                                    else if (currencyvalue == "2")
                                    {
                                        finalint = donationdouble * 0.88;
                                        Label2.Text = "amount = " + finalint;
                                        Label2.Visible = true;

                                        Label2.Text = "amount = " + finalint;
                                        Label2.Visible = true;

                                        //check donation amount
                                        string pettype = petReader["PetType"].ToString();


                                        DateTime today = DateTime.Today;//get todays date
                                        if (pettype == "Cat" && finalint > 50)// if pet type = cat and donation = more than suggested
                                        {
                                            famestr = "yes";//set fame to yes
                                            string custnamestr = queryReader["Name"].ToString();//get name from query and save to string
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame, dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();//run query
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);//update pet to show adopted
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();//clsoe connection
                                            Response.Redirect("default.aspx");//redirect to home page
                                        }
                                        else if (pettype == "Dog" && finalint > 100)
                                        {
                                            famestr = "yes";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            Response.Redirect("default.aspx");
                                        }
                                        else if (pettype == "Ferret" && finalint > 20)
                                        {
                                            famestr = "yes";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            Response.Redirect("default.aspx");
                                        }
                                        else if (pettype == "Chinchilla" && finalint > 20)
                                        {
                                            famestr = "yes";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            Response.Redirect("default.aspx");
                                        }
                                        else if (pettype == "Guinea pig" && finalint > 15)
                                        {
                                            famestr = "yes";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            Response.Redirect("default.aspx");
                                        }
                                        else if (pettype == "Rabbit" && finalint > 30)
                                        {
                                            famestr = "yes";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            Response.Redirect("default.aspx");
                                        }
                                        else
                                        {
                                            famestr = "no";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            //if lei
                                        }
                                    }
                                    else if (currencyvalue == "3")
                                    {
                                        finalint = donationdouble * 0.19;
                                        Label2.Text = "amount = " + finalint;
                                        Label2.Visible = true;

                                        Label2.Text = "amount = " + finalint;
                                        Label2.Visible = true;

                                        //check donation amount
                                        string pettype = petReader["PetType"].ToString();


                                        DateTime today = DateTime.Today;//get todays date
                                        if (pettype == "Cat" && finalint > 50)// if pet type = cat and donation = more than suggested
                                        {
                                            famestr = "yes";//set fame to yes
                                            string custnamestr = queryReader["Name"].ToString();//get name from query and save to string
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame, dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();//run query
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);//update pet to show adopted
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();//clsoe connection
                                            Response.Redirect("default.aspx");//redirect to home page
                                        }
                                        else if (pettype == "Dog" && finalint > 100)
                                        {
                                            famestr = "yes";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            Response.Redirect("default.aspx");
                                        }
                                        else if (pettype == "Ferret" && finalint > 20)
                                        {
                                            famestr = "yes";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            Response.Redirect("default.aspx");
                                        }
                                        else if (pettype == "Chinchilla" && finalint > 20)
                                        {
                                            famestr = "yes";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            Response.Redirect("default.aspx");
                                        }
                                        else if (pettype == "Guinea pig" && finalint > 15)
                                        {
                                            famestr = "yes";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            Response.Redirect("default.aspx");
                                        }
                                        else if (pettype == "Rabbit" && finalint > 30)
                                        {
                                            famestr = "yes";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            Response.Redirect("default.aspx");
                                        }
                                        else
                                        {
                                            famestr = "no";
                                            string custnamestr = queryReader["Name"].ToString();
                                            SqlCommand com = new SqlCommand("INSERT INTO AdoptionData (CustName, Pet_id, donation, fame,  dateofadoption)VALUES('" + custnamestr + "','" + adopt + "','" + finalint + "','" + famestr + "','" + today + "')", scon);
                                            SqlDataReader dr = com.ExecuteReader();
                                            SqlCommand comupdate = new SqlCommand("UPDATE pets SET adopted='true' WHERE Pet_ID='" + adopt + "' ", scon);
                                            SqlDataReader drupdate = comupdate.ExecuteReader();
                                            scon.Close();
                                            //if lei
                                        }
                                    }


    
                                } else
                                {
                                    Label2.Text = "Sorry pet is not avaialable from this Sanctuary";
                                    Label2.Visible = true;
                                }
                                

                            }else
                            {
                                Label2.Text = "Please enter Donation Amount";
                                Label2.Visible = true;
                            }
                        }
                    } else
                    {
                        Label2.Text = "Please enter Valid Customer ID";
                        Label2.Visible = true;
                    }
                }
            } else {
                Label2.Text = "Please enter Customer ID";
                Label2.Visible = true;
            }
        }

        
    }
}
