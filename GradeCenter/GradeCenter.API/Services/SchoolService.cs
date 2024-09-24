
namespace GradeCenter.API.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly GradeCenterDbContext _context;

        public SchoolService(GradeCenterDbContext context)
        {
            _context = context;
        }

        public async Task<Response<Guid>> Add(AddSchoolRequest request)
        {
            bool schoolExist = await _context.Schools.AnyAsync(x => x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));
            if (schoolExist)
                return new() { Succeeded = false, Message = $"There is already a school with that name" };

            Headmaster? headmaster = null;
            if (request.HeadmasterId != null)
            {
                headmaster = await _context.Headmasters.FirstOrDefaultAsync(x => x.Id == request.HeadmasterId);
                if (headmaster == null)
                    return new() { Succeeded = false, Message = $"Couldn't find headmaster with Id {request.HeadmasterId}" };
            }

            try
            {
                var newSchool = new School()
                {
                    Name = request.Name,
                    Address = request.Address
                };

                await _context.Schools.AddAsync(newSchool);
                if (headmaster != null)
                    newSchool.Headmasters.Add(headmaster);

                await _context.SaveChangesAsync();

                return new() { Succeeded = true, ReturnValue = newSchool.Id };
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }
        }

        public async Task<Response<string>> Delete(Guid schoolId)
        {
            try
            {
                var school = await _context.Schools.FirstOrDefaultAsync(x => x.Id == schoolId);
                if (school == null)
                    return new() { Succeeded = false, Message = $"Couldn't find school with Id {schoolId}" };

                _context.Schools.Remove(school);
                await _context.SaveChangesAsync();

                return new() { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }
        }

        public async Task<Response<string>> Edit(SchoolDto schoolDto)
        {
            var school = await _context.Schools.Include(x => x.Headmasters).FirstOrDefaultAsync(x => x.Id == schoolDto.Id);
            if (school == null)
                return new() { Succeeded = false, Message = $"Couldn't find school with Id {schoolDto.Id}" };

            bool isSchoolNameExist = await _context.Schools.AnyAsync(x => x.Id != schoolDto.Id && x.Name.Equals(schoolDto.Name, StringComparison.OrdinalIgnoreCase));
            if (isSchoolNameExist)
                return new() { Succeeded = false, Message = "There is already a school with that name" };

            school.Name = schoolDto.Name;
            school.Address = schoolDto.Address;

            await _context.SaveChangesAsync();
            return new() { Succeeded = true };
        }

        public async Task<IEnumerable<SchoolDto>> GetAll()
        {
            var schools = await _context.Schools
                .Include(x => x.Headmasters)
                    .ThenInclude(x => x.User)
                .ToListAsync();

            return schools.Adapt<List<SchoolDto>>();
        }

        public async Task<SchoolDto?> GetById(Guid schoolId)
        {
            var school = await _context.Schools
                .Include(x => x.Headmasters)
                    .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == schoolId);

            return school?.Adapt<SchoolDto>();
        }
    }
}
