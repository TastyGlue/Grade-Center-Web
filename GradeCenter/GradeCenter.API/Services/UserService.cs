
namespace GradeCenter.API.Services
{
    public class UserService : IUserService
    {
        private readonly GradeCenterDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager, GradeCenterDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<string> AddUser(AddUserRequest user)
        {
            // Check if someone is already registered with this email
            var isEmailExist = await _userManager.FindByEmailAsync(user.Email.ToUpper()) != null;
            if (isEmailExist)
                throw new Exception($"Email is already in use");

            var newUser = new User()
            {
                UserName = user.FullName.Replace(" ", "."),
                NormalizedUserName = user.FullName.Replace(" ", ".").ToUpper(),
                DateOfBirth = user.DateOfBirth,
                FullName = user.FullName,
                Email = user.Email,
                NormalizedEmail = user.Email.ToUpper(),
                IsActive = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // Add new user to database
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            // Assign password to new user
            await _userManager.AddPasswordAsync(newUser, user.Password);
            await _context.SaveChangesAsync();

            return newUser.Id;
        }

        public async Task<bool> ChangePassword(ChangePasswordRequest request)
        {
            // Find user by id
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return false;
            if (!user.IsActive)
                return false;

            // Change password and return the result
            var result = await _userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);
            return result.Succeeded;
        }
    }
}
