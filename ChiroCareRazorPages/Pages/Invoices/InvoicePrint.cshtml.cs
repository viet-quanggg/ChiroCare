using Azure;
using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.IRepository;

namespace ChiroCareRazorPages.Pages.Invoices;

public class InvoicePrint : PageModel
{
    private readonly IInvoiceRepository _invoiceRepository;
    public InvoicePrint(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }
    [BindProperty]
    public Invoice Invoice { get; set; }
    public async Task<IActionResult> OnGet(Guid id)
    {
        Invoice = await _invoiceRepository.GetInvoiceDetail(id);
        return Page();
    }
}