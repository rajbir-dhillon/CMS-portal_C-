using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.IO;

namespace Webnapp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection scon = new SqlConnection("Data Source=sql-server;Initial Catalog=rd5038m;Persist Security Info=True;MultipleActiveResultSets=True;User ID=rd5038m;Password=!1SQLServer");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string nametxt = name.Text;
            string emailtxt = email.Text;
            string addresstxt = address.Text;
            string country = countryList.SelectedValue;
            string contact = tel_no.Text;

            HttpPostedFile postedFile = FileUpload1.PostedFile;
            string FileName = Path.GetFileName(postedFile.FileName);
            string FileExtention = Path.GetExtension(FileName);
            int fileSize = postedFile.ContentLength;

            if(FileExtention.ToLower() == ".jpg" || FileExtention.ToLower() == ".bmp" || FileExtention.ToLower() == ".jpeg" || FileExtention.ToLower() == ".png")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
                using (var sqlWrite = new SqlCommand("INSERT INTO Customers (Name,Email,Address,Country,tel_no,picture) Values('" + nametxt + "','" + emailtxt + "','" + addresstxt + "','" + country + "','" + contact + "',@File)", scon))
                {
                    scon.Open();
                    sqlWrite.Parameters.Add("@File", SqlDbType.Binary).Value = bytes;
                    sqlWrite.ExecuteNonQuery();
                    scon.Close();
                    Response.Redirect("viewcust.aspx");
                }

            } 

            
        }
    }
}