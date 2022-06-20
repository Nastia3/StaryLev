using Application.Dto;
using Application.Options;
using Application.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDb.Models;
using MongoDb.repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        readonly IRepository<User> _userRepository;
        readonly IOptions<AuthOption> _authOptions;
        public UserService(IRepository<User> userRepository,IOptions<AuthOption> authOptions)
        {
            _userRepository = userRepository;
            _authOptions = authOptions;
        }

        private string GetMD5(string input)
        {
            using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }

        private string GenerateJWT(UserInfo dto)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("Nickname",dto.Nickname),
                new Claim("Id",dto.Id)
            };

            foreach (var role in dto.Roles)
            {
                claims.Add(new Claim("role", role.ToString()));
            }

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<UserInfo> AuthenticateAsync(AuthorizeRequest dto)
        {
            var user = await _userRepository.FindOneAsync(u => u.Nickname == dto.Nickname && u.Password == GetMD5(dto.Password));
            if(user is not null)
            {
                var userInfo = new UserInfo() { Id = user.Id, Nickname = user.Nickname, Roles = user.Roles };
                userInfo.Token = GenerateJWT(userInfo);
                return userInfo;
            }
            return null;
        }

        public async Task<UserInfo> RegistrAsync(UserRegistration dto)
        {
            var insertNum = await _userRepository.InsertOneAsync(new User(true) { Nickname = dto.Nickname, Password = GetMD5(dto.Password) });
            if (insertNum == 1)
            {
                var user = await _userRepository.FindOneAsync(u => u.Nickname == dto.Nickname && u.Password == GetMD5(dto.Password));
                if (user is not null)
                {
                    var userInfo = new UserInfo() { Id = user.Id, Nickname = user.Nickname, Roles = user.Roles };
                    userInfo.Token = GenerateJWT(userInfo);
                    return userInfo;
                }
            }
            return null;
        }
    }
}
