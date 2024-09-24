namespace GradeCenter.Data.Models
{
    public class Headmaster
    {
        [Key]
        [ForeignKey(nameof(User))]
        public Guid Id { get; set; }
        public User User { get; set; } = default!;

        [Required]
        public Guid SchoolId { get; set; }
        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; } = default!;
    }
}
