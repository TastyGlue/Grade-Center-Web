
using GradeCenter.Data.Models;

namespace GradeCenter.Data.Seeders
{
    public class TeacherSubjectSeeder : IDataSeeder
    {
        public int Order => 15;

        public async Task Seed(DbContext context)
        {
            //Create data to be seeded
            var teacherSubjects = new List<TeacherSubject>()
            {
                new() { TeacherId = 1, SubjectId = new Guid("1E578F4D-630C-4AAB-8B04-EC5BB1D9EF67") },
                new() { TeacherId = 2, SubjectId = new Guid("E881CCCD-A5E6-4373-B6FB-E37FA99B67AC") },
                new() { TeacherId = 3, SubjectId = new Guid("1F752077-EB39-4CA0-8169-45D18B065C63") },
                new() { TeacherId = 4, SubjectId = new Guid("C8DE0706-0674-46D4-A594-BF30EFFA2270") },
                new() { TeacherId = 5, SubjectId = new Guid("C52DD128-CA75-4D9A-B57A-C558AC138051") }
            };

            foreach (var teacherSubject in teacherSubjects)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<TeacherSubject>().AnyAsync(x => x.TeacherId == teacherSubject.TeacherId && x.SubjectId == teacherSubject.SubjectId);

                //Seed data if not already seeded
                if (!isExist)
                    await context.Set<TeacherSubject>().AddAsync(teacherSubject);

                await context.SaveChangesAsync();
            }
        }
    }
}
