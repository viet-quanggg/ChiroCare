using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess.Data;
using Repository.IRepository;

namespace ChiroCareRazorPages.Pages.Sessions
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;
        private readonly ISessionRepository _sessionRepository;
        public IndexModel(DataAccess.Data.ChiroCareContext context, ISessionRepository sessionRepository)
        {
            _context = context;
            _sessionRepository = sessionRepository;
        }

        public IList<Session> Session { get;set; } = default!;

        [BindProperty]
        public DateTime StartDate { get; set; }
        public async Task OnGetAsync()
        {
            StartDate = DateTime.Today;
            if (_context.Sessions != null)
            {
                Session = await _context.Sessions
                    .Include(s => s.Invoice)
                .Include(s => s.Patient)
                    .Include(s => s.Invoice)
                .Include(s => s.Therapist).ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var updatedList = await _sessionRepository.GetSessionsBySessionDate(StartDate.Date);
            Session = updatedList;
            return Page();
        }
    }
}
