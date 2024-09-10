namespace GradeCenter.API.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;
        private readonly GradeCenterDbContext _context;

        public AdminService(GradeCenterDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Response<int>> AddAdmin(string userId)
        {
            // Find user in database
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new() { Succeeded = false, Message = "Couldn't find user" };

            // Add user to role
            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole.Count > 0)
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRole);
                if (!removeResult.Succeeded)
                    return new() { Succeeded = false, Message = "Couldn't remove user's prior roles" };
            }

            var addResult = await _userManager.AddToRoleAsync(user, "ADMIN");
            if (!addResult.Succeeded)
                return new() { Succeeded = false, Message = "Couldn't add user to \"Admin\"" };

            var newAdmin = new Admin()
            {
                User = user
            };

            try
            {
                await _context.Admins.AddAsync(newAdmin);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }

            return new() { Succeeded = true, ReturnValue = newAdmin.Id };
        }

        public async Task<Response<string>> Edit(AdminDto adminDto)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(x => x.Id == adminDto.Id);
            if (admin == null)
                return new() { Succeeded = false, Message = "Couldn't find admin" };

            try
            {
                _context.Entry(admin).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }

            return new() { Succeeded = true };
        }

        public async Task<IEnumerable<AdminDto>> GetAll()
        {
            var admins = await _context.Admins
                .Include(x => x.User)
                .ToListAsync();

            return admins.Adapt<List<AdminDto>>();
        }

        public async Task<AdminDto?> GetById(int id)
        {
            var admin = await _context.Admins
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            return admin?.Adapt<AdminDto>();
        }
    }
}
