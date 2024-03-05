using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using WaterData.Models;

namespace WaterData.Serializers;

public static class RdbReader
{
    public static async Task<IEnumerable<T>> ReadAsync<T>(Stream stream, Func<T, bool>? whereClauseDelegate = null, CancellationToken cancellationToken = new())
    {
        using var reader = new StreamReader(stream);
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = "\t",
            ShouldSkipRecord = row => row.Row[0].StartsWith("#") || row.Row[0].StartsWith("5s")
        };
        using var csv = new CsvReader(reader, configuration);
        var asyncEnum = csv.GetRecordsAsync<T>(cancellationToken);
        return await asyncEnum
            .Where(whereClauseDelegate ?? (_ => true) )
            .ToListAsync(cancellationToken);
    }
}
