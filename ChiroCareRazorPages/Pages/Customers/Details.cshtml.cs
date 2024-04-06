using BusinessObject;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.IRepository;

namespace ChiroCareRazorPages.Pages.Customers;

public class DetailsModel : PageModel
{
    private readonly ChiroCareContext _context;
    private readonly IUserRepository _userRepository;

    public DetailsModel(ChiroCareContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }

    public User User { get; set; } = default!;

    [BindProperty] public List<Service> Services { get; set; }
    [BindProperty] public Session Session { get; set; }


    public async Task<IActionResult> OnGetAsync(Guid id)
    {
      
        if (id == null || _context.Users == null) return NotFound();

        var user = await _userRepository.GetUserDetail(id);
        ViewData["PatientId"] = new SelectList(_context.Users.Where(u => u.UserId == user.UserId), "UserId", "FullName");
        ViewData["TherapistId"] = new SelectList(_context.Users.Where(u => u.Role == BusinessObject.ChiroEnums.Role.NGƯỜIĐIỀUTRỊ), "UserId", "FullName");
        if (user == null)
            return NotFound();
        User = user;
        return Page();
    }
    
    public async Task<IActionResult> OnPostCreateSessionAsync(Guid id)
    {
        if (_context.Sessions == null || Session == null)
        {
            return Page();
        }
        Session.SessionDate = DateTime.Now;
        Session.InvoiceId = id;
        _context.Sessions.Add(Session);
        await _context.SaveChangesAsync();

        return Redirect("/Invoices/Details?id=" + Session.InvoiceId);
    }
}