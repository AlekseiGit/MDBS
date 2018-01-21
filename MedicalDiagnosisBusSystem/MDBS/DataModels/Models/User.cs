using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class User
    {
        public Guid ID { get; set; }
        public string FullName { get; set; }
        public string DocNumber { get; set; }
    }
}