using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearViewClinic
{
    class DoctorReport
    {

        string doctorReportId;
        string doctorId;
        string patientId;
        string glaucomaTest;
        string possibleDiseases;
        string coverTest;
        string refractionTest;
        string slitLampProcedure;
        string dialationProcedure;
        string framesNeededStatus;
        string conclusion;

 
        public DoctorReport()
        {
   
            this.doctorReportId = "";
            this.doctorId = "";
            this.patientId = "";
            this.glaucomaTest = "";
            this.possibleDiseases = "";
            this.coverTest = "";
            this.refractionTest = "";
            this.slitLampProcedure = "";
            this.dialationProcedure = "";
            this.framesNeededStatus = "";
            this.conclusion = "";

        }


        public DoctorReport(string doctorReportId, string doctorId, string patientId, string glaucomaTest, string possibleDiseases, string coverTest, string refractionTest, string slitLampProcedure, string dialationProcedure,string framesNeededStatus, string conclusion)
        {
            this.doctorReportId = doctorReportId;
            this.doctorId =doctorId ;
            this.patientId =patientId ;
            this.glaucomaTest =glaucomaTest ;
            this.possibleDiseases =possibleDiseases ;
            this.coverTest = coverTest;
            this.refractionTest =refractionTest ;
            this.slitLampProcedure = slitLampProcedure;
            this.dialationProcedure =dialationProcedure ;
            this.framesNeededStatus = framesNeededStatus;
            this.conclusion =conclusion;
        }


        public void addReport()
        {
            string query1 = "Insert into doctor_report(doctorReportId,doctorId,patientId,glaucomaTest,possibleDiseases,coverTest,refractionTest,slitLampProcedure,dialationProcedure,framesNeededStatus,conclusion) values('" + doctorReportId + "','" + doctorId + "','" + patientId + "','" + glaucomaTest + "','" + possibleDiseases + "','" + coverTest + "','" + refractionTest + "','" + slitLampProcedure + "','" + dialationProcedure + "','" + framesNeededStatus + "','" + conclusion + "')";
            Crud inserter = new Crud();
            inserter.insertData(query1);
        }

        public string DoctorReportId
        {
            get { return doctorReportId; }
            set { doctorReportId = value; }
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

        public string GlaucomaStatus
        {
            get { return glaucomaTest; }
            set { glaucomaTest = value; }
        }

        public string PossibleDiseases
        {
            get { return possibleDiseases; }
            set { possibleDiseases = value; }
        }

        public string CoverTest
        {
            get { return coverTest; }
            set { coverTest = value; }
        }
        public string RefractionTest
        {
            get { return refractionTest; }
            set { refractionTest = value; }
        }

        public string SlitLampProcedure
        {
            get { return slitLampProcedure; }
            set { slitLampProcedure = value; }
        }

        public string DialationProcedure
        {
            get { return dialationProcedure; }
            set { dialationProcedure = value; }
        }

        public string FramesNeededStatus
        {
            get { return framesNeededStatus; }
            set { framesNeededStatus = value; }
        }
        public string Conclusion
        {
            get { return conclusion; }
            set { conclusion = value; }
        }
    }
}
