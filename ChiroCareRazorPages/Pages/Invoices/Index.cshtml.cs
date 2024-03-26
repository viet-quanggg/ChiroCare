using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess.Data;

namespace ChiroCareRazorPages.Pages.Invoices
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;

        public IndexModel(DataAccess.Data.ChiroCareContext context)
        {
            _context = context;
        }

        public IList<Invoice> Invoice { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Invoices != null)
            {
                Invoice = await _context.Invoices
                    .Include(i => i.Patient)
                    .Include(i => i.ListSessions)
                .ToListAsync();
            }
        }
    }
}
