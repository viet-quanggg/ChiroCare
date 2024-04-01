using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Data;

namespace ChiroCareRazorPages.Pages
{
    public class LoginModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;
        private readonly IConfiguration _configuration;
        public LoginModel(DataAccess.Data.ChiroCareContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            HttpContext.Session.Clear();
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            HttpContext.Session.Clear();
            var user = _configuration.GetSection("UserCredentials:Username").Value;
            var pass = _configuration.GetSection("UserCredentials:Password").Value;

            if (User.FullName == user && User.PhoneNumber == pass)
            {
                HttpContext.Session.SetInt32("Role", 0);
                return RedirectToPage("/Index");
            }
            TempData["Message"] = "Thông tin đăng nhập sai ! ";

            return Page();


        }
    }
}
