using Application.Dto;
using Application.Services.Interfaces;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using System.Threading.Tasks;

namespace WebApplication.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class UserMutation
    {
        [UseProjection]
        public async Task<UserInfo> Register([Service] IUserService userService, UserRegistration dto) => 
            await userService.RegistrAsync(dto);
        
    }
}
