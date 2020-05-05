using HospitalDBMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.Controllers
{
    public class WardController : Controller
    {
        // GET: Ward
        public ActionResult Index()
        {
            return View(WardDataAccess.GetAllWards());
        }

        public ActionResult Edit(int id)
        {
            Ward ward = WardDataAccess.GetAllWards().FirstOrDefault(x => x.Ward_No == id);

            Ward model = new Ward();

            model.Ward_No = ward.Ward_No;
            model.WardName = ward.WardName;
            model.Location = ward.Location;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Ward ward)
        {
            WardDataAccess.UpdateWard(ward);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View(new Ward());
        }

        [HttpPost]
        public ActionResult Create(Ward ward)
        {
            WardDataAccess.CreateWard(ward);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            Ward ward = WardDataAccess.GetAllWards().FirstOrDefault(x => x.Ward_No == id);

            Ward model = new Ward();

            model.Ward_No = ward.Ward_No;
            model.WardName = ward.WardName;
            model.Location = ward.Location;

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Ward ward = WardDataAccess.GetAllWards().FirstOrDefault(x => x.Ward_No == id);

            WardDataAccess.DeleteWard(ward.Ward_No);
            return RedirectToAction("Index");
        }
    }
}
