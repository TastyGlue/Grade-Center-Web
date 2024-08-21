using GradeCenter.Shared.Models;

namespace GradeCenter.Data.Seeders
{
    public class RoleSeeder : IDataSeeder
    {
        public int Order => 1;

        public async Task Seed(DbContext context)
        {
            //Create data to be seeded
            var roles = CreateRolesList();

            foreach (var role in roles)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<IdentityRole>().AnyAsync(x => x.NormalizedName == role.NormalizedName);

                //Seed data if not already seeded
                if (!isExist)
                    await context.Set<IdentityRole>().AddAsync(role);

                await context.SaveChangesAsync();
            }
        }

        private HashSet<IdentityRole> CreateRolesList()
        {
            // Get names of all roles
            var roleNames = Enum.GetNames(typeof(Roles)).ToList();

            var roles = new HashSet<IdentityRole>();

            // Create the IdentityRole objects
            foreach (var name in roleNames)
            {
                roles.Add(new()
                {
                    Name = name.ToLower(),
                    NormalizedName = name
                });
            }

            return roles;
        }
    }
}
