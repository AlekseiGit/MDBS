using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModels
{
    public class Attachments
    {
        public Attachments(Guid id, Guid messageID, byte[] data, string comment)
        {
            ID = id;
            MessageID = messageID;
            Data = data;
            Comment = comment;
        }

        public Guid ID { get; set; }
        public Guid MessageID { get; set; }
        public byte[] Data { get; set; }
        public string Comment { get; set; }
    }
}