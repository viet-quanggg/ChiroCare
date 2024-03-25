using System;
using System.ComponentModel.DataAnnotations;
using BusinessObject;

public class Service 
{
    [Key]
    public Guid ServiceId { get; set; }

    [Required(ErrorMessage = "Tên dịch vụ không thể để trống!")]
    public string ServiceName { get; set; }

    [DataType(DataType.Currency)]
    [Required(ErrorMessage = "Giá dịch vụ không thể để trống!")]
    [Range(0, 9999999, ErrorMessage = "Giá dịch vụ phải từ 0đ đến 9.999.999đ")]
    public decimal ServicePrice { get; set; }
    public string? ServiceDescription { get; set; }
    public int? CategoryId { get; set; }
    public ServiceCategory? ServiceCategory { get; set; }
    
}