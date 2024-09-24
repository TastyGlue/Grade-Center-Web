namespace GradeCenter.Shared.Models.DTOs
{
    public class HeadmasterDto
    {
        public Guid Id { get; set; }
        public UserDto User { get; set; } = default!;
        public SchoolDto School { get; set; } = default!;
    }
}
