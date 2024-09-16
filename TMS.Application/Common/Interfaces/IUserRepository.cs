using TMS.Domain.Entities;

namespace TMS.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(string userId); 
    }
}