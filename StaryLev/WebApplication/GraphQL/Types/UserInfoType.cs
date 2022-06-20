using Application.Dto;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.GraphQL.Types
{
    [ExtendObjectType("Type")]
    public class UserInfoType : ObjectType<UserInfo>
    {
        protected override void Configure(IObjectTypeDescriptor<UserInfo> descriptor)
        {
            descriptor.Field(_ => _.Id);
            descriptor.Field(_ => _.Nickname);
            descriptor.Field(_ => _.Roles);
            descriptor.Field(_ => _.Token);
        }
    }
}
