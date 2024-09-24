namespace GradeCenter.Shared.Models.DTOs
{
    public class TeacherDto
    {
        public Guid Id { get; set; }
        public UserDto User { get; set; } = default!;
        public SchoolDto School { get; set; } = default!;
        public List<SubjectDto> Subjects { get; set; } = [];
    }
}
