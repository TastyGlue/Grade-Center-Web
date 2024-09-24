namespace GradeCenter.Shared.Models.Requests
{
    public class AddTeacherRequest
    {
        public Guid UserId { get; set; }
        public Guid SchoolId { get; set; }
    }
}
