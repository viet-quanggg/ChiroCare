using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess.Data;
using Repository.IRepository;

namespace ChiroCareRazorPages.Pages.Invoices
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;
        public readonly IInvoiceRepository _invoiceRepository;
        public DetailsModel(DataAccess.Data.ChiroCareContext context, IInvoiceRepository invoiceRepository)
        {
            _context = context;
            _invoiceRepository = invoiceRepository;
        }

      public Invoice Invoice { get; set; } = default!; 
      public List<Service> invoiceServices { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _invoiceRepository == null)
            {
                return NotFound();
            }

            var invoice = await _invoiceRepository.GetInvoiceDetail((Guid)id);
            if (invoice == null)
            {
                return NotFound();
            }
            else 
            {
                Invoice = invoice;
                // foreach (var service in invoice.ListServices)
                // {
                //     Service Eachservice = new Service();
                //     Eachservice = service;
                //     invoiceServices.Add(Eachservice);
                // }
            }
            return Page();
        }
    }
}
