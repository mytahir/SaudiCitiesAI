using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Domain.Entities;
using SaudiCitiesAI.Domain.Enums;
using SaudiCitiesAI.Domain.Interfaces;
using SaudiCitiesAI.Domain.ValueObjects;

namespace SaudiCitiesAI.Application.Services
{
    public class CityPreloadJob : ICitySyncJob
    {
        private readonly ICityExternalProvider _osm;
        private readonly ICityRepository _repo;

        public CityPreloadJob(
            ICityExternalProvider osm,
            ICityRepository repo)
        {
            _osm = osm;
            _repo = repo;
        }

        public async Task SyncAsync(CancellationToken ct = default)
        {
            var snapshots = await _osm.SearchAsync(
                            name: "",
                            limit: 5000,
                            CancellationToken.None);

            foreach (var snap in snapshots)
            {
                if (await _repo.ExistsByOsmIdAsync(snap.OsmId))
                    continue;

                var city = new City(
                    osmId: snap.OsmId,
                    name: snap.Name,
                    coordinates: new Coordinates(
                        snap.Latitude,
                        snap.Longitude),
                    population: snap.Population,
                    wikipedia: snap.Wikipedia,
                    wikidata: snap.Wikidata,
                    region: snap.Region == null
                        ? null
                        : new Region(snap.Region, RegionType.Central) // default
                );

                await _repo.AddAsync(city, CancellationToken.None);
            }
        }
    }
}