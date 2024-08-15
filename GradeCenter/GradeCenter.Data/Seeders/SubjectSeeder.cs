
using GradeCenter.Data.Models;

namespace GradeCenter.Data.Seeders
{
    public class SubjectSeeder : IDataSeeder
    {
        public int Order => 4;

        public async Task Seed(DbContext context)
        {
            //Create data to be seeded
            var subjects = new List<Subject>()
            {
                new() { Id = new Guid("C52DD128-CA75-4D9A-B57A-C558AC138051"), Name = "Mathematics", Signature = "MATH101" },
                new() { Id = new Guid("C8DE0706-0674-46D4-A594-BF30EFFA2270"), Name = "English Literature", Signature = "ENG201" },
                new() { Id = new Guid("1F752077-EB39-4CA0-8169-45D18B065C63"), Name = "Physics", Signature = "PHY301" },
                new() { Id = new Guid("E881CCCD-A5E6-4373-B6FB-E37FA99B67AC"), Name = "History", Signature = "HIST401" },
                new() { Id = new Guid("1E578F4D-630C-4AAB-8B04-EC5BB1D9EF67"), Name = "Geography", Signature = "GEO501" }
            };

            foreach (var subject in subjects)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<Subject>().AnyAsync(x => x.Id == subject.Id);

                //Seed data if not already seeded
                if (!isExist)
                    await context.Set<Subject>().AddAsync(subject);

                await context.SaveChangesAsync();
            }
        }
    }
}
