using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClearViewClinic
{
    class Appointment
    {
        private string appointmentId;
        private string doctorId;
        private string patientId;
        private string visitingDate;
        private string visitingTime;
        private double visitCost;
        private string paymentStatus;
        private string appointmentStatus;

      

        public Appointment()
        {
            this.appointmentId="";
            this.doctorId="";
            this.patientId="";
            this.visitingDate="";
            this.visitCost =0.00;
            this.paymentStatus ="";
            this.appointmentStatus ="";
        }


         public Appointment(string appointmentId,string doctorId,string patientId,string visitingDate,string visitingTime,double visitCost,string paymentStatus,string appointmentStatus)
         {
            this.appointmentId = appointmentId;
            this.doctorId = doctorId;
            this.patientId = patientId;
            this.visitingDate = visitingDate;
            this.visitingTime = visitingTime;
            this.visitCost=visitCost;
            this.paymentStatus = paymentStatus;
            this.appointmentStatus = appointmentStatus;
         }


         public Appointment(string paymentStatus)
         {
             this.paymentStatus = paymentStatus;
         }


         public void addAppointment()
         {
             string newPatient = "Insert into appointment(appointmentId,doctorId,patientId,visitingDate,visitingTime,visitCost,paymentStatus,appointmentStatus) values('" + appointmentId + "','" + doctorId + "','" + patientId + "','" + visitingDate + "','" + visitingTime + "'," + visitCost + ",'" + paymentStatus + "','" + appointmentStatus + "')";
             Crud inserter = new Crud();
             inserter.insertData(newPatient);
         }

         public void updateAppointment(string searchKey) 
         {
             string updatePatient = "Update appointment set doctorId='" + doctorId + "',patientId='" + patientId + "',visitingDate='" + visitingDate + "',visitingTime='" + visitingTime +"',visitCost=" + visitCost + ",paymentStatus='" + paymentStatus + "',appointmentStatus='" + appointmentStatus + "' where patientId='"+searchKey+"'";
             
             Crud updateAppointments = new Crud();
             updateAppointments.updateData(updatePatient);
         }

         public void updatePayment(string searchKey)
         {
             string updatePayment = "Update appointment set paymentStatus='" + paymentStatus+"' where patientId='"+searchKey+"'";
             Crud updatePayments= new Crud();
             updatePayments.updateData(updatePayment);
         } 



        public string AppointmentId
        {
            get { return appointmentId; }
            set { appointmentId = value; }
        }

        public string DoctorId
        {
            get { return doctorId; }
            set { doctorId = value; }
        }

        public string PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }


        public string Visitingdate
        {
            get { return visitingDate; }
            set { visitingDate = value; }
        }

        public string VisitingTime
        {
            get { return visitingTime; }
            set { visitingTime = value; }
        }

        public double VisitCost
        {
            get { return visitCost; }
            set { visitCost = value; }
        }

        public string PaymentStatus
        {
            get { return paymentStatus; }
            set { paymentStatus = value; }
        }

        public string AppointmentStatus
        {
            get { return appointmentStatus; }
            set { appointmentStatus = value; }
        }
      
    }
}
