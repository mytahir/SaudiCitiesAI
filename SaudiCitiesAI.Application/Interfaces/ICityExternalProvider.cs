using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Application.Interfaces
{
    public interface ICityExternalProvider
    {
        Task<IEnumerable<CitySnapshot>> SearchAsync(
            string name,
            int limit,
            CancellationToken ct = default);
    }
}
