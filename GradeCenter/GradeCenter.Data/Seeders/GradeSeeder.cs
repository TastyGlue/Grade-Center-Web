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
                new() { Id = new Guid("1fdf30f4-abb0-4781-8086-ba3eba779a1b"), StudentId = new Guid("0d74d377-f0fc-4cc2-882a-eb6228042782"), SubjectId = new Guid("1e578f4d-630c-4aab-8b04-ec5bb1d9ef67"), TeacherId = new Guid("d98e684b-43f7-43c8-bf76-e97c1aee8d65"), Result = "6", Date = new DateTime(2024, 4, 20) },
                new() { Id = new Guid("fe525d55-3323-4462-9f07-552961df8c0b"), StudentId = new Guid("0d74d377-f0fc-4cc2-882a-eb6228042782"), SubjectId = new Guid("e881cccd-a5e6-4373-b6fb-e37fa99b67ac"), TeacherId = new Guid("a8491d87-0649-40fd-948c-6f6d060b29e3"), Result = "5.50", Date = new DateTime(2024, 4, 20) },
                new() { Id = new Guid("d1fe2f0f-05b9-4368-ac94-21bed420b982"), StudentId = new Guid("0d74d377-f0fc-4cc2-882a-eb6228042782"), SubjectId = new Guid("1f752077-eb39-4ca0-8169-45d18b065c63"), TeacherId = new Guid("cce872aa-1310-4bc5-aefc-5839437d8a94"), Result = "5", Date = new DateTime(2024, 4, 20) },
                new() { Id = new Guid("490dbe2c-567c-40ba-92a8-175dab26d2b5"), StudentId = new Guid("0d74d377-f0fc-4cc2-882a-eb6228042782"), SubjectId = new Guid("c8de0706-0674-46d4-a594-bf30effa2270"), TeacherId = new Guid("9d1a9d1f-ebc9-42de-ae37-a9b078667b0d"), Result = "4.50", Date = new DateTime(2024, 4, 20) },
                new() { Id = new Guid("df68f169-98a8-4db8-b56c-ed2ac1fc852e"), StudentId = new Guid("0d74d377-f0fc-4cc2-882a-eb6228042782"), SubjectId = new Guid("c52dd128-ca75-4d9a-b57a-c558ac138051"), TeacherId = new Guid("74edb9f5-5b35-4cd8-9eaa-7dd201b664d8"), Result = "4", Date = new DateTime(2024, 4, 20) }
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
