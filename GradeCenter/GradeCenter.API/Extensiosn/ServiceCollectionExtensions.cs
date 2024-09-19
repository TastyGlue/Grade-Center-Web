namespace GradeCenter.API.Extensiosn
{
    public static class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            // Configuration of identity system
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.Lockout.MaxFailedAccessAttempts = 0;
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<GradeCenterDbContext>() // Tying the identity to the database context
                .AddUserManager<UserManager<User>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders();

            // Configuring connection to the PostgreSql database
            builder.Services.AddDbContext<GradeCenterDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            
            // Configuring JWT Bearer authentication and the token validation parameters
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecurityKey"]!)),
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true
                    };
                });

            // Register data seeding
            builder.Services.AddTransient<IDataSeeder, AbsenceSeeder>();
            builder.Services.AddTransient<IDataSeeder, AdminSeeder>();
            builder.Services.AddTransient<IDataSeeder, ClassSeeder>();
            builder.Services.AddTransient<IDataSeeder, GradeSeeder>();
            builder.Services.AddTransient<IDataSeeder, HeadmasterSeeder>();
            builder.Services.AddTransient<IDataSeeder, ParentSeeder>();
            builder.Services.AddTransient<IDataSeeder, RoleSeeder>();
            builder.Services.AddTransient<IDataSeeder, SchoolSeeder>();
            builder.Services.AddTransient<IDataSeeder, StudentParentSeeder>();
            builder.Services.AddTransient<IDataSeeder, StudentSeeder>();
            builder.Services.AddTransient<IDataSeeder, SubjectSeeder>();
            builder.Services.AddTransient<IDataSeeder, TeacherSeeder>();
            builder.Services.AddTransient<IDataSeeder, TeacherSubjectSeeder>();
            builder.Services.AddTransient<IDataSeeder, TimetableSeeder>();
            builder.Services.AddTransient<IDataSeeder, UserSeeder>();

            // Register services
            builder.Services.AddTransient<ITokenService, TokenService>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<IHeadmasterService, HeadmasterService>();
            builder.Services.AddTransient<ITeacherService, TeacherService>();
            builder.Services.AddTransient<IStudentService, StudentService>();
            builder.Services.AddTransient<IParentService, ParentService>();
            builder.Services.AddTransient<ISchoolService, SchoolService>();
            builder.Services.AddTransient<IClassService, ClassService>();
            builder.Services.AddTransient<ISubjectService, SubjectService>();

            // Configure model mapping
            builder.Services.ConfigureMappings();

            return builder;
        }
    }
}
