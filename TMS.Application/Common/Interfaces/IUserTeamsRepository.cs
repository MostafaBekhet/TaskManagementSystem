namespace TMS.Application.Common.Interfaces
{
    public interface IUserTeamsRepository
    {
        Task<bool> AreUsersInSameTeam(string userId1, string userId2);
    }
}
