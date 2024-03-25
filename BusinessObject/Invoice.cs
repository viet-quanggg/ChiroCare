using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessObject.ChiroEnums;

namespace BusinessObject
{
    public class Invoice
    {
        public Guid InvoiceId { get;  set; }
        public InvoiceStatus InvoiceStatus { get;  set; }

        [Required(ErrorMessage = "Create date is required")]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get;  set; }
        public Guid? PatientId { get;  set; }
        public User? Patient { get; set; } = null!;

        [Required(ErrorMessage = "Mô tả của hoá đơn không được trống!")]
        public string InvoiceDescription { get;  set; }
        [Required(ErrorMessage = "Số lượng buổi không được để trống !")]
        public int Quantity { get; set; }
        public string? InvoiceNote { get;  set; }
        [DataType(DataType.Currency)]
        public decimal InvoiceTotal { get;  set; }
        public InvoiceMethod InvoiceMethod { get; set; }

        [Required(ErrorMessage = "Dịch vụ cần được chọn")]
        public List<Service> ListServices { get; set; } = new List<Service>();

        [Required(ErrorMessage = "List of sessions is required")]
        public List<Session> ListSessions { get; set; } = new List<Session>();

    }
}