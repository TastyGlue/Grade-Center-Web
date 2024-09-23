
using GradeCenter.Data.Models;
using GradeCenter.Shared.Models.DTOs;

namespace GradeCenter.API.Services
{
    public class TimetableService : ITimetableService
    {
        private readonly GradeCenterDbContext _context;

        public TimetableService(GradeCenterDbContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> Add(AddTimetableRequest request)
        {
            var classObj = await _context.Classes.FirstOrDefaultAsync(x => x.Id == request.ClassId);
            if (classObj == null)
                return new() { Succeeded = false, Message = $"Couldn't find class with Id {request.ClassId}" };

            var subject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == request.SubjectId);
            if (subject == null)
                return new() { Succeeded = false, Message = $"Couldn't find subject with Id {request.SubjectId}" };

            var teacher = await _context.Teachers
                .Include(x => x.TeacherSubjects)
                    .ThenInclude(x => x.Subject)
                .FirstOrDefaultAsync(x => x.Id == request.TeacherId);
            if (teacher == null)
                return new() { Succeeded = false, Message = $"Couldn't find teacher with Id {request.TeacherId}" };

            var teacherSubjects = teacher.TeacherSubjects.Select(x => x.Subject).ToList();
            var isSubjectInTeacherSubjects = teacherSubjects.Any(x => x.Id == subject.Id);
            if (!isSubjectInTeacherSubjects)
                return new() { Succeeded = false, Message = "Mismatch between teacher and subject" };

            try
            {
                var newTimetable = new Timetable()
                {
                    ClassId = request.ClassId,
                    SubjectId = request.SubjectId,
                    TeacherId = request.TeacherId,
                    Year = request.Year,
                    Day = request.Day,
                    StartTime = request.StartTime?.ToUniversalTime(),
                    EndTime = request.EndTime?.ToUniversalTime()
                };

                await _context.Timetables.AddAsync(newTimetable);
                await _context.SaveChangesAsync();

                return new() { Succeeded = true, ReturnValue = newTimetable.Id };
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }
        }

        public async Task<Response<string>> Delete(int timetableId)
        {
            var timetable = await _context.Timetables.FirstOrDefaultAsync(x => x.Id == timetableId);
            if (timetable == null)
                return new() { Succeeded = false, Message = $"Couldn't find timetable item with Id {timetableId}" };

            var isLinkedAbsences = await _context.Absences.AnyAsync(x => x.TimetableId == timetableId);
            if (isLinkedAbsences)
                return new() { Succeeded = false, Message = "Cannot delete this timetable item because there is an absence linked to it" };

            try
            {
                _context.Timetables.Remove(timetable);
                await _context.SaveChangesAsync();

                return new() { Succeeded = true };
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }
        }

        public async Task<Response<string>> Edit(TimetableDto timetableDto)
        {
            var timetable = await _context.Timetables.FirstOrDefaultAsync(x => x.Id == timetableDto.Id);
            if (timetable == null)
                return new() { Succeeded = false, Message = $"Couldn't find absence with Id {timetableDto.Id}" };

            timetable.ClassId = timetableDto.Class.Id;
            timetable.SubjectId = timetableDto.Subject.Id;
            timetable.TeacherId = timetableDto.Teacher.Id;
            timetable.Year = timetableDto.Year;
            timetable.Day = timetableDto.Day;
            timetable.StartTime = timetableDto.StartTime;
            timetable.EndTime = timetableDto.EndTime;

            _context.Entry(timetable).State = EntityState.Modified;

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

        public async Task<IEnumerable<TimetableDto>> GetAll(Guid? classId, int? teacherId)
        {
            IQueryable<Timetable> timetablesQuery = _context.Timetables
                .Include(x => x.Class)
                .Include(x => x.Subject)
                .Include(x => x.Teacher)
                    .ThenInclude(x => x.User);

            if (classId != null)
                timetablesQuery = timetablesQuery.Where(x => x.ClassId == classId);

            if (teacherId != null)
                timetablesQuery = timetablesQuery.Where(x => x.TeacherId == teacherId);

            var timetables = await timetablesQuery.ToListAsync();

            return timetables.Adapt<List<TimetableDto>>();
        }

        public async Task<TimetableDto?> GetById(int timetableId)
        {
            var timetable = await _context.Timetables
                .Include(x => x.Class)
                .Include(x => x.Subject)
                .Include(x => x.Teacher)
                    .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == timetableId);

            return timetable?.Adapt<TimetableDto>();
        }
    }
}
