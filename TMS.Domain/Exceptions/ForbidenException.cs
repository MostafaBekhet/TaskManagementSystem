namespace TMS.Domain.Exceptions
{
    public class ForbiddenException : Exception
    {
        public int StatusCode { get; } = 403;

        public ForbiddenException() { }

        public ForbiddenException(string message): base(message)
        {
        }

    }
}
