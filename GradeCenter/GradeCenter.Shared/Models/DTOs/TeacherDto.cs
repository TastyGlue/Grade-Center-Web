namespace GradeCenter.Shared.Models.DTOs
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; } = default!;
        public SchoolDto School { get; set; } = default!;
        public List<SubjectDto> Subjects { get; set; } = [];
    }
}
