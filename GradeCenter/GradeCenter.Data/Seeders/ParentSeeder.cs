namespace GradeCenter.Data.Seeders
{
    public class ParentSeeder : IDataSeeder
    {
        public int Order => 10;

        public async Task Seed(DbContext context)
        {
            //Create data to be seeded
            var parents = new List<Parent>()
            {
                new() { Id = 1, UserId = "BEDC6441-508F-44E5-BB29-39187A8EBE17" },
                new() { Id = 2, UserId = "72518F74-8E80-46B5-A928-8D1CDCB81E5E" },
                new() { Id = 3, UserId = "2932AF1E-8299-455A-B9F6-ECD8D02EC187" }
            };

            foreach (var parent in parents)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<Parent>().AnyAsync(x => x.Id == parent.Id);

                //Seed data if not already seeded
                if (!isExist)
                    await context.Set<Parent>().AddAsync(parent);

                await context.SaveChangesAsync();
            }
        }
    }
}
