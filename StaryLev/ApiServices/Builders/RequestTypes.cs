using System.ComponentModel;

namespace ApiServices.Builders
{
    public enum RequstType
    {
        None,
        Login,
        Registration,
        GetBooks,
        [Description("like")] Like,
        [Description("dislike")] Dislike,
    }

    public enum SortingType
    {
        None,
        [Description("ASC")] Ascending,
        [Description("DESC")] Descending,
    }

    public enum SortingCategory
    {
        None,
        [Description("title")] Title,
        [Description("year")] Cost,
        [Description("country")] Type,
    }

}
