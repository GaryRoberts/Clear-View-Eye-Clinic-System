using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;  
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ClearViewClinic
{
    public partial class DoctorForm : Form
    {

        
        MySqlConnection conn = new MySqlConnection(Login.connectionString);

        int mouseX = 0, mouseY = 0;
        bool mouseDown;

        public DoctorForm()
        {
            InitializeComponent();
            userLabel.Text = Login.userProfile; 
            loadProfile();
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

        private void doctorForm_Load(object sender, EventArgs e)
        {

        }


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void profileButton_Click(object sender, EventArgs e)
        {
              panelChanger(profilePanel);
              conn.Open();
               MySqlCommand mysqlcommand3 = conn.CreateCommand();
               mysqlcommand3.CommandText = "Select * from employee where employeeId='" + Login.userProfile + "'";
               MySqlDataReader reader = mysqlcommand3.ExecuteReader();
               while (reader.Read())
               {
                   firstNameBox.Text = reader["fname"].ToString();
                   lastNameBox.Text = reader["lname"].ToString();
                   genderComboBox.Text = reader["gender"].ToString();
                   updateImagePathBox.Text = reader["profilePic"].ToString();
                   profileImage.Image = new Bitmap(@"C:\ClearViewClinic\images\"+reader["profilePic"].ToString());
                   profileImage.SizeMode = PictureBoxSizeMode.StretchImage;
                   updateImagePathBox.Text = reader["profilePic"].ToString();

               }
               reader.Close();
               conn.Close();
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            panelChanger(makeReportPanel);
        }

        private void scheduleButton_Click(object sender, EventArgs e)
        {
           panelChanger(viewAppointment);

           //get time from system
           DateTime date = DateTime.Now;
           string formatDate = string.Format("{0:D}", date);

           string queryToday = "appointment inner join patient on appointment.patientId=patient.patientId " + "where visitingDate='" + formatDate + "' and doctorId='"+Login.userProfile+"'";
           Crud inventory = new Crud();
           inventory.createTable(queryToday,currentAppointments);  
        }

        private void inventoryButton_Click(object sender, EventArgs e)
        {
            panelChanger(inventoryPanel);
            Crud inventory = new Crud();
            inventory.createTable("glasses", inventoryDataGrid);

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        public void panelChanger(Panel panelType)
        {
            panelType.Visible = true; //1023; 580 new size
            panelType.Location = new Point(137, 55);
            panelType.Size = new Size(1023,580);
            panelType.BringToFront();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void profilePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void reportPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panelChanger(viewDoctorReport);
        }

        private void preTestButton_Click(object sender, EventArgs e)
        {
            panelChanger(viewReportPanel);
        }

        private void inventoryPanel_Paint(object sender, PaintEventArgs e)
        {

        }


        public void loadProfile()
        {
             MySqlConnection conn = new MySqlConnection(Login.connectionString);
             conn.Open();
             MySqlCommand mysqlcommand3 = conn.CreateCommand();
             mysqlcommand3.CommandText = "Select * from employee where employeeId=" + "'" + Login.userProfile + "'";
            MySqlDataReader reader = mysqlcommand3.ExecuteReader();
            while(reader.Read())
            {
                firstNameBox.Text=reader["fname"].ToString();
                lastNameBox.Text = reader["lname"].ToString();
                genderComboBox.Text = reader["gender"].ToString();
            }

            reader.Close();
        }

        private void ageUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            Employee updateEmp = new Employee(Login.userProfile, firstNameBox.Text, lastNameBox.Text,genderComboBox.Text,updateImagePathBox.Text);
            updateEmp.updateEmployee(Login.userProfile);

            try
            {
                if (updateImagePathBox.Text != "")
                {
                    File.Copy(Crud.fileName, Crud.destination);
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Crud uploadImage = new Crud();
            uploadImage.insertImage(profileImage,updateImagePathBox,Login.userProfile);
            profileImage.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void searchPreReport_Click(object sender, EventArgs e)
        {
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int doctorReport= rnd.Next(1, 9999);

            DoctorReport report = new DoctorReport("doctorReport"+doctorReport,Login.userProfile,patientId.Text,glaucomaTest.Text,possibleDiseases.Text, coverTest.Text, refractionTest.Text, slitLampProcedure.Text, dialationProcedure.Text, framesNeededStatus.Text, conclusion.Text);
            report.addReport();

            patientId.Clear(); glaucomaTest.Clear(); possibleDiseases.Clear(); coverTest.Clear(); refractionTest.Clear(); slitLampProcedure.Clear(); dialationProcedure.Clear(); framesNeededStatus.Text=""; conclusion.Clear();
        }

        private void viewReportPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lockButton_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void userLabel_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                mouseX = MousePosition.X - 200;
                mouseY = MousePosition.Y - 40;
                this.SetDesktopLocation(mouseX, mouseY);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            patientLoad(patientReport, "SELECT * FROM patient where fname LIKE '%" + searchpatientReport.Text + "%'"); 
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(Login.connectionString);
            conn.Open();

            MySqlCommand mysqlcommand = conn.CreateCommand();
            mysqlcommand.CommandText = "Select * from doctor_report where patientId='" + patientReport.Text + "'";
            MySqlDataReader reader = mysqlcommand.ExecuteReader();
            while (reader.Read())
            {
                glaucomaTest2.Text = reader["glaucomaTest"].ToString();
                possibleDiseases2.Text = reader["possibleDiseases"].ToString();
                coverTest2.Text = reader["coverTest"].ToString();
                refractionTest2.Text = reader["refractionTest"].ToString();
                slitLampProcedure2.Text = reader["slitLampProcedure"].ToString();
                dialationProcedure2.Text = reader["dialationProcedure"].ToString();
                conclusion2.Text = reader["conclusion"].ToString();
                framesNeededStatus2.Text = reader["framesNeededStatus"].ToString();

            }
            reader.Close();

            mysqlcommand.CommandText = "Select * from patient where patientId='" + patientReport.Text + "'";
            MySqlDataReader reader2 = mysqlcommand.ExecuteReader();
            while (reader2.Read())
            {
                firstName2.Text = reader2["fname"].ToString() + " " + reader2["lname"].ToString();
                lastName2.Text = reader2["gender"].ToString();
            }
            reader2.Close();
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

        private void searchpatientReport_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            patientLoad(preReportId, "SELECT * FROM patient where fname LIKE '%" + searchPreTest.Text + "%'"); 
        }

    }
}
