
namespace GradeCenter.Data.Seeders
{
    public class HeadmasterSeeder : IDataSeeder
    {
        public int Order => 6;

        public async Task Seed(DbContext context)
        {
            //Create data to be seeded
            var headmasters = new List<Headmaster>()
            {
                new() { Id = 1, UserId = "8E0D4063-0DD3-4359-B261-EBA797ECDFE6", SchoolId = new Guid("5E99AC10-FB03-4305-8022-C68A632F118F") },
                new() { Id = 2, UserId = "E7782632-7525-482C-B0B1-64C1264C8C4A", SchoolId = new Guid("24CBEE20-06B6-4726-902D-66FBBF88B804") }
            };

            foreach (var headmaster in headmasters)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<Headmaster>().AnyAsync(x => x.Id == headmaster.Id);

                //Seed data if not already seeded
                if (!isExist)
                    await context.Set<Headmaster>().AddAsync(headmaster);

                await context.SaveChangesAsync();
            }
        }
    }
}
