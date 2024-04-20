using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using BusinessObject.ChiroEnums;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.IRepository;

namespace ChiroCareRazorPages.Pages.Invoices
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.Data.ChiroCareContext _context;
        public readonly IInvoiceRepository _invoiceRepository;
        public DetailsModel(DataAccess.Data.ChiroCareContext context, IInvoiceRepository invoiceRepository)
        {
            _context = context;
            _invoiceRepository = invoiceRepository;
        }

      public Invoice Invoice { get; set; } 
      public List<Service> invoiceServices { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _invoiceRepository == null)
            {
                return NotFound();
            }

            var invoice = await _invoiceRepository.GetInvoiceDetail((Guid)id);
          
            if (invoice == null)
            {
                return NotFound();
            }
            else 
            {
                Invoice = invoice;
                ViewData["PatientId"] = new SelectList(_context.Users.Where(u => u.UserId == invoice.PatientId), "UserId", "FullName");
                ViewData["TherapistId"] = new SelectList(_context.Users.Where(u => u.Role == BusinessObject.ChiroEnums.Role.NGƯỜIĐIỀUTRỊ), "UserId", "FullName");
            }

            return Page();
        }
        
        [BindProperty] public Session Session { get; set; }
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
        
        [BindProperty] public DateTime? NextAppointmentDateTime { get; set; }
        [BindProperty] public string? NextAppointmentTreatment { get; set; }
        
        public async Task<IActionResult> OnPostUpdateAsync()
            {
                var invoice = await _invoiceRepository.GetInvoiceDetail(Session.InvoiceId);
                Session.SessionDate = DateTime.Now;
                _context.Attach(Session).State = EntityState.Modified;
                if (NextAppointmentDateTime.HasValue && invoice.ListSessions.Count < invoice.Quantity)
                {
                    Session NextSession = new Session();
                    NextSession.SessionId = new Guid();
                    NextSession.SessionDate = DateTime.Now;
                    NextSession.SessionAppointment = NextAppointmentDateTime;
                    NextSession.SessionTreatment = NextAppointmentTreatment;
                    NextSession.InvoiceId = Session.InvoiceId;
                    NextSession.PatientId = Session.PatientId;
                    NextSession.TherapistId = Session.TherapistId;
                    NextSession.Status = SessionStatus.ĐÃĐẶTLỊCH;
                    await _context.Sessions.AddAsync(NextSession);
                    invoice.ListSessions.Add(NextSession);
        
                }
        
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

                return Redirect("/Invoices/Details?id=" + Session.InvoiceId);
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
        
                return Redirect("/Invoices/Details?id=" + Session.InvoiceId);
            }
            
            [BindProperty] public Invoice UpdateInvoice { get; set; }
            public async Task<IActionResult> OnPostUpdateInvoiceAsync(Guid id)
            {
                 var invoice = await _invoiceRepository.GetInvoiceDetail(id);
                 invoice.InvoiceDescription = UpdateInvoice.InvoiceDescription;
                 invoice.InvoiceNote = UpdateInvoice.InvoiceNote;
                 invoice.InvoiceDiagnose = UpdateInvoice.InvoiceDiagnose;
                 invoice.InvoiceMethod = UpdateInvoice.InvoiceMethod;
                 invoice.InvoiceStatus = UpdateInvoice.InvoiceStatus;
                
                _context.Attach(invoice).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(Invoice.InvoiceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Redirect("/Invoices/Details?id=" + invoice.InvoiceId);
            }

            private bool InvoiceExists(Guid id)
            {
                return (_context.Invoices?.Any(e => e.InvoiceId == id)).GetValueOrDefault();
            }
        
    }
}
