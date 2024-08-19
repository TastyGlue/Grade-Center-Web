namespace GradeCenter.Shared.Models.DTOs
{
    public class AbsenceDto
    {
        public int Id { get; set; }
        public StudentDto Student { get; set; } = default!;
        public SubjectDto Subject { get; set; } = default!;
        public DayOfWeek Day { get; set; }
        public DateTime? Time { get; set; }
    }
}
