using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;

namespace ChiroCareRazorPages.Pages.ServiceCategories
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;

        public IndexModel(DataAccess.Data.ChiroCareContext context)
        {
            _context = context;
        }

        public IList<ServiceCategory> ServiceCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ServiceCategories != null)
            {
                ServiceCategory = await _context.ServiceCategories.ToListAsync();
            }
        }
    }
}
