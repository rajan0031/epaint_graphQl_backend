using MyGraphqlApp.Model;

namespace MyGraphqlApp.Type;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field(u => u.Id).Type<NonNullType<IdType>>();
        descriptor.Field(u => u.Name).Description("The full name of the user.");
      
    }
}