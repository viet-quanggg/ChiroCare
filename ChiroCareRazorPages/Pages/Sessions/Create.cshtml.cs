using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using BusinessObject.ChiroEnums;
using DataAccess.Data;

namespace ChiroCareRazorPages.Pages.Sessions
{
    public class CreateModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;

        public CreateModel(DataAccess.Data.ChiroCareContext context)
        {
            _context = context;
        }

        // public Guid invoiceId { get; set; }

        public IActionResult OnGet()
        { 
            // ViewData["InvoiceId"] = id;
            // invoiceId = id;
        ViewData["PatientId"] = new SelectList(_context.Users, "UserId", "FullName");
        ViewData["TherapistId"] = new SelectList(_context.Users.Where(u => u.Role == Role.NGƯỜIĐIỀUTRỊ), "UserId", "FullName");
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
            Session.SessionDate = DateTime.Now;
            Session.InvoiceId = id;
            _context.Sessions.Add(Session);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
