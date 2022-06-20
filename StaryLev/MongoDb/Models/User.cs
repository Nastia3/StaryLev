using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.Models
{
    [BsonIgnoreExtraElements]
    public class User : IModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull]
        public string Id { get; set; }
        [BsonIgnoreIfNull]
        public string Nickname { get; set; }
        [BsonIgnoreIfNull]
        public string Password { get; set; }
        public Role[] Roles { get; set; }

        public User()
        {
        }
        public User(bool IsNew)
        {
            if (IsNew)
            {
                Id = ObjectId.GenerateNewId().ToString();
                Roles = new Role[] { Role.User };
            }
        }
    }

    public enum Role
    {
        User,
        Admin
    }
}
