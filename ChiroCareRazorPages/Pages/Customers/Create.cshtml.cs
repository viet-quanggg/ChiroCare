using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Data;
using Repository.IRepository;

namespace ChiroCareRazorPages.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;
        private readonly IUserRepository _userRepository;
        public CreateModel(DataAccess.Data.ChiroCareContext context, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // if (!ModelState.IsValid || _context.Users == null || User == null)
            //   {
            //       return Page();
            //   }
            // User.Role = 0;
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            User createdUser  = await _userRepository.GetUserDetailByPhoneNumber(User.PhoneNumber);
            return Redirect("/Customers/Details?id=" + createdUser.UserId);

        }
    }
}