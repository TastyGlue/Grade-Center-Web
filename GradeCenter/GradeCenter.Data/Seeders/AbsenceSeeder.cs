using GradeCenter.Data.Models;

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
                new() { Id = 1, StudentId = 1, SubjectId = new Guid("c52dd128-ca75-4d9a-b57a-c558ac138051"), Day = DayOfWeek.Monday , Time = DateTime.MinValue.AddHours(7).AddMinutes(45) },
                new() { Id = 2, StudentId = 1, SubjectId = new Guid("c8de0706-0674-46d4-a594-bf30effa2270"), Day = DayOfWeek.Monday , Time = DateTime.MinValue.AddHours(7).AddMinutes(45) }
            };

            foreach (var absence in absences)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<Absence>().AnyAsync(x => x.Id == absence.Id);

                //Seed data if not already seeded
                if (!isExist)
                {
                    absence.Time = DateTime.SpecifyKind(absence.Time!.Value, DateTimeKind.Utc);
                    await context.Set<Absence>().AddAsync(absence);
                }


                await context.SaveChangesAsync();
            }
        }
    }
}
