namespace ApiServices.Builders
{
    public static class RequestTemplates
    {
        public static string Login { get; set; } = "query($password: String  $nickname: String){logIn(dto: { nickname: $nickname, password: $password}){ id nickname roles token  }}";
        public static string Register { get; set; } = "mutation($password: String  $nickname: String){register(dto: { nickname: $nickname, password: $password}){ id nickname roles token  }}";
        public static string GetBooks { get; set; } = "query($skip: Int!,$take: Int!) { pagingBooks(skip: $skip, take: $take) { items {id bookUrl title imgUrl cost type author description likeUserNicknames dislikeUserNicknames}}}";
        public static string Like { get; set; } = "mutation($bookId: String!) { like(bookId: $bookId) {id bookUrl title imgUrl cost type author description likeUserNicknames dislikeUserNicknames}}";
        public static string Dislike { get; set; } = "mutation($bookId: String!) { dislike($bookId: $bookId) {id bookUrl title imgUrl cost type author description likeUserNicknames dislikeUserNicknames}}";
    }
}