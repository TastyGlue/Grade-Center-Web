namespace GradeCenter.API
{
    public static class Mappings
    {
        public static void ConfigureMappings(this IServiceCollection services)
        {
            TypeAdapterConfig<User, UserDto>.NewConfig()
                .Map(dest => dest.Picture, src => (src.Picture == null) ? null : Convert.ToBase64String(src.Picture));

            TypeAdapterConfig<UserDto, User>.NewConfig()
                .Map(dest => dest.Picture, src => (src.Picture == null) ? null : Convert.FromBase64String(src.Picture));

            TypeAdapterConfig<Parent, ParentDto>.NewConfig()
                .Map(dest => dest.Students, src => src.StudentParents.Select(x => x.Student));

            TypeAdapterConfig<Class, ClassDto>.NewConfig()
                .MaxDepth(4);

            TypeAdapterConfig<Subject, SubjectDto>.NewConfig()
                .Map(dest => dest.Teachers, src => src.TeacherSubjects.Select(x => x.Teacher));

            TypeAdapterConfig<Grade, GradeDto>.NewConfig()
                .MaxDepth(3);
        }
    }
}
