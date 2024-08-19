namespace GradeCenter.Shared.Models.DTOs
{
    public class TimetableDto
    {
        public int Id { get; set; }
        public ClassDto Class { get; set; } = default!;
        public SubjectDto Subject { get; set; } = default!;
        public TeacherDto Teacher { get; set; } = default!;
        public string Year { get; set; } = default!;
        public DayOfWeek? MyProperty { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
