namespace GradeCenter.Shared.Models.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; } = default!;
        public SchoolDto School { get; set; } = default!;
        public ClassDto Class { get; set; } = default!;
        public List<ParentDto> Parents { get; set; } = [];
        public List<GradeDto> Grades { get; set; } = [];
        public List<AbsenceDto> Absences { get; set; } = [];
    }
}
