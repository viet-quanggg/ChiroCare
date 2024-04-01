using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using DataAccess.Data;
using Repository.IRepository;

namespace ChiroCareRazorPages.Pages.Sessions
{
    public class CreateSessionUserDetailsModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;
        private readonly IInvoiceRepository _invoiceRepository;

        public CreateSessionUserDetailsModel(DataAccess.Data.ChiroCareContext context, IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
            _context = context;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            Invoice invoice = await _invoiceRepository.GetInvoiceDetail(id);
        ViewData["PatientId"] = new SelectList(_context.Users.Where(u => u.UserId == invoice.PatientId), "UserId", "FullName");
        ViewData["TherapistId"] = new SelectList(_context.Users.Where(u => u.Role == BusinessObject.ChiroEnums.Role.NGƯỜIĐIỀUTRỊ), "UserId", "FullName");
            return Page();
        }

        [BindProperty]
        public Session Session { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(Guid id)
        {
          if (_context.Sessions == null || Session == null)
            {
                return Page();
            }
            Session.InvoiceId = id;
            Session.SessionDate = DateTime.Now;

            _context.Sessions.Add(Session);
            await _context.SaveChangesAsync();

            return Redirect("/Customers/Details?id=" + Session.PatientId);
        }
    }
}
