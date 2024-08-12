namespace GradeCenter.Data.Models
{
    public class StudentSubject
    {
        [Required]
        public string StudentId { get; set; } = default!;
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = default!;

        [Required]
        public string SubjectId { get; set; } = default!;
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = default!;
    }
}
