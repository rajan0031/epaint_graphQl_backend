using System.Net;

namespace MyGraphqlApp.Exception.GlobalException
{
    public class GlobalException : ApplicationException
    {
        public HttpStatusCode StatusCode { get; }

        public GlobalException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
