using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using BusinessObject;

public class User
{
    [Key]
    public Guid UserId { get; set; }

    [Phone]
    [Required(ErrorMessage = "SĐT không thể để trống!")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Tên người dùng không thể để trống!")]
    [MaxLength(1000)]
    public string FullName { get; set; }

    [DataType(DataType.Date)]
    public DateTime Dob { get; set; }

    public int Gender { get; set; }

    [Required]
    public int Role { get; set; }

    [DataType(DataType.EmailAddress)]
    [AllowNull]
    public string EmailAddress { get; set; }

    public List<Session> UserSessions { get; set; } = new List<Session>();
    public List<Invoice> UserInvoices { get; set; } = new List<Invoice>();
    public List<WorkShift> UserWorkShifts { get; set; } = new List<WorkShift>();
}