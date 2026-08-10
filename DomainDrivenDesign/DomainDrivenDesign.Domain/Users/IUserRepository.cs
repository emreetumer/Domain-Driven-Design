namespace DomainDrivenDesign.Domain.Users;

public interface IUserRepository
{
    Task CreateUserAsync(string name, string password, string email, string country, string city, string street, string postaCode, string fullAddress, CancellationToken cancellationToken = default);
    Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);
}
