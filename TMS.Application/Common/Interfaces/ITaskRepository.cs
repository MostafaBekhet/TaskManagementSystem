using TMS.Domain.Entities;

namespace TMS.Application.Common.Interfaces
{
    public interface ITaskRepository
    {
        Task<ICollection<TaskItem>> GetAllAsync();

        Task<TaskItem?> GetTaskByIdAsync(int id);

        Task<TaskItem?> GetTaskByIdWithAssignedTeamAsync(int id);

        Task AddAsync(TaskItem item);

        Task SaveChangesAsync();

        Task UpdateAsync(TaskItem update);

        Task DeleteAsync(int id);
    }
}
