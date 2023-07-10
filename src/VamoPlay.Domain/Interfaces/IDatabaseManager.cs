namespace VamoPlay.Domain.Interfaces
{
    public interface IDatabaseManager
    {
        Task SeedTestData();

        Task SeedSuperAdmin();
    }
}
