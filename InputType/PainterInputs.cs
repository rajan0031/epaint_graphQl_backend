

namespace MyGraphqlApp.InputType.PainterInputs
{

    public record createPainterInput(string Name, string UserName, string Email, string Password, string PhoneNumber, string Address, string City, List<String> ServiceTypes, int ExperienceYears, double Rating, List<string> PinCodes);

}

