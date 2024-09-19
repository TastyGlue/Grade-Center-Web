namespace GradeCenter.Data.Models
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = default!;

        [Required]
        [StringLength(10)]
        public string Signature { get; set; } = default!;

        [Required]
        public Guid SchoolId { get; set; }
        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; } = default!;

        public ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
    }
}
