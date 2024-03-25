using System;
using System.ComponentModel.DataAnnotations;

public class WorkShift 
{
    [Key]
    public Guid ShiftId { get; set; }

    // public Guid TherapistId { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public int ShiftStatus { get; set; }

    public string ShiftNote { get; set; }
    
    // public User ShiftUser { get; set; }
}