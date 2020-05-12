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
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;

namespace ClearViewClinic
{
    public partial class CashierForm : Form
    {

        MySqlConnection conn = new MySqlConnection(Login.connectionString);

        int mouseX = 0, mouseY = 0;
        bool mouseDown;

        public CashierForm()
        {
            InitializeComponent();
            userLabel.Text = Login.userProfile;
            comboBoxLoad();
           
        }

        private void CashierForm_Load(object sender, EventArgs e)
        {

        }

        public void panelChanger(Panel panelType)
        {
            panelType.Visible = true; //1023; 580 new size
            panelType.Location = new Point(137, 55);
            panelType.Size = new Size(1023, 580);
            panelType.BringToFront();
        }

        private void comboBoxLoad()
        {
            MySqlConnection conn = new MySqlConnection(Login.connectionString);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select glassId from glasses group by glassId", conn);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                searchGlasses.DisplayMember = "glassId";
                searchGlasses.ValueMember = "glassId";
                searchGlasses.DataSource = ds.Tables[0];

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


        private void salesButton_Click(object sender, EventArgs e)
        {
            panelChanger(viewSalesPanel);
            
            string querySales = "sale inner join patient on sale.patientId=patient.patientId";
            Crud sales = new Crud();
            sales.createTable(querySales, salesDataGrid);   
        }

        private void purchaseButton_Click(object sender, EventArgs e)
        {
            panelChanger(newPurchasePanel);
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
            panelChanger(profilePanel);

            MySqlConnection conn = new MySqlConnection(Login.connectionString);
            conn.Open();

            MySqlCommand mysqlcommand3 = conn.CreateCommand();
            mysqlcommand3.CommandText = "Select * from employee where employeeId='" + userLabel.Text + "'";
            MySqlDataReader reader = mysqlcommand3.ExecuteReader();
            while (reader.Read())
            {
                firstNameBox.Text = reader["fname"].ToString();
                lastNameBox.Text = reader["lname"].ToString();
                genderComboBox.Text = reader["gender"].ToString();
                profileImage.Image = new Bitmap(@"C:\ClearViewClinic\images\" + reader["profilePic"].ToString());
                profileImage.SizeMode = PictureBoxSizeMode.StretchImage;
                updateImagePathBox.Text = reader["profilePic"].ToString();
            }
            reader.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelChanger(balancePaymentPanel);
        }

        private void balancePaymentPanel_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelChanger(inventoryPanel);
            Crud inventory = new Crud();
            inventory.createTable("glasses", inventoryDataGrid);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            patientLoad(paymentID,"SELECT * FROM patient where fname LIKE '%" +patientIdSearch.Text + "%'"); 
        }


        public void patientLoad(ComboBox account,string query)
        {
            MySqlConnection conn = new MySqlConnection(Login.connectionString);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query,conn);
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

        private void button2_Click(object sender, EventArgs e)
        {
            double payment,balanceAmount1,totalBalance;
            payment = Math.Round(Convert.ToDouble(decreaseBalance.Text), 2);
            balanceAmount1 = Math.Round(Convert.ToDouble(owingAmount.Text), 2);
            totalBalance = balanceAmount1-payment;

            Sale saleBalance= new Sale(totalBalance);
            saleBalance.updateBalance(paymentID.Text);

            patientIdSearch.Clear();
            paymentID.Text="";
            firstNamePayment.Clear();
            lastNamePayment.Clear();
            decreaseBalance.Clear();
            patientIdSearch.Clear();
            owingAmount.Clear();

        }

        private void updateButton_Click(object sender, EventArgs e)
        {

            Employee updateEmp = new Employee(Login.userProfile, firstNameBox.Text, lastNameBox.Text, genderComboBox.Text,updateImagePathBox.Text);
            updateEmp.updateEmployee(Login.userProfile);

            string dpath = @"C:\Users\Gary\Desktop\images\";

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

        private void button6_Click(object sender, EventArgs e)
        {
            patientLoad(visitingPayment, "SELECT * FROM patient where fname LIKE '%" + searchpatientBalance.Text + "%'"); 
          
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panelChanger(visitPayment);
        }

        private void button8_Click(object sender, EventArgs e)
        {

            Appointment updatePayment = new Appointment("Paid");
            updatePayment.updatePayment(visitingPayment.Text);

            conn.Open();
            MySqlCommand mysqlcommand3 = conn.CreateCommand();
            mysqlcommand3.CommandText = "select * from patient where patientId='" + visitingPayment.Text + "'";
            MySqlDataReader reader = mysqlcommand3.ExecuteReader();
            while (reader.Read())
            {
                DateTime date2 = DateTime.Now;
                string formatDate2 = string.Format("{0:D}", date2);
                visitReceiptMaker(reader["patientId"].ToString(), reader["fname"].ToString(), reader["lname"].ToString(), reader["telephone"].ToString(), formatDate2);
            }
            reader.Close();
            conn.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            MySqlConnection conn = new MySqlConnection(Login.connectionString);
            conn.Open();

            MySqlCommand mysqlcommand = conn.CreateCommand();
            mysqlcommand.CommandText = "Select * from glasses where glassId='" + searchGlasses.Text + "'";
            MySqlDataReader reader2 = mysqlcommand.ExecuteReader();
            while (reader2.Read())
            {
                 int quantity = int.Parse(reader2["quantity"].ToString());

                 if (quantity > 0)
                 {
                     glassesImage.Image = new Bitmap(@"C:\ClearViewClinic\images\" + reader2["glassesPic"].ToString());
                     glassesImage.SizeMode = PictureBoxSizeMode.StretchImage;
                     glassesCost.Text = reader2["initialCost"].ToString();
                     glassesBrand.Text = reader2["GlassesBrand"].ToString();
                     quantityBox.Text= reader2["quantity"].ToString();
                   
                 }
                 else
                 {
                     MessageBox.Show("You have none of these glasses in inventory at the moment.");
                 }
            }
            reader2.Close();
        }

        private void AddGlass_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int saleId = rnd.Next(1,9999);

            DateTime date = DateTime.Now;
            string formatDate = string.Format("{0:D}", date);

            double totalCost,amountPaidNow,balance;
            totalCost=Math.Round(Convert.ToDouble(glassesCost.Text), 2);   
            amountPaidNow = Math.Round(Convert.ToDouble(amountPaid.Text), 2);
            balance=totalCost-amountPaidNow;

            string saleIdNum = "sale" + saleId.ToString();
            Sale saleRecord = new Sale(saleIdNum, patientSaleId.Text, glassesBrand.Text, frameColourCombo.Text, formatDate, totalCost, balance);
            Crud clash = new Crud();
            TextBox sales = new TextBox();
            sales.Text = saleIdNum;
            clash.handleClash("sale","saleId","sale",sales);
            saleRecord.addSale();


            int quantity = int.Parse(quantityBox.Text);
            int newQuantity = quantity-1;
            conn.Open();
            MySqlCommand mysqlcommand2 = new MySqlCommand("Update glasses set quantity="+newQuantity+" where glassId='"+searchGlasses.Text+"'", conn);
            if (mysqlcommand2.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("INVENTORY UPDATED");
            }
            

      
            MySqlCommand mysqlcommand3 = conn.CreateCommand();
            mysqlcommand3.CommandText = "Select * from patient where patientId='" + patientSaleId.Text + "'";
            MySqlDataReader reader = mysqlcommand3.ExecuteReader();
            while (reader.Read())
            {
                DateTime date2 = DateTime.Now;
                string formatDate2 = string.Format("{0:D}", date2);
                glassesReceiptMaker(reader["patientId"].ToString(), reader["fname"].ToString(), reader["lname"].ToString(), reader["telephone"].ToString(),glassesBrand.Text,frameColourCombo.Text,formatDate2, totalCost, amountPaidNow, balance);
            }
            reader.Close();
            conn.Close();

            glassesBrand.Clear(); glassesCost.Clear(); amountPaid.Clear(); frameColourCombo.Text = ""; patientSaleId.Clear();
            searchGlasses.Text = ""; amountPaid.Clear();
            
        }


        private void glassesReceiptMaker(string patientId, string firstName, string lastName, string telephone, string brand, string frameColour, string date, double totalCost, double amountPaid, double balance)
        {
            string dpath = @"C:\ClearViewClinic\Receipts\";

            try
            {
                if (!Directory.Exists(dpath))
                {
                    Directory.CreateDirectory(dpath);
                }
            }
            catch (Exception ex)
            {
                // handle them here
            }
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(@"C:\ClearViewClinic\Receipts\" + patientId + "_glassesPayment.pdf", FileMode.Create));
            doc.Open();

            Paragraph heading = new Paragraph("--------------------------------------------------------  CLEAR VIEW EYE CLINIC  ----------------------------------------------------\n");
            Paragraph motto = new Paragraph("----------------------------------------------------- correcting the vision of tomorrow  ----------------------------------------------\n");
            Paragraph patientDetails = new Paragraph("\nDate:" + date + "\n\nPATIENT DETAILS:\nPatient Id: " + patientId + "\nFirst Name: " + firstName + "\nLast Name: " + lastName + "\nTelephone: " + telephone + "\n\n\n");
            Paragraph glassesDetails = new Paragraph("GLASSES DETAILS:\nGlasses brand: " + brand + "\nColour of frame: " + frameColour + "\nProduct Cost: " + totalCost + "\n\n");
            Paragraph paymentStatus = new Paragraph("PAYMENT INFORMATION:\nTotal Cost:" + totalCost + "\nAmount paid now:" + amountPaid + "\nBalance owing:" + balance + "\n\nCashier signature: _________________________");
            doc.Add(heading);
            doc.Add(motto);
            doc.Add(patientDetails);
            doc.Add(glassesDetails);
            doc.Add(paymentStatus);
            doc.Close();

            System.Diagnostics.Process.Start(@"C:\ClearViewClinic\Receipts\" + patientId + "_glassesPayment.pdf");
        }

        private void visitReceiptMaker(string patientId, string firstName, string lastName, string telephone, string date)
        {
            string dpath = @"C:\ClearViewClinic\Receipts\";

            try
            {
                if (!Directory.Exists(dpath))
                {
                    Directory.CreateDirectory(dpath);
                }
            }
            catch (Exception ex)
            {
               
            }
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(@"C:\ClearViewClinic\Receipts\" + patientId + "_generalPayment.pdf", FileMode.Create));
            doc.Open();

            Paragraph heading = new Paragraph("--------------------------------------------------------  CLEAR VIEW EYE CLINIC  ----------------------------------------------------\n");
            Paragraph motto = new Paragraph("----------------------------------------------------- correcting the vision of tomorrow  ----------------------------------------------\n");
            Paragraph patientDetails = new Paragraph("\nDate:" + date + "\n\nPATIENT DETAILS:\nPatient Id: " +patientId+ "\nFirst Name: " + firstName + "\nLast Name: " + lastName + "\nTelephone: " + telephone + "\n\n\n");

            Paragraph paymentStatus = new Paragraph("PAYMENT INFORMATION:\nTotal Cost:$6000\nAmount paid:$6000\n\nCashier signature: _________________________");
            doc.Add(heading);
            doc.Add(motto);
            doc.Add(patientDetails);
            doc.Add(paymentStatus);
            doc.Close();
            System.Diagnostics.Process.Start(@"C:\ClearViewClinic\Receipts\"+patientId+"_generalPayment.pdf");
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
            mouseDown = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            conn.Open();
              MySqlCommand mysqlcommand = conn.CreateCommand();
              string searchQuery = "SELECT * FROM patient where patientId='" + paymentID.Text + "'";

              mysqlcommand.CommandText = searchQuery;
              MySqlDataReader reader = mysqlcommand.ExecuteReader();

              while (reader.Read())
              {
                  firstNamePayment.Text = reader["fname"].ToString();
                  lastNamePayment.Text = reader["lname"].ToString();
              }

              reader.Close();


              mysqlcommand.CommandText = "Select * from sale where patientId='" + paymentID.Text + "'";
           MySqlDataReader reader1 = mysqlcommand.ExecuteReader();
           while (reader1.Read())
           {
               owingAmount.Text = reader1["balance"].ToString();
           }
           reader.Close(); 


            string queryToday = "sale inner join patient on sale.patientId=patient.patientId " + "where sale.patientId='" + paymentID.Text + "'";
            Crud inventory = new Crud();
            inventory.createTable(queryToday, balanceDataView);
            conn.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            string formatDate = string.Format("{0:D}", date);

            string queryToday = "appointment inner join patient on appointment.patientId=patient.patientId " + "where appointment.patientId='" + visitingPayment.Text + "'";
            Crud inventory = new Crud();
            inventory.createTable(queryToday, appointmentGrid);  
        }

        private void searchpatientBalance_TextChanged(object sender, EventArgs e)
        {

        }

        private void visitingPayment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void viewReportButton_Click(object sender, EventArgs e)
        {
            panelChanger(viewReportPanel);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            patientLoad(preReportId, "SELECT * FROM patient where fname LIKE '%" + searchPreTest.Text + "%'"); 
        }

        private void button1_Click_1(object sender, EventArgs e)
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
