using Application.Services.Interfaces;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.AspNetCore.Http;
using MongoDb.Models;
using System.Threading.Tasks;

namespace WebApplication.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class BookMutation
    {
        [Authorize]
        [UseProjection]
        public async Task<Book> Like(string bookId, [Service] IFeelingService feelService, [Service] IHttpContextAccessor httpContext) =>
            await feelService.LikeAsync(
                bookId, 
                httpContext.HttpContext.User.FindFirst("Nickname").Value);

        [Authorize]
        [UseProjection]
        public async Task<Book> Dislike(string bookId, [Service] IFeelingService feelService, [Service] IHttpContextAccessor httpContext) =>
            await feelService.DislikeAsync(
                bookId,
                httpContext.HttpContext.User.FindFirst("Nickname").Value);
    }
}
