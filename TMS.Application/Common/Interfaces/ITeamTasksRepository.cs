using TMS.Domain.Entities;

namespace TMS.Application.Common.Interfaces
{
    public interface ITeamTasksRepository
    {
        Task<ICollection<TaskItem>> GetAllLeadTasksAsync(string userId);
        Task<ICollection<TaskItem>> GetAllUserTasksAsync(string userId);
    }
}