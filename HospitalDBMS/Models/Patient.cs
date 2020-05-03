using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalDBMS.Models
{
    public class Patient
    {
        public int Patient_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Contact { get; set; }
        public DateTime AdmitDateAndTime { get; set; }
        public Ward Ward { get; set; }
        public Doctor Doctor { get; set; }
        public Report Report { get; set; }
        public DateTime ReportDateAndTime { get; set; }
    }
}