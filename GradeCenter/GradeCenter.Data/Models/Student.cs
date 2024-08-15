namespace GradeCenter.Data.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = default!;
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = default!;

        [Required]
        public Guid SchoolId { get; set; }
        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; } = default!;

        [Required]
        public Guid ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public Class Class { get; set; } = default!;

        public ICollection<StudentParent> StudentParents { get; set; } = new List<StudentParent>();
        public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();

        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
        public ICollection<Absence> Absences { get; set; } = new List<Absence>();
    }
}
