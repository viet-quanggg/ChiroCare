using BusinessObject;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Management;

public class SessionManagement
{
    ChiroCareContext _context;
    private static SessionManagement instance;
    private static readonly object instanceLock = new object();
    private SessionManagement()
    {
    }
    public static SessionManagement Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new SessionManagement();
                }
            }
            return instance;
        }
    }

    public async Task<List<Session>> GetSessionsToday()
    {
        try
        {
            DateTime today = DateTime.Today;
            _context = new ChiroCareContext();
            var sessionList = await _context.Sessions
                .Include(s => s.Invoice)
                .ThenInclude(i => i.Patient)
                .ThenInclude(i => i.UserSessions)
                .Where(s =>s.SessionAppointment.Value.Date == today.Date)
                .ToListAsync();
            return sessionList;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        } 
    }
    
    public async Task<List<Session>> GetSessionsByAppointmentDate(DateTime selectedDate)
    {
        try
        {
            DateTime today = DateTime.Today;
            _context = new ChiroCareContext();
            var sessionList = await _context.Sessions
                .Include(s => s.Invoice)
                .ThenInclude(i => i.Patient)
                .ThenInclude(i => i.UserSessions)
                .Where(s =>s.SessionAppointment.Value.Date == selectedDate.Date)
                .ToListAsync();
            return sessionList;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        } 
    }
    
    public async Task<List<Session>> GetSessionsBySessionDate(DateTime selectedDate)
    {
        try
        {
            DateTime today = DateTime.Today;
            _context = new ChiroCareContext();
            var sessionList = await _context.Sessions
                .Include(s => s.Invoice)
                .ThenInclude(i => i.Patient)
                .Where(s =>s.SessionDate.Date == selectedDate.Date)
                .ToListAsync();
            return sessionList;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        } 
    }

}