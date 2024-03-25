namespace Repository.IRepository;

public interface IServiceRepository
{
    Task<ICollection<Service>> GetAllServices();

    Task<Service> GetService(Guid serGuid);
}