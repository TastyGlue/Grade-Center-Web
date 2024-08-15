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
                new() { Id = new Guid("52761D50-5812-4FE4-A800-8FD26E6D6662"), SchoolId = new Guid("5E99AC10-FB03-4305-8022-C68A632F118F"), Grade = 8, Signature = "A", ClassTeacherId = 1 }
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
