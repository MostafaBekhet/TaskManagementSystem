using TMS.Domain.Entities;

namespace TMS.Application.Common.Interfaces
{
    public interface ITeamRepository
    {
        Task AddAsync(Team team);
        Task DeleteAsync(int id);
        Task<ICollection<Team>> GetAllAsync();
        Task<List<Team>> GetAllTeamWithMembers();
        Task<List<Team>> GetAllTeamWithMembers(string userId);
        Task<ICollection<Team>> GetAllUserTeamsAsync(string userId);
        Task<Team?> GetTeamByIdAsync(int teamId);
        Task<Team?> GetTeamByIdWithUserTeamsAsync(int teamId);
        Task SaveChangesAsync();
        Task UpdateAsync(Team update);
    }
}