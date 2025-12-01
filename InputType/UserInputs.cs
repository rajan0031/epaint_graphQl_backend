namespace MyGraphqlApp.InputType;


public record CreateUserInput(string Name, string Email, string Role);


public record UpdateUserInput(int Id, string? Name, string? Email, string? Role);