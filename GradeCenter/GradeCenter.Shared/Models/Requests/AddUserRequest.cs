namespace GradeCenter.Shared.Models.Requests
{
    public class AddUserRequest
    {
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; }
        public string Password { get; set; } = default!;
    }
}
