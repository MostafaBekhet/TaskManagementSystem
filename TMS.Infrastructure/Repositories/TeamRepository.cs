using Microsoft.EntityFrameworkCore;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;
using TMS.Infrastructure.Persistence;

namespace TMS.Infrastructure.Repositories
{
    public class TeamRepository(TMSDbContext _dbContext) : ITeamRepository
    {

        public async Task AddAsync(Team team)
        {
            await _dbContext.Teams.AddAsync(team);
        }

        public async Task<ICollection<Team>> GetAllAsync()
        {
            return await _dbContext.Teams.ToListAsync();
        }

        public async Task<ICollection<Team>> GetAllUserTeamsAsync(string userId)
        {
            return await _dbContext.Users
                                   .Where(u => u.Id == userId)
                                   .SelectMany(u => u.UserTeams.Select(ut => ut.Team))
                                   .ToListAsync();
        }

        public async Task<Team?> GetTeamByIdAsync(int teamId)
        {
            return await _dbContext.Teams.FirstOrDefaultAsync(t => t.TeamId == teamId);
        }

        public async Task UpdateAsync(Team update)
        {
            _dbContext.Teams.Update(update);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _dbContext.Teams.Where(t => t.TeamId == id).ExecuteDeleteAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Team?> GetTeamByIdWithUserTeamsAsync(int teamId)
        {
            return await _dbContext.Teams
                                   .Include(t => t.UserTeams)
                                   .FirstOrDefaultAsync(t => t.TeamId == teamId);
        }

        public async Task<List<Team>> GetAllTeamWithMembers()
        {
            return await _dbContext.Teams
                                   .Include(t => t.UserTeams)
                                        .ThenInclude(ut => ut.User)
                                   .Include(t => t.AssignedTasks)
                                   .ToListAsync();
        }

        public async Task<List<Team>> GetAllTeamWithMembers(string userId)
        {
            return await _dbContext.Teams
                                   .Include(t => t.UserTeams)
                                        .ThenInclude(ut => ut.User)
                                        .Where(t => t.UserTeams.Any(ut => ut.UserId == userId))
                                   .Include(t => t.AssignedTasks)
                                   .ToListAsync();
        }
    }
}
