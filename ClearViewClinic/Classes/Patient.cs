using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearViewClinic
{
    class Patient:Person
    {
        private string patientId;
        private string telephone;

        public Patient()
        {
            this.patientId = "";
            this.Fname = "";
            this.Lname = "";
            this.Gender = "";
            this.telephone = "";
        }

        public Patient(string patientId, string fname, string lname,string gender,string telephone)
        {
            this.patientId=patientId;
            this.Fname=fname;
            this.Lname=lname;
            this.Gender =gender;
            this.telephone = telephone;
        }

        public void addPatient()
        {
            string newPatient = "Insert into patient (patientId,fname,lname,gender,telephone) values('" + patientId + "','" + Fname + "','" + Lname + "','"+Gender+"','"+ telephone+"')";
            Crud inserter = new Crud();
            inserter.insertData(newPatient);
        }

        public void updatePatient(string searchKey)
        {
            string updatePatient = "Update patient set fname='" + Fname + "',lname='" + Lname + "',gender='" +Gender + "',telephone='" +telephone+ "' where patientId='"+searchKey+"'";
            Crud updatePat = new Crud();
            updatePat.updateData(updatePatient);
        } 

        public string PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        public string visitDate
        {
            get { return visitDate; }
            set { visitDate = value; }
        }

        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

    }
}
