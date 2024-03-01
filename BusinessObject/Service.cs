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
    public decimal ServicePrice { get; set; }
    public string ServiceDescription { get; set; }

    public int CategoryId { get; set; }
    public ServiceCategory ServiceCategory { get; set; }
    
}