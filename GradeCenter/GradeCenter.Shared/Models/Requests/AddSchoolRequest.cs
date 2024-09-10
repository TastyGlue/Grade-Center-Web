namespace GradeCenter.Shared.Models.Requests
{
    public class AddSchoolRequest
    {
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public int? HeadmasterId { get; set; }
    }
}
