using Application.Dto;
using HotChocolate.Types;

namespace WebApplication.GraphQL.Types
{
    [ExtendObjectType("Type")]
    public class UserRegistrationType : ObjectType<UserRegistration>
    {
        protected override void Configure(IObjectTypeDescriptor<UserRegistration> descriptor)
        {
            descriptor.Field(_ => _.Nickname);
            descriptor.Field(_ => _.Password);
        }
    }
}
