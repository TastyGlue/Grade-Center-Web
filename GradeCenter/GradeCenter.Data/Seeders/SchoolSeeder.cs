namespace GradeCenter.Data.Seeders
{
    public class SchoolSeeder : IDataSeeder
    {
        public int Order => 3;

        public async Task Seed(DbContext context)
        {
            //Create data to be seeded
            var schools = new List<School>()
            {
                new() { Id = new Guid("5e99ac10-fb03-4305-8022-c68a632f118f"), Name = "Riverside High School", Address = "101 River Road, Education Town, Country" },
                new() { Id = new Guid("24cbee20-06b6-4726-902d-66fbbf88b804"), Name = "Hillcrest Academy", Address = "202 Hilltop Avenue, Suburbia, Country" }
            };

            foreach (var school in schools)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<School>().AnyAsync(x => x.Id == school.Id);

                //Seed data if not already seeded
                if (!isExist)
                    await context.Set<School>().AddAsync(school);

                await context.SaveChangesAsync();
            }
        }
    }
}
