using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModels
{
    public class Patient
    {
        public Guid ID { get; set; }
        public string FullName { get; set; }
        public int Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string MedicalCardNumber { get; set; }
        public string Info { get; set; }
        public string Note { get; set; }
    }
}