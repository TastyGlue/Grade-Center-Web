namespace GradeCenter.Shared.Models.DTOs
{
    public class SchoolDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public List<HeadmasterDto> Headmasters { get; set; } = [];
    }
}
