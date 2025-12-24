public record CitySnapshot(
    string Name,
    string? Region,
    double Latitude,
    double Longitude,
    int? Population,
    string? Wikipedia,
    long OsmId
)
{
    public object Wikidata { get; internal set; }
}
