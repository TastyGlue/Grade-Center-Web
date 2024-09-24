namespace GradeCenter.Data.Models
{
    public class StudentParent
    {
        [Required]
        public Guid StudentId { get; set; } = default!;
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = default!;

        [Required]
        public Guid ParentId { get; set; } = default!;
        [ForeignKey(nameof(ParentId))]
        public Parent Parent { get; set; } = default!;
    }
}
