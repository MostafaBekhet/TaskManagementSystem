namespace TMS.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string entity, object key)
            : base($"Entity \"{entity}\" ({key}) was not found.")
        {
        }
    }
}
