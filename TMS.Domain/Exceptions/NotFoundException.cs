namespace TMS.Domain.Exceptions
{
    public class NotFoundException : Exception
    {

        public int StatusCode { get; } = 404;

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string entity, object key)
            : base($"\"{entity}\" with givin ({key}) was not found.")
        {
        }
    }
}
