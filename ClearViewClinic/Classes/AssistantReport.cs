using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearViewClinic
{
    class AssistantReport : Employee
    {
        private string reportId;
        private string patientId;
        private string issues;
        private string reportDate;
        private int readingTest;
        private string familyHistory;
        private double eyePressure; 
        private double sphereDistance;
        private double sphereNear;
        private double cylinderDistance;
        private double cylinderNear;
        private double axisDistance;
        private double axisNear;
        private double prismDistance;
        private double prismNear;
        private double baseDistance;
        private double baseNear;


        public AssistantReport()
        {
            this.reportId = "";
            this.patientId = "";
            this.UserId = "";
            this.reportDate = "";
            this.issues = "";
            this.readingTest =0;
            this.familyHistory = "";
            this.eyePressure = 0;
            this.sphereDistance = 0;
            this.sphereNear = 0;
            this.cylinderDistance = 0;
            this.cylinderNear = 0;
            this.axisDistance = 0;
            this.axisNear = 0;
            this.prismDistance = 0;
            this.prismNear = 0;
            this.baseDistance = 0;
            this.baseNear = 0;
        }

        public AssistantReport(string reportId, string userId,string patientId, string reportDate, string issues, int readingTest, string familyHistory, double eyePressure, double sphereDistance, double sphereNear, double cylinderDistance, double cylinderNear, double axisDistance, double axisNear,double prismDistance,double prismNear, double baseDistance, double baseNear)
        {
            this.reportId=reportId;
            this.patientId=patientId;
            this.UserId= userId;
            this.reportDate=reportDate;
            this.issues=issues;
            this.readingTest=readingTest ;
            this.familyHistory=familyHistory;
            this.eyePressure=eyePressure ;
            this.sphereDistance=sphereDistance;     
            this.sphereNear=sphereNear ;
            this.cylinderDistance=cylinderDistance ;
            this.cylinderNear=cylinderNear;
            this.axisDistance=axisDistance;
            this.axisNear=axisNear;
            this.prismDistance = prismDistance;
            this.baseDistance=baseDistance;
            this.prismNear = prismNear;
            this.baseNear=baseNear ;
        }


        public void addPreReport(string reportId,string patientId,string userId,string reportDate, string issues, int readingTest, string familyHistory, double eyePressure, double sphereDistance, double sphereNear, double cylinderDistance, double cylinderNear, double axisDistance, double axisNear, double baseDistance,double prismDistance,double prismNear, double baseNear)
        {
            string query = "Insert into pre_test(reportId,patientId,userId,reportDate,issues,readingTest,familyHistory,eyePressure,sphereDistance,sphereNear,cylinderDistance,cylinderNear,axisDistance,axisNear,prismDistance,prismNear,baseDistance,baseNear) values('" + reportId + "','" + patientId + "','" + userId + "','" + reportDate + "','" + issues + "','" + readingTest + "','" + familyHistory + "','" + eyePressure +"','" + sphereDistance + "','" + sphereNear + "','" + cylinderDistance + "','" + cylinderNear + "','" + axisDistance + "','" + axisNear +"','" +prismDistance+"','"+prismNear+"','" + baseDistance + "','" + baseNear + "')";
            Crud inserter = new Crud();
            inserter.insertData(query);
        }

        public string ReportId
        {
            get { return reportId; }
            set { reportId = value; }
        }

        public string PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        public string ReportDate
        {
            get { return reportDate; }
            set { reportDate = value; }
        }

        public string Issues
        {
            get { return issues; }
            set { issues = value; }
        }

        public string FamilyHistory
        {
            get { return familyHistory; }
            set { familyHistory = value; }
        }

        public int ReadingTest
        {
            get { return readingTest; }
            set { readingTest = value; }
        }
        public double EyePressure
        {
            get { return eyePressure; }
            set { eyePressure = value; }
        }

        public double SphereDistance
        {
            get { return sphereDistance; }
            set { sphereDistance = value; }
        }

        public double SphereNear
        {
            get { return sphereNear; }
            set { sphereNear = value; }
        }

        public double CylinderDistanced
        {
            get { return cylinderDistance; }
            set { cylinderDistance = value; }
        }
        public double CylinderNear
        {
            get { return cylinderNear; }
            set { cylinderNear = value; }
        }
        public double AxisDistance
        {
            get { return axisDistance; }
            set { axisDistance = value; }
        }
        public double AxisNear
        {
            get { return axisNear; }
            set { axisNear = value; }
        }
        public double BaseDistance
        {
            get { return baseDistance; }
            set { baseDistance = value; }
        }

        public double BaseNear
        {
            get { return baseNear; }
            set { baseNear = value; }
        }
    }
}
