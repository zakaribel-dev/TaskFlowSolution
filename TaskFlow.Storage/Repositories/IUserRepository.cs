using TaskFlow.Domain.Entities;

namespace TaskFlow.Storage.Repositories;

public interface IUserRepository : IRepository<User>
{
    User? GetByEmail(string email);
}
