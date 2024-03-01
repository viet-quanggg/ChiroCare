using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;

namespace ChiroCareRazorPages.Pages.ServiceCategories
{
    public class EditModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;

        public EditModel(DataAccess.Data.ChiroCareContext context)
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

            var servicecategory =  await _context.ServiceCategories.FirstOrDefaultAsync(m => m.CategoryId == id);
            if (servicecategory == null)
            {
                return NotFound();
            }
            ServiceCategory = servicecategory;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ServiceCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceCategoryExists(ServiceCategory.CategoryId))
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

        private bool ServiceCategoryExists(int id)
        {
          return (_context.ServiceCategories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
