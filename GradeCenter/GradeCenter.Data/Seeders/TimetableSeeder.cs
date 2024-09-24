using GradeCenter.Data.Models;

namespace GradeCenter.Data.Seeders
{
    public class TimetableSeeder : IDataSeeder
    {
        public int Order => 12;

        public async Task Seed(DbContext context)
        {
            //Create data to be seeded
            var timetables = new List<Timetable>()
            {
                new() { Id = new Guid("10ee4505-3e2e-4836-b012-f059715c3234"), ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("c52dd128-ca75-4d9a-b57a-c558ac138051"), TeacherId = new Guid("74edb9f5-5b35-4cd8-9eaa-7dd201b664d8"), Year = "2023/2024 Spring", Day = DayOfWeek.Monday , StartTime = DateTime.MinValue.AddHours(7).AddMinutes(45), EndTime = DateTime.MinValue.AddHours(8).AddMinutes(25) },
                new() { Id = new Guid("9f3d0db3-0503-4ca8-8d62-fccbbb8ee357"), ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("c8de0706-0674-46d4-a594-bf30effa2270"), TeacherId = new Guid("9d1a9d1f-ebc9-42de-ae37-a9b078667b0d"), Year = "2023/2024 Spring", Day = DayOfWeek.Monday , StartTime = DateTime.MinValue.AddHours(8).AddMinutes(30), EndTime = DateTime.MinValue.AddHours(9).AddMinutes(10) },
                new() { Id = new Guid("f2babe47-69a5-4310-92e0-a06e02a898bd"), ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("1f752077-eb39-4ca0-8169-45d18b065c63"), TeacherId = new Guid("cce872aa-1310-4bc5-aefc-5839437d8a94"), Year = "2023/2024 Spring", Day = DayOfWeek.Tuesday , StartTime = DateTime.MinValue.AddHours(7).AddMinutes(45), EndTime = DateTime.MinValue.AddHours(8).AddMinutes(25) },
                new() { Id = new Guid("6a265dbf-3e9e-4ad0-8122-3f4710fad156"), ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("e881cccd-a5e6-4373-b6fb-e37fa99b67ac"), TeacherId = new Guid("a8491d87-0649-40fd-948c-6f6d060b29e3"), Year = "2023/2024 Spring", Day = DayOfWeek.Tuesday , StartTime = DateTime.MinValue.AddHours(8).AddMinutes(30), EndTime = DateTime.MinValue.AddHours(9).AddMinutes(10) },
                new() { Id = new Guid("7489e617-51a4-45ae-83db-02bbe938dd5b"), ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("1e578f4d-630c-4aab-8b04-ec5bb1d9ef67"), TeacherId = new Guid("d98e684b-43f7-43c8-bf76-e97c1aee8d65"), Year = "2023/2024 Spring", Day = DayOfWeek.Wednesday , StartTime = DateTime.MinValue.AddHours(7).AddMinutes(45), EndTime = DateTime.MinValue.AddHours(8).AddMinutes(25) },
                new() { Id = new Guid("b7185155-39cd-4744-a855-4fd86ebcb85c"), ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("c52dd128-ca75-4d9a-b57a-c558ac138051"), TeacherId = new Guid("74edb9f5-5b35-4cd8-9eaa-7dd201b664d8"), Year = "2023/2024 Spring", Day = DayOfWeek.Wednesday , StartTime = DateTime.MinValue.AddHours(8).AddMinutes(30), EndTime = DateTime.MinValue.AddHours(9).AddMinutes(10) },
                new() { Id = new Guid("49b03bbf-3e0d-4a3c-8ddd-489fb5c4336a"), ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("c8de0706-0674-46d4-a594-bf30effa2270"), TeacherId = new Guid("9d1a9d1f-ebc9-42de-ae37-a9b078667b0d"), Year = "2023/2024 Spring", Day = DayOfWeek.Thursday , StartTime = DateTime.MinValue.AddHours(7).AddMinutes(45), EndTime = DateTime.MinValue.AddHours(8).AddMinutes(25) },
                new() { Id = new Guid("d751841a-c3fc-44e8-b8e1-c51eeffb046c"), ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("1f752077-eb39-4ca0-8169-45d18b065c63"), TeacherId = new Guid("cce872aa-1310-4bc5-aefc-5839437d8a94"), Year = "2023/2024 Spring", Day = DayOfWeek.Thursday , StartTime = DateTime.MinValue.AddHours(8).AddMinutes(30), EndTime = DateTime.MinValue.AddHours(9).AddMinutes(10) },
                new() { Id = new Guid("fb31d3fd-0d5e-4411-9e2a-84484aa57690"), ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("e881cccd-a5e6-4373-b6fb-e37fa99b67ac"), TeacherId = new Guid("a8491d87-0649-40fd-948c-6f6d060b29e3"), Year = "2023/2024 Spring", Day = DayOfWeek.Friday , StartTime = DateTime.MinValue.AddHours(7).AddMinutes(45), EndTime = DateTime.MinValue.AddHours(8).AddMinutes(25) },
                new() { Id = new Guid("530b30b9-a3e0-4383-b7cf-dd70d66de254"), ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("1e578f4d-630c-4aab-8b04-ec5bb1d9ef67"), TeacherId = new Guid("d98e684b-43f7-43c8-bf76-e97c1aee8d65"), Year = "2023/2024 Spring", Day = DayOfWeek.Friday , StartTime = DateTime.MinValue.AddHours(8).AddMinutes(30), EndTime = DateTime.MinValue.AddHours(9).AddMinutes(10) }
            };

            foreach (var timetable in timetables)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<Timetable>().AnyAsync(x => x.Id == timetable.Id);

                //Seed data if not already seeded
                if (!isExist)
                {
                    timetable.StartTime = DateTime.SpecifyKind(timetable.StartTime!.Value, DateTimeKind.Utc);
                    timetable.EndTime = DateTime.SpecifyKind(timetable.EndTime!.Value, DateTimeKind.Utc);
                    await context.Set<Timetable>().AddAsync(timetable);
                }


                await context.SaveChangesAsync();
            }
        }
    }
}
