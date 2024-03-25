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
    public class IndexModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;

        public IndexModel(DataAccess.Data.ChiroCareContext context)
        {
            _context = context;
        }

        public IList<Session> Session { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Sessions != null)
            {
                Session = await _context.Sessions
                .Include(s => s.Patient)
                .Include(s => s.Therapist).ToListAsync();
            }
        }
    }
}
