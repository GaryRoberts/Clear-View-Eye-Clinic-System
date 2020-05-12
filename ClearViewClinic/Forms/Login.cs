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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ClearViewClinic
{
    public partial class Login : Form
    {

        public static string userProfile ="";
        public static string host = "";
        public static string port = "";
        public static string password = "";
        public static string database = "";
        public static string username = "";
        public static string connectionString = "";

        public Login()
        {
            InitializeComponent();

            connection();
        }

        int mouseX = 0, mouseY = 0;
        bool mouseDown;



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

  
        private void button1_Click(object sender, EventArgs e)
        {
        
            int counter=0;

            MySqlConnection conn = new MySqlConnection(Login.connectionString);
            conn.Open();

            MySqlCommand mysqlcommand3 = conn.CreateCommand();
            mysqlcommand3.CommandText = "Select * from login_details";
            MySqlDataReader reader = mysqlcommand3.ExecuteReader();
            while (reader.Read())
            {
                //userProfile = idBox.Text;
                if (reader["employeeId"].ToString() == idBox.Text && reader["password"].ToString() == Hash(passwordBox.Text) && reader["authLevel"].ToString()=="Doctor")
                {     
                    counter = 1;
                    userProfile = reader["employeeId"].ToString();
                    DoctorForm doctor = new DoctorForm();
                    doctor.Show();
                    this.Visible = false;
                    this.Close();
                }

                if (reader["employeeId"].ToString() == idBox.Text && reader["password"].ToString() == Hash(passwordBox.Text) && reader["authLevel"].ToString() == "Assistant")
                {
                    counter = 1;
                    userProfile = reader["employeeId"].ToString();
                    AssistantForm assistant = new AssistantForm();
                    assistant.Show();
                    this.Visible = false;
                    this.Close();
                }

                if (reader["employeeId"].ToString() == idBox.Text && reader["password"].ToString() == Hash(passwordBox.Text) && reader["authLevel"].ToString() == "Cashier")
                {
                    counter = 1;
                    userProfile = reader["employeeId"].ToString();
                    CashierForm cashier = new CashierForm();
                    cashier.Show();
                    this.Visible = false;
                    this.Close();
                }

                if (reader["employeeId"].ToString() == idBox.Text && reader["password"].ToString() == Hash(passwordBox.Text) && reader["authLevel"].ToString() == "Admin")
                {
                    counter = 1;
                    userProfile = reader["employeeId"].ToString();
                    AdminForm admin = new AdminForm();
                    admin.Show();
                    this.Visible = false;
                    this.Close();
                }

                if (reader["employeeId"].ToString() == idBox.Text && reader["password"].ToString() == Hash(passwordBox.Text) && reader["authLevel"].ToString() == "Receptionist")
                {
                    counter = 1;
                    userProfile = reader["employeeId"].ToString();
                    ReceptionistForm recep = new ReceptionistForm();
                    recep.Show();
                    this.Visible = false;
                    this.Close();
                }
            }

            if (counter==0)
            {
                this.Close();
                ErrorForm error = new ErrorForm();
                error.Show(); 
            }

            reader.Close();
          
        }

        public string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
 
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void idBox_TextChanged(object sender, EventArgs e)
        {

        }

        public static void connection()
        {
            DatabaseSettings setting = new DatabaseSettings();

            BinaryFormatter bf = new BinaryFormatter();

            FileStream fsin = new FileStream("DatabaseSettings.binary", FileMode.Open, FileAccess.Read, FileShare.None);
            try
            {
                using (fsin)
                {
                    setting = (DatabaseSettings)bf.Deserialize(fsin);

                    username= setting.username;
                    port = setting.port;
                    database= setting.database;
                    password= setting.password;
                    host= setting.host;
                    connectionString = "server=" + setting.host + "; port=" + setting.port + "; password="+ setting.password+"; username=" + setting.username + "; database=" + setting.database;
                }
            }
            catch
            {
                MessageBox.Show("An error occured when connecting to the database");
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
