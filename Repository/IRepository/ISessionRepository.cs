using BusinessObject;

namespace Repository.IRepository;

public interface ISessionRepository
{
    Task<List<Session>> GetSessionsToday();
}