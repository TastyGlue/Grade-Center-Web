namespace GradeCenter.Data.Seeders
{
    public class UserSeeder : IDataSeeder
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserSeeder(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public int Order => 2;

        public async Task Seed(DbContext context)
        {
            //Add different users 
            await SeedUser(context, new Guid("edeb7970-01ce-4df5-a99d-70090d66379b"), new DateTime(1980, 3, 15), "123 Admin Lane, Capital City, Country", "Alice Johnson", "admin");
            await SeedUser(context, new Guid("a26c2c8a-d575-4c12-bb7b-e9af1b2e79b2"), new DateTime(1981, 4, 16), "58 Admin Avenue, Capital City, Country", "Brandon Clark", "admin");
            await SeedUser(context, new Guid("8e0d4063-0dd3-4359-b261-eba797ecdfe6"), new DateTime(1975, 7, 22), "45 School Drive, Education Town, Country", "Robert Thompson", "headmaster");
            await SeedUser(context, new Guid("e7782632-7525-482c-b0b1-64c1264c8c4a"), new DateTime(1972, 2, 14), "75 Palm Drive, Education Town, Country", "Chris Iverson", "headmaster");
            await SeedUser(context, new Guid("0d74d377-f0fc-4cc2-882a-eb6228042782"), new DateTime(2010, 6, 12), "678 Elm Street, Suburbia, Country", "Emily Parker", "student");
            await SeedUser(context, new Guid("2cb059ac-d7b3-42ac-a41e-f9660713773f"), new DateTime(2011, 4, 25), "12 Maple Avenue, Suburbia, Country", "Michael Brown", "student");
            await SeedUser(context, new Guid("70b90b3b-7cc4-4ce7-9313-0da8fc23f29f"), new DateTime(2009, 9, 30), "89 Oak Street, Suburbia, Country", "Sophia Green", "student");
            await SeedUser(context, new Guid("242cf21d-31ec-4dd7-bdcb-365b895e4ffe"), new DateTime(2010, 11, 5), "34 Pine Road, Suburbia, Country", "Joshua White", "student");
            await SeedUser(context, new Guid("5ba8cb31-c401-45e9-a401-e47748fda3c3"), new DateTime(2011, 1, 18), "56 Birch Lane, Suburbia, Country", "Olivia Davis", "student");
            await SeedUser(context, new Guid("bedc6441-508f-44e5-bb29-39187a8ebe17"), new DateTime(1982, 8, 19), "678 Elm Street, Suburbia, Country", "Jennifer Parker", "parent");
            await SeedUser(context, new Guid("72518f74-8e80-46b5-a928-8d1cdcb81e5e"), new DateTime(1984, 12, 3), "12 Maple Avenue, Suburbia, Country", "Daniel Brown", "parent");
            await SeedUser(context, new Guid("2932af1e-8299-455a-b9f6-ecd8d02ec187"), new DateTime(1981, 5, 7), "89 Oak Street, Suburbia, Country", "Laura Green", "parent");
            await SeedUser(context, new Guid("d98e684b-43f7-43c8-bf76-e97c1aee8d65"), new DateTime(1985, 9, 10), "123 Cedar Street, Education Town, Country", "Sarah Williams", "teacher");
            await SeedUser(context, new Guid("a8491d87-0649-40fd-948c-6f6d060b29e3"), new DateTime(1983, 3, 28), "456 Spruce Avenue, Education Town, Country", "James Wilson", "teacher");
            await SeedUser(context, new Guid("cce872aa-1310-4bc5-aefc-5839437d8a94"), new DateTime(1987, 6, 15), "789 Fir Road, Education Town, Country", "Patricia Harris", "teacher");
            await SeedUser(context, new Guid("9d1a9d1f-ebc9-42de-ae37-a9b078667b0d"), new DateTime(1982, 11, 20), "321 Walnut Lane, Education Town, Country", "Kevin Martinez", "teacher");
            await SeedUser(context, new Guid("74edb9f5-5b35-4cd8-9eaa-7dd201b664d8"), new DateTime(1988, 4, 4), "654 Chestnut Street, Education Town, Country", "Angela Clark", "teacher");
        }

        private async Task SeedUser(DbContext context, Guid userId, DateTime dateOfBirth, string address, string fullName, string roleName)
        {
            //Check if data is already seeded
            bool isExist = await context.Set<User>().AnyAsync(x => x.Id == userId);

            if (!isExist)
            {
                string username = fullName.Replace(" ", ".");
                string email = username + "@gmail.com";

                //Create data to be seeded
                var user = new User
                {
                    Id = userId,
                    UserName = username,
                    NormalizedUserName = username.ToUpper(),
                    DateOfBirth = DateTime.SpecifyKind(dateOfBirth, DateTimeKind.Utc),
                    Address = address,
                    FullName = fullName,
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    IsActive = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //Get password hash
                user.PasswordHash = _passwordHasher.HashPassword(user, "Passw0rd");
                await context.Set<User>().AddAsync(user);

                //Get role by roleName
                var role = await context.Set<IdentityRole<Guid>>().FirstOrDefaultAsync(x => x.NormalizedName == roleName.ToUpper());
                if (role != null)
                {
                    //Add user to role
                    context.Set<IdentityUserRole<Guid>>().Add(new IdentityUserRole<Guid>
                    {
                        UserId = user.Id,
                        RoleId = role.Id
                    });
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
