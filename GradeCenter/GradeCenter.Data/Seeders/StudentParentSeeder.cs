namespace GradeCenter.Data.Seeders
{
    public class StudentParentSeeder : IDataSeeder
    {
        public int Order => 14;

        public async Task Seed(DbContext context)
        {
            //Create data to be seeded
            var studentParents = new List<StudentParent>()
            {
                new() { StudentId = 1, ParentId = 1 },
                new() { StudentId = 2, ParentId = 2 },
                new() { StudentId = 3, ParentId = 3 }
            };

            foreach (var studentParent in studentParents)
            {
                //Check if data is already seeded
                bool isExist = await context.Set<StudentParent>().AnyAsync(x => x.StudentId == studentParent.StudentId && x.ParentId == studentParent.ParentId);

                //Seed data if not already seeded
                if (!isExist)
                    await context.Set<StudentParent>().AddAsync(studentParent);

                await context.SaveChangesAsync();
            }
        }
    }
}
