using Microsoft.EntityFrameworkCore;
using TMS.Application.Common.Interfaces;
using TMS.Infrastructure.Persistence;

namespace TMS.Infrastructure.Repositories
{
    public class UserTeamsRepository(TMSDbContext _dbContext) : IUserTeamsRepository
    {
        public async Task<bool> AreUsersInSameTeam(string userId1 , string userId2)
        {
            var found = await _dbContext.UserTeams
                                        .Where(ut => ut.UserId == userId1 || ut.UserId == userId2)
                                        .GroupBy(ut => ut.TeamId)
                                        .Where(g => g.Count() > 1)
                                        .Select(g => g.Key)
                                        .FirstOrDefaultAsync();

            return found != default;
        }
    }
}
