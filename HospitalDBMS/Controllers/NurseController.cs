using HospitalDBMS.Models;
using HospitalDBMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.Controllers
{
    public class NurseController : Controller
    {
        // GET: Nurse
        public ActionResult Index(string searchString)
        {
            var nurses = NurseWardDataAccess.GetAllNurses();

            if (!string.IsNullOrEmpty(searchString))
            {
                nurses = nurses.Where(x => x.Ward.WardName.StartsWith(searchString));
            }
            return View(nurses);
        }

        public ActionResult Edit(int id)
        {
            Nurse nurse = NurseWardDataAccess.GetAllNurses().FirstOrDefault(x => x.Nurse_Id == id);

            NurseWardViewModel viewModel = new NurseWardViewModel();

            viewModel.Nurse_Id = nurse.Nurse_Id;
            viewModel.FirstName = nurse.FirstName;
            viewModel.LastName = nurse.LastName;
            viewModel.Contact = nurse.Contact;
            viewModel.WardId = nurse.Ward.Ward_No;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(NurseWardViewModel nurse)
        {
            NurseWardDataAccess.UpdateNurse(nurse);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View(new NurseWardViewModel());
        }

        [HttpPost]
        public ActionResult Create(NurseWardViewModel nurse)
        {
            NurseWardDataAccess.CreateNurse(nurse);
            return RedirectToAction("Index");
        }
    }
}