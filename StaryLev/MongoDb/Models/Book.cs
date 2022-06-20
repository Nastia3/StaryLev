using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MongoDb.Models
{
    [BsonIgnoreExtraElements]
    public class Book : IModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull]
        public string Id { get; set; }
        [BsonIgnoreIfNull]
        public string Posters { get; set; }
        [BsonIgnoreIfNull]
        public string BookUrl { get; set; }
        [BsonIgnoreIfNull]
        public string Title { get; set; }
        [BsonIgnoreIfNull]
        public string ImgUrl { get; set; }
        [BsonIgnoreIfNull]
        public string Cost { get; set; }
        [BsonIgnoreIfNull]
        public string Type { get; set; }
        [BsonIgnoreIfNull]
        public string Author { get; set; }
        [BsonIgnoreIfNull]
        public string Description { get; set; }
        [BsonIgnoreIfNull]
        public IList<string> LikeUserNicknames { get; set; }
        [BsonIgnoreIfNull]
        public IList<string> DislikeUserNicknames { get; set; }
        public Book()
        {
            LikeUserNicknames = new List<string>();
            DislikeUserNicknames = new List<string>();
        }
        public Book(bool IsNew)
        {
            if (IsNew)
            {
                Id = ObjectId.GenerateNewId().ToString();
            }
            LikeUserNicknames = new List<string>();
            DislikeUserNicknames = new List<string>();
        }
    }
}
