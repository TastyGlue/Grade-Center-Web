namespace GradeCenter.API.Services
{
    public class StudentService : IStudentService
    {
        private readonly UserManager<User> _userManager;
        private readonly GradeCenterDbContext _context;

        public StudentService(UserManager<User> userManager, GradeCenterDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<Response<Guid>> AddStudent(AddStudentRequest request)
        {
            // Find user in database
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return new() { Succeeded = false, Message = "Couldn't find user" };

            // Find class in database
            var classObj = await _context.Classes.Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == request.ClassId);
            if (classObj == null)
                return new() { Succeeded = false, Message = "Couldn't find class" };

            // Find class in database
            var school = await _context.Schools
                .Include(x => x.Classes)
                    .ThenInclude(x => x.Students)
                .FirstOrDefaultAsync(x => x.Id == request.SchoolId);
            if (school == null)
                return new() { Succeeded = false, Message = "Couldn't find school" };

            // Add user to role
            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole.Count > 0)
                return new() { Succeeded = false, Message = "User already has a role" };

            var addResult = await _userManager.AddToRoleAsync(user, "STUDENT");
            if (!addResult.Succeeded)
                return new() { Succeeded = false, Message = "Couldn't add user to \"Student\"" };

            var newStudent = new Student()
            {
                User = user,
                Class = classObj
            };

            try
            {
                await _context.Students.AddAsync(newStudent);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }

            return new() { Succeeded = true, ReturnValue = newStudent.Id };
        }

        public async Task<Response<string>> Edit(StudentDto studentDto)
        {
            var student = await _context.Students
                .Include(x => x.Class)
                .FirstOrDefaultAsync(x => x.Id == studentDto.Id);
            if (student == null)
                return new() { Succeeded = false, Message = "Couldn't find student" };

            try
            {
                if (student.ClassId != studentDto.Class?.Id)
                    student.ClassId = studentDto.Class?.Id;

                _context.Entry(student).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }

            return new() { Succeeded = true };
        }

        public async Task<IEnumerable<StudentDto>> GetAll(Guid? schoolId)
        {
            IQueryable<Student> studentsQuery = _context.Students
                .Include(x => x.User)
                .Include(x => x.Class)
                    .ThenInclude(x => x.School)
                .Include(x => x.Grades);

            if (schoolId != null)
                studentsQuery = studentsQuery.Where(x => x.Class != null && x.Class.SchoolId == schoolId);

            var students = await studentsQuery.ToListAsync();

            return students.Adapt<List<StudentDto>>();
        }

        public async Task<StudentDto?> GetById(Guid id)
        {
            var student = await _context.Students
                .Include(x => x.User)
                .Include(x => x.Class)
                .Include(x => x.Grades)
                .FirstOrDefaultAsync(x => x.Id == id);

            return student?.Adapt<StudentDto>();
        }
    }
}
