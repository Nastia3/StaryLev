using System;
using GraphQL;

namespace ApiServices.Builders
{
    public static class RequestBuilder
    {
        public static GraphQLRequest BuildRequest(RequstType requstType = RequstType.None,
            SortingType sortingType = SortingType.None, SortingCategory sortingCategory = SortingCategory.None,
             object[] @params = null)
        {
            GraphQLRequest graphQLRequest = new GraphQLRequest();

            switch (requstType)
            {
                case RequstType.Login:
                    graphQLRequest = BuildLoginRequest(@params);
                    break;
                case RequstType.Registration:
                    graphQLRequest = BuildRegistrationRequest(@params);
                    break;
                case RequstType.GetBooks:
                    graphQLRequest = BuildGetBooksRequest(@params);
                    break;
                case RequstType.Like:
                    graphQLRequest = BuildLikeRequest(@params);
                    break;
                case RequstType.Dislike:
                    graphQLRequest = BuildDislikeRequest(@params);
                    break;
                default:
                    break;
            }

            return graphQLRequest;
        }

        private static GraphQLRequest BuildLoginRequest(object[] @params)
        {
            if (@params == null || @params.Length != 2)
            {
                throw new Exception(ErrorMessage.ParamsNotCorrect);
            }

            var graphQLRequest = new GraphQLRequest
            {
                Query = RequestTemplates.Login,
                Variables = new
                {
                    nickname = @params[0],
                    password = @params[1]
                }
            };

            return graphQLRequest;
        }

        private static GraphQLRequest BuildRegistrationRequest(object[] @params)
        {
            if (@params == null || @params.Length != 2)
            {
                throw new Exception(ErrorMessage.ParamsNotCorrect);
            }

            var graphQLRequest = new GraphQLRequest
            {
                Query = RequestTemplates.Register,
                Variables = new
                {
                    nickname = @params[0],
                    password = @params[1]
                }
            };

            return graphQLRequest;
        }

        private static GraphQLRequest BuildGetBooksRequest(object[] @params)
        {
            if (@params == null || @params.Length != 2)
            {
                throw new Exception(ErrorMessage.ParamsNotCorrect);
            }

            var graphQLRequest = new GraphQLRequest
            {
                Query = RequestTemplates.GetBooks,
                Variables = new
                {
                    skip = @params[0],
                    take = @params[1]
                }
            };

            return graphQLRequest;
        }

        private static GraphQLRequest BuildLikeRequest(object[] @params)
        {
            if (@params == null || @params.Length != 1)
            {
                throw new Exception(ErrorMessage.ParamsNotCorrect);
            }

            var graphQLRequest = new GraphQLRequest
            {
                Query = RequestTemplates.Like,
                Variables = new
                {
                    bookId = @params[0],
                }
            };

            return graphQLRequest;
        }

        private static GraphQLRequest BuildDislikeRequest(object[] @params)
        {
            if (@params == null || @params.Length != 1)
            {
                throw new Exception(ErrorMessage.ParamsNotCorrect);
            }

            var graphQLRequest = new GraphQLRequest
            {
                Query = RequestTemplates.Dislike,
                Variables = new
                {
                    bookId = @params[0],
                }
            };

            return graphQLRequest;
        }
    }
}
