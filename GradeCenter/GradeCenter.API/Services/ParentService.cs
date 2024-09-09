namespace GradeCenter.API.Services
{
    public class ParentService : IParentService
    {
        private readonly UserManager<User> _userManager;
        private readonly GradeCenterDbContext _context;

        public ParentService(UserManager<User> userManager, GradeCenterDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<Response<int>> AddParent(AddParentRequest request)
        {
            // Find user in database
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return new() { Succeeded = false, Message = "Couldn't find user" };

            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole.Count > 0)
                return new() { Succeeded = false, Message = "User already has a role and can't be assigned to this role" };
            else
            {
                var result = await _userManager.AddToRoleAsync(user, "PARENT");
                if (!result.Succeeded)
                    return new() { Succeeded = false, Message = "There was an error adding user to the \"PARENT\"" };
            }

            Parent newParent = new()
            {
                User = user
            };

            try
            {
                await _context.Parents.AddAsync(newParent);
                await _context.SaveChangesAsync();

                foreach (var studentId in request.StudentIds)
                {
                    await _context.StudentParents.AddAsync(new()
                    {
                        StudentId = studentId,
                        ParentId = newParent.Id
                    });
                }
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }

            return new() { Succeeded = true, ReturnValue = newParent.Id };
        }

        public async Task<Response<string>> Edit(ParentDto parentDto)
        {
            var parent = await _context.Parents
                .Include(x => x.StudentParents)
                    .ThenInclude(x => x.Student)
                .FirstOrDefaultAsync(x => x.Id == parentDto.Id);

            if (parent == null)
                return new() { Succeeded = false, Message = "Couldn't find parent" };

            var currentStudents = parent.StudentParents.ToList();
            var editStudentsResult = await EditParentStudents(parent, parentDto.Students);
            if (!editStudentsResult.Succeeded)
                return editStudentsResult;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }

            return new() { Succeeded = true };
        }

        public async Task<IEnumerable<ParentDto>> GetAll()
        {
            var parents = await _context.Parents
                .Include(x => x.StudentParents)
                    .ThenInclude(x => x.Student)
                .ToListAsync();

            return parents.Adapt<List<ParentDto>>();
        }

        public async Task<ParentDto?> GetById(int id)
        {
            var parent = await _context.Parents
                .Include(x => x.StudentParents)
                    .ThenInclude(x => x.Student)
                .FirstOrDefaultAsync();

            return parent?.Adapt<ParentDto>();
        }

        private async Task<Response<string>> EditParentStudents(Parent parent, List<StudentDto> newStudents)
        {
            try
            {
                parent.StudentParents.Clear();

                var newStudentParents = new List<StudentParent>();
                foreach (var newStudent in newStudents)
                {
                    newStudentParents.Add(new()
                    {
                        StudentId = newStudent.Id,
                        ParentId = parent.Id
                    });
                }

                parent.StudentParents = newStudentParents;

                await _context.SaveChangesAsync();

                return new() { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }
        }
    }
}
