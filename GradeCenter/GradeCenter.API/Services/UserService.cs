namespace GradeCenter.API.Services
{
    public class UserService : IUserService
    {
        private readonly GradeCenterDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<User> userManager, GradeCenterDbContext context, ITokenService tokenService)
        {
            _userManager = userManager;
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<Response<Guid>> AddUser(AddUserRequest user)
        {
            // Check if someone is already registered with this email
            var isEmailExist = await _userManager.FindByEmailAsync(user.Email.ToUpper()) != null;
            if (isEmailExist)
                return new() { Succeeded = false, Message = "Email is already in use" };

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

            try
            {
                // Add new user to database
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                // Assign password to new user
                await _userManager.AddPasswordAsync(newUser, user.Password);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }

            return new() { Succeeded = true, ReturnValue = newUser.Id };
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

        public async Task<Response<string>> Edit(UserDto userDto, StringValues authHeader)
        {
            var token = _tokenService.GetTokenContentFromAuthHeader(authHeader);
            if (token == null)
                return new() { Succeeded = false, Message = "Authentication failed" };

            // Check if request came from the account owner or an admin
            if (userDto.Id != token.UserId && token.Role != Roles.ADMIN)
                return new() { Succeeded = false, Message = "Authentication failed" };

            // Check if user exists in the database
            var user = await _userManager.FindByIdAsync(userDto.Id.ToString());
            if (user == null)
                return new() { Succeeded = false, Message = "Couldn't find user" };

            // Attempt to change Email
            if (user.Email != userDto.Email)
            {
                string emailChangeToken = await _userManager.GenerateChangeEmailTokenAsync(user, userDto.Email);
                var result = await _userManager.ChangeEmailAsync(user, userDto.Email, emailChangeToken);

                if (!result.Succeeded)
                    return new() { Succeeded = false, Message = string.Join(", ", result.Errors) };

                user.Email = userDto.Email;
            }
            
            // Attempt to change Name
            if (user.FullName != userDto.FullName)
            {
                if (token.Role != Roles.ADMIN)
                    return new() { Succeeded = false, Message = "You don't have the permissions for this operation" };

                user.FullName = userDto.FullName;
            }

            // Attempt to change Date of Birth
            if (user.DateOfBirth != userDto.DateOfBirth)
            {
                if (token.Role != Roles.ADMIN)
                    return new() { Succeeded = false, Message = "You don't have the permissions for this operation" };

                user.DateOfBirth = userDto.DateOfBirth;
            }

            // Attempt to change Address
            if (user.Address != userDto.Address)
                user.Address = userDto.Address;

            // Attempty to change Profile Picture
            if (Convert.ToBase64String(user.Picture ?? []) != userDto.Picture)
                user.Picture = Convert.FromBase64String(userDto.Picture ?? string.Empty);

            // Attempt to change IsActive
            if (user.IsActive != userDto.IsActive && token.Role != Roles.ADMIN)
                user.IsActive = userDto.IsActive;

            // Save changes to database
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new() { Succeeded = false, Message = "Database error" };
            }

            return new() { Succeeded = true };
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _context.Users.ToListAsync();

            return users.Adapt<List<UserDto>>();
        }

        public async Task<UserDto?> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            return user?.Adapt<UserDto>();
        }

        public async Task<IEnumerable<UserDto>> GetUsersWithoutRoles()
        {
            // Get users that are not assigned roles
            var users = await _context.Users
                .Where(x => !_context.UserRoles.Any(y => y.UserId == x.Id))
                .ToListAsync();

            // Adapt User type to UserDto
            return users.Adapt<List<UserDto>>();
        }

        public async Task<Response<bool>> RemoveFromRole(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return new() { Succeeded = false, Message = "Couldn't find user" };

            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole.Count == 0)
                return new() { Succeeded = false, Message = "User doesn't have a role" };

            foreach (var role in userRole)
                await _userManager.RemoveFromRoleAsync(user, role.ToUpper());

            return new() { Succeeded = true };
        }
    }
}
