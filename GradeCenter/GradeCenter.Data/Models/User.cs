namespace GradeCenter.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public byte[]? Picture { get; set; } = null;
        public bool IsActive { get; set; }
    }
}
