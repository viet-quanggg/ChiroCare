using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace ChiroCareRazorPages.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;

        public DeleteModel(DataAccess.Data.ChiroCareContext context)
        {
            _context = context;
        }

        [BindProperty]
      public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);

            if (user == null)
            {
                return NotFound();
            }
            else 
            {
                User = user;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                try
                {
                    User = user;
                    _context.Users.Remove(User);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An error occurred while deleting the user. Please try again later or contact support.";
                }
              
            }

            return RedirectToPage("./Index");
        }
    }
}
