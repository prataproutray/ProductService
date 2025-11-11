namespace ProductService.core.domain;

public interface IUserRepository 
{
    Task<List<User>> GetAllAsync();
    Task<int> CreateAsync(User user);
    Task<User> GetUserByIdAsync(int userid);
 }