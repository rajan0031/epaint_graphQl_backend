using MyGraphqlApp.Data;
using MyGraphqlApp.Interface;
using MyGraphqlApp.Model;
using BCrypt.Net;

namespace MyGraphqlApp.Service;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public List<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    public async Task<User> CreateUserAsync(string name, string userName, string email, string PhoneNumber, string Password, int role)
    {
        var user = new User { Name = name, UserName = userName, Email = email, PhoneNumber = PhoneNumber, Password = Password, Role = role };

       
        user.Password = BCrypt.Net.BCrypt.HashPassword(Password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateUserAsync(
     int id,
     string? name,
     string? userName,
     string? email,
     string? phoneNumber,
     int role
 )
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;

        if (!string.IsNullOrEmpty(name))
            user.Name = name;

        if (!string.IsNullOrEmpty(userName))
            user.UserName = userName;

        if (!string.IsNullOrEmpty(email))
            user.Email = email;

        if (!string.IsNullOrEmpty(phoneNumber))
            user.PhoneNumber = phoneNumber;

        if (role != null)
            user.Role = role;

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

    public async Task<User> getUserById(int id)
    {


        var result = await _context.Users.FindAsync(id);


        if (result == null)
        {
            throw new Exception("User details is not found for this id");
        }
        return result!;
    }


}