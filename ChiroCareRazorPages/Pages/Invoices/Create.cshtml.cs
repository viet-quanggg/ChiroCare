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
        private readonly IServiceRepository _serviceRepository;
        public CreateModel(DataAccess.Data.ChiroCareContext context, IUserRepository userRepository, IInvoiceRepository invoiceRepository, IServiceRepository serviceRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _invoiceRepository = invoiceRepository;
            _serviceRepository = serviceRepository;
        }
        [BindProperty]
        public String phone { get; set; }
        public List<Service> Services { get; set; }
        [BindProperty]
        public List<Guid> serviceIds { get; set; }
        public async Task OnGet(string id)
        {
            if (id == null)
            {
                var services = await _context.Services.ToListAsync();
                ViewData["Services"] = new SelectList(services.Select(s => new {
                    ServiceId = s.ServiceId,
                    DisplayName = $"{s.ServiceName} - {s.ServicePrice.ToString("N0")} VND"
                }), "ServiceId", "DisplayName");
            }
            else
            {
                phone = id;
                await OnPostIndex(phone);
                var services = await _context.Services.ToListAsync();
                ViewData["Services"] = new SelectList(services.Select(s => new {
                    ServiceId = s.ServiceId,
                    DisplayName = $"{s.ServiceName} - {s.ServicePrice.ToString("N0")} VND"
                }), "ServiceId", "DisplayName");
            }
          
        }


        [BindProperty]
        public Invoice Invoice { get; set; } = default!;
        [BindProperty] public User User { get; set; } = default!;
        
         public async Task<IActionResult> OnPostCreate(string[] servicesIds)
        {
            string phoneNum = HttpContext.Session.GetString("phone");
            User.PhoneNumber = phoneNum;
            var invoiceUser = await _userRepository.GetUserDetailByPhoneNumber(phoneNum);
            if (invoiceUser != null)
            {
                
                
                Invoice.CreateDate = DateTime.Now;
                _context.Invoices.Add(Invoice);
                foreach (var ser in serviceIds)
                {
                    Service service = await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.ServiceId == ser);
                    Invoice.InvoiceServices.Add(new InvoiceService
                    {
                        InvoiceId = Invoice.InvoiceId,
                        ServiceId = service.ServiceId
                    });
                    Invoice.InvoiceTotal += Invoice.Quantity * service.ServicePrice;
                }

                if (Invoice.DiscountPercent != null)
                {
                    decimal discountFraction = (decimal)Invoice.DiscountPercent / 100; // Convert percent to fraction
                    decimal afterDiscount = Invoice.InvoiceTotal * (1 - discountFraction); // Calculate discounted amount
                    Invoice.InvoiceTotal = afterDiscount; // Apply discount
                }

            
                Invoice.Patient = invoiceUser;
            
                invoiceUser.UserInvoices.Add(Invoice);
                _context.Entry(User).State = EntityState.Detached;
                await _context.SaveChangesAsync();
                return Redirect("/Customers/Details?id=" + invoiceUser.UserId);

            }
            if (invoiceUser == null)
                {
                    User.Role = 0;
                    User.PhoneNumber = phoneNum;
                    _context.Users.Add(User);
                    await _context.SaveChangesAsync();
                    User createdUser = await _userRepository.GetUserDetailByPhoneNumber(phoneNum);
                
                    Invoice.CreateDate = DateTime.Now;
                    _context.Invoices.Add(Invoice);
                    foreach (var ser in serviceIds)
                    {
                        Service service = await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.ServiceId == ser);
                        Invoice.InvoiceServices.Add(new InvoiceService
                        {
                            InvoiceId = Invoice.InvoiceId,
                            ServiceId = service.ServiceId
                        });
                        Invoice.InvoiceTotal += Invoice.Quantity * service.ServicePrice;
                    }
                    
                    if (Invoice.DiscountPercent != null)
                    {
                        decimal? afterDiscount = Invoice.InvoiceTotal * ((100 -Invoice.DiscountPercent)/100);
                        Invoice.InvoiceTotal = (decimal)afterDiscount;
                    }
            
                    Invoice.Patient = createdUser;
            
                    createdUser.UserInvoices.Add(Invoice);
                    _context.Entry(User).State = EntityState.Detached;
                    await _context.SaveChangesAsync();
                    return Redirect("/Customers/Details?id=" + createdUser.UserId);
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
                var services = await _context.Services.ToListAsync();
                ViewData["Services"] = new SelectList(services.Select(s => new {
                    ServiceId = s.ServiceId,
                    DisplayName = $"{s.ServiceName} - {s.ServicePrice.ToString("N0")} VND"
                }), "ServiceId", "DisplayName");
                return Page();
            
        }
        
        
    }
}
