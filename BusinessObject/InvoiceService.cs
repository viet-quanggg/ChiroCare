namespace BusinessObject;

public class InvoiceService
{
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; }

    public Guid ServiceId { get; set; }
    public Service Service { get; set; }
}