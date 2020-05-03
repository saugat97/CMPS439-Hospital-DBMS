using HospitalDBMS.Models;
using HospitalDBMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index(string searchString)
        {
            var patients = PatientReportDataAccess.GetAllPatients();

            if (!string.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(x => x.Report.Description.StartsWith(searchString));
            }

            return View(patients);
        }

        public ActionResult Edit(int id)
        {
            Patient patient = PatientReportDataAccess.GetAllPatients().FirstOrDefault(x => x.Patient_Id == id);

            PatientReportViewModel viewModel = new PatientReportViewModel();

            viewModel.Patient_Id = patient.Patient_Id;
            viewModel.FirstName = patient.FirstName;
            viewModel.LastName = patient.LastName;
            viewModel.Gender = patient.Gender;
            viewModel.Age = patient.Age;
            viewModel.Contact = patient.Contact;
            viewModel.AdmitDateAndTime = patient.AdmitDateAndTime;
            viewModel.ReportDateAndTime = patient.ReportDateAndTime;
            viewModel.ReportId = patient.Report.Report_Id;
            
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(PatientReportViewModel patient)
        {
            PatientReportDataAccess.UpdatePatient(patient);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View(new PatientReportViewModel());
        }

        [HttpPost]
        public ActionResult Create(PatientReportViewModel patient)
        {
            PatientReportDataAccess.CreatePatient(patient);
            return RedirectToAction("Index");
        }
    }
}

