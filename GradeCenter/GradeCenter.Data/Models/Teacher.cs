﻿namespace GradeCenter.Data.Models
{
    public class Teacher
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

        public ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
    }
}
