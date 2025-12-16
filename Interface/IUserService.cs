using MyGraphqlApp.Model;
using MyGraphqlApp.dtos;
// using  MyGraphqlApp.dtos.UserDto;
namespace MyGraphqlApp.Interface;

public interface IUserService
{
    List<UserDto.GetAllUserDto> GetAllUsers();
    Task<UserDto.GetAllUserDto> CreateUserAsync(string name, string userName, string email, string Password, string PhoneNumber, int role);
    Task<User?> UpdateUserAsync(int id, string? name, string userName, string? email, string PhoneNumber, int role);
    Task<bool> DeleteUserAsync(int id);

    public Task<User> getUserById(int id);

    public Task<UserDto.LoginResponse> loginUser(UserDto.loginDto loginDto);

    public string changePassword(UserDto.ChangePasswordDto changePasswordDto);
}