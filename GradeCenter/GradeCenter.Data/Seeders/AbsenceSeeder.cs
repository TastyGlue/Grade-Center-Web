namespace GradeCenter.Data.Seeders
{
    public class AbsenceSeeder : IDataSeeder
    {
        public int Order => 13;

        public async Task Seed(DbContext context)
        {
            //Create data to be seeded
            var absences = new List<Absence>()
            {
                new() { Id = 1, StudentId = 1, SubjectId = new Guid("C52DD128-CA75-4D9A-B57A-C558AC138051"), Day = DayOfWeek.Monday , Time = DateTime.MinValue.AddHours(7).AddMinutes(45) },
                new() { Id = 2, StudentId = 1, SubjectId = new Guid("C8DE0706-0674-46D4-A594-BF30EFFA2270"), Day = DayOfWeek.Monday , Time = DateTime.MinValue.AddHours(7).AddMinutes(45) }
            };

            foreach (var absence in absences)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<Absence>().AnyAsync(x => x.Id == absence.Id);

                //Seed data if not already seeded
                if (!isExist)
                    await context.Set<Absence>().AddAsync(absence);

                await context.SaveChangesAsync();
            }
        }
    }
}
