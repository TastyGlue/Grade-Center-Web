namespace GradeCenter.Data
{
    public class GradeCenterDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public GradeCenterDbContext(DbContextOptions<GradeCenterDbContext> options) : base(options)
        {
        }

        //Creating database tables
        public DbSet<School> Schools { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Headmaster> Headmasters { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<StudentParent> StudentParents { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Creating composite keys for linking tables
            builder.Entity<StudentParent>()
                .HasKey(sp => new { sp.StudentId, sp.ParentId });
            builder.Entity<TeacherSubject>()
                .HasKey(sp => new { sp.TeacherId, sp.SubjectId });

            //Creating M:M relations between linking tables
            builder.Entity<StudentParent>()
                .HasOne(sp => sp.Student)
                .WithMany(s => s.StudentParents)
                .HasForeignKey(sp => sp.StudentId);

            builder.Entity<StudentParent>()
                .HasOne(sp => sp.Parent)
                .WithMany(p => p.StudentParents)
                .HasForeignKey(sp => sp.ParentId);

            builder.Entity<TeacherSubject>()
                .HasOne(sp => sp.Teacher)
                .WithMany(s => s.TeacherSubjects)
                .HasForeignKey(sp => sp.TeacherId);

            builder.Entity<TeacherSubject>()
                .HasOne(sp => sp.Subject)
                .WithMany(p => p.TeacherSubjects)
                .HasForeignKey(sp => sp.SubjectId);
        }
    }
}
