using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess.Data;

namespace ChiroCareRazorPages.Pages.Sessions
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;

        public DetailsModel(DataAccess.Data.ChiroCareContext context)
        {
            _context = context;
        }

      public Session Session { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Patient)
                .Include(s => s.Therapist)
                .Include(s => s.Invoice)
                .FirstOrDefaultAsync(m => m.SessionId == id);
            if (session == null)
            {
                return NotFound();
            }
            else 
            {
                Session = session;
            }
            return Page();
        }
    }
}
