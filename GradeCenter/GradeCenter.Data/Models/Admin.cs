namespace GradeCenter.Data.Models
{
    public class Admin
    {
        [Key]
        [ForeignKey(nameof(User))]
        public Guid Id { get; set; }
        public User User { get; set; } = default!;
    }
}
