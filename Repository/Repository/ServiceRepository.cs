using DataAccess.Management;
using Repository.IRepository;

namespace Repository.Repository;

public class ServiceRepository : IServiceRepository
{
     public Task<ICollection<Service>> GetAllServices() => ServiceManagement.Instance.getServices();
     public Task<Service> GetService(Guid serGuid) => ServiceManagement.Instance.GetService(serGuid);

}