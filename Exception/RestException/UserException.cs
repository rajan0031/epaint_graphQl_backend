using System.Net;

namespace MyGraphqlApp.Exception.UserException
{
    public class UserException : ApplicationException
    {
        public HttpStatusCode StatusCode { get; }

        public UserException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
