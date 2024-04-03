using BusinessObject;

namespace Repository.IRepository;

public interface ISessionRepository
{
    Task<List<Session>> GetSessionsToday();
    Task<List<Session>> GetSessionsByAppointmentDate(DateTime selectedDate);
    Task<List<Session>> GetSessionsBySessionDate(DateTime selectedDate);
}