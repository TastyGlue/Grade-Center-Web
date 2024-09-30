namespace GradeCenter.API.Services
{
    public class AbsenceService : IAbsenceService
    {
        private readonly GradeCenterDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public AbsenceService(GradeCenterDbContext context, UserManager<User> userManager, ITokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<CustomResult<Guid>> Add(AddAbsenceRequest request)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == request.StudentId);
            if (student == null)
                return new() { Succeeded = false, Message = $"Couldn't find student with Id {request.StudentId}" };

            var subject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == request.SubjectId);
            if (subject == null)
                return new() { Succeeded = false, Message = $"Couldn't find subject with Id {request.SubjectId}" };

            var timetable = await _context.Timetables.FirstOrDefaultAsync(x => x.Id == request.TimetableId);
            if (timetable == null)
                return new() { Succeeded = false, Message = $"Couldn't find timetable item with Id {request.TimetableId}" };

            if (timetable.Day != request.Date.DayOfWeek)
                return new() { Succeeded = false, Message = "There is a mismatch between the timetable and the day of the absence being added" };

            if (!timetable.Year.Contains(request.Date.Year.ToString()))
                return new() { Succeeded = false, Message = "There is a mismatch between the timetable and the year of the absence being added" };

            try
            {
                var newAbsence = new Absence()
                {
                    StudentId = request.StudentId,
                    SubjectId = request.SubjectId,
                    TimetableId = request.TimetableId,
                    Date = request.Date
                };

                await _context.Absences.AddAsync(newAbsence);
                await _context.SaveChangesAsync();

                return new() { Succeeded = true, ReturnValue = newAbsence.Id };
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }
        }

        public async Task<CustomResult<string>> Delete(Guid absenceId)
        {
            var absence = await _context.Absences.FirstOrDefaultAsync(x => x.Id == absenceId);
            if (absence == null)
                return new() { Succeeded = false, Message = $"Couldn't find absence with Id {absenceId}" };

            try
            {
                _context.Absences.Remove(absence);
                await _context.SaveChangesAsync();

                return new() { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }
        }

        public async Task<CustomResult<string>> Edit(AbsenceDto absenceDto)
        {
            var absence = await _context.Absences.FirstOrDefaultAsync(x => x.Id == absenceDto.Id);

            if (absence == null)
                return new() { Succeeded = false, Message = $"Couldn't find absence with Id {absenceDto.Id}" };

            if (absenceDto.Timetable.Day != absenceDto.Date.DayOfWeek)
                return new() { Succeeded = false, Message = "There is a mismatch between the timetable and the day of the absence being added" };

            absence.StudentId = absenceDto.Student.Id;
            absence.SubjectId = absenceDto.Subject.Id;
            absence.TimetableId = absenceDto.Timetable.Id;
            absence.Date = absenceDto.Date;

            _context.Entry(absence).State = EntityState.Modified;

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

        public async Task<IEnumerable<AbsenceDto>> GetAll(Guid? studentId)
        {
            IQueryable<Absence> absencesQuery = _context.Absences
                .Include(x => x.Student)
                    .ThenInclude(x => x.User)
                .Include(x => x.Subject)
                .Include(x => x.Timetable)
                    .ThenInclude(x => x.Teacher)
                        .ThenInclude(x => x.User);

            if (studentId != null)
                absencesQuery = absencesQuery.Where(x => x.StudentId == studentId);

            var absences = await absencesQuery.ToListAsync();

            return absences.Adapt<List<AbsenceDto>>();
        }

        public async Task<AbsenceDto?> GetById(Guid absenceId)
        {
            var absence = await _context.Absences
                .Include(x => x.Student)
                    .ThenInclude(x => x.User)
                .Include(x => x.Subject)
                .Include(x => x.Timetable)
                    .ThenInclude(x => x.Teacher)
                        .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == absenceId);

            return absence?.Adapt<AbsenceDto>();
        }
    }
}
