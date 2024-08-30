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
        public async Task<Response<int>> AddStudent(AddStudentRequest request)
        {
            // Find user in database
            var user = await _userManager.FindByIdAsync(request.UserId);
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

            // Check if the user is already a student in the school
            bool isUserStudentInSchool = school.Classes.Any(x => x.Students.Any(y => y.UserId == request.UserId));
            if (isUserStudentInSchool)
                return new() { Succeeded = false, Message = "User is already registered as a student in this school" };

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

        public async Task<Response<string>> Edit(int studentId, StudentDto studentDto)
        {
            if (studentId != studentDto.Id)
                return new() { Succeeded = false, Message = "Id mismatch in request" };

            var student = await _context.Students
                .Include(x => x.Class)
                .FirstOrDefaultAsync(x => x.Id == studentId);
            if (student == null)
                return new() { Succeeded = false, Message = "Couldn't find student" };

            try
            {
                if (student.ClassId == studentDto.Class.Id)
                    student.ClassId = studentDto.Class.Id;

                _context.Entry(student).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }

            return new() { Succeeded = true };
        }

        public async Task<IEnumerable<StudentDto>> GetAll(string? schoolId)
        {
            var students = await _context.Students
                .Include(x => x.User)
                .Include(x => x.Class)
                    .ThenInclude(x => x.School)
                .Include(x => x.Grades)
                .ToListAsync();

            if (schoolId != null)
            {
                try
                {
                    var filteredResults = students.Where(x => x.Class.SchoolId == new Guid(schoolId)).ToList();
                    return filteredResults.Adapt<List<StudentDto>>();
                }
                catch (Exception)
                {
                    return [];
                }
            }

            return students.Adapt<List<StudentDto>>();
        }

        public async Task<StudentDto?> GetById(int id)
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
