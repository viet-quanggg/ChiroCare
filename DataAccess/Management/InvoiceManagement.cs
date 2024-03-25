using BusinessObject;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Management;

public class InvoiceManagement
{
    ChiroCareContext _context;
    private static InvoiceManagement instance;
    private static readonly object instanceLock = new object();
    
    public static InvoiceManagement Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new InvoiceManagement();
                }
            }
            return instance;
        }
    }
    
    public async Task CreateNewInvoice(Invoice invoice)
    {
        _context = new ChiroCareContext();
        try
        {
            var existedInvoice = await _context.Invoices.FirstOrDefaultAsync(i => i.InvoiceId == invoice.InvoiceId);
            if (existedInvoice == null)
            {
                 await _context.Invoices.AddAsync(invoice);
                 await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    } 
    
    public  Task<Invoice> GetInvoiceDetail(Guid id)
    {
        _context = new ChiroCareContext();
        try
        {
            var invoice =  _context.Invoices
                .Include(i => i.Patient)
                .Include(i => i.ListServices)
                .Include(i => i.ListSessions)
                    .ThenInclude(i => i.Therapist)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);
        
            return invoice;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
}