using Microsoft.EntityFrameworkCore;
using TMS.Application.Tasks.Common.Interfaces;
using TMS.Domain.Entities;
using TMS.Infrastructure.Persistence;

namespace TMS.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TMSDbContext _dbContext;

        public TaskRepository(TMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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
            return await _dbContext.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId);
        }

        public async Task UpdateAsync(TaskItem item)
        {
            throw new NotImplementedException();
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
