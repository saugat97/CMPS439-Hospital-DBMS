using HospitalDBMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.ViewModels
{
    public class DoctorDeptViewModel
    {
        public int Doctor_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public int Dept_Id { get; set; }

        public IEnumerable<SelectListItem> DepartmentList { get; set; }

        public DoctorDeptViewModel()
        {
            DepartmentList = DataAccess.GetAllDepartmentList();
        }
    }
}