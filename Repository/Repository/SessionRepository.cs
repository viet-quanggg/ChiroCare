using BusinessObject;
using DataAccess.Management;
using Repository.IRepository;

namespace Repository.Repository;

public class SessionRepository : ISessionRepository
{
    public Task<List<Session>> GetSessionsToday() => SessionManagement.Instance.GetSessionsToday();

    public Task<List<Session>> GetSessionsByAppointmentDate(DateTime selectedDate) => SessionManagement.Instance.GetSessionsByAppointmentDate(selectedDate);
    public Task<List<Session>> GetSessionsBySessionDate(DateTime selectedDate) =>
        SessionManagement.Instance.GetSessionsBySessionDate(selectedDate);

    public Task<bool> CreateSession(Session session)
    {
        return SessionManagement.Instance.CreateSession(session);
    }
}