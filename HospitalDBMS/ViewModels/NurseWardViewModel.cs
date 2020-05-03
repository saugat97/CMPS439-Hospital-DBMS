using HospitalDBMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalDBMS.ViewModels
{
    public class NurseWardViewModel
    {
        public int Nurse_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public int WardId { get; set; }

        public IEnumerable<SelectListItem> WardList { get; set; }

        public NurseWardViewModel()
        {
            WardList = NurseWardDataAccess.GetAllWardList();
        }
    }
}