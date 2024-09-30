namespace GradeCenter.Data.Models
{
    public class PendingUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; } = default!;
        public Roles Role { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
