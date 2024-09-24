namespace GradeCenter.Shared.Models
{
    public class TokenContent
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public Roles Role { get; set; }
    }
}
