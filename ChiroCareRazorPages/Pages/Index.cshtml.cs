using BusinessObject;
using BusinessObject.ChiroEnums;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;

namespace ChiroCareRazorPages.Pages;

public class IndexModel : PageModel
{
    private readonly ChiroCareContext _context;
    private readonly ILogger<IndexModel> _logger;
    private readonly ISessionRepository _sessionRepository;


    public IndexModel(ILogger<IndexModel> logger, ISessionRepository sessionRepository, ChiroCareContext context)
    {
        _logger = logger;
        _sessionRepository = sessionRepository;
        _context = context;
    }

    [BindProperty] public IList<Session> Sessions { get; set; }

    [BindProperty] public DateTime StartDate { get; set; }


    [BindProperty] public Session Session { get; set; } = default!;


    public async Task<IActionResult> OnGetAsync()
    {
        StartDate = DateTime.Today.Date;
        var list = await _sessionRepository.GetSessionsToday();
        Sessions = list;
        ViewData["TherapistId"] =
            new SelectList(_context.Users.Where(u => u.Role == Role.NGƯỜIĐIỀUTRỊ), "UserId", "FullName");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var updatedList = await _sessionRepository.GetSessionsByAppointmentDate(StartDate.Date);
        Sessions = updatedList;
        ViewData["TherapistId"] =
            new SelectList(_context.Users.Where(u => u.Role == Role.NGƯỜIĐIỀUTRỊ), "UserId", "FullName");
        return Page();
    }


    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostUpdateAsync()
    {
        Session.SessionDate = DateTime.Now;
        _context.Attach(Session).State = EntityState.Modified;


        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SessionExists(Session.SessionId))
                return NotFound();
            throw;
        }

        return Redirect("/Home");
    }

    private bool SessionExists(Guid id)
    {
        return (_context.Sessions?.Any(e => e.SessionId == id)).GetValueOrDefault();
    }

    public async Task<IActionResult> OnPostDeleteAsync()
    {
        if (Session.SessionId == null || _context.Sessions == null) return NotFound();
        var session = await _context.Sessions.FindAsync(Session.SessionId);

        if (session != null)
        {
            Session = session;
            _context.Sessions.Remove(Session);
            await _context.SaveChangesAsync();
        }

        return Redirect("/Home");
    }
}