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
                new() { Id = new Guid("0d74d377-f0fc-4cc2-882a-eb6228042782"), ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662") },
                new() { Id = new Guid("2cb059ac-d7b3-42ac-a41e-f9660713773f"), ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662") },
                new() { Id = new Guid("70b90b3b-7cc4-4ce7-9313-0da8fc23f29f"), ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662") },
                new() { Id = new Guid("242cf21d-31ec-4dd7-bdcb-365b895e4ffe"), ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662") },
                new() { Id = new Guid("5ba8cb31-c401-45e9-a401-e47748fda3c3"), ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662") }
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
