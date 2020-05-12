using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ClearViewClinic
{
    class Crud
    {
        
         MySqlConnection conn = new MySqlConnection(Login.connectionString);

         public static string fileName = "";
         public static string destination = "";
         public static string clash = "";

        public void insertData(string insertQuery)
        {

            conn.Open();
            MySqlCommand mysqlcommand = new MySqlCommand(insertQuery, conn);
            if (mysqlcommand.ExecuteNonQuery() == 1)
            {
                Success message = new Success();

                Form fc = Application.OpenForms["Success"];

                if (fc != null)
                    fc.Close();
                else
                   message.Show();

                message.Show();
            }
            else
            {
                Failure error = new Failure();
                error.Show();
            }
            conn.Close();

        }

        public void insertImage(PictureBox pictureBox,TextBox textBox,string userId)
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.png; *.bmp)|*.jpg; *.jpeg; *.gif; *.png; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                pictureBox.Image = new Bitmap(open.FileName);
                // image file path  
                FileInfo info = new FileInfo(open.FileName);
                string fileNameWithoutPath = info.Name;
                textBox.Text = fileNameWithoutPath;


                string dpath = @"C:\ClearViewClinic\images\";

                try
                {
                    if (!Directory.Exists(dpath))
                    {
                        Directory.CreateDirectory(dpath);
                    }
                }
                catch (Exception ex)
                {
                    // handle
                }

                try
                 {

                if (open.FileName != "")
                {
                    fileName = open.FileName.ToString();
                    destination = dpath + fileNameWithoutPath;
                    if (File.Exists(destination))
                    {
                        destination = dpath + "avatar.png";
                        MessageBox.Show("That image is already in use.Please rename image file and try again");
                        textBox.Text = "default.jpg";
                        pictureBox.Image = new Bitmap(dpath + "default.jpg");
                    }
                }
               }
                catch(Exception ex)
                {
                    MessageBox.Show("An error occured while uploading image");
                } 

            }
        }

        public void updateData(string updateQuery)
        {
            conn.Open();
            MySqlCommand mysqlcommand2 = new MySqlCommand(updateQuery, conn);
            if (mysqlcommand2.ExecuteNonQuery() == 1)
            {
                Success message = new Success();

                Form fc = Application.OpenForms["Success"];

                if (fc != null)
                    fc.Close();
                else
                    message.Show();

                message.Show();
            }
            else
            {
                Failure error = new Failure();
                error.Show();
            }
            conn.Close();
        }

        public void deleteData(string tableName,string primarykey,string key)
        {
            conn.Open();
            string query = "DELETE from " + tableName + " where " + primarykey + "='" + key + "'";
            MySqlCommand mysqlcommand4 = new MySqlCommand(query, conn);
            if (mysqlcommand4.ExecuteNonQuery() == 1)
            {
                Success message = new Success();
                message.Show();
            }
            else
            {
                Failure error = new Failure();
                error.Show();
            }
            conn.Close();
        }

        public void loadProfile(string userId,Label userLabel,TextBox box1,TextBox box2,TextBox box3,TextBox box4)
        {
            conn.Open();
            MySqlCommand mysqlcommand3 = conn.CreateCommand();
            mysqlcommand3.CommandText = "Select * from employee where employeeId="+"'"+userId+"'";
            MySqlDataReader reader = mysqlcommand3.ExecuteReader();
            while (reader.Read())
            {
                box1.Text = reader["fname"].ToString();
                box2.Text = reader["lname"].ToString();
                box3.Text = reader["age"].ToString();
                box4.Text = reader["gender"].ToString();
            }

            reader.Close();
        }

        public void createTable(string tableName,DataGridView gridView)
        {
            conn.Open();
            string query = "Select * from "+tableName;
            MySqlDataAdapter sda = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            gridView.DataSource = dt;
            conn.Close();
        }

        public void createTable2(string query, DataGridView gridView)
        {
            conn.Open();

            MySqlDataAdapter sda = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            gridView.DataSource = dt;
            conn.Close();
        }

        public void patientSearchResult(string tableName, DataGridView gridView,string searchId)
        {
            conn.Open();
            string query = "Select * from "+tableName+" where patientId='"+searchId+"'";
            MySqlDataAdapter sda = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            gridView.DataSource = dt;
            conn.Close();
        }

        public void handleClash(string tableName,string primaryKey,string idType,TextBox newId)
        {
            conn.Open();
            MySqlCommand mysqlcommand3 = conn.CreateCommand();
            mysqlcommand3.CommandText = "select * from " + tableName;
            MySqlDataReader reader = mysqlcommand3.ExecuteReader();
            while (reader.Read())
            {
                if (reader[primaryKey].ToString() == newId.Text && tableName!= "employee")
                {
                    Random rnd = new Random();
                   int generateId = rnd.Next(1, 9999);
                   newId.Text = idType + generateId.ToString();
                } 

                if (reader[primaryKey].ToString() == newId.Text && tableName=="employee")
                {
                    Crud.clash = "error";
                } 
            }
            reader.Close();
        }

    }
}
