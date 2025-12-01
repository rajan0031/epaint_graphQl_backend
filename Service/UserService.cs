using MyGraphqlApp.Data;
using MyGraphqlApp.Interface;
using MyGraphqlApp.Model;

namespace MyGraphqlApp.Service;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<User> GetAllUsers()
    {
        return _context.Users;
    }

    public async Task<User> CreateUserAsync(string name, string email, string role)
    {
        var user = new User { Name = name, Email = email, Role = role };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateUserAsync(int id, string? name, string? email, string? role)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;

        if (!string.IsNullOrEmpty(name)) user.Name = name;
        if (!string.IsNullOrEmpty(email)) user.Email = email;
        if (!string.IsNullOrEmpty(role)) user.Role = role;

        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}