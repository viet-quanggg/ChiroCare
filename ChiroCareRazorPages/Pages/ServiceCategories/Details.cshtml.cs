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
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;

        public DetailsModel(DataAccess.Data.ChiroCareContext context)
        {
            _context = context;
        }

      public ServiceCategory ServiceCategory { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ServiceCategories == null)
            {
                return NotFound();
            }

            var servicecategory = await _context.ServiceCategories
                .Include(c => c.Services)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
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
    }
}
