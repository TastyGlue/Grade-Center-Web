using GradeCenter.Data.Models;

namespace GradeCenter.API.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly GradeCenterDbContext _context;

        public SubjectService(GradeCenterDbContext context)
        {
            _context = context;
        }

        public async Task<Response<Guid>> Add(AddSubjectRequest request)
        {
            var school = await _context.Schools.FirstOrDefaultAsync(x => x.Id == request.SchoolId);
            if (school == null)
                return new() { Succeeded = false, Message = $"Couldn't find school with Id {request.SchoolId}" };

            try
            {
                var newSubject = new Subject()
                {
                    Name = request.Name,
                    Signature = request.Signature,
                    SchoolId = request.SchoolId
                };

                await _context.Subjects.AddAsync(newSubject);
                await _context.SaveChangesAsync();

                return new() { Succeeded = true, ReturnValue = newSubject.Id };
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }
        }

        public async Task<Response<string>> Edit(SubjectDto subjectDto)
        {
            var subject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == subjectDto.Id);

            if (subject == null)
                return new() { Succeeded = false, Message = $"Couldn't find subject with Id {subjectDto.Id}" };

            subject.SchoolId = subjectDto.School.Id;
            subject.Name = subjectDto.Name;
            subject.Signature = subjectDto.Signature;

            _context.Entry(subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new() { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }
        }

        public async Task<IEnumerable<SubjectDto>> GetAll(Guid? schoolId)
        {
            IQueryable<Subject> subjectsQuery = _context.Subjects
                .Include(x => x.School)
                .Include(x => x.TeacherSubjects)
                    .ThenInclude(x => x.Teacher)
                        .ThenInclude(x => x.User);

            if (schoolId != null)
                subjectsQuery = subjectsQuery.Where(x => x.SchoolId == schoolId);

            var subjects = await subjectsQuery.ToListAsync();

            return subjects.Adapt<List<SubjectDto>>();
        }

        public async Task<SubjectDto?> GetById(Guid subjectId)
        {
            var subject = await _context.Subjects
                .Include(x => x.School)
                .Include(x => x.TeacherSubjects)
                    .ThenInclude(x => x.Teacher)
                        .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == subjectId);

            return subject?.Adapt<SubjectDto>();
        }
    }
}
