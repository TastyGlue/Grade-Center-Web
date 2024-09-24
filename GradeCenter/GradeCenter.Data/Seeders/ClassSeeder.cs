namespace GradeCenter.Data.Seeders
{
    public class ClassSeeder : IDataSeeder
    {
        public int Order => 8;

        public async Task Seed(DbContext context)
        {
            //Create data to be seeded
            var classes = new List<Class>()
            {
                new() { Id = new Guid("52761d50-5812-4fe4-a800-8fd26e6d6662"), SchoolId = new Guid("5e99ac10-fb03-4305-8022-c68a632f118f"), Grade = 8, Signature = "A", ClassTeacherId = new Guid("d98e684b-43f7-43c8-bf76-e97c1aee8d65") }
            };

            foreach (var classObj in classes)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<Class>().AnyAsync(x => x.Id == classObj.Id);

                //Seed data if not already seeded
                if (!isExist)
                    await context.Set<Class>().AddAsync(classObj);

                await context.SaveChangesAsync();
            }
        }
    }
}
