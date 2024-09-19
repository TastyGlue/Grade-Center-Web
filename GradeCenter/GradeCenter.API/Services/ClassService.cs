using GradeCenter.Data.Models;

namespace GradeCenter.API.Services
{
    public class ClassService : IClassService
    {
        private readonly GradeCenterDbContext _context;

        public ClassService(GradeCenterDbContext context)
        {
            _context = context;
        }

        public async Task<Response<Guid>> Add(AddClassRequest request)
        {
            var school = await _context.Schools.FirstOrDefaultAsync(x => x.Id == request.SchoolId);
            if (school == null)
                return new() { Succeeded = false, Message = $"Couldn't find school with Id {request.SchoolId}" };

            if (request.Grade < 1 || request.Grade > 12)
                return new() { Succeeded = false, Message = "Grade must have a value between 1 and 12" };

            if (request.ClassTeacherId != null)
            {
                var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == request.ClassTeacherId);
                if (teacher == null)
                    return new() { Succeeded = false, Message = $"Couldn't find teacher with Id {request.ClassTeacherId}" };
            }

            bool isGradeAndSignatureExist = await _context.Classes
                .AnyAsync(x => 
                    x.Grade == request.Grade 
                    && x.Signature.Equals(request.Signature, StringComparison.OrdinalIgnoreCase)
                    && x.SchoolId == request.SchoolId);

            if (isGradeAndSignatureExist)
                return new() { Succeeded = false, Message = "This class already exists" };

            try
            {
                var newClass = new Class()
                {
                    School = school,
                    Grade = request.Grade,
                    Signature = request.Signature,
                    ClassTeacherId = request.ClassTeacherId
                };

                await _context.Classes.AddAsync(newClass);
                await _context.SaveChangesAsync();

                return new() { Succeeded = true, ReturnValue = newClass.Id };
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }
        }

        public async Task<Response<string>> Delete(Guid classId)
        {
            var classObj = await _context.Classes
                .Include(x => x.Students)
                .FirstOrDefaultAsync(x => x.Id == classId);
            if (classObj == null)
                return new() { Succeeded = false, Message = $"Couldn't find class with Id {classId}" };

            var studentsInClass = classObj.Students.ToList();

            try
            {
                foreach (var student in studentsInClass)
                {
                    student.ClassId = null;
                    _context.Entry(student).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();

                _context.Classes.Remove(classObj);
                await _context.SaveChangesAsync();

                return new() { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }
        }

        public async Task<Response<string>> Edit(ClassDto classDto)
        {
            var classObj = await _context.Classes
                .Include(x => x.Students)
                .FirstOrDefaultAsync(x => x.Id == classDto.Id);

            if (classObj == null)
                return new() { Succeeded = false, Message = $"Couldn't find class with Id {classDto.Id}" };

            classObj.SchoolId = classDto.School.Id;
            classObj.Grade = classDto.Grade;
            classObj.Signature = classDto.Signature;
            classObj.ClassTeacherId = classDto.ClassTeacher?.Id;

            var editStudentsResult = await EditClassStudents(classObj, classDto.Students);

            if (!editStudentsResult.Succeeded)
                return editStudentsResult;

            await _context.SaveChangesAsync();

            return new() { Succeeded = true };
        }

        public async Task<IEnumerable<ClassDto>> GetAll(string? schoolId)
        {
            var classes = await _context.Classes
                .Include(x => x.School)
                    .ThenInclude(x => x.Headmasters)
                        .ThenInclude(x => x.User)
                .Include(x => x.Students)
                    .ThenInclude(x => x.User)
                .Include(x => x.ClassTeacher)
                    .ThenInclude(x => x.User)
                .ToListAsync();

            if (schoolId != null)
            {
                classes = classes.Where(x => x.SchoolId.ToString() == schoolId).ToList();
            }

            return classes.Adapt<List<ClassDto>>();
        }

        public async Task<ClassDto?> GetById(Guid classId)
        {
            var classObj = await _context.Classes
                .Include(x => x.School)
                    .ThenInclude(x => x.Headmasters)
                        .ThenInclude(x => x.User)
                .Include(x => x.Students)
                    .ThenInclude(x => x.User)
                .Include(x => x.ClassTeacher)
                    .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == classId);

            return classObj?.Adapt<ClassDto>();
        }

        private async Task<Response<string>> EditClassStudents(Class classObj, List<StudentDto> newStudents)
        {
            try
            {
                var currentStudentsIds = classObj.Students.Select(x => x.Id).ToHashSet();
                var newStudentsIds = newStudents.Select(x => x.Id).ToHashSet();

                bool areStudentsEqual = currentStudentsIds.SetEquals(newStudentsIds);

                if (!areStudentsEqual)
                {
                    foreach (var student in classObj.Students)
                    {
                        student.ClassId = null;
                    }

                    var students = newStudents.Adapt<List<Student>>();

                    foreach (var student in students)
                    {
                        student.ClassId = classObj.Id;
                        _context.Entry(student).State = EntityState.Modified;
                    }

                    await _context.SaveChangesAsync();

                }
                    
                return new() { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }
        }
    }
}
