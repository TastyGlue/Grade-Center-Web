namespace GradeCenter.Shared.Models.Requests
{
    public class AddParentRequest
    {
        public Guid UserId { get; set; }
        public List<Guid> StudentIds { get; set; } = [];
    }
}
