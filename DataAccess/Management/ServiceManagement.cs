using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Management;

public class ServiceManagement
{
    ChiroCareContext _context;
    private static ServiceManagement instance;
    private static readonly object instanceLock = new object();
    private ServiceManagement()
    {
    }
    public static ServiceManagement Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new ServiceManagement();
                }
            }
            return instance;
        }
    }

    public ServiceManagement(ChiroCareContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Service>> getServices()
    {
        try
        {
            var services = await _context.Services.Include(s => s.ServiceCategory).ToListAsync();
            return services;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}