using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearViewClinic
{
    class Employee:Person
    {
        private string userId;
        private string profilePic;


        public Employee()
        {
            this.userId="";
            this.Fname="";
            this.Lname="";
            this.Gender="";
            this.profilePic = "";
        }

        public Employee(string userId,string fname,string lname,string gender,string profilePic)
        {
            this.userId = userId;
            this.Fname = fname;
            this.Lname = lname;
            this.Gender = gender;
            this.profilePic =profilePic;
        }

        public void addEmployee()
        {
            string query = "Insert into employee (employeeId,fname,lname,gender,profilePic) values('" + userId + "','"+ Fname +"','"+Lname+"','"+Gender+"','"+profilePic+"')";
            Crud inserter = new Crud();
            inserter.insertData(query);

        }

        public void updateEmployee(string searchKey)
        {

            string query1 = "Update employee set fname='" + Fname + "',lname='" + Lname +"',gender='" + Gender + "',profilePic='" + ProfilePic + "' where employeeId='" + searchKey + "'";
            Crud updateEmp = new Crud();
            updateEmp.updateData(query1);
        } 

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }


        public string ProfilePic
        {
            get { return profilePic; }
            set { profilePic = value; }
        }

    }
}
