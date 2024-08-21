namespace GradeCenter.Shared.Models.Requests
{
    public class ChangePasswordRequest
    {
        public string UserId { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
    }
}
