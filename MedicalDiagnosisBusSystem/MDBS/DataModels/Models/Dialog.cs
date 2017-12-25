using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModels
{
    public class Dialog
    {
        public Guid ID { get; set; }
        public string Info { get; set; }
        public string Diagnosis { get; set; }
        public string TherapyPlan { get; set; }
        //public Guid ParentMessageID { get; set; }
        public Guid PatientID { get; set; }
        public string PatientName { get; set; }
        public Guid From { get; set; }
        //public Guid To { get; set; }
        public string FromName { get; set; }
        public string MessageDate { get; set; }
        //public int Status { get; set; }
    }
}