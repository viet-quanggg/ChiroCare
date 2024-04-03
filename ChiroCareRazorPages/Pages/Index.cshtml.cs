using Azure;
using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.IRepository;

namespace ChiroCareRazorPages.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ISessionRepository _sessionRepository;
    public IndexModel(ILogger<IndexModel> logger, ISessionRepository sessionRepository)
    {
        _logger = logger;
        _sessionRepository = sessionRepository;
    }
    [BindProperty]
    public IList<Session> Sessions { get; set; }
    [BindProperty]
    public DateTime StartDate { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        StartDate = DateTime.Today.Date; 
        var list = await _sessionRepository.GetSessionsToday();
        Sessions = list;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var updatedList = await _sessionRepository.GetSessionsByAppointmentDate(StartDate.Date);
        Sessions = updatedList;
        return Page();
    }
}