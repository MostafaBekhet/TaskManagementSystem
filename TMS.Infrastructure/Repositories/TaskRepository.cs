using Microsoft.EntityFrameworkCore;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;
using TMS.Infrastructure.Persistence;

namespace TMS.Infrastructure.Repositories
{
    public class TaskRepository(TMSDbContext _dbContext) : ITaskRepository
    {

        public async Task AddAsync(TaskItem task)
        {
            await _dbContext.Tasks.AddAsync(task);
        }

        public async Task<ICollection<TaskItem>> GetAllAsync()
        {
            return await _dbContext.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int taskId)
        {
            return await _dbContext.Tasks
                                   .Include(t => t.AssigendToTeam)
                                        .ThenInclude(at => at.UserTeams)
                                   .Include(t => t.AssignedToUser)
                                   .Include(t => t.TaskComments)
                                        .ThenInclude(c => c.User)
                                   .FirstOrDefaultAsync(t => t.TaskId == taskId);
        }

        public async Task<TaskItem?> GetTaskByIdWithAssignedTeamAsync(int taskId)
        {
            return await _dbContext.Tasks
                                   .Include(t => t.AssigendToTeam)
                                   .ThenInclude(tm => tm.UserTeams)
                                   .ThenInclude(ut => ut.User)
                                   .FirstOrDefaultAsync(t => t.TaskId == taskId);
        }

        public async Task UpdateAsync(TaskItem update)
        {
             _dbContext.Tasks.Update(update);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _dbContext.Tasks.Where(t => t.TaskId == id).ExecuteDeleteAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
