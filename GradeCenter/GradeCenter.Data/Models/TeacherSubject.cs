namespace GradeCenter.Data.Models
{
    public class TeacherSubject
    {
        [Required]
        public string TeacherId { get; set; } = default!;
        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; } = default!;

        [Required]
        public string SubjectId { get; set; } = default!;
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = default!;
    }
}
