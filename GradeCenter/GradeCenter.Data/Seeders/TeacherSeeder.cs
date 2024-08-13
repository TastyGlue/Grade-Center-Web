
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
                new() { Id = 1, UserId = "D98E684B-43F7-43C8-BF76-E97C1AEE8D65", SchoolId = new Guid("5E99AC10-FB03-4305-8022-C68A632F118F") },
                new() { Id = 2, UserId = "A8491D87-0649-40FD-948C-6F6D060B29E3", SchoolId = new Guid("5E99AC10-FB03-4305-8022-C68A632F118F") },
                new() { Id = 3, UserId = "CCE872AA-1310-4BC5-AEFC-5839437D8A94", SchoolId = new Guid("5E99AC10-FB03-4305-8022-C68A632F118F") },
                new() { Id = 4, UserId = "9D1A9D1F-EBC9-42DE-AE37-A9B078667B0D", SchoolId = new Guid("5E99AC10-FB03-4305-8022-C68A632F118F") },
                new() { Id = 5, UserId = "74EDB9F5-5B35-4CD8-9EAA-7DD201B664D8", SchoolId = new Guid("5E99AC10-FB03-4305-8022-C68A632F118F") }
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
