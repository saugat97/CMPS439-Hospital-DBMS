using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalDBMS.Models
{
    public class Staff
    {
        public int Staff_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StaffType { get; set; }
        public string Contact { get; set; }
        public Ward Ward { get; set; }
    }
}