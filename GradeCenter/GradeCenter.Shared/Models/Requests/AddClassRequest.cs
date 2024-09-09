namespace GradeCenter.Shared.Models.Requests
{
    public class AddClassRequest
    {
        public string SchoolId { get; set; } = default!;
        public int Grade { get; set; }
        public string Signature { get; set; } = default!;
        public int? ClassTeacherId { get; set; }
    }
}
