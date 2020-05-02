using HospitalDBMS.Models;
using HospitalDBMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index(string searchString)
        {
            var doctors = DataAccess.GetAllDoctors();

            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(s => s.Department.Speciality.StartsWith(searchString));
            }

            return View(doctors);
        }

        public ActionResult Edit(int id)
        {
            Doctor doctor = DataAccess.GetAllDoctors().FirstOrDefault(x => x.Doctor_Id == id);

            DoctorDeptViewModel viewModel = new DoctorDeptViewModel();

            viewModel.Doctor_Id = doctor.Doctor_Id;
            viewModel.FirstName = doctor.FirstName;
            viewModel.LastName = doctor.LastName;
            viewModel.Contact = doctor.Contact;
            viewModel.Dept_Id = doctor.Department.Department_Id;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(DoctorDeptViewModel doctor)
        {
            DataAccess.UpdateDoctor(doctor);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View(new DoctorDeptViewModel());
        }

        [HttpPost]
        public ActionResult Create(DoctorDeptViewModel doctor)
        {
            DataAccess.CreateDoctor(doctor);
            return RedirectToAction("Index");
        }


    }
}