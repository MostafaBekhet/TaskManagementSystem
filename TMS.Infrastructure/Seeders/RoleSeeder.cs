using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Constants;
using TMS.Infrastructure.Persistence;

namespace TMS.Infrastructure.Seeders
{
    internal class RoleSeeder(TMSDbContext dbContext) : IRoleSeeder
    {
        public async Task Seed()
        {

            if (await dbContext.Database.CanConnectAsync())
            {
                if(!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
            }

        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles = [
                    new (UserRoles.RegularUser)
                    {
                        NormalizedName = UserRoles.RegularUser.ToUpper()
                    },
                    new (UserRoles.TeamLead)
                    {
                        NormalizedName = UserRoles.TeamLead.ToUpper()
                    },
                    new (UserRoles.Administrator)
                    {
                        NormalizedName = UserRoles.Administrator.ToUpper()
                    },
                ];

            return roles;
        }
    }
}
