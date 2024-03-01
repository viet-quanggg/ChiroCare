using System.ComponentModel.DataAnnotations;

namespace BusinessObject;

using System;
using System.ComponentModel.DataAnnotations;

public class Session 
{
    [Key]
    public Guid SessionId { get; set; }

    public Guid TherapistId { get; set; }

    public Guid PatientId { get; set; }

    [Required(ErrorMessage = "Thông tin buổi khám không được trống!")]
    public string SessionInfo { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime SessionDate { get; set; }
    
    public User Patient { get; set; }

    public User Therapist { get; set; }

    public ICollection<Service> Services { get; set; }
}
