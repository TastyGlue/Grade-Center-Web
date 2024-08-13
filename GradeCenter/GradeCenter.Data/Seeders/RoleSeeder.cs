namespace GradeCenter.Data.Seeders
{
    public class RoleSeeder : IDataSeeder
    {
        public int Order => 1;

        public async Task Seed(DbContext context)
        {
            //Create data to be seeded
            var roles = new HashSet<IdentityRole>()
                {
                    new() { Name = "admin", NormalizedName = "ADMIN" },
                    new() { Name = "headmaster", NormalizedName = "HEADMASTER" },
                    new() { Name = "teacher", NormalizedName = "TEACHER" },
                    new() { Name = "student", NormalizedName = "STUDENT" },
                    new() { Name = "parent", NormalizedName = "PARENT" },
                };

            foreach (var role in roles)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<IdentityRole>().AnyAsync(x => x.Name == role.Name);

                //Seed data if not already seeded
                if (!isExist)
                    await context.Set<IdentityRole>().AddAsync(role);

                await context.SaveChangesAsync();
            }
        }
    }
}
