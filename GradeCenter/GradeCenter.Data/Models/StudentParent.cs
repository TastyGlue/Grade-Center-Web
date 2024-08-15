namespace GradeCenter.Data.Models
{
    public class StudentParent
    {
        [Required]
        public int StudentId { get; set; } = default!;
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = default!;

        [Required]
        public int ParentId { get; set; } = default!;
        [ForeignKey(nameof(ParentId))]
        public Parent Parent { get; set; } = default!;
    }
}
