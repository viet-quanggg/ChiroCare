using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class Invoice
    {
        public Guid InvoiceId { get;  set; }
        public int InvoiceStatus { get;  set; }

        [Required(ErrorMessage = "Create date is required")]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get;  set; }

        public string InvoiceDiagnose { get;  set; }
        public Guid PatientId { get;  set; }

        [Required(ErrorMessage = "Invoice description is required")]
        public string InvoiceDescription { get;  set; }
        public string InvoiceNote { get;  set; }
        public decimal InvoiceTotal { get;  set; }

        
        [Required(ErrorMessage = "List of services is required")]
        public List<Service> ListServices { get;  set; }

        
        [Required(ErrorMessage = "List of sessions is required")]
        public List<Session> ListSessions { get;  set; } 

        public User Patient { get; set; } = null!;
    }
}