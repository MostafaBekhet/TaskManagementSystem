using Microsoft.EntityFrameworkCore;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;
using TMS.Infrastructure.Persistence;

namespace TMS.Infrastructure.Repositories
{
    public class CommentRepository(TMSDbContext _dbContext) : ICommentRepository
    {
        public async Task AddAsync(TaskComment comment)
        {
            await _dbContext.TaskComments.AddAsync(comment);
        }

        public async Task DeleteAsync(int commentId)
        {
            await _dbContext.TaskComments.Where(c => c.CommentId == commentId).ExecuteDeleteAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
