

using MediatR;
using ProductService.core.application.abstraction;
using ProductService.core.domain;

namespace ProductService.core.application.command;
public class CreateUserCommand : IRequest<int>
{
  public  User user { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    IUserRepository _userRepository;
    public CreateUserCommandHandler(IUserRepository userRepository )
    {
        _userRepository = userRepository;
    }
    public async Task<int> Handle(CreateUserCommand createUserCommand, CancellationToken cancellationToken)
    {
       return await _userRepository.CreateAsync(createUserCommand.user);
    }
}