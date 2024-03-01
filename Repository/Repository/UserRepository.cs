using DataAccess.Management;
using Repository.IRepository;

namespace Repository.Repository;

public class UserRepository : IUserRepository
{
    public Task<User> GetUserDetail(Guid userId) => UserManagement.Instance.GetUserDetail(userId);

    public Task<User> GetUserDetailByPhoneNumber(string phoneNumber) =>
        UserManagement.Instance.GetUserDetailByPhoneNumber(phoneNumber);

}