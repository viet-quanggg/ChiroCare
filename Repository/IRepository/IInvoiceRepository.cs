using BusinessObject;

namespace Repository.IRepository;

public interface IInvoiceRepository
{
    Task CreateNewInvoice(Invoice invoice);
    Task<Invoice> GetInvoiceDetail(Guid guid);
}