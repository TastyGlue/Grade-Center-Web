namespace GradeCenter.Shared.Models.DTOs
{
    public class SubjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Signature { get; set; } = default!;
        public SchoolDto School { get; set; } = default!;
        public List<TeacherDto> Teachers { get; set; } = [];
    }
}
