using System.Text;
using WaterData.Nwis.Models;
using WaterData.Serializers;

namespace WaterData.Tests.Serializers;

public class RdpReaderTest
{
    [Theory(DisplayName =
        "Given a collection of sites data in USGS RDB format, When that rdb is read, Then the data should be deserialized into valid 'NwisSites'")]
    [EmbeddedResourceData("WaterData.Tests/Resources/sites.rdb")]
    public async Task TestReadRdb(string content)
    {
        var stream = new MemoryStream(Encoding.ASCII.GetBytes(content));
        var sites = await RdbReader.ReadAsync<NwisSite>(stream);
        Assert.NotNull(sites);
    }
}
