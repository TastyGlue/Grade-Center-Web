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
                new() { StudentId = new Guid("0d74d377-f0fc-4cc2-882a-eb6228042782"), ParentId = new Guid("bedc6441-508f-44e5-bb29-39187a8ebe17") },
                new() { StudentId = new Guid("2cb059ac-d7b3-42ac-a41e-f9660713773f"), ParentId = new Guid("72518f74-8e80-46b5-a928-8d1cdcb81e5e") },
                new() { StudentId = new Guid("70b90b3b-7cc4-4ce7-9313-0da8fc23f29f"), ParentId = new Guid("2932af1e-8299-455a-b9f6-ecd8d02ec187") }
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
