using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Domain.Interfaces;
using SaudiCitiesAI.Domain.Entities;
using SaudiCitiesAI.Domain.ValueObjects;
using SaudiCitiesAI.Domain.Enums;

namespace SaudiCitiesAI.Infrastructure.BackgroundJobs.Jobs
{
    public class CityOsmSyncJob : ICitySyncJob
    {
        private readonly ICityExternalProvider _osmProvider;
        private readonly ICityRepository _cityRepository;

        public CityOsmSyncJob(
            ICityExternalProvider osmProvider,
            ICityRepository cityRepository)
        {
            _osmProvider = osmProvider;
            _cityRepository = cityRepository;
        }

        public async Task SyncAsync(CancellationToken ct)
        {
            var snapshots = await _osmProvider.SearchAsync(
                name: "",
                limit: 5000,
                ct);

            foreach (var snap in snapshots)
            {
                if (await _cityRepository.ExistsByOsmIdAsync(snap.OsmId))
                    continue;

                var city = new City(
                    osmId: snap.OsmId,
                    name: snap.Name,
                    coordinates: new Coordinates(
                        snap.Latitude,
                        snap.Longitude),
                    population: snap.Population,
                    wikipedia: snap.Wikipedia,
                    wikidata: null,
                    region: snap.Region == null
                        ? null
                        : new Region(snap.Region, RegionType.Central) // safe default
                );

                await _cityRepository.AddAsync(city, ct);
            }

            await _cityRepository.SaveChangesAsync(ct);
        }
    }
}