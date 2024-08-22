namespace GradeCenter.Shared.Models
{
    public class TokenContent
    {
        public string UserId { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public Roles Role { get; set; }
    }
}
