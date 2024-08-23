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
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return new() { Succeeded = false, Message = "Couldn't find user"};

            var school = await _context.Schools.Include(x => x.Headmasters).FirstOrDefaultAsync(x => x.Id == request.SchoolId);
            if (school == null)
                return new() { Succeeded = false, Message = "Couldn't find school" };

            bool isUserHeadmasterInSchool = school.Headmasters.Any(x => x.UserId == request.UserId);
            if (isUserHeadmasterInSchool)
                return new() { Succeeded = false, Message = "User is already registered as a headmaster in a school" };

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

            return new() { Succeeded = true, ReturnValue = newHeadmaster.Id};
        }

        public async Task<Response<string>> Edit(int headmasterId, HeadmasterDto headmasterDto)
        {
            if (headmasterId != headmasterDto.Id)
                return new() { Succeeded = false, Message = "Id mismatch in request" };

            var headmaster = await _context.Headmasters.FirstOrDefaultAsync(x => x.Id == headmasterId);
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

            return new() { Succeeded = true }; ;
        }

        public async Task<IEnumerable<HeadmasterDto>> GetAll()
        {
            var headmasters = await _context.Headmasters
                .Include(x => x.User)
                .Include(x => x.School)
                .ToListAsync();

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
