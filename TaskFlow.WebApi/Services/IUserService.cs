using TaskFlow.Domain.Entities;

namespace TaskFlow.WebApi.Services;

public interface IUserService
{
    User Register(string? name, string? email, string? password);
    string? Login(string? email, string? password);
}
