using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalDBMS.Models
{
    public class Doctor
    {
        public int Doctor_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public Department Department { get; set; }
    }
}