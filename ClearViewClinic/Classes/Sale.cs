using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearViewClinic
{
    class Sale:Glasses
    {
        private string saleId;
        private string patientId;
        private string frameColour;
        private string saleDate;
        private double totalCost;  
        private double balance;


        public Sale()
        {
            this.saleId = "";
            this.patientId="";
            this.GlassesBrand = "";
            this.frameColour = "";
            this.saleDate="";
            this.totalCost=0.00;
            this.balance=0.00;
        }

        public Sale(string saleId,string patientId,string glassesBrand,string frameColour,string saleDate,double totalCost,double balance)
        {
            this.saleId = saleId;
            this.patientId = patientId;
            this.GlassesBrand = glassesBrand;
            this.frameColour = frameColour;
            this.saleDate = saleDate;
            this.totalCost = totalCost;
            this.balance =balance;
        }

        public Sale(double balance)
        {
            this.balance = balance;
        }

        public void addSale()
        {
            string addSaleRecord = "Insert into sale(saleId,patientId,GlassesBrand,frameColour,saleDate,totalCost,balance) values('" + saleId + "','" + patientId + "','" + GlassesBrand + "','" + frameColour + "','" + saleDate + "','" + totalCost + "','" + balance + "')";
            Crud inserter = new Crud();
            inserter.insertData(addSaleRecord);
        }

        public void updateSale(string searchKey)
        {

            string saleUpdate = "Update glasses set GlassesBrand='" + GlassesBrand + "',frameColour='" + frameColour+ "saleDate=" + saleDate + ",totalCost='" + totalCost + ",balance='"+ balance +"' where saleId='" + searchKey + "'";
            Crud updater = new Crud();
            updater.updateData(saleUpdate);
        }


        public void updateBalance(string searchKey)
        {

            string paymentUpdate = "Update sale set balance="+ balance +" where patientId='" + searchKey + "'";
            Crud updater = new Crud();
            updater.updateData(paymentUpdate);
        }



        public string PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }


        public string FrameColour
        {
            get { return frameColour; }
            set { frameColour = value; }
        }

        public string Date
        {
            get { return saleDate; }
            set { saleDate = value; }
        }

        public double TotalCost
        {
            get { return totalCost; }
            set { totalCost = value; }
        }

        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }
    }
}
