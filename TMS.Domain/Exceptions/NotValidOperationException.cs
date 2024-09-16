namespace TMS.Domain.Exceptions
{
    public class NotValidOperationException : Exception
    {
        public int StatusCode { get; } = 409;

        public NotValidOperationException(string message) : base(message)
        {
        }
    }
}
