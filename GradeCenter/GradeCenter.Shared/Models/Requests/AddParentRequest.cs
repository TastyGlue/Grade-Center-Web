namespace GradeCenter.Shared.Models.Requests
{
    public class AddParentRequest
    {
        public string UserId { get; set; } = default!;
        public List<int> StudentIds { get; set; } = [];
    }
}
