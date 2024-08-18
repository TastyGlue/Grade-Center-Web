﻿namespace GradeCenter.API.Extensiosn
{
    public static class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            // Configuration of identity system
            builder.Services.AddIdentity<User, IdentityRole<string>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<GradeCenterDbContext>() // Tying the identity to the database context
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"]!)),
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

            return builder;
        }
    }
}
