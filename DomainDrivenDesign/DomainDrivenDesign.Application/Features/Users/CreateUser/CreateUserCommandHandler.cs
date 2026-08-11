using DomainDrivenDesign.Domain.Abstractions;
using DomainDrivenDesign.Domain.Users;
using MediatR;

namespace DomainDrivenDesign.Application.Features.Users.CreateUser;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        //kontrol işlemleri

        await _userRepository.CreateUserAsync(
            request.Name,
            request.Password,
            request.Email,
            request.Country,
            request.City,
            request.Street,
            request.PostaCode,
            request.FullAddress);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
