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
    public class DeleteModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;

        public DeleteModel(DataAccess.Data.ChiroCareContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ServiceCategory ServiceCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ServiceCategories == null)
            {
                return NotFound();
            }

            var servicecategory = await _context.ServiceCategories.FirstOrDefaultAsync(m => m.CategoryId == id);

            if (servicecategory == null)
            {
                return NotFound();
            }
            else 
            {
                ServiceCategory = servicecategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ServiceCategories == null)
            {
                return NotFound();
            }
            var servicecategory = await _context.ServiceCategories.FindAsync(id);

            if (servicecategory != null)
            {
                ServiceCategory = servicecategory;
                _context.ServiceCategories.Remove(ServiceCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
