namespace GradeCenter.Data.Models
{
    public class Absence
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = default!;

        [Required]
        public Guid SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = default!;

        [Required]
        public DayOfWeek Day { get; set; }

        public DateTime? Time { get; set; }
    }
}
