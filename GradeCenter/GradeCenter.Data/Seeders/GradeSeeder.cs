namespace GradeCenter.Data.Seeders
{
    public class GradeSeeder : IDataSeeder
    {
        public int Order => 11;

        public async Task Seed(DbContext context)
        {
            //Create data to be seeded
            var grades = new List<Grade>()
            {
                new() { Id = 1, StudentId = 1, SubjectId = new Guid("1E578F4D-630C-4AAB-8B04-EC5BB1D9EF67"), TeacherId = 1, Result = "6", Date = new DateTime(2024, 4, 20) },
                new() { Id = 2, StudentId = 1, SubjectId = new Guid("E881CCCD-A5E6-4373-B6FB-E37FA99B67AC"), TeacherId = 2, Result = "5.50", Date = new DateTime(2024, 4, 20) },
                new() { Id = 3, StudentId = 1, SubjectId = new Guid("1F752077-EB39-4CA0-8169-45D18B065C63"), TeacherId = 3, Result = "5", Date = new DateTime(2024, 4, 20) },
                new() { Id = 4, StudentId = 1, SubjectId = new Guid("C8DE0706-0674-46D4-A594-BF30EFFA2270"), TeacherId = 4, Result = "4.50", Date = new DateTime(2024, 4, 20) },
                new() { Id = 5, StudentId = 1, SubjectId = new Guid("C52DD128-CA75-4D9A-B57A-C558AC138051"), TeacherId = 5, Result = "4", Date = new DateTime(2024, 4, 20) }
            };

            foreach (var grade in grades)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<Grade>().AnyAsync(x => x.Id == grade.Id);

                //Seed data if not already seeded
                if (!isExist)
                    await context.Set<Grade>().AddAsync(grade);

                await context.SaveChangesAsync();
            }
        }
    }
}
