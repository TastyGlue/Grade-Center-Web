namespace GradeCenter.Shared.Models.DTOs
{
    public class AdminDto
    {
        public Guid Id { get; set; }
        public UserDto User { get; set; } = default!;
    }
}
