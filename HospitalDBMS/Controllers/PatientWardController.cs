using HospitalDBMS.Models;
using HospitalDBMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.Controllers
{
    public class PatientWardController : Controller
    {
        // GET: PatientWard
        public ActionResult Index(string searchString)
        {
            var patients = PatientReportDataAccess.GetAllPatients();

            if (!string.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(x => x.Ward.WardName.StartsWith(searchString));
            }

            return View(patients);
        }

        public ActionResult Edit(int id)
        {
            Patient patient = PatientWardDataAccess.GetAllPatients().FirstOrDefault(x => x.Patient_Id == id);

            PatientWardViewModel viewModel = new PatientWardViewModel();

            viewModel.Patient_Id = patient.Patient_Id;
            viewModel.FirstName = patient.FirstName;
            viewModel.LastName = patient.LastName;
            viewModel.Gender = patient.Gender;
            viewModel.Age = patient.Age;
            viewModel.Contact = patient.Contact;
            viewModel.AdmitDateAndTime = patient.AdmitDateAndTime;
            viewModel.Ward_Id = patient.Ward.Ward_No;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(PatientWardViewModel patient)
        {
            PatientWardDataAccess.UpdatePatient(patient);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View(new PatientWardViewModel());
        }

        [HttpPost]
        public ActionResult Create(PatientWardViewModel patient)
        {
            PatientWardDataAccess.CreatePatient(patient);
            return RedirectToAction("Index");
        }
    }
}

