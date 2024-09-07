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

        public Guid? ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public Class? Class { get; set; }

        public ICollection<StudentParent> StudentParents { get; set; } = new List<StudentParent>();

        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
        public ICollection<Absence> Absences { get; set; } = new List<Absence>();
    }
}
