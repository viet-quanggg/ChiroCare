using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using DataAccess.Management;
using Repository.IRepository;

namespace ChiroCareRazorPages.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;
        private readonly IUserRepository _userRepository;
        public DetailsModel(DataAccess.Data.ChiroCareContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

      public User User { get; set; } = default!; 
      [BindProperty]
      public List<Service> Services { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetUserDetail(id);
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
    }
}
