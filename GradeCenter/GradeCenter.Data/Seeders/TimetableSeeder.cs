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
                new() { Id = 1, ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("c52dd128-ca75-4d9a-b57a-c558ac138051"), TeacherId = 5, Year = "2023/2024 Spring", Day = DayOfWeek.Monday , StartTime = DateTime.MinValue.AddHours(7).AddMinutes(45), EndTime = DateTime.MinValue.AddHours(8).AddMinutes(25) },
                new() { Id = 2, ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("c8de0706-0674-46d4-a594-bf30effa2270"), TeacherId = 4, Year = "2023/2024 Spring", Day = DayOfWeek.Monday , StartTime = DateTime.MinValue.AddHours(8).AddMinutes(30), EndTime = DateTime.MinValue.AddHours(9).AddMinutes(10) },
                new() { Id = 3, ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("1f752077-eb39-4ca0-8169-45d18b065c63"), TeacherId = 3, Year = "2023/2024 Spring", Day = DayOfWeek.Tuesday , StartTime = DateTime.MinValue.AddHours(7).AddMinutes(45), EndTime = DateTime.MinValue.AddHours(8).AddMinutes(25) },
                new() { Id = 4, ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("e881cccd-a5e6-4373-b6fb-e37fa99b67ac"), TeacherId = 2, Year = "2023/2024 Spring", Day = DayOfWeek.Tuesday , StartTime = DateTime.MinValue.AddHours(8).AddMinutes(30), EndTime = DateTime.MinValue.AddHours(9).AddMinutes(10) },
                new() { Id = 5, ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("1e578f4d-630c-4aab-8b04-ec5bb1d9ef67"), TeacherId = 1, Year = "2023/2024 Spring", Day = DayOfWeek.Wednesday , StartTime = DateTime.MinValue.AddHours(7).AddMinutes(45), EndTime = DateTime.MinValue.AddHours(8).AddMinutes(25) },
                new() { Id = 6, ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("c52dd128-ca75-4d9a-b57a-c558ac138051"), TeacherId = 5, Year = "2023/2024 Spring", Day = DayOfWeek.Wednesday , StartTime = DateTime.MinValue.AddHours(8).AddMinutes(30), EndTime = DateTime.MinValue.AddHours(9).AddMinutes(10) },
                new() { Id = 7, ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("c8de0706-0674-46d4-a594-bf30effa2270"), TeacherId = 4, Year = "2023/2024 Spring", Day = DayOfWeek.Thursday , StartTime = DateTime.MinValue.AddHours(7).AddMinutes(45), EndTime = DateTime.MinValue.AddHours(8).AddMinutes(25) },
                new() { Id = 8, ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("1f752077-eb39-4ca0-8169-45d18b065c63"), TeacherId = 3, Year = "2023/2024 Spring", Day = DayOfWeek.Thursday , StartTime = DateTime.MinValue.AddHours(8).AddMinutes(30), EndTime = DateTime.MinValue.AddHours(9).AddMinutes(10) },
                new() { Id = 9, ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("e881cccd-a5e6-4373-b6fb-e37fa99b67ac"), TeacherId = 2, Year = "2023/2024 Spring", Day = DayOfWeek.Friday , StartTime = DateTime.MinValue.AddHours(7).AddMinutes(45), EndTime = DateTime.MinValue.AddHours(8).AddMinutes(25) },
                new() { Id = 10, ClassId = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SubjectId = new Guid("1e578f4d-630c-4aab-8b04-ec5bb1d9ef67"), TeacherId = 1, Year = "2023/2024 Spring", Day = DayOfWeek.Friday , StartTime = DateTime.MinValue.AddHours(8).AddMinutes(30), EndTime = DateTime.MinValue.AddHours(9).AddMinutes(10) }
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
