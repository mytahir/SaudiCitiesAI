using System.Threading;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Application.Interfaces
{
    public interface ILongCatAIService
    {
        Task<string> GenerateAsync(string prompt, CancellationToken ct = default);
    }
}