using HospitalDBMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.ViewModels
{
    public class StaffWardViewModel
    {
        public int Staff_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StaffType { get; set; }
        public string Contact { get; set; }
        public int Ward_Id { get; set; }

        public IEnumerable<SelectListItem> WardList { get; set; }

        public StaffWardViewModel()
        {
            WardList = StaffWardDataAccess.GetAllWardList();
        }
    }
}