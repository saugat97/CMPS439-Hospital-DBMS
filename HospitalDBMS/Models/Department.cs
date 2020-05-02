using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalDBMS.Models
{
    public class Department
    {
        public int Department_Id { get; set; }
        public string Speciality { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public int NoOfDoctors { get; set; }
    }
}