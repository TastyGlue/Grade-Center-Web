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

        public async Task<Response<int>> AddHeadmaster(AddHeadmasterRequest request)
        {
            // Find user in database
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return new() { Succeeded = false, Message = "Couldn't find user" };

            // Find school in database
            var school = await _context.Schools.Include(x => x.Headmasters).FirstOrDefaultAsync(x => x.Id == request.SchoolId);
            if (school == null)
                return new() { Succeeded = false, Message = "Couldn't find school" };

            // Check if the user is already a headmaster at the school
            bool isUserHeadmasterInSchool = school.Headmasters.Any(x => x.UserId == request.UserId);
            if (isUserHeadmasterInSchool)
                return new() { Succeeded = false, Message = "User is already registered as a headmaster in this school" };

            // Add user to role
            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole.Count > 0)
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRole);
                if (!removeResult.Succeeded)
                    return new() { Succeeded = false, Message = "Couldn't remove user's prior roles" };
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
                await _context.Headmasters.AddAsync(newHeadmaster);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }

            return new() { Succeeded = true, ReturnValue = newHeadmaster.Id };
        }

        public async Task<Response<string>> Edit(HeadmasterDto headmasterDto)
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

        public async Task<IEnumerable<HeadmasterDto>> GetAll(string? schoolId)
        {
            var headmasters = await _context.Headmasters
                .Include(x => x.User)
                .Include(x => x.School)
                .ToListAsync();

            if (schoolId != null)
            {
                try
                {
                    var filteredResult = headmasters.Where(x => x.SchoolId == new Guid(schoolId)).ToList();
                    return filteredResult.Adapt<List<HeadmasterDto>>();
                }
                catch (Exception)
                {
                    return [];
                }
            }

            return headmasters.Adapt<List<HeadmasterDto>>();
        }

        public async Task<HeadmasterDto?> GetById(int id)
        {
            var headmaster = await _context.Headmasters
                .Include(x => x.User)
                .Include(x => x.School)
                .FirstOrDefaultAsync();

            return headmaster?.Adapt<HeadmasterDto>();
        }
    }
}
