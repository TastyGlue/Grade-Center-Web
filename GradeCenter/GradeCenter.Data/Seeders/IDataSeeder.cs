namespace GradeCenter.Data.Seeders
{
    public interface IDataSeeder
    {
        int Order { get; }
        Task Seed(DbContext context);
    }
}
