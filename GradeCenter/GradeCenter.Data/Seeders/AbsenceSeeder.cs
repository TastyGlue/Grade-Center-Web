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
                new() { Id = new Guid("a1894a34-fae0-4188-8bec-1fd9a3a02da5"), StudentId = new Guid("0d74d377-f0fc-4cc2-882a-eb6228042782"), SubjectId = new Guid("c52dd128-ca75-4d9a-b57a-c558ac138051"), TimetableId = new Guid("10ee4505-3e2e-4836-b012-f059715c3234"), Date = new DateOnly(2023, 9, 25) },
                new() { Id = new Guid("bee6820f-1c3e-48e8-b0d7-46d1eb8acefa"), StudentId = new Guid("0d74d377-f0fc-4cc2-882a-eb6228042782"), SubjectId = new Guid("c8de0706-0674-46d4-a594-bf30effa2270"), TimetableId = new Guid("9f3d0db3-0503-4ca8-8d62-fccbbb8ee357"), Date = new DateOnly(2023, 9, 25) }
            };

            foreach (var absence in absences)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<Absence>().AnyAsync(x => x.Id == absence.Id);

                //Seed data if not already seeded
                if (!isExist)
                {
                    await context.Set<Absence>().AddAsync(absence);
                }


                await context.SaveChangesAsync();
            }
        }
    }
}
