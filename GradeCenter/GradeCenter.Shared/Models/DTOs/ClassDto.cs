namespace GradeCenter.Shared.Models.DTOs
{
    public class ClassDto
    {
        public Guid Id { get; set; }
        public SchoolDto School { get; set; } = default!;
        public int Grade { get; set; }
        public string Signature { get; set; } = default!;
        public TeacherDto? ClassTeacher { get; set; }
        public List<StudentDto> Students { get; set; } = [];
    }
}
