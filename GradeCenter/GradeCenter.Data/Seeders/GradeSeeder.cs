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
                new() { Id = 1, StudentId = 1, SubjectId = new Guid("1e578f4d-630c-4aab-8b04-ec5bb1d9ef67"), TeacherId = 1, Result = "6", Date = new DateTime(2024, 4, 20) },
                new() { Id = 2, StudentId = 1, SubjectId = new Guid("e881cccd-a5e6-4373-b6fb-e37fa99b67ac"), TeacherId = 2, Result = "5.50", Date = new DateTime(2024, 4, 20) },
                new() { Id = 3, StudentId = 1, SubjectId = new Guid("1f752077-eb39-4ca0-8169-45d18b065c63"), TeacherId = 3, Result = "5", Date = new DateTime(2024, 4, 20) },
                new() { Id = 4, StudentId = 1, SubjectId = new Guid("c8de0706-0674-46d4-a594-bf30effa2270"), TeacherId = 4, Result = "4.50", Date = new DateTime(2024, 4, 20) },
                new() { Id = 5, StudentId = 1, SubjectId = new Guid("c52dd128-ca75-4d9a-b57a-c558ac138051"), TeacherId = 5, Result = "4", Date = new DateTime(2024, 4, 20) }
            };

            foreach (var grade in grades)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<Grade>().AnyAsync(x => x.Id == grade.Id);

                //Seed data if not already seeded
                if (!isExist)
                {
                    grade.Date = DateTime.SpecifyKind(grade.Date!.Value, DateTimeKind.Utc);
                    await context.Set<Grade>().AddAsync(grade);
                }


                await context.SaveChangesAsync();
            }
        }
    }
}
