using HospitalDBMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.ViewModels
{
    public class PatientReportViewModel
    {
        public int Patient_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Contact { get; set; }
        public DateTime AdmitDateAndTime { get; set; }
        public DateTime ReportDateAndTime { get; set; }
        public int ReportId { get; set; }
        public IEnumerable<SelectListItem> ReportList { get; set; }

        public PatientReportViewModel()
        {
            ReportList = PatientReportDataAccess.GetAllReportList();
        }
    }
}