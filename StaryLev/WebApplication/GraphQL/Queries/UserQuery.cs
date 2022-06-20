using Application.Dto;
using Application.Services.Interfaces;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class UserQuery
    {
        [UseProjection]
        public async Task<UserInfo> LogIn([Service] IUserService userService, AuthorizeRequest dto) =>
            await userService.AuthenticateAsync(dto);

    }
}
