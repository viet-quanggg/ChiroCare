using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using DataAccess.Data;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;

namespace ChiroCareRazorPages.Pages.Invoices
{
    public class CreateModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        public CreateModel(DataAccess.Data.ChiroCareContext context, IUserRepository userRepository, IInvoiceRepository invoiceRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _invoiceRepository = invoiceRepository;
        }
        
        public List<Service> Services { get; set; }
        [BindProperty]
        public List<Guid> serviceIds { get; set; }
        public async void OnGet()
        {
            Total = 100000;
            ViewData["Services"] = new SelectList(_context.Services, "ServiceId", "ServiceName");
        }
        

        [BindProperty]
        public Invoice Invoice { get; set; } = default!;
        [BindProperty] public User User { get; set; } = default!;
        
        public async Task<IActionResult> OnPostCreate(string[] servicesIds)
        {
            string phoneNum = HttpContext.Session.GetString("phone");
            User.PhoneNumber = phoneNum;
            User invoiceUser = await _userRepository.GetUserDetailByPhoneNumber(phoneNum);
            if (invoiceUser != null)
            {
                List<Service> invoiceService = new List<Service>();
                foreach (var ser in serviceIds)
                {
                    Service service = await _context.Services.FirstOrDefaultAsync(s => s.ServiceId == ser);
                    invoiceService.Add(service);
                    Invoice.InvoiceTotal += service.ServicePrice;
                }

                Invoice.ListServices = invoiceService;
                // Invoice.InvoiceStatus = 0;
                Invoice.CreateDate = DateTime.Now;
                _context.Invoices.Add(Invoice); 
                
                Invoice.Patient = invoiceUser;
            
                invoiceUser.UserInvoices.Add(Invoice);
            
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");

            }else if (invoiceUser == null)
            {
                User.Role = 0;
                _context.Users.Add(User);
                await _context.SaveChangesAsync();
                User createdUser = await _userRepository.GetUserDetailByPhoneNumber(phoneNum);
                
                List<Service> invoiceService = new List<Service>();
                foreach (var ser in serviceIds)
                {
                    Service service = await _context.Services.FirstOrDefaultAsync(s => s.ServiceId == ser);
                    invoiceService.Add(service);
                    Invoice.InvoiceTotal += service.ServicePrice;
                }

                Invoice.ListServices = invoiceService;
                // Invoice.InvoiceStatus = 0;
                Invoice.CreateDate = DateTime.Now;
                _context.Invoices.Add(Invoice);
            
                Invoice.Patient = createdUser;
            
                createdUser.UserInvoices.Add(Invoice);
                _context.Entry(User).State = EntityState.Detached;
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();

        }
        [BindProperty]
        public string PhoneNumber { get; set; }
        
        public async Task<IActionResult> OnPostIndex(string phone)
        {
            if (!String.IsNullOrEmpty(phone))
                HttpContext.Session.SetString("phone", phone);
                User = await _userRepository.GetUserDetailByPhoneNumber(phone);
                PhoneNumber = phone;
                if (User == null)
                {
                    // If User is null, populate the input field with the provided phone number
                    PhoneNumber = phone;
                }
                OnGet();
                return Page();
            
        }

        [BindProperty]
        public decimal Total { get; set; }
        public async Task<IActionResult> OnPostCalculate(string[] servicesIds)
        {
            if (servicesIds != null)
            {
                foreach (var ser in serviceIds)
                {
                    Service service = await _context.Services.FirstOrDefaultAsync(s => s.ServiceId == ser);
                    if(service!= null)
                    Total += service.ServicePrice;
                }
                OnGet();
                return Page();
            }
            OnGet();
            return Page();
        }
        
    }
}
