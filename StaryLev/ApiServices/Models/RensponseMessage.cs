using Application.Dto;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ApiServices.Models
{
    public class RensponseBookMessage
    {
        [JsonProperty("pagingBooks")]
        public BookList BookList { get; set; }
    }

    public class BookList
    {
        [JsonProperty("items")]
        public List<Book> Books { get; set; }
    }


    public class RensponseLoginMessage
    {
        [JsonProperty("logIn")]
        public UserInfo UserInfo { get; set; }
    }

    public class RensponseRegisterMessage
    {
        [JsonProperty("register")]
        public UserInfo UserInfo { get; set; }
    }

    public class RensponseLikeMessage
    {
        [JsonProperty("like")]
        public Book Book { get; set; }
    }

    public class RensponseDislikeMessage
    {
        [JsonProperty("dislike")]
        public Book Book { get; set; }
    }
}
