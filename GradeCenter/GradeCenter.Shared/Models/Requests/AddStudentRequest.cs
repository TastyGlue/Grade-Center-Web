namespace GradeCenter.Shared.Models.Requests
{
    public class AddStudentRequest
    {
        public Guid UserId { get; set; }
        public Guid SchoolId { get; set; }
        public Guid ClassId { get; set; }
    }
}
