using MyGraphqlApp.Model;
using MyGraphqlApp.dtos;
namespace MyGraphqlApp.Interface;

public interface IUserService
{
    List<User> GetAllUsers();
    Task<User> CreateUserAsync(string name, string userName, string email,string Password,string PhoneNumber, int role);
    Task<User?> UpdateUserAsync(int id, string? name,string userName, string? email, string PhoneNumber, int role);
    Task<bool> DeleteUserAsync(int id);

    public Task<User> getUserById(int id);

    public Task<dynamic> loginUser(UserDto.loginDto loginDto);
}