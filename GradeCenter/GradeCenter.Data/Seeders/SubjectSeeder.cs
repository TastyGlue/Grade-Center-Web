
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
                new() { Id = new Guid("c52dd128-ca75-4d9a-b57a-c558ac138051"), Name = "Mathematics", Signature = "MATH101" },
                new() { Id = new Guid("c8de0706-0674-46d4-a594-bf30effa2270"), Name = "English Literature", Signature = "ENG201" },
                new() { Id = new Guid("1f752077-eb39-4ca0-8169-45d18b065c63"), Name = "Physics", Signature = "PHY301" },
                new() { Id = new Guid("e881cccd-a5e6-4373-b6fb-e37fa99b67ac"), Name = "History", Signature = "HIST401" },
                new() { Id = new Guid("1e578f4d-630c-4aab-8b04-ec5bb1d9ef67"), Name = "Geography", Signature = "GEO501" }
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
