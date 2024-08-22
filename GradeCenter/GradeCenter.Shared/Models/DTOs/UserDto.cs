namespace GradeCenter.Shared.Models.DTOs
{
    public class UserDto
    {
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Picture { get; set; }
        public bool IsActive { get; set; }
    }
}
