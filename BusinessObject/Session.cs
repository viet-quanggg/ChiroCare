using System.ComponentModel.DataAnnotations;
using BusinessObject.ChiroEnums;

namespace BusinessObject;

using System;
using System.ComponentModel.DataAnnotations;

public class Session 
{
    [Key]
    public Guid SessionId { get; set; }
    public Guid TherapistId { get; set; }
    public User Therapist { get; set; }

    public Guid? PatientId { get; set; }
    public User? Patient { get; set; }

    public string SessionTreatment { get; set; }
    [Required(ErrorMessage = "Thông tin buổi khám không được trống!")]
    public string? SessionInfo { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime SessionDate { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime? SessionAppointment { get; set; }
    public SessionStatus Status { get; set; }
    public Guid? InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }
    
    public ICollection<Service>? Services { get; set; }
}
