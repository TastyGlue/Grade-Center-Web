namespace GradeCenter.Data.Models
{
    public class Timetable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ClassId { get; set; } = default!;
        [ForeignKey(nameof(ClassId))]
        public Class Class { get; set; } = default!;

        [Required]
        public string SubjectId { get; set; } = default!;
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = default!;

        [Required]
        public string TeacherId { get; set; } = default!;
        [ForeignKey(nameof(TeacherId))]
        public Teacher Teacher { get; set; } = default!;

        [Required]
        [StringLength(10)]
        public string Year { get; set; } = default!;

        [Required]
        [StringLength(15)]
        public string DayOfWeek { get; set; } = default!;

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }
    }
}
