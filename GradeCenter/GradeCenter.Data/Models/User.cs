namespace GradeCenter.Data.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; } = default!;
        public byte[]? Picture { get; set; } = null;
        public bool IsActive { get; set; }
    }
}
