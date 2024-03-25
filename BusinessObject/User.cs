using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using BusinessObject;
using BusinessObject.ChiroEnums;

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
    [Required(ErrorMessage = "Ngày sinh không thể để trống!")]
    public DateTime Dob { get; set; }
    
    [Required(ErrorMessage = "Giới tính không thể để trống!")]
    public int Gender { get; set; }
    public string? Job { get; set; }
    public string? History { get; set; }
    [Required]
    public Role Role { get; set; }

    [DataType(DataType.EmailAddress)]
    public string? EmailAddress { get; set; }

    public List<Session> UserSessions { get; set; } = new List<Session>();
    public List<Invoice> UserInvoices { get; set; } = new List<Invoice>();
    // public List<WorkShift>? UserWorkShifts { get; set; } = new List<WorkShift>();
}