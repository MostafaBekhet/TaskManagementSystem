using Microsoft.EntityFrameworkCore;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;
using TMS.Infrastructure.Persistence;

namespace TMS.Infrastructure.Repositories
{
    public class UserRepository(TMSDbContext _dbContext) : IUserRepository
    {

        public async Task<User?> GetByIdAsync(string userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

    }
}
