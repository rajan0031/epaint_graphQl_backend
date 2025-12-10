
using UserEx = MyGraphqlApp.Exception.UserException.UserException;
using GlobalEx = MyGraphqlApp.Exception.GlobalException.GlobalException;


namespace MyGraphqlApp.Exception.GraphqlException
{
    public class GraphqlErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            var exception = error.Exception;

            if (exception is UserEx ue)
            {
                return error
                    .WithMessage(ue.Message)
                    .WithCode("USER_ERROR")
                    .SetExtension("statusCode", (int)ue.StatusCode);
            }

            if (exception is GlobalEx ge)
            {
                return error
                    .WithMessage(ge.Message)
                    .WithCode("GLOBAL_ERROR")
                    .SetExtension("statusCode", (int)ge.StatusCode);
            }

            // fallback for unexpected errors
            return error
                .WithMessage("Unexpected GraphQL error occurred.")
                .WithCode("INTERNAL_ERROR")
                .SetExtension("statusCode", 500);
        }
    }
}
