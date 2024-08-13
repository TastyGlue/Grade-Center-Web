
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
                new() { Id = 1, UserId = "EDEB7970-01CE-4DF5-A99D-70090D66379B", SchoolId = new Guid("5E99AC10-FB03-4305-8022-C68A632F118F") },
                new() { Id = 2, UserId = "A26C2C8A-D575-4C12-BB7B-E9AF1B2E79B2", SchoolId = new Guid("24CBEE20-06B6-4726-902D-66FBBF88B804") }
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
