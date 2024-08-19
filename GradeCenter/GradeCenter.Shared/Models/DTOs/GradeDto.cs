namespace GradeCenter.Shared.Models.DTOs
{
    public class GradeDto
    {
        public int Id { get; set; }
        public StudentDto Student { get; set; } = default!;
        public SubjectDto Subject { get; set; } = default!;
        public TeacherDto? Teacher { get; set; }
        public string Result { get; set; } = default!;
        public DateTime Date { get; set; }
    }
}
