using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using System.Data;

namespace ChiroCareRazorPages.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;

        public IndexModel(DataAccess.Data.ChiroCareContext context)
        {
            _context = context;
        }

        public IList<User> User { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                User = await _context.Users
                    .OrderByDescending(a => a.UserId)
                    .ToListAsync();
            }
        }
    }
}