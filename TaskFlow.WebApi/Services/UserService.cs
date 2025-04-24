using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;
using TaskFlow.Storage.Repositories;

namespace TaskFlow.WebApi.Services;

public class UserService(IUserRepository repo, IConfiguration config) : IUserService
{
    private readonly IUserRepository _repo = repo;
    private readonly IConfiguration _config = config;

    public User Register(string name, string email, string password)
    {
        if (_repo.GetByEmail(email) is not null)
            throw new Exception("Email already in use.");

        var hash = BCrypt.Net.BCrypt.HashPassword(password);

        var user = new User
        {
            Name = name,
            Email = email,
            PasswordHash = hash,
            Role = UserRole.User
        };

        _repo.Add(user);
        _repo.SaveChanges();

        return user;
    }

    public string Login(string email, string password)
    {
        var user = _repo.GetByEmail(email) ?? throw new Exception("Invalid credentials.");

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new Exception("Invalid credentials.");

        return GenerateJwt(user);
    }

    private string GenerateJwt(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
