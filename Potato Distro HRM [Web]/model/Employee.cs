using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Potato_Distro_HRM__Web_.model {
    public class Employee {

        public int id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string birthdate { get; set; }
        public string address { get; set; }
        public char gender { get; set; }
        public string contact { get; set; }
        public string superId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public double salary { get; set; }
        public int department { get; set; }
    }
}