
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
                new() { TeacherId = new Guid("d98e684b-43f7-43c8-bf76-e97c1aee8d65"), SubjectId = new Guid("1e578f4d-630c-4aab-8b04-ec5bb1d9ef67") },
                new() { TeacherId = new Guid("a8491d87-0649-40fd-948c-6f6d060b29e3"), SubjectId = new Guid("e881cccd-a5e6-4373-b6fb-e37fa99b67ac") },
                new() { TeacherId = new Guid("cce872aa-1310-4bc5-aefc-5839437d8a94"), SubjectId = new Guid("1f752077-eb39-4ca0-8169-45d18b065c63") },
                new() { TeacherId = new Guid("9d1a9d1f-ebc9-42de-ae37-a9b078667b0d"), SubjectId = new Guid("c8de0706-0674-46d4-a594-bf30effa2270") },
                new() { TeacherId = new Guid("74edb9f5-5b35-4cd8-9eaa-7dd201b664d8"), SubjectId = new Guid("c52dd128-ca75-4d9a-b57a-c558ac138051") }
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
