using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalDBMS.Models
{
    public class Ward
    {
        public int Ward_No { get; set; }
        public string WardName { get; set; }
        public string Location { get; set; }
        public int NoOfStaff { get; set; }
        public int NoOfNurses { get; set; }
    }
}