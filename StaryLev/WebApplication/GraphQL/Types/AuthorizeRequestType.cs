using Application.Dto;
using HotChocolate.Types;

namespace WebApplication.GraphQL.Types
{
    [ExtendObjectType("Type")]
    public class AuthorizeRequestType : ObjectType<AuthorizeRequest>
    {
        protected override void Configure(IObjectTypeDescriptor<AuthorizeRequest> descriptor)
        {
            descriptor.Field(_ => _.Nickname);
            descriptor.Field(_ => _.Password);
        }
    }
}
