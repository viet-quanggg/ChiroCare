using BusinessObject;
using DataAccess.Management;
using Repository.IRepository;

namespace Repository.Repository;

public class SessionRepository : ISessionRepository
{
    public Task<List<Session>> GetSessionsToday() => SessionManagement.Instance.GetSessionsToday();
}