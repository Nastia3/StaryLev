using ApiServices.Builders;
using ApiServices.Models;
using Application.Dto;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System.Threading.Tasks;

namespace ApiServices.Services
{
    public class UserServices
    {
        private readonly GraphQLHttpClient graphQLClient;
        public UserServices(string endPoint)
        {
            graphQLClient = new GraphQLHttpClient(endPoint, new NewtonsoftJsonSerializer());
        }

        public async Task<UserInfo> Login(string nickname, string password)
        {
            var request = RequestBuilder.BuildRequest(RequstType.Login, @params: new[] { nickname, password });

            return (await graphQLClient.SendQueryAsync<RensponseLoginMessage>(request)).Data.UserInfo;
        }

        public async Task<UserInfo> Register(string nickname, string password)
        {
            var request = RequestBuilder.BuildRequest(RequstType.Registration, @params: new[] { nickname, password });

            return (await graphQLClient.SendQueryAsync<RensponseRegisterMessage>(request)).Data.UserInfo;
        }
    }
}
