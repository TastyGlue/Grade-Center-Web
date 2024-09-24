namespace GradeCenter.Shared.Models.DTOs
{
    public class ParentDto
    {
        public Guid Id { get; set; }
        public UserDto User { get; set; } = default!;
        public List<StudentDto> Students { get; set; } = [];
    }
}
