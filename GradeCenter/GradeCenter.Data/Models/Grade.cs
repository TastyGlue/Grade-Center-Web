namespace GradeCenter.Data.Models
{
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = default!;

        [Required]
        public Guid SubjectId { get; set; } = default!;
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = default!;

        [Required]
        public Guid TeacherId { get; set; } = default!;
        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; } = default!;

        [Required]
        public string Result { get; set; } = default!;

        public DateTime? Date { get; set; }
    }
}
