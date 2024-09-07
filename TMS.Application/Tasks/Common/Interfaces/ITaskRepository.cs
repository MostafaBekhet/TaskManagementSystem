using TMS.Domain.Entities;

namespace TMS.Application.Tasks.Common.Interfaces
{
    public interface ITaskRepository
    {
        Task<ICollection<TaskItem>> GetAllAsync();

        Task<TaskItem?> GetTaskByIdAsync(int id);

        Task AddAsync(TaskItem item);

        Task SaveChangesAsync();

        Task UpdateAsync(TaskItem item);

        Task DeleteAsync(int id);
    }
}
