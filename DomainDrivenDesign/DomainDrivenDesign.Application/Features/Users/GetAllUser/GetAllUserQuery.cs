using DomainDrivenDesign.Domain.Users;
using MediatR;

namespace DomainDrivenDesign.Application.Features.Users.GetAllUser;

public sealed record GetAllUserQuery() : IRequest<List<User>>;

internal class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<User>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetAllAsync(cancellationToken);
    }
}
