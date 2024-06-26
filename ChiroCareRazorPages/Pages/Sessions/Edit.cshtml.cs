using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using BusinessObject.ChiroEnums;
using DataAccess.Data;
using Repository.IRepository;

namespace ChiroCareRazorPages.Pages.Sessions
{
    public class EditModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;
        private readonly IInvoiceRepository _invoiceRepository;
        public EditModel(DataAccess.Data.ChiroCareContext context, IInvoiceRepository invoiceRepository)
        {
            _context = context;
            _invoiceRepository = invoiceRepository;
        }

        [BindProperty]
        public Session Session { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session =  await _context.Sessions
                .Include(s => s.Invoice)
                .FirstOrDefaultAsync(m => m.SessionId == id);
            if (session == null)
            {
                return NotFound();
            }
            Session = session;
           ViewData["InvoiceId"] = new SelectList(_context.Invoices.Where(i => i.InvoiceId == Session.InvoiceId), "InvoiceId", "InvoiceId");
           ViewData["PatientId"] = new SelectList(_context.Users.Where(u => u.UserId == session.PatientId), "UserId", "FullName");
           ViewData["TherapistId"] = new SelectList(_context.Users.Where(u => u.Role == BusinessObject.ChiroEnums.Role.NGƯỜIĐIỀUTRỊ), "UserId", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            Session.SessionDate = DateTime.Now;
            _context.Attach(Session).State = EntityState.Modified;
            
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(Session.SessionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Redirect("/Invoices/Details?id=" + Session.InvoiceId);
        }

        private bool SessionExists(Guid id)
        {
          return (_context.Sessions?.Any(e => e.SessionId == id)).GetValueOrDefault();
        }
    }
}
