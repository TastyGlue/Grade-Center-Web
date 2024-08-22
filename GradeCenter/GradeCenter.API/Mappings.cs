using Mapster;

namespace GradeCenter.API
{
    public static class Mappings
    {
        public static void ConfigureMappings(this IServiceCollection services)
        {
            TypeAdapterConfig<User, UserDto>.NewConfig()
                .Map(dest => dest.Picture, src => (src.Picture == null) ? null : Convert.ToBase64String(src.Picture));
        }
    }
}
