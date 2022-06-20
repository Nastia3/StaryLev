using Newtonsoft.Json;
using System.Collections.Generic;

namespace ApiServices.Models
{
    public class Book
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("bookUrl")]
        public string BookUrl { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("imgUrl")]
        public string ImgUrl { get; set; }
        [JsonProperty("cost")]
        public string Cost { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("likeUserNicknames")]
        public IList<string> LikeUserNicknames { get; set; }
        [JsonProperty("dislikeUserNicknames")]
        public IList<string> DislikeUserNicknames { get; set; }
    }
}