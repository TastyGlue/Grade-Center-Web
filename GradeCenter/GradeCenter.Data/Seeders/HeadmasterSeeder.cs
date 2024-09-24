
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
                new() { Id = new Guid("8e0d4063-0dd3-4359-b261-eba797ecdfe6"), SchoolId = new Guid("5e99ac10-fb03-4305-8022-c68a632f118f") },
                new() { Id = new Guid("e7782632-7525-482c-b0b1-64c1264c8c4a"), SchoolId = new Guid("24cbee20-06b6-4726-902d-66fbbf88b804") }
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
