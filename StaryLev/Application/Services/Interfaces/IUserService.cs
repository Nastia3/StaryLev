using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserInfo> RegistrAsync(UserRegistration dto);
        Task<UserInfo> AuthenticateAsync(AuthorizeRequest dto);
    }
}
