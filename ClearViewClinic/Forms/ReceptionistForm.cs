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
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace ClearViewClinic
{
    public partial class ReceptionistForm : Form
    {
        MySqlConnection conn = new MySqlConnection(Login.connectionString);

        int mouseX = 0, mouseY = 0;
        bool mouseDown;

        public ReceptionistForm()
        {
            InitializeComponent();
            comboBoxLoad();
            userLabel.Text = Login.userProfile;
        }

        private void comboBoxLoad()
        {
            MySqlConnection conn = new MySqlConnection(Login.connectionString);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from login_details inner join employee on login_details.employeeId=employee.employeeId where login_details.authLevel='Doctor'", conn);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                doctorEntry.DisplayMember = "employeeId";
                doctorEntry.ValueMember = "employeeId";
                doctorEntry.DataSource = ds.Tables[0];

                doctorEntry2.DisplayMember = "employeeId";
                doctorEntry2.ValueMember = "employeeId";
                doctorEntry2.DataSource = ds.Tables[0];

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

            string query = "select employee.employeeId,fname,lname from employee inner join login_details on employee.employeeId=login_details.employeeId where login_details.authLevel='Doctor'";
            Crud employeeList = new Crud();
            employeeList.createTable2(query, doctorList);

            Crud employeeList2 = new Crud();
            employeeList2.createTable2(query, doctorList2);  
       
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
                profileImage.Image = new Bitmap(@"C:\ClearViewClinic\images\" + reader["profilePic"].ToString());
                profileImage.SizeMode = PictureBoxSizeMode.StretchImage;
                updateImagePathBox.Text=reader["profilePic"].ToString();
            }
            reader.Close();
            conn.Close();
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelChanger(appointmentPanel);
            Random rnd = new Random();
            int appointment=rnd.Next(1,9999);
            int patient = rnd.Next(1, 9999);
            appointmentIdBox.Text = "appoint"+appointment;
            patientIdBox.Text = "patient" +patient;

        }

        private void ReceptionistForm_Load(object sender, EventArgs e)
        {

        }

        public void panelChanger(Panel panelType)
        {
            //method to make panels visible
            panelType.Visible = true; //1023; 580 new size
            panelType.Location = new Point(137, 55);
            panelType.Size = new Size(1023, 580);
            panelType.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelChanger(viewAppointment);

            //get time from system
            DateTime date = DateTime.Now;
            string formatDate = string.Format("{0:D}",date);

            string queryToday = "appointment inner join patient on appointment.patientId=patient.patientId " + "where visitingDate='" + formatDate + "'";
            Crud schedule= new Crud();
            schedule.createTable(queryToday, currentAppointments);   

        }

        private void updateAppointment_Click(object sender, EventArgs e)
        {
          
        }

        public double calculateCost(int age)
        {
            double cost = 0.00;

            if (age > 0 && age < 18)
            {
                cost = 5500.00;
            }

            if (age > 60)
            {
                cost = 5500.00;
            }

            return cost;
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            panelChanger(updateAppointment);
        }

        private void updateButton_Click(object sender, EventArgs e)
        {

            Employee updateEmp = new Employee(Login.userProfile,firstNameBox.Text, lastNameBox.Text, genderComboBox.Text, updateImagePathBox.Text);
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
            uploadImage.insertImage(profileImage, updateImagePathBox, Login.userProfile);
            profileImage.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void addAppointment_Click(object sender, EventArgs e)
        {
            int counter=0;
            int overload = 0;
            conn.Open();
            MySqlCommand mysqlcommand3 = conn.CreateCommand();
            mysqlcommand3.CommandText = "Select * from appointment where visitingDate='"+dateTimePicker.Text+"' and visitingTime='"+timeComboBox.Text+"' and doctorId='"+doctorEntry.Text+"'";
            MySqlDataReader reader = mysqlcommand3.ExecuteReader();
            while (reader.Read())
            {
                counter++;
  
            }
            reader.Close();
            conn.Close();

            if (counter > 4)
            {
                overload = 1;
                MessageBox.Show("Sorry.No more persons can be appointed to that period");
            }

      

            double visitCost=0;

            visitCost = 6000.00;

            if (!isPhoneNumber(telephone.Text))
            {
                MessageBox.Show("Not a valid phone number");
            }

            if (overload < 1 && isPhoneNumber(telephone.Text))
            {
                string paymentStatus = "UnPaid";
                string appointmentStatus = "Active";

                Crud clash = new Crud();
                clash.handleClash("appointment","appointmentId","appoint",appointmentIdBox);

                Appointment addAppointment = new Appointment(appointmentIdBox.Text, doctorEntry.Text, patientIdBox.Text, dateTimePicker.Text, timeComboBox.Text, visitCost, paymentStatus, appointmentStatus);
                addAppointment.addAppointment();

                Patient patient = new Patient(patientIdBox.Text, firstNameEntry.Text, lastNameEntry.Text,genderEntry.Text,telephone.Text);
                patient.addPatient();

                firstNameEntry.Clear(); lastNameEntry.Clear(); genderEntry.Text=""; telephone.Clear();

                Random rnd = new Random();
                int appointment2= rnd.Next(1, 9999);
                int patient2 = rnd.Next(1, 9999);
                appointmentIdBox.Text = "appoint" + appointment2;
                patientIdBox.Text = "patient" + patient2;
            }
        }

        private void firstNameEntry_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void genderEntry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void viewAppointment_Paint(object sender, PaintEventArgs e)
        {

        }

        private void currentAppointments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void profilePanel_Paint(object sender, PaintEventArgs e)
        {

        }

    

        private void appointmentPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void profileImage_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void updateAppointment_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void doctorEntry2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            conn.Open();

            MySqlCommand mysqlcommand3 = conn.CreateCommand();
            mysqlcommand3.CommandText = "select * from appointment inner join patient on appointment.patientId=patient.patientId " + "where appointment.patientId='"+appointmentUpdateSearch.Text +"'";
            MySqlDataReader reader = mysqlcommand3.ExecuteReader();
            while (reader.Read())
            {
                firstNameBox2.Text = reader["fname"].ToString();
                lastNameBox2.Text = reader["lname"].ToString();
                genderBox2.Text = reader["gender"].ToString();
                telephoneBox2.Text = reader["telephone"].ToString();
                dateTimePicker2.Text = reader["visitingDate"].ToString();
                telephoneBox2.Text = reader["telephone"].ToString();
                timeBox.Text = reader["visitingTime"].ToString();
                appointmentBox2.Text = reader["appointmentId"].ToString();
                payment2.Text = reader["paymentStatus"].ToString();
                statusBox.Text = reader["appointmentStatus"].ToString();
                doctorEntry2.Text = reader["doctorId"].ToString();
            }
            reader.Close();
            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {

             int counter=0;
            int overload = 0;
            conn.Open();
            MySqlCommand mysqlcommand3 = conn.CreateCommand();
            mysqlcommand3.CommandText = "Select * from appointment where visitingDate='" + dateTimePicker2.Text + "' and visitingTime='" + timeBox.Text + "' and doctorId='" + doctorEntry2.Text + "'";
            MySqlDataReader reader = mysqlcommand3.ExecuteReader();
            while (reader.Read())
            {
                counter++;
  
            }
            reader.Close();
            conn.Close();

            if (counter > 4)
            {
                overload = 1;
                MessageBox.Show("Sorry.No more persons can be appointed to that period");
            }

            double visitCost=0;

            visitCost = 6000.00;

            if (!isPhoneNumber(telephoneBox2.Text))
            {
                MessageBox.Show("Not a valid phone number");
            }

            if (overload < 1 && isPhoneNumber(telephoneBox2.Text))
             {
                 Crud appointment = new Crud();
                 appointment.handleClash("appointment", "appointmentId", appointmentBox2.Text, appointmentBox2);
                
                Crud patient = new Crud();
                patient.handleClash("patient", "patientId",appointmentBox2.Text,appointmentBox2);

                Appointment updateSchedule = new Appointment(appointmentBox2.Text, doctorEntry2.Text, appointmentUpdateSearch.Text, dateTimePicker2.Text, timeBox.Text, 6000.00, payment2.Text, statusBox.Text);
                updateSchedule.updateAppointment(appointmentUpdateSearch.Text);

                Patient updateEmp = new Patient(appointmentUpdateSearch.Text, firstNameBox2.Text, lastNameBox2.Text,genderBox2.Text, telephoneBox2.Text);
                updateEmp.updatePatient(appointmentUpdateSearch.Text);
            }
        }

        public static bool isPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(\+[0-9]{11})$").Success;
        }

        private void lockButton_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
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

        private void exitLabel_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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


        public void doctorLoad(ComboBox account, string query)
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



        private void searchDoctorName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void searchAppointment_Click(object sender, EventArgs e)
        {

            string querySearch = "appointment inner join patient on appointment.patientId=patient.patientId " + "where visitingDate='" + searchedDate.Text+ "'";
            Crud schedule = new Crud();
            schedule.createTable(querySearch, searchResultsDataGrid);   
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panelChanger(searchPanel);
        } 
    }
}
