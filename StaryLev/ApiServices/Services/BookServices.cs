using System.Collections.Generic;
using System.Threading.Tasks;
using ApiServices.Builders;
using ApiServices.Models;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace ApiServices.Services
{
    public class BookApiServices
    {
        private const string Authorization = "Authorization";
        private const string Token = "Bearer ";
        private readonly GraphQLHttpClient graphQLClient;

        public BookApiServices(string endPoint)
        {
            graphQLClient = new GraphQLHttpClient(endPoint, new NewtonsoftJsonSerializer());
        }

        public async Task<List<Book>> GetAllBookAsync(int elementsSkip, int elementsTake)
        {
            var request = RequestBuilder.BuildRequest(RequstType.GetBooks, @params: new object[] { elementsSkip, elementsTake });

            return (await graphQLClient.SendQueryAsync<RensponseBookMessage>(request)).Data.BookList.Books;
        }

        public async Task<Book> LikeBookAsync(string bookId, string jwtToken)
        {
            var request = RequestBuilder.BuildRequest(RequstType.Like, @params: new[] { bookId });

            graphQLClient.HttpClient.DefaultRequestHeaders.Add(Authorization, Token + jwtToken);

            return (await graphQLClient.SendQueryAsync<RensponseLikeMessage>(request)).Data.Book;
        }

        public async Task<Book> DislikeBookAsync(string bookId, string jwtToken)
        {
            var request = RequestBuilder.BuildRequest(RequstType.Dislike, @params: new[] { bookId });

            graphQLClient.HttpClient.DefaultRequestHeaders.Add(Authorization, Token + jwtToken);

            return (await graphQLClient.SendQueryAsync<RensponseDislikeMessage>(request)).Data.Book;
        }
    }
}