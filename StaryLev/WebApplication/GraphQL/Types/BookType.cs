using HotChocolate.Types;
using MongoDb.Models;

namespace WebApplication.GraphQL.Types
{
    [ExtendObjectType("Type")]
    public class BookType : ObjectType<Book>
    {
        protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
        {
            descriptor.Field(_ => _.Id);
            descriptor.Field(_ => _.BookUrl);
            descriptor.Field(_ => _.Title);
            descriptor.Field(_ => _.ImgUrl);
            descriptor.Field(_ => _.Cost);
            descriptor.Field(_ => _.Type);
            descriptor.Field(_ => _.Author);
            descriptor.Field(_ => _.Posters);
            descriptor.Field(_ => _.Description);
            descriptor.Field(_ => _.LikeUserNicknames);
            descriptor.Field(_ => _.DislikeUserNicknames);
        }
    }
}
