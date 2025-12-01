using MyGraphqlApp.Model;

namespace MyGraphqlApp.Interface;

public interface IUserService
{
    IQueryable<User> GetAllUsers();
    Task<User> CreateUserAsync(string name, string email, string role);
    Task<User?> UpdateUserAsync(int id, string? name, string? email, string? role);
    Task<bool> DeleteUserAsync(int id);
}