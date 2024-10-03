using GradeCenter.Data.Models;

namespace GradeCenter.API.Services
{
    public class HeadmasterService : IHeadmasterService
    {
        private readonly UserManager<User> _userManager;
        private readonly GradeCenterDbContext _context;

        public HeadmasterService(UserManager<User> userManager, GradeCenterDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<CustomResult<Guid>> AddHeadmaster(AddHeadmasterRequest request)
        {
            // Find user in database
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return new() { Succeeded = false, Message = "Couldn't find user" };

            // Find school in database
            var school = await _context.Schools.Include(x => x.Headmasters).FirstOrDefaultAsync(x => x.Id == request.SchoolId);
            if (school == null)
                return new() { Succeeded = false, Message = "Couldn't find school" };

            // Add user to role
            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole.Count > 0)
            {
                if (!IsUserParentOnly(userRole))
                    return new() { Succeeded = false, Message = "User already has a role" };
            }

            var addResult = await _userManager.AddToRoleAsync(user, "HEADMASTER");
            if (!addResult.Succeeded)
                return new() { Succeeded = false, Message = "Couldn't add user to \"Headmaster\"" };

            var newHeadmaster = new Headmaster()
            {
                User = user,
                School = school
            };

            try
            {
                user.IsActive = true;
                _context.Entry(user).State = EntityState.Modified;

                await _context.Headmasters.AddAsync(newHeadmaster);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }

            return new() { Succeeded = true, ReturnValue = newHeadmaster.Id };
        }

        public async Task<CustomResult<string>> Edit(HeadmasterDto headmasterDto)
        {
            var headmaster = await _context.Headmasters.FirstOrDefaultAsync(x => x.Id == headmasterDto.Id);
            if (headmaster == null)
                return new() { Succeeded = false, Message = "Couldn't find headmaster" };

            if (headmaster.SchoolId != headmasterDto.School.Id)
                headmaster.SchoolId = headmasterDto.School.Id;

            try
            {
                _context.Entry(headmaster).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }

            return new() { Succeeded = true };
        }

        public async Task<IEnumerable<HeadmasterDto>> GetAll(Guid? schoolId)
        {
            IQueryable<Headmaster> headmastersQuery = _context.Headmasters
                .Include(x => x.User)
                .Include(x => x.School);

            if (schoolId != null)
                headmastersQuery = headmastersQuery.Where(x => x.SchoolId == schoolId);

            var headmasters = await headmastersQuery.ToListAsync();

            return headmasters.Adapt<List<HeadmasterDto>>();
        }

        public async Task<HeadmasterDto?> GetById(Guid id)
        {
            var headmaster = await _context.Headmasters
                .Include(x => x.User)
                .Include(x => x.School)
                .FirstOrDefaultAsync(x => x.Id == id);

            return headmaster?.Adapt<HeadmasterDto>();
        }

        private static bool IsUserParentOnly(IList<string> roles)
        {
            if (roles.Count > 1)
                return false;

            return roles.First().Equals("parent", StringComparison.OrdinalIgnoreCase);
        }
    }
}
