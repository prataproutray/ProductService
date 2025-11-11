using MediatR;
using ProductService.core.application.abstraction;
using ProductService.core.domain;

namespace ProductService.core.application.query;

public class GetUserById : IRequest<User>
{
   public int userid { get; set; }
}

public class GetAllUser() : IRequest<List<User>>{}

public class GetUserByIdCommandHandler: IRequestHandler<GetUserById, User>
{
    IUserRepository _userRepository;
    public GetUserByIdCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public Task<User> Handle(GetUserById query, CancellationToken cancellationToken)
    {
        return _userRepository.GetUserByIdAsync(query.userid);
    }
}
public class GetAllUserCommandHandler : IRequestHandler<GetAllUser, List<User>>
{
    IUserRepository _userRepostory;
    public GetAllUserCommandHandler(IUserRepository userRepository)
    {
        _userRepostory = userRepository;
    }
    public async Task<List<User>> Handle(GetAllUser query, CancellationToken cancellationToken)
    {
        return await _userRepostory.GetAllAsync();
    }
}