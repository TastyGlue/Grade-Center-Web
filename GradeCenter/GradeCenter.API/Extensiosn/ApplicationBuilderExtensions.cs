namespace GradeCenter.API.Extensiosn
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> MigrateDb(this WebApplication app)
        {
            // Create a service scope to extract an instance of the database context
            using var serviceScope = app.Services.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            var dbContext = serviceProvider.GetRequiredService<GradeCenterDbContext>();

            // Applying pending migrations or creating the database if it doesn't exist
            await dbContext.Database.MigrateAsync();

            // Retrieving all the data seeding services and ordering them
            var seeders = serviceProvider.GetServices<IDataSeeder>()
                .OrderBy(x => x.Order);

            // Looping through each seed and adding it to the database
            foreach (var seeder in seeders)
                await seeder.Seed(dbContext);

            return app;
        }
    }
}
