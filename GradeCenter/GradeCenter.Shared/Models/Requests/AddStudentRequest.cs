namespace GradeCenter.Shared.Models.Requests
{
    public class AddStudentRequest
    {
        public string UserId { get; set; } = default!;
        public Guid SchoolId { get; set; }
        public Guid ClassId { get; set; }
    }
}
