using Microsoft.EntityFrameworkCore;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;
using TMS.Infrastructure.Persistence;

namespace TMS.Infrastructure.Repositories
{
    public class TeamTasksRepository(TMSDbContext _dbContext) : ITeamTasksRepository
    {
        public async Task<ICollection<TaskItem>> GetAllLeadTasksAsync(string userId)
        {

            //getting all teams ids which he leads
            var teamIds = await _dbContext.Teams.Where(t => t.UserTeams.Any(ut => ut.UserId == userId))
                                                .Select(t => t.TeamId)
                                                .ToListAsync();


            return await _dbContext.Tasks.Where(t => t.CreatedByUserId == userId
                                            || t.AssignedToUserId == userId
                                            || (t.AssignedToTeamId.HasValue &&
                                                teamIds.Contains(t.AssignedToTeamId.Value))).ToListAsync();
        }

        public async Task<ICollection<TaskItem>> GetAllUserTasksAsync(string userId)
        {
            return await _dbContext.Tasks.Where(t => t.CreatedByUserId == userId
                                                  || t.AssignedToUserId == userId).ToListAsync();
        }
    }
}
