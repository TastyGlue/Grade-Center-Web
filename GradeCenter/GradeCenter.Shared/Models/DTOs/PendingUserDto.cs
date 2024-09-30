namespace GradeCenter.Shared.Models.DTOs
{
    public class PendingUserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; } = default!;
        public Roles Role { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
