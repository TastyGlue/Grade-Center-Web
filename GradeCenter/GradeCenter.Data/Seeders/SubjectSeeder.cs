
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
                new() { Id = new Guid(""), Name = "Mathematics", Signature = "MATH101" },
                new() { Id = new Guid(""), Name = "English Literature", Signature = "ENG201" },
                new() { Id = new Guid(""), Name = "Physics", Signature = "PHY301" },
                new() { Id = new Guid(""), Name = "History", Signature = "HIST401" },
                new() { Id = new Guid(""), Name = "Geography", Signature = "GEO501" }
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
