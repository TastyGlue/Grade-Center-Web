namespace GradeCenter.Data.Models
{
    public class Parent
    {
        [Key]
        [ForeignKey(nameof(User))]
        public Guid Id { get; set; }
        public User User { get; set; } = default!;

        public ICollection<StudentParent> StudentParents { get; set; } = new List<StudentParent>();
    }
}
