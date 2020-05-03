using HospitalDBMS.Models;
using HospitalDBMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult Index(string searchString)
        {
            var staffs = StaffWardDataAccess.GetAllStaffs();

            if (!string.IsNullOrEmpty(searchString))
            {
                staffs = staffs.Where(x => x.Ward.WardName.StartsWith(searchString));
            }
            return View(staffs);
        }

        public ActionResult Edit(int id)
        {
            Staff staff = StaffWardDataAccess.GetAllStaffs().FirstOrDefault(x => x.Staff_Id == id);

            StaffWardViewModel viewModel = new StaffWardViewModel();

            viewModel.Staff_Id = staff.Staff_Id;
            viewModel.FirstName = staff.FirstName;
            viewModel.LastName = staff.LastName;
            viewModel.StaffType = staff.StaffType;
            viewModel.Contact = staff.Contact;
            viewModel.Ward_Id = staff.Ward.Ward_No;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(StaffWardViewModel staff)
        {
            StaffWardDataAccess.UpdateStaff(staff);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View(new StaffWardViewModel());
        }

        [HttpPost]
        public ActionResult Create(StaffWardViewModel staff)
        {
            StaffWardDataAccess.CreateStaff(staff);
            return RedirectToAction("Index");
        }
    }
}