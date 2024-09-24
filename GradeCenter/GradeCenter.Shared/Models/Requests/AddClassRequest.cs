namespace GradeCenter.Shared.Models.Requests
{
    public class AddClassRequest
    {
        public Guid SchoolId { get; set; } = default!;
        public int Grade { get; set; }
        public string Signature { get; set; } = default!;
        public Guid? ClassTeacherId { get; set; }
    }
}
