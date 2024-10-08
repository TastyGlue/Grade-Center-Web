﻿namespace GradeCenter.Data.Models
{
    public class Teacher
    {
        [Key]
        [ForeignKey(nameof(User))]
        public Guid Id { get; set; }
        public User User { get; set; } = default!;

        [Required]
        public Guid SchoolId { get; set; }
        [ForeignKey(nameof(SchoolId))]
        public School School { get; set; } = default!;

        public ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
    }
}
