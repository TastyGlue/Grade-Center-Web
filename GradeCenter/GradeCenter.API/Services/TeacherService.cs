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

        public async Task<Response<int>> AddTeacher(AddTeacherRequest request)
        {
            // Find user in database
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return new() { Succeeded = false, Message = "Couldn't find user" };

            // Find school in database
            var school = await _context.Schools.Include(x => x.Teachers).FirstOrDefaultAsync(x => x.Id == request.SchoolId);
            if (school == null)
                return new() { Succeeded = false, Message = "Couldn't find school" };

            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole.Count > 0)
                return new() { Succeeded = false, Message = "User already has a role and can't be assigned to this role" };
            else
            {
                var result = await _userManager.AddToRoleAsync(user, "TEACHER");
                if (!result.Succeeded)
                    return new() { Succeeded = false, Message = "There was an error adding user to the \"TEACHER\"" };
            }

            // Check if the user is already a teacher at the school
            bool isUserTeacherInSchool = school.Teachers.Any(x => x.UserId == request.UserId);
            if (isUserTeacherInSchool)
                return new() { Succeeded = false, Message = "User is already registered as a teacher in this school" };

            var newTeacher = new Teacher()
            {
                User = user,
                School = school
            };

            try
            {
                await _context.Teachers.AddAsync(newTeacher);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }

            return new() { Succeeded = true, ReturnValue = newTeacher.Id };
        }

        public async Task<Response<string>> Edit(TeacherDto teacherDto)
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
            var editSubjectsResult = await EditTeacherSubjects(currentSubjects, teacherDto.Subjects, teacherDto.Id);
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

        public async Task<IEnumerable<TeacherDto>> GetAll(string? schoolId)
        {
            var teachers = await _context.Teachers
                .Include(x => x.User)
                .Include(x => x.TeacherSubjects)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.School)
                .ToListAsync();

            if (schoolId != null)
            {
                try
                {
                    var filteredResults = teachers.Where(x => x.SchoolId == new Guid(schoolId)).ToList();
                    return filteredResults.Adapt<List<TeacherDto>>();
                }
                catch (Exception)
                {
                    return [];
                }
            }

            return teachers.Adapt<List<TeacherDto>>();
        }

        public async Task<TeacherDto?> GetById(int id)
        {
            var teacher = await _context.Teachers
                .Include(x => x.User)
                .Include(x => x.TeacherSubjects)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.School)
                .FirstOrDefaultAsync(x => x.Id == id);

            return teacher?.Adapt<TeacherDto>();
        }

        private async Task<Response<string>> EditTeacherSubjects(List<TeacherSubject> currentSubjects, List<SubjectDto> newSubjects, int teacherId)
        {
            try
            {
                if (currentSubjects.Count > 0)
                {
                    List<Guid> removedSubjects = [];

                    foreach (var currentSubject in currentSubjects)
                    {
                        if (!newSubjects.Any(x => x.Id == currentSubject.SubjectId))
                        {
                            removedSubjects.Add(currentSubject.SubjectId);
                            _context.TeacherSubjects.Remove(currentSubject);
                        }
                    }

                    currentSubjects.RemoveAll(x => removedSubjects.Any(y => y == x.SubjectId));
                }


                foreach (var newSubject in newSubjects)
                {
                    if (!currentSubjects.Any(x => x.SubjectId == newSubject.Id))
                    {
                        await _context.TeacherSubjects.AddAsync(new()
                        {
                            TeacherId = teacherId,
                            SubjectId = newSubject.Id
                        });
                    }
                }

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
