namespace GradeCenter.Data.Models
{
    public class StudentParent
    {
        [Required]
        public string StudentId { get; set; } = default!;
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = default!;

        [Required]
        public string ParentId { get; set; } = default!;
        [ForeignKey(nameof(ParentId))]
        public Parent Parent { get; set; } = default!;
    }
}
