using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Data;

namespace ChiroCareRazorPages.Pages.ServiceCategories
{
    public class CreateModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;

        public CreateModel(DataAccess.Data.ChiroCareContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ServiceCategory ServiceCategory { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.ServiceCategories == null || ServiceCategory == null)
            {
                return Page();
            }

            _context.ServiceCategories.Add(ServiceCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
