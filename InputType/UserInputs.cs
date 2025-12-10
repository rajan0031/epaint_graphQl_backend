namespace MyGraphqlApp.InputType;


public record CreateUserInput(string Name, string UserName, string Email, string PhoneNumber, string Password, int Role);


public record UpdateUserInput(int Id, string UserName, string? Name, string? Email, string PhoneNumber, int Role);



public record loginInput(string email, string password);