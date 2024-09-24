namespace GradeCenter.Shared.Models.Requests
{
    public class AddHeadmasterRequest
    {
        public Guid UserId { get; set; }
        public Guid SchoolId { get; set; }
    }
}
