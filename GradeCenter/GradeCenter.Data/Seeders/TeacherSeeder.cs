namespace GradeCenter.Data.Seeders
{
    public class TeacherSeeder : IDataSeeder
    {
        public int Order => 7;

        public async Task Seed(DbContext context)
        {
            //Create data to be seeded
            var teachers = new List<Teacher>()
            {
                new() { Id = 1, UserId = "d98e684b-43f7-43c8-bf76-e97c1aee8d65", SchoolId = new Guid("5e99ac10-fb03-4305-8022-c68a632f118f") },
                new() { Id = 2, UserId = "a8491d87-0649-40fd-948c-6f6d060b29e3", SchoolId = new Guid("5e99ac10-fb03-4305-8022-c68a632f118f") },
                new() { Id = 3, UserId = "cce872aa-1310-4bc5-aefc-5839437d8a94", SchoolId = new Guid("5e99ac10-fb03-4305-8022-c68a632f118f") },
                new() { Id = 4, UserId = "9d1a9d1f-ebc9-42de-ae37-a9b078667b0d", SchoolId = new Guid("5e99ac10-fb03-4305-8022-c68a632f118f") },
                new() { Id = 5, UserId = "74edb9f5-5b35-4cd8-9eaa-7dd201b664d8", SchoolId = new Guid("5e99ac10-fb03-4305-8022-c68a632f118f") }
            };

            foreach (var teacher in teachers)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<Teacher>().AnyAsync(x => x.Id == teacher.Id);

                //Seed data if not already seeded
                if (!isExist)
                    await context.Set<Teacher>().AddAsync(teacher);

                await context.SaveChangesAsync();
            }
        }
    }
}
