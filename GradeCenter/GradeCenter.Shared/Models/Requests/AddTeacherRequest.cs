namespace GradeCenter.Shared.Models.Requests
{
    public class AddTeacherRequest
    {
        public string UserId { get; set; } = default!;
        public Guid SchoolId { get; set; }
    }
}
