using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearViewClinic
{
    class LoginDetail:Employee
    {
        private string authLevel;
        private string password;

        public LoginDetail()
        {
            this.UserId = "";
            this.AuthLevel = "";
            this.password="";
        }

        public LoginDetail(string userId,string password, string level)
        {
            this.UserId =userId;
            this.AuthLevel =level;
            this.password= password;
        }

        public void addCredentials()
        {
            string query = "Insert into login_details (employeeId,password,authLevel) values('" + UserId + "','" + password + "','"+AuthLevel+"')";
            Crud inserter = new Crud();
            inserter.insertData(query); 
        }


        public string AuthLevel
        {
            get { return authLevel; }
            set { authLevel = value; }
        }
    }
}
