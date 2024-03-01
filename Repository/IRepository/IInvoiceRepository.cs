using BusinessObject;

namespace Repository.IRepository;

public interface IInvoiceRepository
{
    Task CreateNewInvocice(Invoice invoice);
    Task<Invoice> GetInvoiceDetail(Guid guid);
}