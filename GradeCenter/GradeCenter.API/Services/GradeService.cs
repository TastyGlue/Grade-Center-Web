namespace GradeCenter.API.Services
{
    public class GradeService : IGradeService
    {
        private readonly GradeCenterDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public GradeService(GradeCenterDbContext context, UserManager<User> userManager, ITokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<Response<Guid>> Add(AddGradeRequest request)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == request.StudentId);
            if (student == null)
                return new() { Succeeded = false, Message = $"Couldn't find student with Id {request.StudentId}" };

            var subject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == request.SubjectId);
            if (subject == null)
                return new() { Succeeded = false, Message = $"Couldn't find subject with Id {request.SubjectId}" };

            var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == request.TeacherId);
            if (teacher == null)
                return new() { Succeeded = false, Message = $"Couldn't find teacher with Id {request.TeacherId}" };

            try
            {
                var newGrade = new Grade()
                {
                    StudentId = request.StudentId,
                    SubjectId = request.SubjectId,
                    TeacherId = request.TeacherId,
                    Result = request.Result,
                    Date = request.Date
                };

                await _context.Grades.AddAsync(newGrade);
                await _context.SaveChangesAsync();

                return new() { Succeeded = true, ReturnValue = newGrade.Id };
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }
        }

        public async Task<Response<string>> Delete(Guid gradeId)
        {
            var grade = await _context.Grades.FirstOrDefaultAsync(x => x.Id == gradeId);
            if (grade == null)
                return new() { Succeeded = false, Message = $"Couldn't find grade with Id {gradeId}" };

            try
            {
                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();

                return new() { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }
        }

        public async Task<Response<string>> Edit(GradeDto gradeDto)
        {
            var grade = await _context.Grades.FirstOrDefaultAsync(x => x.Id == gradeDto.Id);

            if (grade == null)
                return new() { Succeeded = false, Message = $"Couldn't find grade with Id {gradeDto.Id}" };

            grade.StudentId = gradeDto.Student.Id;
            grade.SubjectId = gradeDto.Subject.Id;
            grade.TeacherId = gradeDto.Teacher.Id;
            grade.Result = gradeDto.Result;
            grade.Date = gradeDto.Date;

            _context.Entry(grade).State = EntityState.Modified;

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

        public async Task<IEnumerable<GradeDto>> GetAll(Guid? subjectId, Guid? teacherId)
        {
            IQueryable<Grade> gradesQuery = _context.Grades
                .Include(x => x.Student)
                    .ThenInclude(x => x.User)
                .Include(x => x.Subject)
                .Include(x => x.Teacher)
                    .ThenInclude(x => x.User);

            if (subjectId != null)
                gradesQuery = gradesQuery.Where(x => x.SubjectId == subjectId);

            if (teacherId != null)
                gradesQuery = gradesQuery.Where(x => x.TeacherId == teacherId);

            var grades = await gradesQuery.ToListAsync();

            return grades.Adapt<List<GradeDto>>();
        }

        public async Task<GradeDto?> GetById(Guid gradeId)
        {
            var grade = await _context.Grades
                .Include(x => x.Student)
                    .ThenInclude(x => x.User)
                .Include(x => x.Subject)
                .Include(x => x.Teacher)
                    .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == gradeId);

            return grade?.Adapt<GradeDto>();
        }
    }
}
