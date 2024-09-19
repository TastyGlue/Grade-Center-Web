namespace GradeCenter.Shared.Models.Requests
{
    public class AddSubjectRequest
    {
        public string Name { get; set; } = default!;
        public string Signature { get; set; } = default!;
        public Guid SchoolId { get; set; }
    }
}
