namespace GradeCenter.Data.Models
{
    public class Absence
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = default!;

        [Required]
        public Guid SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = default!;

        [Required]
        public Guid TimetableId { get; set; }
        [ForeignKey(nameof(TimetableId))]
        public Timetable Timetable { get; set; } = default!;

        [Required]
        public DateOnly Date { get; set; }
    }
}
