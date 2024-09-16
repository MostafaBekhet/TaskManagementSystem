using TMS.Domain.Entities;

namespace TMS.Application.Common.Interfaces
{
    public interface ICommentRepository
    {
        Task AddAsync(TaskComment comment);
        Task DeleteAsync(int commentId);
        Task SaveChangesAsync();
    }
}