using BusinessObject;
using DataAccess.Management;
using Repository.IRepository;

namespace Repository.Repository;

public class InvoiceRepository : IInvoiceRepository
{
    public async Task CreateNewInvoice(Invoice invoice)
    {
         InvoiceManagement.Instance.CreateNewInvoice(invoice);
    }

    public Task<Invoice> GetInvoiceDetail(Guid guid) => InvoiceManagement.Instance.GetInvoiceDetail(guid);

}