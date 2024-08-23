namespace GradeCenter.Shared.Models.Requests
{
    public class AddHeadmasterRequest
    {
        public string UserId { get; set; } = default!;
        public Guid SchoolId { get; set; }
    }
}
