
namespace GradeCenter.Data.Seeders
{
    public class AdminSeeder : IDataSeeder
    {
        public int Order => 5;

        public async Task Seed(DbContext context)
        {
            //Create data to be seeded
            var admins = new List<Admin>()
            {
                new() { Id = 1, UserId = "edeb7970-01ce-4df5-a99d-70090d66379b" },
                new() { Id = 2, UserId = "a26c2c8a-d575-4c12-bb7b-e9af1b2e79b2" }
            };

            foreach (var admin in admins)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<Admin>().AnyAsync(x => x.Id == admin.Id);

                //Seed data if not already seeded
                if (!isExist)
                    await context.Set<Admin>().AddAsync(admin);

                await context.SaveChangesAsync();
            }
        }
    }
}
