using HospitalDBMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            return View(DepartmentDataAccess.GetAllDepartments());
        }

        public ActionResult Edit(int id)
        {
            Department department = DepartmentDataAccess.GetAllDepartments().FirstOrDefault(x => x.Department_Id == id);

            Department model = new Department();

            model.Department_Id = department.Department_Id;
            model.Speciality = department.Speciality;
            model.Location = department.Location;
            model.Contact = department.Contact;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            DepartmentDataAccess.UpdateDepartment(department);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View(new Department());
        }

        [HttpPost]
        public ActionResult Create(Department department)
        {
            DepartmentDataAccess.CreateDepartment(department);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            Department department = DepartmentDataAccess.GetAllDepartments().FirstOrDefault(x => x.Department_Id == id);

            Department model = new Department();

            model.Department_Id = department.Department_Id;
            model.Speciality = department.Speciality;
            model.Location = department.Location;
            model.Contact = department.Contact;

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Department department = DepartmentDataAccess.GetAllDepartments().FirstOrDefault(x => x.Department_Id == id);

            DepartmentDataAccess.DeleteDepartment(department.Department_Id);
            return RedirectToAction("Index");
        }
        

        
    }
   
}