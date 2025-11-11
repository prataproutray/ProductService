
using System.Reflection;
using ProductService.core.domain;
using ProductService.infrastructure.infrastructure.DBContext;

namespace ProductService.infrastructure.infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        ProductServiceContext _productServiceContext;
        public UserRepository(ProductServiceContext productServiceContext)
        {
            _productServiceContext = productServiceContext;
        }
        public Task<int> CreateAsync(User user)
        {
            _productServiceContext.Users.Add(user);
            return Task.FromResult(_productServiceContext.SaveChanges());

        }

        public Task<List<User>> GetAllAsync()
        {
            return Task.FromResult(_productServiceContext.Users.ToList());
        }

        public Task<User> GetUserByIdAsync(int userid)
        {
            return Task.FromResult<User>(_productServiceContext.Users.SingleOrDefault(usr=>usr.userId == userid));
        }
    }
}