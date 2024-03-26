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

namespace ChiroCareRazorPages.Pages.Sessions
{
    public class EditModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;

        public EditModel(DataAccess.Data.ChiroCareContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Session Session { get; set; } = default!;
        public Guid invoiceId { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            HttpContext.Session.Clear();
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session =  await _context.Sessions
                .Include(s => s.Invoice)
                .ThenInclude(s => s.Patient)
                .Include(s => s.Patient)
                .FirstOrDefaultAsync(m => m.SessionId == id);
            if (session == null)
            {
                return NotFound();
            }
            Session = session;
            invoiceId = session.InvoiceId;
           ViewData["PatientId"] = new SelectList(_context.Users.Where(u => u.UserId == Session.PatientId), "UserId", "FullName");
           ViewData["TherapistId"] = new SelectList(_context.Users.Where(u => u.Role == Role.NGƯỜIĐIỀUTRỊ), "UserId", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            
            Session.SessionDate = DateTime.Now;
            Session.InvoiceId = invoiceId;
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

            return RedirectToPage("./Index");
        }

        private bool SessionExists(Guid id)
        {
          return (_context.Sessions?.Any(e => e.SessionId == id)).GetValueOrDefault();
        }
    }
}
