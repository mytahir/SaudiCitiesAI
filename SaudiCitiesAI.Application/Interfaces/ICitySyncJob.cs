namespace SaudiCitiesAI.Application.Interfaces
{
    public interface ICitySyncJob
    {
        Task SyncAsync(CancellationToken ct = default);
    }
}