using DomainDrivenDesign.Domain.Users;
using DomainDrivenDesign.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUserAsync(string name, string password, string email, string country, string city, string street, string postaCode, string fullAddress, CancellationToken cancellationToken = default)
    {
        User user = User.CreateUser(
            name,
            email,
            password,
            country,
            city,
            street,
            postaCode,
            fullAddress);

        await _context.Users.AddAsync(user);

        return user;
    }

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users.ToListAsync(cancellationToken);
    }
}
