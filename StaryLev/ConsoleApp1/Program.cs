using ApiServices.Services;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {

            //var password = "password";
            //var nickname = "NastiaRoma";

            //var graphQLClient = new GraphQLHttpClient("https://localhost:44306/graphql/", new NewtonsoftJsonSerializer());

            //var query = "query($password: String  $nickname: String){logIn(dto: { nickname: $nickname, password: $password}){ id nickname roles token  }}";


            //var heroRequest = new GraphQLRequest
            //{
            //    Query = query,
            //    Variables = new
            //    {
            //        password = password,
            //        nickname = nickname

            //    }
            //};

            ////var heroRequest = RequestBuilder.BuildRequest(RequstType.Query);


            //var graphQLResponse = await graphQLClient.SendQueryAsync<RensponseLoginMessage>(heroRequest);

            BlobCacheService blobCacheService = new BlobCacheService();

            UserServices userServices = new UserServices(@"https://localhost:5000/graphql/");

            BookApiServices bookServices = new BookApiServices(@"https://localhost:5000/graphql/");

            var list = await bookServices.GetAllBookAsync(1, 5);

            await blobCacheService.CacheCurrentCollection(list, "1");

            var cacheList = await blobCacheService.GetCollection("1");

            foreach(var book in cacheList)
            {
                Console.WriteLine(book.Title);
            }

            var user = await userServices.Login("romka", "romka");

            //var res = await bookServices.LikeBookAsync("624dce1f896cf2b11a0f2d01", user.Token);

            //var res = await bookServices.GetAllBook(3, 5);



            //Console.WriteLine(res.LikeUserNicknames.Count);
            //Console.WriteLine(res.LikeUserNicknames[0]);
            //Console.WriteLine(res[0].Country);
            //Console.WriteLine(graphQLResponse.Data[0].Title.ToString());
            //Console.WriteLine(graphQLResponse.Data[0].Title);


            Console.ReadLine();
        }
    }
}
