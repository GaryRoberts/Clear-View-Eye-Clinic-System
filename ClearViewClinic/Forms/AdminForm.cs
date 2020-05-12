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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;


namespace ClearViewClinic
{
    public partial class AdminForm : Form
    {
        MySqlConnection conn = new MySqlConnection(Login.connectionString);
        int mouseX = 0, mouseY = 0;
        bool mouseDown;
        public static string imagePath = "";

        public AdminForm()
        {
            InitializeComponent();
            userLabel.Text = Login.userProfile;
            comboBoxLoad();
         
        }

        private void comboBoxLoad()
        {
            MySqlConnection conn = new MySqlConnection(Login.connectionString);
            DataSet glasses = new DataSet();
            try
            {
                conn.Open();

                MySqlCommand cmd2 = new MySqlCommand("select * from glasses", conn);
                MySqlDataAdapter da2 = new MySqlDataAdapter();
                da2.SelectCommand = cmd2;
                da2.Fill(glasses);

                searchUpdateKey.DisplayMember = "glassId";
                searchUpdateKey.ValueMember = "glassId";
                searchUpdateKey.DataSource = glasses.Tables[0];

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
           
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            panelChanger(addGlassPanel);

            glassImageEntryBox.Text = "glasses.jpg";

            Random rnd = new Random();
            int glassesId = rnd.Next(1, 9999);
            glassIdEntry.Text = "glasses"+glassesId.ToString();
        }


        public void panelChanger(Panel panelType)
        {
            panelType.Visible = true; //1023; 580 new size
            panelType.Location = new Point(137, 55);
            panelType.Size = new Size(1023, 580);
            panelType.BringToFront();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DatabaseSettings setting = new DatabaseSettings
            {
                username = usernameTextBox.Text,
                port = portTextBox.Text,
                database = databaseNameTextBox.Text,
                password = passwordTextBox.Text,
                host = hostTextBox.Text
            };

            BinaryFormatter bf = new BinaryFormatter();

            FileStream fsout = new FileStream("DatabaseSettings.binary", FileMode.Create, FileAccess.Write, FileShare.None);
            try
            {
                using (fsout)
                {
                    bf.Serialize(fsout, setting);
                    MessageBox.Show("DATABASE SETTINGS WERE STORED SUCCESSFULLY");
                }
            }
            catch
            {
                MessageBox.Show("An error occured");
            }   
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DatabaseSettings setting = new DatabaseSettings();

            BinaryFormatter bf = new BinaryFormatter();

            FileStream fsin = new FileStream("DatabaseSettings.binary", FileMode.Open, FileAccess.Read, FileShare.None);
            try
            {
                using (fsin)
                {
                    setting = (DatabaseSettings)bf.Deserialize(fsin);

                    usernameTextBox.Text = setting.username;
                    portTextBox.Text = setting.port;
                    databaseNameTextBox.Text = setting.database;
                    passwordTextBox.Text = setting.password;
                    hostTextBox.Text = setting.host;
                }
            }
            catch
            {
                MessageBox.Show("An error occured");
            }  
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panelChanger(settingsPanel);
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
             Crud.clash = "error";

            int error=0;

            if (emailIsValid(employeeId.Text))

                error = 0;
            else
            {
                error = 1;
                MessageBox.Show("Not a valid email.Please try again");
            }


            if (confirmPassword.Text != password.Text)
            {
                error = 1;
                MessageBox.Show("Passwords do not match");
            }
           
            

           if(error<1)
           {


               if (imagePath1.Text == "" || imagePath1.Text == "avatar.png")
               {
                   imagePath1.Text = "avatar.png";
               }
               else
               {
                 
                   File.Copy(Crud.fileName, Crud.destination);
               }

               Crud clash = new Crud();
               clash.handleClash("employee","employeeId",employeeId.Text,employeeId);

           
                   Employee addEmp = new Employee(employeeId.Text, userName.Text, lastName.Text, gender.Text, imagePath1.Text);
                   addEmp.addEmployee();


                   LoginDetail cred = new LoginDetail(employeeId.Text, Hash(password.Text), position.Text);
                   cred.addCredentials();

                   Success closing = new Success();
                   closing.Close();
                   refreshInputs();

              
           }
          
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
            panelChanger(addUserPanel);
            imagePath1.Text = "avatar.png";
             
        }

        private void AddGlass_Click(object sender, EventArgs e)
        {
            int quantity = int.Parse(quantityUpDown.Text);

            double cost = 0.0;
            cost = Math.Round(Convert.ToDouble(costEntry.Text),2);

            Crud clash = new Crud();
            clash.handleClash("glasses","glassId","glasses",glassIdEntry);

                Glasses addGlass = new Glasses(glassIdEntry.Text, brandEntry.Text, cost, quantity, glassImageEntryBox.Text);
                addGlass.addGlasses();

                if (glassImageEntryBox.Text == "" || glassImageEntryBox.Text == "glasses.jpg")
                {
                    glassImageEntryBox.Text = "glasses.jpg";
                }
                else
                {
                    try
                    {
                        File.Copy(Crud.fileName, Crud.destination);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occured while uploading image");
                    }
                }
         
                    refreshInputs();
                    glassImageEntryBox.Text = "glasses.jpg";

                    Random rnd = new Random();
                    int glassesId = rnd.Next(1, 9999);
                    glassIdEntry.Text = "glasses" + glassesId.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelChanger(inventoryPanel);
            Crud inventory = new Crud();
            inventory.createTable("glasses",inventoryDataGrid);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelChanger(editGlassesPanel);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            panelChanger(viewUsersPanel);
            Crud inventory = new Crud();
            inventory.createTable("employee",usersDataGrid);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelChanger(profilePanel);
        }

        private void button11_Click(object sender, EventArgs e)
        {
             MySqlConnection conn = new MySqlConnection(Login.connectionString);
            conn.Open();

            MySqlCommand mysqlcommand3 = conn.CreateCommand();
            mysqlcommand3.CommandText = "Select * from employee where employeeId='" + userIdSearch.Text + "'";
            MySqlDataReader reader = mysqlcommand3.ExecuteReader();
            while (reader.Read())
            {
                firstNameUpdate.Text=reader["fname"].ToString();
                lastNameUpdate.Text = reader["lname"].ToString();
                genderUpdate.Text = reader["gender"].ToString();
                updateProfilePic.Image = new Bitmap(@"C:\ClearViewClinic\images\"+reader["profilePic"].ToString());
                updateProfilePic.SizeMode = PictureBoxSizeMode.StretchImage;
                updateImagePathBox.Text = reader["profilePic"].ToString();
          }
            reader.Close();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            int quantity = int.Parse(quantityUpdate.Text);

            double cost = 0.0;
            cost = Math.Round(Convert.ToDouble(costUpdate.Text), 2);

            Glasses updateGlass = new Glasses(searchUpdateKey.Text, brandUpdate.Text, cost, quantity,imageAddress.Text);
            updateGlass.updateGlasses(searchUpdateKey.Text);

            try
            {
                if (imageAddress.Text != "" && imageAddress.Text !="glasses.jpg")
                {
                    File.Copy(Crud.fileName, Crud.destination);
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

            MySqlConnection conn = new MySqlConnection(Login.connectionString);
            conn.Open();

            MySqlCommand mysqlcommand = conn.CreateCommand();
            mysqlcommand.CommandText = "Select * from glasses where glassId='"+searchUpdateKey.Text+"'";
            MySqlDataReader reader = mysqlcommand.ExecuteReader();
            while (reader.Read())
            {
                brandUpdate.Text = reader["glassesBrand"].ToString();
                costUpdate.Text = reader["initialCost"].ToString();
                quantityUpdate.Text = reader["quantity"].ToString();
                updateGlassesImage.Image = new Bitmap(@"C:\ClearViewClinic\images\" + reader["glassesPic"].ToString());
                imagePath = @"C:\ClearViewClinic\images\" + reader["glassesPic"].ToString();
                updateGlassesImage.SizeMode = PictureBoxSizeMode.StretchImage;
                imageAddress.Text = reader["glassesPic"].ToString();
            }
            reader.Close();
        }

        private void profileLastNameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void updateButton_Click(object sender, EventArgs e)
        {

         
            updateProfilePic.SizeMode = PictureBoxSizeMode.StretchImage;

            Employee updateEmp = new Employee(userIdSearch.Text, firstNameUpdate.Text, lastNameUpdate.Text,genderUpdate.Text, updateImagePathBox.Text);
            updateEmp.updateEmployee(userIdSearch.Text);

            if (imagePath1.Text != "" && imagePath1.Text!="avatar.png")
            {
                File.Copy(Crud.fileName, Crud.destination);
            }


        }

        private void userIdSearch_TextChanged(object sender, EventArgs e)
        {

        }
        private void ageUpdate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void brandEntry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Crud uploadImage = new Crud();
            uploadImage.insertImage(profileImageBox,imagePath1,userName.Text);
            profileImageBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void imagePath1_TextChanged(object sender, EventArgs e)

        {

        }

        private void addGlassPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addUserPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            Crud uploadImage = new Crud();
            uploadImage.insertImage(updateProfilePic,updateImagePathBox,userIdSearch.Text);
            updateProfilePic.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void updateImagePathBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            Crud uploadImage = new Crud();
            uploadImage.insertImage(glassImageEntry, glassImageEntryBox, Login.userProfile);
            glassImageEntry.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void updateImagePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void lockButton_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Crud deleteUser=new Crud();
            deleteUser.deleteData("login_details","employeeId",removeUser.Text);
    
        }

        private void button17_Click(object sender, EventArgs e)
        {
            panelChanger(deleteUser);
        }

        private void button20_Click(object sender, EventArgs e)
        {
  
        }

    

        public static bool emailIsValid(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

       private void refreshInputs()
       {
               employeeId.Clear();
               userName.Clear();
               lastName.Clear(); 
               gender.Text=""; 
               imagePath1.Clear();
               employeeId.Clear();
               password.Clear();
               confirmPassword.Clear();
               position.Text="";

               glassIdEntry.Clear();
               brandEntry.Clear();
               costEntry.Clear();
               quantityUpDown.Text ="1";
               glassImageEntryBox.Clear();
               glassImageEntry.Image = new Bitmap(@"C:\ClearViewClinic\images\glasses.jpg");
          

       }

        public string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }

        private void viewUsersPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void usersDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

    }
}
