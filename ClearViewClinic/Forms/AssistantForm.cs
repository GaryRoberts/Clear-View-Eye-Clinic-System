using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class AssistantForm : Form
    {
        MySqlConnection conn = new MySqlConnection(Login.connectionString);
        int mouseX = 0, mouseY = 0;
        bool mouseDown;

        public AssistantForm()
        {
            InitializeComponent();
            userLabel.Text = Login.userProfile;
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
            panelChanger(profilePanel);
            conn.Open();
            MySqlCommand mysqlcommand3 = conn.CreateCommand();
            mysqlcommand3.CommandText = "Select * from employee where employeeId='" + Login.userProfile+ "'";
          
            MySqlDataReader reader = mysqlcommand3.ExecuteReader();
            while (reader.Read())
            {
                firstNameBox.Text = reader["fname"].ToString();
                lastNameBox.Text = reader["lname"].ToString();
                genderComboBox.Text = reader["gender"].ToString();
                updateImagePathBox.Text = reader["profilePic"].ToString();
                profileImage.Image = new Bitmap(@"C:\ClearViewClinic\images\" + reader["profilePic"].ToString());
                profileImage.SizeMode = PictureBoxSizeMode.StretchImage;
                updateImagePathBox.Text = reader["profilePic"].ToString();

            }
            reader.Close();
            conn.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void AssistantForm_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelChanger(viewAppointment);
            //get time from system
            DateTime date = DateTime.Now;
            string formatDate = string.Format("{0:D}", date);

           
            string queryToday = "appointment inner join patient on appointment.patientId=patient.patientId "+"where visitingDate='" + formatDate + "'";
            Crud inventory = new Crud();
            inventory.createTable(queryToday,currentAppointments);  
        }

        public void panelChanger(Panel panelType)
        {
            panelType.Visible = true; //1023; 580 new size
            panelType.Location = new Point(137, 55);
            panelType.Size = new Size(1023, 580);
            panelType.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panelChanger(doReportPanel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelChanger(viewReportPanel);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void profilePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            Crud uploadImage = new Crud();
            uploadImage.insertImage(profileImage, updateImagePathBox, Login.userProfile);
        }

        private void updateImagePathBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void updateButton_Click(object sender, EventArgs e)
        {
    
            Employee updateEmp = new Employee(Login.userProfile, firstNameBox.Text, lastNameBox.Text,genderComboBox.Text, updateImagePathBox.Text);
              updateEmp.updateEmployee(Login.userProfile);


             string dpath = @"C:\Users\Gary\Desktop\images\";
            if (updateImagePathBox.Text != "")
            {
               File.Copy(updateImagePathBox.Text, dpath + updateImagePathBox.Text);
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            double eyePressure1,sphereDistance1,sphereNear1,cylinderDistanced1, cylinderNear1, axisDistance1, axisNear1, prismDistance1,prismNear1,baseDistance1, baseNear1;
            eyePressure1 = Math.Round(Convert.ToDouble(eyePressure.Text), 2); 
            sphereDistance1=Math.Round(Convert.ToDouble(sphDistance.Text), 2);
            sphereNear1=Math.Round(Convert.ToDouble(sphNear.Text), 2);
            cylinderDistanced1=Math.Round(Convert.ToDouble(cylDistance.Text), 2);
            cylinderNear1=Math.Round(Convert.ToDouble(cylNear.Text), 2);
            axisDistance1=Math.Round(Convert.ToDouble(axisDistance.Text), 2);
            axisNear1=Math.Round(Convert.ToDouble(axisNear.Text), 2);
            prismDistance1 = Math.Round(Convert.ToDouble(prismDistance.Text), 2);
            prismNear1 = Math.Round(Convert.ToDouble(prismNear.Text), 2);
            baseDistance1=Math.Round(Convert.ToDouble(baseDistance.Text), 2);
            baseNear1=Math.Round(Convert.ToDouble(baseNear.Text), 2);

           

             //get time from system
            DateTime date = DateTime.Now;
            string fullDate= date.ToString("MM-dd-yyyy hh:mm tt");
            int test = int.Parse(readingTest.Text);

            Random rnd = new Random();
            int reportId = rnd.Next(1, 9999);

            AssistantReport report = new AssistantReport();
            report.addPreReport("report"+reportId.ToString(), patientReportId.Text, Login.userProfile, fullDate, eyeIssues.Text, test, familyHistoryBox.Text, eyePressure1, sphereDistance1, sphereNear1, cylinderDistanced1, cylinderNear1, axisDistance1, axisNear1, prismDistance1, prismNear1, baseDistance1, baseNear1);
           
            eyePressure.Clear(); sphDistance.Clear(); sphNear.Clear(); cylDistance.Clear();cylNear.Clear(); axisDistance.Clear(); axisNear.Clear(); baseDistance.Clear(); baseNear.Clear();
            prismDistance.Clear(); prismNear.Clear(); familyHistoryBox.Text = ""; readingTest.Clear(); eyeIssues.Clear(); patientReportId.Clear();
        }

       

        private void viewReportPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox42_TextChanged(object sender, EventArgs e)
        {

        }

        private void updateButton_Click_1(object sender, EventArgs e)
        {
          
            Employee updateEmp = new Employee(Login.userProfile, firstNameBox.Text, lastNameBox.Text,genderComboBox.Text, "glass.jpg");
            updateEmp.updateEmployee(Login.userProfile);
        }

        private void updateButton_Click_2(object sender, EventArgs e)
        {

            Employee updateEmp = new Employee(Login.userProfile, firstNameBox.Text, lastNameBox.Text,genderComboBox.Text, updateImagePathBox.Text);
            updateEmp.updateEmployee(Login.userProfile);

            try
            {
                if (updateImagePathBox.Text != "")
                {
                    File.Copy(Crud.fileName, Crud.destination);
                }

            }
         catch(Exception ex)
            {
              
            }
           
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            Crud uploadImage = new Crud();
            uploadImage.insertImage(profileImage, updateImagePathBox, Login.userProfile);
            profileImage.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void lockButton_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                mouseX = MousePosition.X - 200;
                mouseY = MousePosition.Y - 40;
                this.SetDesktopLocation(mouseX, mouseY);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown =true;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            patientLoad(preReportId, "SELECT * FROM patient where fname LIKE '%" + searchPreTest.Text + "%'"); 
        }

        public void patientLoad(ComboBox account, string query)
        {
            MySqlConnection conn = new MySqlConnection(Login.connectionString);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                account.DisplayMember = "patientId";
                account.ValueMember = "patientId";
                account.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                //Exception Message
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(Login.connectionString);
            conn.Open();

            MySqlCommand mysqlcommand = conn.CreateCommand();
            mysqlcommand.CommandText = "Select * from pre_test where patientId='" + preReportId.Text + "'";
            MySqlDataReader reader = mysqlcommand.ExecuteReader();
            while (reader.Read())
            {
                readingTest2.Text = reader["readingTest"].ToString();
                eyeIssues2.Text = reader["issues"].ToString();
                familyHistory2.Text = reader["familyHistory"].ToString();
                sphereDistance2.Text = reader["sphereDistance"].ToString();
                sphereNear2.Text = reader["sphereNear"].ToString();
                cylinderDistance2.Text = reader["cylinderDistance"].ToString();
                cylinderNear2.Text = reader["cylinderNear"].ToString();
                axisDistance2.Text = reader["axisDistance"].ToString();
                axisNear2.Text = reader["axisNear"].ToString();
                prismDistance2.Text = reader["prismDistance"].ToString();
                prismNear2.Text = reader["prismNear"].ToString();
                baseDistance2.Text = reader["baseDistance"].ToString();
                baseNear2.Text = reader["baseNear"].ToString();
                eyePressureRead.Text = reader["eyePressure"].ToString();


            }
            reader.Close();

            mysqlcommand.CommandText = "Select * from patient where patientId='" + preReportId.Text + "'";
            MySqlDataReader reader2 = mysqlcommand.ExecuteReader();
            while (reader2.Read())
            {
                patientName.Text = reader2["fname"].ToString() + " " + reader2["lname"].ToString();
                patientGender.Text = reader2["gender"].ToString();
            }
            reader2.Close();
        }
    }
}
