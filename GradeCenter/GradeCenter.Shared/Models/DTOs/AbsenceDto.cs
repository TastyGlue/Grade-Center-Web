namespace GradeCenter.Shared.Models.DTOs
{
    public class AbsenceDto
    {
        public int Id { get; set; }
        public StudentDto Student { get; set; } = default!;
        public SubjectDto Subject { get; set; } = default!;
        public TimetableDto Timetable { get; set; } = default!;
        public DateOnly Date { get; set; }
    }
}
