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
            await SeedUser(context, new Guid("EDEB7970-01CE-4DF5-A99D-70090D66379B"), new DateOnly(1980, 3, 15), "123 Admin Lane, Capital City, Country", "Alice Johnson", "admin");
            await SeedUser(context, new Guid("8E0D4063-0DD3-4359-B261-EBA797ECDFE6"), new DateOnly(1975, 7, 22), "45 School Drive, Education Town, Country", "Robert Thompson", "headmaster");
            await SeedUser(context, new Guid("0D74D377-F0FC-4CC2-882A-EB6228042782"), new DateOnly(2010, 6, 12), "678 Elm Street, Suburbia, Country", "Emily Parker", "student");
            await SeedUser(context, new Guid("2CB059AC-D7B3-42AC-A41E-F9660713773F"), new DateOnly(2011, 4, 25), "12 Maple Avenue, Suburbia, Country", "Michael Brown", "student");
            await SeedUser(context, new Guid("70B90B3B-7CC4-4CE7-9313-0DA8FC23F29F"), new DateOnly(2009, 9, 30), "89 Oak Street, Suburbia, Country", "Sophia Green", "student");
            await SeedUser(context, new Guid("242CF21D-31EC-4DD7-BDCB-365B895E4FFE"), new DateOnly(2010, 11, 5), "34 Pine Road, Suburbia, Country", "Joshua White", "student");
            await SeedUser(context, new Guid("5BA8CB31-C401-45E9-A401-E47748FDA3C3"), new DateOnly(2011, 1, 18), "56 Birch Lane, Suburbia, Country", "Olivia Davis", "student");
            await SeedUser(context, new Guid("BEDC6441-508F-44E5-BB29-39187A8EBE17"), new DateOnly(1982, 8, 19), "678 Elm Street, Suburbia, Country", "Jennifer Parker", "parent");
            await SeedUser(context, new Guid("72518F74-8E80-46B5-A928-8D1CDCB81E5E"), new DateOnly(1984, 12, 3), "12 Maple Avenue, Suburbia, Country", "Daniel Brown", "parent");
            await SeedUser(context, new Guid("2932AF1E-8299-455A-B9F6-ECD8D02EC187"), new DateOnly(1981, 5, 7), "89 Oak Street, Suburbia, Country", "Laura Green", "parent");
            await SeedUser(context, new Guid("D98E684B-43F7-43C8-BF76-E97C1AEE8D65"), new DateOnly(1985, 9, 10), "123 Cedar Street, Education Town, Country", "Sarah Williams", "teacher");
            await SeedUser(context, new Guid("A8491D87-0649-40FD-948C-6F6D060B29E3"), new DateOnly(1983, 3, 28), "456 Spruce Avenue, Education Town, Country", "James Wilson", "teacher");
            await SeedUser(context, new Guid("CCE872AA-1310-4BC5-AEFC-5839437D8A94"), new DateOnly(1987, 6, 15), "789 Fir Road, Education Town, Country", "Patricia Harris", "teacher");
            await SeedUser(context, new Guid("9D1A9D1F-EBC9-42DE-AE37-A9B078667B0D"), new DateOnly(1982, 11, 20), "321 Walnut Lane, Education Town, Country", "Kevin Martinez", "teacher");
            await SeedUser(context, new Guid("74EDB9F5-5B35-4CD8-9EAA-7DD201B664D8"), new DateOnly(1988, 4, 4), "654 Chestnut Street, Education Town, Country", "Angela Clark", "teacher");
        }

        private async Task SeedUser(DbContext context, Guid userId, DateOnly dateOfBirth, string address, string fullName, string roleName)
        {
            //Check if data is already seeded
            bool isExist = await context.Set<User>().AnyAsync(x => x.Id == userId.ToString());

            if (!isExist)
            {
                string username = fullName.Replace(" ", ".");
                string email = username + "@gmail.com";

                //Create data to be seeded
                var user = new User
                {
                    Id = userId.ToString(),
                    UserName = username,
                    NormalizedUserName = username.ToUpper(),
                    DateOfBirth = dateOfBirth,
                    Address = address,
                    FullName = fullName,
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    IsActive = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //Get password hash
                user.PasswordHash = _passwordHasher.HashPassword(user, "password");
                await context.Set<User>().AddAsync(user);

                //Get role by roleName
                var role = await context.Set<IdentityRole>().FirstOrDefaultAsync(x => x.NormalizedName == roleName.ToUpper());
                if (role != null)
                {
                    //Add user to role
                    context.Set<IdentityUserRole<string>>().Add(new IdentityUserRole<string>
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
