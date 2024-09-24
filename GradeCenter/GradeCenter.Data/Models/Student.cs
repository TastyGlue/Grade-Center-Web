namespace GradeCenter.Data.Models
{
    public class Student
    {
        [Key]
        [ForeignKey(nameof(User))]
        public Guid Id { get; set; }
        public User User { get; set; } = default!;

        public Guid? ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public Class? Class { get; set; }

        public ICollection<StudentParent> StudentParents { get; set; } = new List<StudentParent>();

        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
        public ICollection<Absence> Absences { get; set; } = new List<Absence>();
    }
}
