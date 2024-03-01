namespace Repository.IRepository;

public interface IUserRepository
{
    Task<User> GetUserDetail(Guid userId);

    Task<User> GetUserDetailByPhoneNumber(string phoneNumber);
}