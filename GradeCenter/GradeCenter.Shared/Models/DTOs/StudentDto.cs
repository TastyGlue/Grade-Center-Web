namespace GradeCenter.Shared.Models.DTOs
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public UserDto User { get; set; } = default!;
        public ClassDto? Class { get; set; }
        public List<ParentDto> Parents { get; set; } = [];
        public List<GradeDto> Grades { get; set; } = [];
        public List<AbsenceDto> Absences { get; set; } = [];
    }
}
