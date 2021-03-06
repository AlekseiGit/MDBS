﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModels
{
    public class Patient
    {
        public Guid ID { get; set; }
        //public string FullName { get; set; }
        public string MedicalCardNumber { get; set; }
        public string Sex { get; set; }
        public int Weight { get; set; }
        //public string DrugsCount { get; set; }
        public string BirthDate { get; set; }
        public string Age { get; set; }
        //public string CurrentTherapy { get; set; }
        //public string IllStart { get; set; }
        public string VisitDate { get; set; }
        public string UsedDrugs { get; set; }
        public string RemissionPeriod { get; set; }
        public string LastExacerbation { get; set; }
        public string AppliedTherapy { get; set; }
        public string SurveyResults { get; set; }
        public string Complaints { get; set; }
        public string Info { get; set; }
        //public string Note { get; set; }
    }
}