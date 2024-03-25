using BusinessObject;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Management;

public class UserManagement
{
    ChiroCareContext _context;
    private static UserManagement instance;
    private static readonly object instanceLock = new object();
    public static UserManagement Instance
    {
        get
        {
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new UserManagement();
                }
            }
            return instance;
        }
    }
    
    public  Task<User> GetUserDetail(Guid id)
    {
        _context = new ChiroCareContext();
        try
        {
            var user =  _context.Users
                .Include(u => u.UserInvoices)
                .ThenInclude(invoice => invoice.ListServices)
                .Include(u => u.UserSessions)
                .FirstOrDefaultAsync(u => u.UserId == id);
        
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=> u.UserId == id);
            if (user == null)
            {
                throw new Exception("Không tìm thấy người dùng!");
            }
            else
            {
                _context.Users.Remove(user);
               await _context.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
    }
    
    public  Task<User> GetUserDetailByPhoneNumber(string phoneNumber)
    {
        _context = new ChiroCareContext();
        try
        {
            var user =  _context.Users
                .Include(u => u.UserInvoices)
                    .ThenInclude(u => u.ListSessions)
                .Include(u => u.UserInvoices)
                    .ThenInclude(s => s.ListServices)
                .Include(u => u.UserSessions)
                .FirstOrDefaultAsync(u => u.PhoneNumber.Equals(phoneNumber));
        
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}