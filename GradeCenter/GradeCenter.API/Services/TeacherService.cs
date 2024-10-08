﻿namespace GradeCenter.API.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly UserManager<User> _userManager;
        private readonly GradeCenterDbContext _context;

        public TeacherService(UserManager<User> userManager, GradeCenterDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<CustomResult<Guid>> AddTeacher(AddTeacherRequest request)
        {
            // Find user in database
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return new() { Succeeded = false, Message = "Couldn't find user" };

            // Find school in database
            var school = await _context.Schools.Include(x => x.Teachers).FirstOrDefaultAsync(x => x.Id == request.SchoolId);
            if (school == null)
                return new() { Succeeded = false, Message = "Couldn't find school" };

            // Add user to role
            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole.Count > 0)
            {
                if (!IsUserParentOnly(userRole))
                    return new() { Succeeded = false, Message = "User already has a role" };
            }

            var addResult = await _userManager.AddToRoleAsync(user, "TEACHER");
            if (!addResult.Succeeded)
                return new() { Succeeded = false, Message = "Couldn't add user to \"Teacher\"" };

            var newTeacher = new Teacher()
            {
                User = user,
                School = school
            };

            try
            {
                user.IsActive = true;
                _context.Entry(user).State = EntityState.Modified;

                await _context.Teachers.AddAsync(newTeacher);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }

            return new() { Succeeded = true, ReturnValue = newTeacher.Id };
        }

        public async Task<CustomResult<string>> Edit(TeacherDto teacherDto)
        {
            var teacher = await _context.Teachers
                .Include(x => x.TeacherSubjects)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.School)
                .FirstOrDefaultAsync(x => x.Id == teacherDto.Id);
            if (teacher == null)
                return new() { Succeeded = false, Message = "Couldn't find teacher" };

            if (teacher.SchoolId != teacherDto.School.Id)
                teacher.SchoolId = teacherDto.School.Id;

            var currentSubjects = teacher.TeacherSubjects.ToList();
            var editSubjectsResult = await EditTeacherSubjects(teacher, teacherDto.Subjects);
            if (!editSubjectsResult.Succeeded)
                return editSubjectsResult;

            try
            {
                _context.Entry(teacher).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }

            return new() { Succeeded = true };
        }

        public async Task<IEnumerable<TeacherDto>> GetAll(Guid? schoolId)
        {
            IQueryable<Teacher> teachersQuery = _context.Teachers
                .Include(x => x.User)
                .Include(x => x.TeacherSubjects)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.School);

            if (schoolId != null)
                teachersQuery = teachersQuery.Where(x => x.SchoolId == schoolId);

            var teachers = await teachersQuery.ToListAsync();

            return teachers.Adapt<List<TeacherDto>>();
        }

        public async Task<TeacherDto?> GetById(Guid id)
        {
            var teacher = await _context.Teachers
                .Include(x => x.User)
                .Include(x => x.TeacherSubjects)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.School)
                .FirstOrDefaultAsync(x => x.Id == id);

            return teacher?.Adapt<TeacherDto>();
        }

        private async Task<CustomResult<string>> EditTeacherSubjects(Teacher teacher, List<SubjectDto> newSubjects)
        {
            try
            {
                var currentSubjectsIds = teacher.TeacherSubjects.Select(x => x.SubjectId).ToHashSet();
                var newSubjectsIds = newSubjects.Select(x => x.Id).ToHashSet();

                bool areSubjectsEqual = currentSubjectsIds.SetEquals(newSubjectsIds);

                if (!areSubjectsEqual)
                {
                    teacher.TeacherSubjects.Clear();

                    var newTeacherSubjects = new List<TeacherSubject>();
                    foreach (var newSubject in newSubjects)
                    {
                        newTeacherSubjects.Add(new()
                        {
                            TeacherId = teacher.Id,
                            SubjectId = newSubject.Id
                        });
                    }

                    teacher.TeacherSubjects = newTeacherSubjects;

                    await _context.SaveChangesAsync(); 
                }

                return new() { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }
        }

        private static bool IsUserParentOnly(IList<string> roles)
        {
            if (roles.Count > 1)
                return false;

            return roles.First().Equals("parent", StringComparison.OrdinalIgnoreCase);
        }
    }
}
