using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Storage.Repositories;

public class UserRepository(TaskFlowDbContext context) : IUserRepository
{
    private readonly TaskFlowDbContext _context = context;

    public IEnumerable<User> GetAll() =>
        _context.Users.Include(u => u.Projects).ToList();

    public User? GetById(int id) =>
        _context.Users.Include(u => u.Projects).FirstOrDefault(u => u.Id == id);

    public User? GetByEmail(string email) =>
        _context.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

    public void Add(User entity) => _context.Users.Add(entity);

    public void Update(User entity) => _context.Users.Update(entity);

    public void Delete(int id)
    {
        var user = _context.Users.Find(id);
        if (user is not null)
            _context.Users.Remove(user);
    }

    public void SaveChanges() => _context.SaveChanges();
}
