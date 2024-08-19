namespace GradeCenter.Shared.Models.DTOs
{
    public class AdminDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; } = default!;
    }
}
