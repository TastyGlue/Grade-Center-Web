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
                new() { Id = 1, UserId = "bedc6441-508f-44e5-bb29-39187a8ebe17" },
                new() { Id = 2, UserId = "72518f74-8e80-46b5-a928-8d1cdcb81e5e" },
                new() { Id = 3, UserId = "2932af1e-8299-455a-b9f6-ecd8d02ec187" }
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
