
namespace GradeCenter.Data.Seeders
{
    public class StudentSeeder : IDataSeeder
    {
        public int Order => 9;

        public async Task Seed(DbContext context)
        {
            //Create data to be seeded
            var students = new List<Student>()
            {
                new() { Id = 1, UserId = "0D74D377-F0FC-4CC2-882A-EB6228042782", SchoolId = new Guid("5E99AC10-FB03-4305-8022-C68A632F118F"), ClassId = new Guid("52761D50-5812-4FE4-A800-8FD26E6D6662") },
                new() { Id = 2, UserId = "2CB059AC-D7B3-42AC-A41E-F9660713773F", SchoolId = new Guid("5E99AC10-FB03-4305-8022-C68A632F118F"), ClassId = new Guid("52761D50-5812-4FE4-A800-8FD26E6D6662") },
                new() { Id = 3, UserId = "70B90B3B-7CC4-4CE7-9313-0DA8FC23F29F", SchoolId = new Guid("5E99AC10-FB03-4305-8022-C68A632F118F"), ClassId = new Guid("52761D50-5812-4FE4-A800-8FD26E6D6662") },
                new() { Id = 4, UserId = "242CF21D-31EC-4DD7-BDCB-365B895E4FFE", SchoolId = new Guid("5E99AC10-FB03-4305-8022-C68A632F118F"), ClassId = new Guid("52761D50-5812-4FE4-A800-8FD26E6D6662") },
                new() { Id = 5, UserId = "5BA8CB31-C401-45E9-A401-E47748FDA3C3", SchoolId = new Guid("5E99AC10-FB03-4305-8022-C68A632F118F"), ClassId = new Guid("52761D50-5812-4FE4-A800-8FD26E6D6662") },
            };

            foreach (var student in students)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<Student>().AnyAsync(x => x.Id == student.Id);

                //Seed data if not already seeded
                if (!isExist)
                    await context.Set<Student>().AddAsync(student);

                await context.SaveChangesAsync();
            }
        }
    }
}
