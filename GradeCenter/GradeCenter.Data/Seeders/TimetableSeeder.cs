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
                new() { Id = 1, ClassId = new Guid("52761D50-5812-4FE4-A800-8FD26E6D6662"), SubjectId = new Guid("C52DD128-CA75-4D9A-B57A-C558AC138051"), TeacherId = 5, Year = "2023/2024 Spring", Day = DayOfWeek.Monday , StartTime = DateTime.MinValue.AddHours(7).AddMinutes(45), EndTime = DateTime.MinValue.AddHours(8).AddMinutes(25) },
                new() { Id = 2, ClassId = new Guid("52761D50-5812-4FE4-A800-8FD26E6D6662"), SubjectId = new Guid("C8DE0706-0674-46D4-A594-BF30EFFA2270"), TeacherId = 4, Year = "2023/2024 Spring", Day = DayOfWeek.Monday , StartTime = DateTime.MinValue.AddHours(8).AddMinutes(30), EndTime = DateTime.MinValue.AddHours(9).AddMinutes(10) },
                new() { Id = 3, ClassId = new Guid("52761D50-5812-4FE4-A800-8FD26E6D6662"), SubjectId = new Guid("1F752077-EB39-4CA0-8169-45D18B065C63"), TeacherId = 3, Year = "2023/2024 Spring", Day = DayOfWeek.Tuesday , StartTime = DateTime.MinValue.AddHours(7).AddMinutes(45), EndTime = DateTime.MinValue.AddHours(8).AddMinutes(25) },
                new() { Id = 4, ClassId = new Guid("52761D50-5812-4FE4-A800-8FD26E6D6662"), SubjectId = new Guid("E881CCCD-A5E6-4373-B6FB-E37FA99B67AC"), TeacherId = 2, Year = "2023/2024 Spring", Day = DayOfWeek.Tuesday , StartTime = DateTime.MinValue.AddHours(8).AddMinutes(30), EndTime = DateTime.MinValue.AddHours(9).AddMinutes(10) },
                new() { Id = 5, ClassId = new Guid("52761D50-5812-4FE4-A800-8FD26E6D6662"), SubjectId = new Guid("1E578F4D-630C-4AAB-8B04-EC5BB1D9EF67"), TeacherId = 1, Year = "2023/2024 Spring", Day = DayOfWeek.Wednesday , StartTime = DateTime.MinValue.AddHours(7).AddMinutes(45), EndTime = DateTime.MinValue.AddHours(8).AddMinutes(25) },
                new() { Id = 6, ClassId = new Guid("52761D50-5812-4FE4-A800-8FD26E6D6662"), SubjectId = new Guid("C52DD128-CA75-4D9A-B57A-C558AC138051"), TeacherId = 5, Year = "2023/2024 Spring", Day = DayOfWeek.Wednesday , StartTime = DateTime.MinValue.AddHours(8).AddMinutes(30), EndTime = DateTime.MinValue.AddHours(9).AddMinutes(10) },
                new() { Id = 7, ClassId = new Guid("52761D50-5812-4FE4-A800-8FD26E6D6662"), SubjectId = new Guid("C8DE0706-0674-46D4-A594-BF30EFFA2270"), TeacherId = 4, Year = "2023/2024 Spring", Day = DayOfWeek.Thursday , StartTime = DateTime.MinValue.AddHours(7).AddMinutes(45), EndTime = DateTime.MinValue.AddHours(8).AddMinutes(25) },
                new() { Id = 8, ClassId = new Guid("52761D50-5812-4FE4-A800-8FD26E6D6662"), SubjectId = new Guid("1F752077-EB39-4CA0-8169-45D18B065C63"), TeacherId = 3, Year = "2023/2024 Spring", Day = DayOfWeek.Thursday , StartTime = DateTime.MinValue.AddHours(8).AddMinutes(30), EndTime = DateTime.MinValue.AddHours(9).AddMinutes(10) },
                new() { Id = 9, ClassId = new Guid("52761D50-5812-4FE4-A800-8FD26E6D6662"), SubjectId = new Guid("E881CCCD-A5E6-4373-B6FB-E37FA99B67AC"), TeacherId = 2, Year = "2023/2024 Spring", Day = DayOfWeek.Friday , StartTime = DateTime.MinValue.AddHours(7).AddMinutes(45), EndTime = DateTime.MinValue.AddHours(8).AddMinutes(25) },
                new() { Id = 10, ClassId = new Guid("52761D50-5812-4FE4-A800-8FD26E6D6662"), SubjectId = new Guid("1E578F4D-630C-4AAB-8B04-EC5BB1D9EF67"), TeacherId = 1, Year = "2023/2024 Spring", Day = DayOfWeek.Friday , StartTime = DateTime.MinValue.AddHours(8).AddMinutes(30), EndTime = DateTime.MinValue.AddHours(9).AddMinutes(10) }
            };

            foreach (var timetable in timetables)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<Timetable>().AnyAsync(x => x.Id == timetable.Id);

                //Seed data if not already seeded
                if (!isExist)
                    await context.Set<Timetable>().AddAsync(timetable);

                await context.SaveChangesAsync();
            }
        }
    }
}
