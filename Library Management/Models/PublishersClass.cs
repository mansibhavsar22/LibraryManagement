using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_Management.Models
{
    public class PublishersClass
    {
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}