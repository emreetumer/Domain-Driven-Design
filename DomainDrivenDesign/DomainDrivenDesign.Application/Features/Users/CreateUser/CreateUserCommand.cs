using MediatR;

namespace DomainDrivenDesign.Application.Features.Users.CreateUser;

public sealed record CreateUserCommand(
    string Name,
    string Password,
    string Email,
    string Country,
    string City,
    string Street,
    string PostaCode,
    string FullAddress) : IRequest;
